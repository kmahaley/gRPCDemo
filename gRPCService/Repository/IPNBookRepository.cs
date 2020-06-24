using Org.Example.NMVnext.Sevices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NMVnextGRPCService.Repository
{
    public interface IPNBookRepository
    {
        IEnumerable<Book> GetAllBooks();

        Book GetBook(int Id);

        Book AddBook(Book Book);

        Book UpdateBook(int Id, Book Book);

        void DeleteBook(int Id);
    }
}
