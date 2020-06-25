using gRPCDotNetCoreClient.Data;
using System.Collections.Generic;

namespace gRPCDotNetCoreClient.Repository
{
    public class MockBookRepositoryImpl : IBookRepository
    {
        private readonly Dictionary<int, BookDto> map;

        public MockBookRepositoryImpl()
        {
            this.map = new Dictionary<int, BookDto>();
        }

        public IEnumerable<BookDto> GetAllBooks()
        {
            return map.Values;
        }

        public BookDto GetBook(int id)
        {
            BookDto result;
            map.TryGetValue(id, out result);
            return result;
        }

        public BookDto AddBook(BookDto bookDto)
        {
            map.Add(bookDto.Id, bookDto);
            return GetBook(bookDto.Id);
        }

        public BookDto UpdateBook(int Id, BookDto bookDto)
        {
            if(map.ContainsKey(Id)) {
                map[Id] = bookDto;
                return GetBook(Id);
            }
            return null;
        }

        public void DeleteBook(int Id)
        {
            map.Remove(Id);
        }
    }
}