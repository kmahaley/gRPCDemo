using gRPCDotNetCoreClient.Data;
using System.Collections.Generic;

namespace gRPCDotNetCoreClient.Repository
{
    public interface IBookRepository
    {
        IEnumerable<BookDto> GetAllBooks();

        BookDto GetBook(int Id);

        BookDto AddBook(BookDto bookDto);

        BookDto UpdateBook(int Id, BookDto bookDto);

        void DeleteBook(int Id);
    }
}