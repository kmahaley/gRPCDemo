using System.Collections.Generic;
using System;
using Nest;
using gRPCDotNetCoreClient.Data;

namespace gRPCDotNetCoreClient.Repository
{
    public class BookRepositoryImpl : IBookRepository
    {
        private readonly IElasticClient elasticClient;

        public BookRepositoryImpl(IElasticClient elasticClient)
        {
            this.elasticClient = elasticClient;
        }

        public IEnumerable<BookDto> GetAllBooks()
        {
            throw new NotImplementedException();
        }

        public BookDto GetBook(int id)
        {
            GetResponse<BookDto> getResponse = elasticClient.Get<BookDto>(id);
            return getResponse.Source;
        }

        public BookDto AddBook(BookDto bookModel)
        {
            IndexResponse indexResponse = elasticClient.IndexDocument(bookModel);
            throw new NotImplementedException();
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