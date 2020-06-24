using Grpc.Core;
using Microsoft.Extensions.Logging;
using NMVnextGRPCService.Repository;
using Org.Example.NMVnext.Sevices;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace NMVnextGRPCService.Services
{
    public class NMBookService : NMAPIBookService.NMAPIBookServiceBase
    {
        private readonly ILogger<NMBookService> logger;

        private readonly INMBookRepository repository;

        public NMBookService(ILogger<NMBookService> logger, INMBookRepository repository) {
            this.logger = logger;
            this.repository = repository;
        } 

        public override Task<Book> AddBook(Book request, ServerCallContext context)
        {
            logger.LogInformation("you hit NM service API: {time}", DateTimeOffset.Now);
            Book book = null;
            try
            {
                book = repository.AddBook(request);
            }
            catch (Exception ex) {
                throw new RpcException(new Status(StatusCode.InvalidArgument, ex.Message));
            }
            logger.LogInformation($"****** Added {book.Id} {book.Name}");
            return Task.FromResult(book);
        }

        public override Task<Book> GetBook(IntegerMessageType request, ServerCallContext context)
        {
            logger.LogInformation("you hit NM service API: {time}", DateTimeOffset.Now);
            var book = repository.GetBook(request.Id);
            return Task.FromResult(book);
        }

        public override Task<BookPacket> GetAllBooks(VoidMessageType request, ServerCallContext context)
        {
            logger.LogInformation("you hit NM service API: {time}", DateTimeOffset.Now);
            var books = repository.GetAllBooks().ToList();

            BookPacket response = new BookPacket();
            response.Items.AddRange(books);
            return Task.FromResult(response);
        }

        public override Task<Book> PutBook(PutMessageType request, ServerCallContext context)
        {
            logger.LogInformation("you hit NM service API: {time}", DateTimeOffset.Now);
            var book = repository.UpdateBook(request.Id, request.Book);
            return Task.FromResult(book);
        }


        public override Task<VoidMessageType> DeleteBook(IntegerMessageType request, ServerCallContext context)
        {
            logger.LogInformation("you hit NM service API: {time}", DateTimeOffset.Now);
            repository.DeleteBook(request.Id);
            return Task.FromResult(new VoidMessageType());
        }
    }
}
