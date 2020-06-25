using AutoMapper;
using Grpc.Core;
using gRPCDotNetCoreClient.Data;
using gRPCDotNetCoreClient.Repository;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Org.Example.NMVnext.Sevices;
using System.Collections.Generic;
using System.Linq;

namespace Book.Services
{
    public class GRPCPNBookService : IBookRepository
    {
        private readonly ILogger<GRPCPNBookService> logger;

        private readonly IConfiguration config;

        private PNAPIBookService.PNAPIBookServiceClient client;

        private readonly IMapper mapper;

        public GRPCPNBookService(ILogger<GRPCPNBookService> logger, 
            IConfiguration config,
            PNAPIBookService.PNAPIBookServiceClient client, 
            IMapper mapper) {

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
                logger.LogInformation("***** before calling GRPC PN server !!!!");
                var result = client.PNAddBook(request);
                
            }
            catch (RpcException ex)
            {
                logger.LogError($"Failed!!! {bookDto.Id} with {ex.Message}");
            }
            return bookDto;
        }

        public void DeleteBook(int Id)
        {
            var request = new IntegerMessageType()
            {
                Id = Id
            };
            logger.LogInformation("***** before calling GRPC PN server !!!!");
            client.PNDeleteBook(request);
        }

        public IEnumerable<BookDto> GetAllBooks()
        {
            logger.LogInformation("***** before calling GRPC PN server !!!!");
            BookPacket bookPacket = client.PNGetAllBooks(new VoidMessageType());
            List<BookDto> result = new List<BookDto>();
            return bookPacket.Items.Select(book => mapper.Map<BookDto>(book));
        }

        public BookDto GetBook(int Id)
        {
            var request = new IntegerMessageType()
            {
                Id = Id
            };
            Org.Example.NMVnext.Sevices.Book book = client.PNGetBook(request);
            return mapper.Map<BookDto>(book);
        }

        public BookDto UpdateBook(int Id, BookDto bookDto)
        {
            Org.Example.NMVnext.Sevices.Book bookRequest = mapper.Map<Org.Example.NMVnext.Sevices.Book>(bookDto);
            var request = new PutMessageType()
            {
                Id = Id,
                Book = bookRequest
            };
            logger.LogInformation("***** before calling GRPC PN server !!!!");
            Org.Example.NMVnext.Sevices.Book book = client.PNPutBook(request);
            return mapper.Map<BookDto>(book);
        }
    }
}
