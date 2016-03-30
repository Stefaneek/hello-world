using System.Security.Cryptography.X509Certificates;

namespace Library
{
    public class Book
    {
        public int Id { get; set; }
        //List<Book> ZbiorKsiazek = new List<Book>();
        public float Price { get; set; }
        public string Title { get; set; }
        public int Publicationdate { get; set; }
        public Author Autor { get; set; }
        public Publishinghouse Wydawnictwo { get; set; }
        
    }

   
}