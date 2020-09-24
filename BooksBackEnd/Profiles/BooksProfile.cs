using AutoMapper;
using BooksBackEnd.Domain;
using BooksBackEnd.Models.Books;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BooksBackEnd.Profiles
{
    public class BooksProfile : Profile
    {
        public BooksProfile()
        {
            // Book => BooksResponseItem
            CreateMap<Book, BooksResponseItem>();
        }
    }
}
