using System;
using System.Collections.Generic;
using System.Linq;
using static System.ConsoleColor;

namespace Library
{
    class Program
    {
        static void Main(string[] args)
        {
            using (var db = new LibraryContext())
            {
                bool wyjdz = false;
                bool wyloguj = false;
                while (wyjdz != true)
                {          
                    Console.Clear();
                    Console.WriteLine("1.Załóż konto\n2.Zaloguj się\n3.Wyjdź");
                    Console.Write("Wybiecz jedną z opcji: ");
                    int opcja;
                    wyloguj = false;
                    bool convert = Int32.TryParse(Console.ReadLine(), out opcja);
                    if (!convert)
                    {
                        Console.WriteLine("Nie podałeś liczby!!!");
                    }
                    convert = false;
                    switch (opcja)
                    {
                        case 1:
                        {
                                // Zakładanie konta
                            NewAccount();
                            break;
                        }
                        case 2:
                        {
                                Console.Clear();
                                Console.WriteLine("Podaj login: ");
                            string login = Console.ReadLine();
                            Console.WriteLine("Podaj hasło: ");
                            string password = Console.ReadLine();
                            var account = db.Accounts.FirstOrDefault(m => m.Login.ToUpper() == login.ToUpper() && m.Password.ToUpper() == password.ToUpper());
                            if (login != null && (password != null && (account != null && (account.Login.ToUpper() == login.ToUpper() && account.Password.ToUpper() == password.ToUpper()))))
                            {
                                bool permision = account.Permissions;
                                switch (permision)
                                {
                                        //Opcje dla Admina
                                        case true:
                                    {
                                        while (wyloguj != true)
                                        {
                                                    Console.Clear();
                                                    Console.WriteLine("Witaj Adminie!");
                                                    Console.WriteLine("1.Dodaj Książke \n2.Usuń Książke \n3.Zarządzaj kontami \n4.Przejżyj propozycje \n5.Wyloguj");
                                                    Console.Write("Wybierz jedną z opcji: ");
                                                    convert = Int32.TryParse(Console.ReadLine(), out opcja);
                                                    if (!convert)
                                                    {
                                                        Console.Clear();
                                                        Console.WriteLine("Nie podałeś liczby!!!");
                                                    }
                                                    convert = false;
                                            switch (opcja)
                                            {
                                                        //Dodawanie książki
                                                        case 1:
                                                {
                                                    AddBook();
                                                    break;
                                                }
                                                            //Usuwanie książki
                                                        case 2:
                                                {
                                                    DeleteBook();
                                                    
                                                    break;
                                                }
                                                            // Zarządzanie kontami
                                                        case 3:
                                                {
                                                                Console.Clear();
                                                    var allaccount = db.Accounts;
                                                    int query = 1;
                                                                Console.WriteLine("Lista użytkowników: ");
                                                    foreach (var acc in allaccount)
                                                    {
                                                                    Console.WriteLine(query + ". Login: " + acc.Login );
                                                                    query++;
                                                    }
                                                    query = 1;
                                                    Console.WriteLine("\n\n\n");
                                                    Console.Write("1.Zarządzaj uprawnieniami 2.Usuń użytkownika : ");
                                                    convert = Int32.TryParse(Console.ReadLine(), out opcja);
                                                    if (convert)
                                                    {
                                                        switch (opcja)
                                                        {
                                                            case 1:
                                                            {
                                                                ChangePermission();
                                                                break;
                                                            }
                                                                        case 2:
                                                            {
                                                                                Console.Clear();
                                                                DeleteAccount();
                                                                                break;
                                                            }
                                                        }
                                                    }
                                                    else
                                                    {
                                                        Console.WriteLine("Nie podałeś liczby");
                                                    }

                                                    break;
                                                }
                                                            //Przegladanie sugestii
                                                        case 4:
                                                {
                                                                /*
                                                                Console.Clear();
                                                                Console.WriteLine("Propozycje przedstawione przez naszych klijentów");
                                                    int query = 1;
                                                    var suggestions = db.Suggestions;
                                                                if(suggestions!= null)
                                                                { 
                                                    foreach (var sugg in suggestions)
                                                    {
                                                        Console.WriteLine(query + ". " + sugg.Book.Title + " " + sugg.Book.Autor);
                                                        query++;
                                                    }
                                                                }*/
                                                               // else
                                                              //  {
                                                                    Console.WriteLine("Na tą chwile brak propozycji");
                                                               // }
                                                                Console.ReadLine();
                                                    break;
                                                }
                                                            // Wyloguj
                                                        case 5:
                                                {
                                                    wyloguj = true;
                                                    break;                                                   
                                                }
                                            }
                                                }                                             
                                                break;
                                    }
                                        case false:
                                    {
                                        while (wyloguj != true)
                                                { 
                                            Console.Clear();
                                        Console.WriteLine("Witaj " + account.Login + " !");
                                        Console.WriteLine("1.Wypożycz książke \n2.Wyloguj.");
                                                    Console.Write("Wybierz jedną z opcji: ");
                                                    convert = Int32.TryParse(Console.ReadLine(), out opcja);
                                        if (!convert)
                                        {
                                                        Console.Clear();
                                                        Console.WriteLine("Nie podałeś liczby!!!");
                                        }
                                        convert = false;
                                        switch (opcja)
                                        {
                                                        // Wypożyczanie książki
                                            case 1:
                                            {
                                                Console.Clear();
                                                int query = 1;
                                                var allbooks = db.Books;
                                                foreach (var book in allbooks)
                                                {
                                                    Console.WriteLine(query + ". " + book.Title);
                                                    query++;
                                                }
                                                Console.WriteLine("Która książkę chcesz wypożyczyć (podaj jej numer)");
                                                convert = Int32.TryParse(Console.ReadLine(), out opcja);
                                                if (convert)
                                                {
                                                    if (opcja - 1 <= allbooks.ToList().Count)
                                                    {
                                                        var bookToRent = allbooks.ToList().ElementAt(opcja - 1);
                                                        account.Koszyk.Add(bookToRent);
                                                        Console.Clear();
                                                        Console.WriteLine("Książka została wypożyczona!");
                                                    }
                                                    else
                                                    {
                                                        Console.Clear();
                                                        Console.WriteLine("Nie posiadamy książki o takim numerze");
                                                    }
                                                }
                                                else
                                                {
                                                    Console.Clear();
                                                    Console.WriteLine("Nie podałeś liczby");
                                                }
                                                Console.ReadLine();
                                                break;
                                            }
                                                        // Proponowanie książki
                                     /*                   case 2:
                                            {
                                               // SuggestBoog();                                              
                                                break;
                                            }*/
                                                            // Wyloguj
                                            case 2:
                                            {
                                                wyloguj = true;
                                                                Console.WriteLine("Dziękujemy za skorzystanie z naszych usług");
                                                Console.ReadLine();
                                                break;
                                            }
                                        }
                                                }
                                                break;                                    
                                }        
                                }
                                Console.ReadKey();
                            }
                            else
                            {
                                Console.WriteLine("Brak takiego konta");
                            }

                            break;
                        }
                            // Wyjdz
                        case 3:
                        {
                            Console.Clear();
                            Console.ForegroundColor = Green;
                            Console.WriteLine("Żegnaj");
                            Console.ReadKey();
                            wyjdz = true;
                            break;
                        }
                        default:
                        {
                                Console.WriteLine("Liczba z poza zakresu");
                            break;
                        }
                    }

                }
            }
        }

