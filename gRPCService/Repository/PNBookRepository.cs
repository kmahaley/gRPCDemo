using Microsoft.Extensions.Logging;
using Org.Example.NMVnext.Sevices;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NMVnextGRPCService.Repository
{
    public class PNBookRepository : IPNBookRepository
    {
        private readonly ConcurrentDictionary<int, Book> map;

        private readonly ILogger<PNBookRepository> logger;

        public PNBookRepository(ILogger<PNBookRepository> logger)
        {
            this.map = new ConcurrentDictionary<int, Book>();
            this.logger = logger;
        }

        public IEnumerable<Book> GetAllBooks()
        {
            return map.Values;
        }

        public Book GetBook(int id)
        {
            Book result;
            map.TryGetValue(id, out result);
            return result;
        }

        public Book AddBook(Book book)
        {
            logger.LogInformation("inside PN repository");
            var pages = book.Pages;
            book.Pages = pages;
            map.TryAdd(book.Id, book);
            return GetBook(book.Id);
        }

        public Book UpdateBook(int Id, Book book)
        {
            if (map.ContainsKey(Id))
            {
                book.Id = Id;
                map[Id] = book;
                return GetBook(Id);
            }
            return null;
        }

        public void DeleteBook(int Id)
        {
            map.TryRemove(Id, out Book val);
        }
    
    }
}
