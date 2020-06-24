using Org.Example.NMVnext.Sevices;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NMVnextGRPCService.Repository
{
    public class NMBookRepository : INMBookRepository
    {
        private readonly ConcurrentDictionary<int, Book> map;

        public NMBookRepository()
        {
            this.map = new ConcurrentDictionary<int, Book>();
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
            var pages = book.Pages * 1000;
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