        private static void DeleteAccount()
        {
            using (var db = new LibraryContext())
            {
                int query;
                var allaccount = db.Accounts;
                bool convert;
                int opcja;
                query = 1;
                Console.WriteLine("Lista użytkowników: ");
                foreach (var acc in allaccount)
                {
                    Console.WriteLine(query + ". Login: " + acc.Login);
                    query++;
                }
                Console.WriteLine("Podaj numer użytkownika którego uchcesz usunąć: ");
                convert = Int32.TryParse(Console.ReadLine(), out opcja);
                if (convert)
                {
                    if (opcja - 1 <= allaccount.ToList().Count)
                    {
                        Console.Clear();
                        var user =
                            allaccount.ToList().ElementAt(opcja - 1);
                        db.Accounts.Remove(user);
                        db.SaveChanges();
                        Console.WriteLine("Użytkownik został usunięty");                      
                    }
                    else
                    {
                        Console.WriteLine("Brak użytkownika o takim numerze");
                    }
                }
                else
                {
                    Console.WriteLine("Nie podałeś liczby");
                }
                Console.ReadLine();
            }
        }

        private static void ChangePermission()
        {
            using (var db = new LibraryContext())
            {
               bool convert;
                int opcja;

            Console.Clear();
                var allaccount = db.Accounts;
                int query = 1;
                Console.WriteLine("Lista użytkowników: ");
                
                foreach (var acc in allaccount)
            {
                Console.WriteLine(query + ". Login: " + acc.Login);
                query++;
            }
            Console.Write(
                "Podaj numer użytkownika którego uprawnienia chcesz zmienić: ");
            convert = Int32.TryParse(Console.ReadLine(), out opcja);
            if (convert)
            {
                if (opcja - 1 <= allaccount.ToList().Count)
                {
                    Console.Clear();
                    var user =
                        allaccount.ToList().ElementAt(opcja - 1);
                    Console.WriteLine(user.Login);
                    Console.WriteLine("1.Zwiększ(T)uprawnienia tego użytkownika \n2.Zmniejsz(F) uprawnienia tego użytkownika\n Wybierz jedna z opcji (T = zwiększ) lub (F = zmniejsz):  ");
                    string choose = Console.ReadLine();
                    if (choose != null && choose.ToUpper() == "T")
                    {
                        user.Permissions = true;
                        Console.WriteLine("Uprawnienia " + user.Login + "zostały zwiększone");
                        Console.ReadLine();
                    }
                    if (choose != null && choose.ToUpper() == "F")
                    {
                        user.Permissions = false;
                        Console.WriteLine("Uprawnienia " + user.Login + "zostały zmniejszone");
                        Console.ReadLine();
                    }
                    else
                    {
                        Console.WriteLine("Nie wybrałeś żadnej z dostępnych opcji");
                        Console.ReadLine();
                    }
                }
                else
                {
                    Console.WriteLine("Brak użytkownika o takim numerze");
                }
            }
            else
            {
                Console.WriteLine("Nie podałeś liczby");
            }
            }
        }

