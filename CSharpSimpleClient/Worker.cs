using System;
using System.Threading;
using System.Threading.Tasks;
using CSharpSimpleClient.Factory;
using Grpc.Core;
using Grpc.Net.Client;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Org.Example.NMVnext.Sevices;

namespace CSharpSimpleClient
{
    public class Worker : BackgroundService
    {
        private readonly ILogger<Worker> logger;
        private readonly IConfiguration config;
        private NMAPIBookService.NMAPIBookServiceClient client;
        private readonly BookFactory bookFactory;

        private NMAPIBookService.NMAPIBookServiceClient clientInstance
        {
            get
            {
                if (client == null) {
                    var channel = GrpcChannel.ForAddress("http://localhost:50000");
                    client = new NMAPIBookService.NMAPIBookServiceClient(channel);
                }
                return client;
            }
            
        }

        public Worker(ILogger<Worker> logger, IConfiguration config, BookFactory bookFactory)
        {
            this.logger = logger;
            this.config = config;
            this.bookFactory = bookFactory;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                logger.LogInformation("Worker running at: {time}", DateTimeOffset.Now);
                
                var books = await bookFactory.GetListOfBooks(2);
                
                books.ForEach(x => AddBookToGrpcServer(x));

                await Task.Delay(3000, stoppingToken);
            }
        }

        private void AddBookToGrpcServer(Book book) {
            Book result = null;
            try
            {
                result = clientInstance.AddBook(book);
                if (result == null)
                {
                    logger.LogInformation($"Failed!!! {result.Id}");
                }
                else
                {
                    logger.LogInformation($"Success!!! {result.Id}");
                }
            }
            catch (RpcException ex) {
                logger.LogError($"Failed!!! {book.Id} with {ex.Message}");
            }
            
        }
    }
}
