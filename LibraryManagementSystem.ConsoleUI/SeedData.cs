using LibraryManagementSystem.Business.Services;
using LibraryManagementSystem.DataAccess.Entities;
using LibraryManagementSystem.DataAccess.SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryManagementSystem.ConsoleUI
{
    public class SeedData
    {
        public SeedData()
        {
            IUserService userService = new UserService(new UserDal(new SQLiteDbContext()));

            userService.Add(new User { Name = "Abbas", Surname = "Gazabas", Phone = "05123456789" });
            userService.Add(new User { Name = "Emir", Surname = "EMEN", Phone = "05987654321" });

            IBorrowService borrowService = new BorrowService(new BorrowDal(new SQLiteDbContext()));
            //borrowService.Add(new Borrow { BookId = 1, UserId = 1,  });

            ICategoryService categoryService = new CategoryService(new CategoryDal(new SQLiteDbContext()));

            categoryService.Add(new Category { Name = "Bilim Kurgu", Description = "Description" });
            categoryService.Add(new Category { Name = "Fantastik", Description = "Description" });
            categoryService.Add(new Category { Name = "Polisiye", Description = "Description" });
            categoryService.Add(new Category { Name = "Felsefe", Description = "Description" });
            categoryService.Add(new Category { Name = "Aşk", Description = "Description" });
            categoryService.Add(new Category { Name = "Biyografi", Description = "Description" });
            categoryService.Add(new Category { Name = "Tarih", Description = "Description" });
            categoryService.Add(new Category { Name = "Korku", Description = "Description" });
            categoryService.Add(new Category { Name = "Eğitim", Description = "Description" });

            IBookService bookService = new BookService(new BookDal(new SQLiteDbContext()));

            bookService.Add(new Book { Name = "Vakıf ve İmparatorluk", Author = "Isaac Asimov", CategoryId = 1, CopyCount = 55, ISBN = "6053757748" });
            bookService.Add(new Book { Name = "Frankenstein", Author = "Mary Shelley", CategoryId = 2, CopyCount = 55, ISBN = "6053327328" });
            bookService.Add(new Book { Name = "Dr.Jekyll ve Mr.Hyde", Author = "Robert Louis Stevenson", CategoryId = 3, CopyCount = 55, ISBN = "6053325546" });
            bookService.Add(new Book { Name = "Sofie'nin Dünyası", Author = "Jostein Gaarder", CategoryId = 4, CopyCount = 55, ISBN = "9758434578" });
            bookService.Add(new Book { Name = "Kürk Mantolu Madonna", Author = "Sabahattin Ali", CategoryId = 5, CopyCount = 55, ISBN = "9753638027" });
            bookService.Add(new Book { Name = "Çocukluğum", Author = "Maksim Gorki", CategoryId = 6, CopyCount = 55, ISBN = "6053321915" });
            bookService.Add(new Book { Name = "Cesur Yeni Dünya", Author = "Aldous Huxley", CategoryId = 1, CopyCount = 24, ISBN = "9756902167" });
            bookService.Add(new Book { Name = "Gece Yarısı Kütüphanesi", Author = "Matt Haig", CategoryId = 1, CopyCount = 16, ISBN = "6051981837" });
            bookService.Add(new Book { Name = "Dune", Author = "Frank Herbert", CategoryId = 1, CopyCount = 28, ISBN = "605375479X" });
            bookService.Add(new Book { Name = "Fahrenheit 451", Author = "Ray Bradbury", CategoryId = 1, CopyCount = 32, ISBN = "6053757810" });
            bookService.Add(new Book { Name = "Cesur Yeni Dünya", Author = "Aldous Huxley", CategoryId = 1, CopyCount = 53, ISBN = "9756902167" });
            bookService.Add(new Book { Name = "Doğu Ekspresinde Cinayet", Author = "Agatha Christie", CategoryId = 3, CopyCount = 42, ISBN = "9754050945" });
            bookService.Add(new Book { Name = "Ne Yaptığını Biliyorum", Author = "Alice Feeney", CategoryId = 3, CopyCount = 17, ISBN = "6257550858" });
            bookService.Add(new Book { Name = "Beyoğlu Rapsodisi", Author = "Ahmet Ümit", CategoryId = 3, CopyCount = 69, ISBN = "9750846206" });
            bookService.Add(new Book { Name = "Ölüm Meleği", Author = "Agatha Christie", CategoryId = 3, CopyCount = 36, ISBN = "9752103251" });
            bookService.Add(new Book { Name = "Acı Kahve", Author = "Agatha Christie", CategoryId = 3, CopyCount = 46, ISBN = "9754058784" });
            bookService.Add(new Book { Name = "Cerrah", Author = "Tess Gerritsen", CategoryId = 3, CopyCount = 57, ISBN = "6050950288" });
            bookService.Add(new Book { Name = "Enstitü", Author = "Stephen King", CategoryId = 8, CopyCount = 50, ISBN = "9752126049" });
            bookService.Add(new Book { Name = "Hayvan Mezarlığı", Author = "Stephen King", CategoryId = 8, CopyCount = 53, ISBN = "9754051526" });
            bookService.Add(new Book { Name = "Göz", Author = "Stephen King", CategoryId = 8, CopyCount = 54, ISBN = "9754054215" });
            bookService.Add(new Book { Name = "Kuzuların Sessizliği", Author = "Thomas Harris", CategoryId = 1, CopyCount = 21, ISBN = "6055092980" });
            bookService.Add(new Book { Name = "Martin Eden", Author = "Jack London", CategoryId = 5, CopyCount = 13, ISBN = "6053322121" });
            bookService.Add(new Book { Name = "Aşk Hikayesi", Author = "İskender Pala", CategoryId = 5, CopyCount = 8, ISBN = "6258096913" });
            bookService.Add(new Book { Name = "Veronika Ölmek İstiyor", Author = "Paulo Coelho", CategoryId = 5, CopyCount = 33, ISBN = "9750730151" });
            bookService.Add(new Book { Name = "The Witcher: Son Dilek", Author = "Andrzej Sapkowski", CategoryId = 2, CopyCount = 43 , ISBN = "605299018X"});
            bookService.Add(new Book { Name = "The Witcher 2: Kader Kılıcı", Author = "Andrzej Sapkowski", CategoryId = 2, CopyCount = 43 , ISBN = "605299195X"});
            bookService.Add(new Book { Name = "The Witcher 3: Elflerin Kanı", Author = "Andrzej Sapkowski", CategoryId = 2, CopyCount = 43, ISBN = "6052992719"});

        }
    }
}
