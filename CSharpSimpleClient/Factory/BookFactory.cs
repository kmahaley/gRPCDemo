using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Org.Example.NMVnext.Sevices;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CSharpSimpleClient.Factory
{
    public class BookFactory
    {
        private readonly ILogger<BookFactory> logger;

        private readonly IConfiguration config;

        public BookFactory(ILogger<BookFactory> logger, IConfiguration config)
        {
            this.logger = logger;
            this.config = config;
        }

        public Task<List<Book>> GetListOfBooks(int numberOf) {

            var random = new Random();
            List<Book> books = new List<Book>();
            for (int i = 0; i < numberOf; i++) {

                books.Add(GetBook(random.Next(900,1000)));
            }
            return Task.FromResult(books);
        }

        public Book GetBook(int id) {
            var book = new Book()
            {
                Id = id,
                Name = "apple-banana",
                Author = "anuthorName",
                Pages = 123,
                Cost = 12.12f
            };

            return book;
        }

    }
}
