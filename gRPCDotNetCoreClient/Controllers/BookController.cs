using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using gRPCDotNetCoreClient.Data;
using gRPCDotNetCoreClient.Models;
using gRPCDotNetCoreClient.Repository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace gRPCDotNetCoreClient.Controllers
{
    
        //api/book
        [ApiController]
        [Route("api/book")]
        public class BookController : ControllerBase
        {
            private readonly IBookRepository bookRepository;// inject gRPC_C#Client

            private readonly IMapper mapper;

            private readonly ILogger<BookController> logger;

            public BookController(IBookRepository bookRepository, IMapper mapper, ILogger<BookController> logger)
            {
                this.bookRepository = bookRepository;
                this.mapper = mapper;
                this.logger = logger;
            }

            //GET api/book
            [HttpGet]
            public ActionResult<IEnumerable<BookModel>> GetAllBooks()
            {
                IEnumerable<BookDto> books = bookRepository.GetAllBooks();
                IEnumerable<BookModel> bookModels = mapper.Map<IEnumerable<BookModel>>(books);
                return Ok(bookModels);
            }

            //GET api/book/{id}
            [HttpGet("{id}")]
            public ActionResult<BookModel> GetBook(int Id)
            {

                BookDto book = bookRepository.GetBook(Id);
                if (book != null)
                {
                    BookModel bookModel = mapper.Map<BookModel>(book);
                    return Ok(bookModel);
                }
                return NotFound();
            }

            //POST api/book/
            [HttpPost]
            public ActionResult<BookModel> AddBook(BookModel requestBookModel)
            {
                logger.LogInformation("***** controller of REST service !!!!");
                BookDto requestBook = mapper.Map<BookDto>(requestBookModel);
            BookDto responseBook = bookRepository.AddBook(requestBook);
                BookModel responseBookModel = mapper.Map<BookModel>(responseBook);
                return Created(nameof(GetBook), responseBook);
            }

            [HttpPut("{id}")]
            public ActionResult<BookModel> PutBook(int id, BookModel request)
            {
                BookDto requestBook = mapper.Map<BookDto>(request);
                BookDto bookDto = bookRepository.UpdateBook(id, requestBook);
                BookModel responseBookModel = mapper.Map<BookModel>(bookDto);
                return Ok(responseBookModel);
            }

            [HttpDelete("{id}")]
            public ActionResult<BookModel> DeleteBook(int id)
            {
                bookRepository.DeleteBook(id);
                return NoContent();
            }
        }
    
}
