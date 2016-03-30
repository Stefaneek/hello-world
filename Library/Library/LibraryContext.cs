using System.Data.Entity;
using System.Runtime.InteropServices;

namespace Library
{
    public class LibraryContext : DbContext
    {
        public LibraryContext()
        { }
        public DbSet<Book> Books { get; set; }
        public DbSet<Author> Authors { get; set; }
        public DbSet<Account> Accounts { get; set; }
        public DbSet<Publishinghouse> Publishinghouses { get; set; }
        public DbSet<Suggestion> Suggestions { get; set; }

       
    }
    
   

}