using BookStoreBackEnd.Models.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookStoreBackEnd.Models.DataManager
{
    public class BookDataManager : IDataRepository<Book, BookDto>
    {
        readonly BookStoreContext _bookstorecontext;

        public BookDataManager(BookStoreContext storeContext)
        {
            _bookstorecontext = storeContext;
        }
        public void Add(Book entity)
        {
            throw new NotImplementedException();
        }

        public void Delete(Book entity)
        {
            _bookstorecontext.Book.Remove(entity);
            _bookstorecontext.SaveChanges();
        }

        public Book Get(long id)
        {
            // This explicitly loads related data from the database  at a later time. Here we explicitly loaded BookAuthors and it publishers.
            _bookstorecontext.ChangeTracker.LazyLoadingEnabled = false;
            var book = _bookstorecontext.Book
                      .SingleOrDefault(b => b.Id == id);

            if (book==null) 
            {
                return null;
            }
            _bookstorecontext.Entry(book)
                .Collection(b => b.BookAuthors)
                .Load();

            _bookstorecontext.Entry(book)
                .Reference(b => b.Publisher)
                .Load();

            return book;
            
        }

        public IEnumerable<Book> GetAll()
        {
            throw new NotImplementedException();
        }

        public BookDto GetDto(long id)
        {
            throw new NotImplementedException();
        }

        public void Update(Book entityToUpdate, Book entity)
        {
            throw new NotImplementedException();
        }
    }
}