        private static void DeleteBook()
        {
            using (var db = new LibraryContext())
            {

             bool convert;
                int opcja;
            Console.Clear();
            int query = 1;
            var allbooks = db.Books;
            foreach (var book in allbooks)
            {
                Console.WriteLine(query + ". " + book.Title + " " + book.Autor);
                query++;
            }
            Console.Write("Która książkę chcesz usunąć (podaj jej numer): ");
            convert = Int32.TryParse(Console.ReadLine(), out opcja);
            if (convert)
            {
                if (opcja - 1 <= allbooks.ToList().Count)
                {
                    var bookToRemove = allbooks.ToList().ElementAt(opcja - 1);

                    db.Books.Remove(bookToRemove);
                    db.SaveChanges();
                    Console.Clear();

                    Console.WriteLine("Książka została usunięta!");
                }
                else
                {
                    Console.Clear();
                    Console.WriteLine("Nie posiadamy książki o takim numerze");
                }
            }
            else
            {
                Console.WriteLine("Nie podałeś liczby");
            }
            Console.ReadLine();
            }
        }

        private static void SuggestBoog()
        {
            using (var db = new LibraryContext())
            {
                
           
            Console.Clear();
            Console.WriteLine("Podaj Imie i Nazwisko Autor: ");
            string authormFirstLastName = Console.ReadLine();
            var author =
                db.Authors.FirstOrDefault(
                    m => m.FirstLastName.ToUpper() == authormFirstLastName.ToUpper());
            if (author == null)
            {
                author = new Author { FirstLastName = authormFirstLastName };

            }
            Console.WriteLine("Podaj tytuł");
            string title = Console.ReadLine();

                var book = db.Books.FirstOrDefault( m =>m.Title.ToUpper() == title.ToUpper() && m.Autor.FirstLastName.ToUpper() == author.FirstLastName.ToUpper());
                           
            if (book != null)
            {
                Console.WriteLine(
                    "Posiadamy już książkę tego autora o tym samym tytule");
            }
            else
            {
                book = new Book {Autor = author, Title = title};
                var bookSuggestion = new Suggestion { Book = book };
                db.Suggestions.Add(bookSuggestion);
                db.SaveChanges();
                Console.WriteLine("Dziękuje za pomoc w rozowuju naszej biblioteki");
            }
                Console.ReadLine();
            }
        }

