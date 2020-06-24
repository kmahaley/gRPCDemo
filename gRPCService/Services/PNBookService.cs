using Grpc.Core;
using Microsoft.Extensions.Logging;
using NMVnextGRPCService.Repository;
using Org.Example.NMVnext.Sevices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NMVnextGRPCService.Services
{
    public class PNBookService : PNAPIBookService.PNAPIBookServiceBase
    {
        private readonly ILogger<PNBookService> logger;

        private readonly IPNBookRepository repository;

        public PNBookService(ILogger<PNBookService> logger, IPNBookRepository repository) {
            this.logger = logger;
            this.repository = repository;
        }
        
        public override Task<Book> PNAddBook(Book request, ServerCallContext context)
        {
            logger.LogInformation("you hit PN service API: {time}", DateTimeOffset.Now);
            var book = repository.AddBook(request);
            return Task.FromResult(book);
        }

        public override Task<Book> PNGetBook(IntegerMessageType request, ServerCallContext context)
        {
            logger.LogInformation("you hit PN service API: {time}", DateTimeOffset.Now);
            var book = repository.GetBook(request.Id);
            return Task.FromResult(book);
        }

        public override Task<BookPacket> PNGetAllBooks(VoidMessageType request, ServerCallContext context)
        {
            logger.LogInformation("you hit PN service API: {time}", DateTimeOffset.Now);
            var books = repository.GetAllBooks().ToList();
            
            BookPacket response = new BookPacket();
            response.Items.AddRange(books);
            return Task.FromResult(response);
        }

        public override Task<Book> PNPutBook(PutMessageType request, ServerCallContext context)
        {
            logger.LogInformation("you hit PN service API: {time}", DateTimeOffset.Now);
            var book = repository.UpdateBook(request.Id, request.Book);
            return Task.FromResult(book);
        }

        public override Task<VoidMessageType> PNDeleteBook(IntegerMessageType request, ServerCallContext context)
        {
            logger.LogInformation("you hit PN service API: {time}", DateTimeOffset.Now);
            repository.DeleteBook(request.Id);
            return Task.FromResult(new VoidMessageType());
        }

    }
}
