using AutoMapper;
using AutoMapper.QueryableExtensions;
using BooksBackEnd.Domain;
using BooksBackEnd.Models.Books;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BooksBackEnd.Controllers
{
    public class BooksController : ControllerBase
    {
        BooksDataContext _context;
        IMapper _mapper;
        MapperConfiguration _mapperConfig;

        public BooksController(BooksDataContext context, IMapper mapper, MapperConfiguration mapperConfig)
        {
            _context = context;
            _mapper = mapper;
            _mapperConfig = mapperConfig;
        }

        [HttpGet("/books")]
        public async Task<ActionResult> GetAllBooks()
        {
            var response = new GetAllBooksResponse();
            var books = await _context.Books
                .Where(b=> b.IsInInventory == true)
                .ProjectTo<BooksResponseItem>(_mapperConfig)
                .ToListAsync();

            response.Data = books;
            return Ok(response);
        }
    }
}
