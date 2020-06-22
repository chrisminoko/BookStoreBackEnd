using BookStoreBackEnd.Models.Repository;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookStoreBackEnd.Models.DataManager
{
    public class AuthorDataManager : IDataRepository<Author, AuthorDto>
    {
        //  implemented the interface with all it methods;
        //created a readonly  property for the database context
        // used that in the  constructor.
        readonly BookStoreContext _bookstorecontext;
        public AuthorDataManager(BookStoreContext storeContext)
        {
            _bookstorecontext = storeContext;
        }


        // we have made use of Eager loading which will load related data explicity from the database as part of the query.
        public IEnumerable<Author> GetAll() 
        {
            return _bookstorecontext.Author
                .Include(author => author.AuthorContact)
                .ToList();
        }

        public Author Get(long Id)
        {
            var author = _bookstorecontext.Author
                .SingleOrDefault(b => b.Id == Id);
            return author;
        }

        public void Add(Author entity)
        {
            _bookstorecontext.Author.Add(entity);
            _bookstorecontext.SaveChanges();
        }

        public void Delete(Author entity)
        {
            _bookstorecontext.Remove(entity);
            _bookstorecontext.SaveChanges();
        }


        public AuthorDto GetDto(long id)
        {
            _bookstorecontext.ChangeTracker.LazyLoadingEnabled = true;
            using (var context = new BookStoreContext()) 
            {
                var author = context.Author
                            .SingleOrDefault(b =>b.Id== id);

                throw new NotImplementedException();
            }
        
        }

        public void Update(Author entityToUpdate, Author entity)
        {
            entityToUpdate = _bookstorecontext.Author
                              .Include(a => a.AuthorContact)
                              .Include(a => a.BookAuthors)
                              .Single(b => b.Id == entityToUpdate.Id);

            entityToUpdate.Name = entity.Name;
            entityToUpdate.AuthorContact.Address = entity.AuthorContact.Address;
            entityToUpdate.AuthorContact.ContactNumber = entity.AuthorContact.ContactNumber;

            var deletedBooks = entityToUpdate.BookAuthors.Except(entity.BookAuthors).ToList();
            var addedBooks = entity.BookAuthors.Except(entityToUpdate.BookAuthors).ToList();


            deletedBooks.ForEach(bookToDelet =>
              entityToUpdate.BookAuthors.Remove(
                  entityToUpdate.BookAuthors
                  .First(b => b.BookId == bookToDelet.BookId)
                  ));

            foreach (var addedBook in addedBooks)
            {
                _bookstorecontext.Entry(addedBook).State = EntityState.Added;
            }

            _bookstorecontext.SaveChanges();
        }
    }
}