        private static void AddBook()
        {
            using (var db = new LibraryContext())
            {
                bool convert;
                bool convert1;
                Console.Clear();
                Console.WriteLine("Podaj Imie i Nazwisko Autor: ");
                string authormFirstLastName = Console.ReadLine();
                var author = db.Authors.FirstOrDefault(m => m.FirstLastName.ToUpper() == authormFirstLastName.ToUpper());
                if (author == null)
                {
                    author = new Author {FirstLastName = authormFirstLastName};
                    db.Authors.Add(author);
                    db.SaveChanges();
                }
                Console.WriteLine("Podaj Nazwe Wydawnictwa ");
                string publishinghousename = Console.ReadLine();
                Console.WriteLine("Strone www wydawnictwa ");
                string publishinghousewebsite = Console.ReadLine();
                var publishinghouse =
                    db.Publishinghouses.FirstOrDefault( m =>
                            m.Name.ToUpper() == publishinghousename.ToUpper() &&
                            m.Website.ToUpper() == publishinghousewebsite.ToUpper());
                if (publishinghouse == null)
                {
                    publishinghouse = new Publishinghouse {Name = publishinghousename, Website = publishinghousewebsite};
                    db.Publishinghouses.Add(publishinghouse);
                    db.SaveChanges();
                }

                Console.WriteLine("Podaj tytuł");
                string title = Console.ReadLine();
                Console.WriteLine("Podaj Cene ");
                int price;
                convert = Int32.TryParse(Console.ReadLine(), out price);
                if (!convert)
                {
                    Console.WriteLine("Nie podałeś liczby!");
                }
                Console.WriteLine("Podaj rok wydania ");
                int year;
                convert1 = Int32.TryParse(Console.ReadLine(), out year);
                if (!convert1)
                {
                    Console.WriteLine("Nie podałeś liczby!");
                }
                if (!convert && !convert1)
                {
                    Console.WriteLine("Dodawanie nie powiodło się");
                }
                
                else
                {
                    var book = new Book
                    {
                        Autor = author,Price = price,Publicationdate = year,Title = title,Wydawnictwo = publishinghouse
                        
                        
                        
                    };
                    db.Books.Add(book);
                    db.SaveChanges();
                    Console.WriteLine("Książka została dodana");
                   

                }
            }
            
        }

        private static void NewAccount()
        {
            using (var db = new LibraryContext())
            {
                Console.Clear();
                Console.WriteLine("Podaj login: ");
                string login = Console.ReadLine();
                Console.WriteLine("Podaj hasło: ");
                string password = Console.ReadLine();
                var test = db.Accounts.FirstOrDefault(m => m.Login.ToUpper() == login.ToUpper() && m.Password.ToUpper() == password.ToUpper());
                if (test == null)
                {
                    var account = new Account
                    {
                        Koszyk = new List<Book>(),
                        Login = login,
                        Password = password
                    };
                    db.Accounts.Add(account);
                    db.SaveChanges();
                }
                else
                {
                    Console.WriteLine("Takie konto juz isnieje");
                    Console.ReadKey();
                }
            }
        }

        private static void AddData()
        {
            // Przykładowe dane
            Author adamMickiewicz = new Author { FirstLastName = "Adam Mickiewicz", Id = 1 };
            Author juliuszSłowacki = new Author { FirstLastName = "Juliusz Słowacki", Id = 2 };
            Author janTwadrowski = new Author { FirstLastName = "Jan Twadrdowski", Id = 3 };
            Publishinghouse WSIP = new Publishinghouse
            {
                Id = 1,
                Name = "WSIP",
                Website = "www.wsip.pl"
            };
            Account adminAccount = new Account
            {
                Id = 1,
                Koszyk = new List<Book>(),
                Login = "Mateusz",
                Password = "Mateusz",
                Permissions = true

            };
            Book book = new Book
            {
                Id = 1,
                Autor = adamMickiewicz,
                Price = (float)50.23,
                Publicationdate = 2003,
                Title = "Pan Tadeusz",
                Wydawnictwo = WSIP

            };
            Book book1 = new Book
            {
                Id = 2,
                Autor = adamMickiewicz,
                Price = (float)65.23,
                Publicationdate = 2000,
                Title = "Dziady",
                Wydawnictwo = WSIP

            };
            Book book3 = new Book
            {
                Id = 2,
                Autor = juliuszSłowacki,
                Price = (float)64.23,
                Publicationdate = 2006,
                Title = "Anhelli",
                Wydawnictwo = WSIP

            };


            using (var db = new LibraryContext())
            {
               // var remove = db.Authors.FirstOrDefault(m => m.FirstLastName == "Adam Mickiewicz");
                //db.Authors.Remove(remove);
                //db.SaveChanges();
                 //  db.Authors.Add(  juliuszSłowacki);
                try
                {
                   
                  //  db.Authors.Add(janTwadrowski);
                   //  db.Publishinghouses.Add(WSIP);
                   //   db.Accounts.Add(adminAccount);
                   //  db.Books.Add(book);
                   //    db.Books.Add(book1);
                      db.Books.Add(book3);
                    db.SaveChanges();
                }
                catch ( Exception  exception   )
                {
                    Console.WriteLine("hihi");
                }

            }
        }
    }
}
    
