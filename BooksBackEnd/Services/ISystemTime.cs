using System;

namespace BooksBackEnd.Services
{
    public interface ISystemTime
    {
        DateTime GetCurrent();
    }
}