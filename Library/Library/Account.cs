using System.Collections.Generic;

namespace Library
{
    public class Account
    {
        public string Password   { get; set; }
        public string Login  { get; set; }
        public int Id { get; set; }      
        public  List<Book> Koszyk { get; set; }
        public bool Permissions { get; set; } = false;
        public Account()
        {
            Koszyk = new List<Book>();
        }
    }
}