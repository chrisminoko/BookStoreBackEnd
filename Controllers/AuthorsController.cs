using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BookStoreBackEnd.Models;
using BookStoreBackEnd.Models.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BookStoreBackEnd.Controllers
{
    [Route("api/authors")]
    [ApiController]
    public class AuthorsController : ControllerBase
    {
        private readonly IDataRepository<Author, AuthorDto> _dataRepository;
        public AuthorsController(IDataRepository<Author, AuthorDto> dataRepository)
        {
            _dataRepository = dataRepository;
        }

        // Get All the authors : api/Authors
        [HttpGet]
        public IActionResult Get() 
        {
            var authors = _dataRepository.GetAll();
            return Ok(authors);
        }
        //Fetches the author based on Id
        [HttpGet("{id}", Name = "GetAuthors")]
        public IActionResult Get(int id) 
        {
            var author = _dataRepository.GetDto(id);
            if (author==null)
            {
                return NotFound("Author is null");
            }
            return Ok(author);
        }

        //Post : api
        //Adds to the api
        [HttpPost]
        public IActionResult Post([FromBody] Author author)
        {

            if(author is null) 
            {
                return BadRequest("Author is null");

            }
            if (!ModelState.IsValid) 
            {
                return BadRequest();
            }
            _dataRepository.Add(author);
            return CreatedAtRoute("GetAuthor", new { Id = author.Id }, null);
        }

        //Delet

        [HttpPut("id")]
        public IActionResult Put(int id ,[FromBody] Author author)
        {
            if (author == null) 
            {
                return NotFound("The author is null");
            }
            var authorToUpdate = _dataRepository.Get(id);
            if (authorToUpdate == null)
            {
                return NotFound("The author is not  found");
            }
            if (!ModelState.IsValid) 
            {
                return BadRequest();
            }
            _dataRepository.Update(authorToUpdate, author);
            return NoContent();
        
        }

    }
}