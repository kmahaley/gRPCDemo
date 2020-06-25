using AutoMapper;
using Grpc.Core;
using gRPCDotNetCoreClient.Data;
using gRPCDotNetCoreClient.Repository;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Org.Example.NMVnext.Sevices;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Book.Services
{
    public class GRPCNMBookService : IBookRepository
    {
        private readonly ILogger<GRPCPNBookService> logger;

        private readonly IConfiguration config;

        private NMAPIBookService.NMAPIBookServiceClient client;

        private readonly IMapper mapper;

        public GRPCNMBookService(ILogger<GRPCPNBookService> logger, IConfiguration config, NMAPIBookService.NMAPIBookServiceClient client, IMapper mapper)
        {
            this.logger = logger;
            this.config = config;
            this.client = client;
            this.mapper = mapper;
        }

        public BookDto AddBook(BookDto bookDto)
        {
            Org.Example.NMVnext.Sevices.Book request = mapper.Map<Org.Example.NMVnext.Sevices.Book>(bookDto);
            try
            {
                logger.LogInformation("***** before calling GRPC NM server !!!!");
                var result = client.AddBook(request);

            }
            catch (RpcException ex)
            {
                logger.LogError($"Failed!!! {bookDto.Id} with {ex.Message}");
            }
            return bookDto;
        }

        public IEnumerable<BookDto> GetAllBooks()
        {
            BookPacket bookPacket = client.GetAllBooks(new VoidMessageType());
            List<BookDto> result = new List<BookDto>();
            return bookPacket.Items.Select(book => mapper.Map<BookDto>(book));
        }

        public BookDto GetBook(int Id)
        {
            var request = new IntegerMessageType()
            {
                Id = Id
            };
            Org.Example.NMVnext.Sevices.Book book = client.GetBook(request);
            return mapper.Map<BookDto>(book);
        }

        public BookDto UpdateBook(int Id, BookDto bookDto)
        {
            throw new NotImplementedException();
        }

        public void DeleteBook(int Id)
        {
            throw new NotImplementedException();
        }
    }
}
