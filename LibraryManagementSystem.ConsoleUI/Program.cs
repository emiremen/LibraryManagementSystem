using LibraryManagementSystem.Business.Services;
using LibraryManagementSystem.ConsoleUI;
using LibraryManagementSystem.DataAccess.Entities;
using LibraryManagementSystem.DataAccess.SQLite;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using System.Globalization;



int _page = 1;

HostApplicationBuilder builder = Host.CreateApplicationBuilder(args);

builder.Services.AddSingleton<SQLiteDbContext>();
builder.Services.AddSingleton<IBookDal, BookDal>();
builder.Services.AddSingleton<ICategoryDal, CategoryDal>();
builder.Services.AddSingleton<IUserDal, UserDal>();
builder.Services.AddSingleton<IBorrowDal, BorrowDal>();
builder.Services.AddSingleton<IUserService, UserService>();
builder.Services.AddSingleton<ICategoryService, CategoryService>();
builder.Services.AddSingleton<IBookService, BookService>();
builder.Services.AddSingleton<IBorrowService, BorrowService>();

builder.Build();



SeedData seedData = new();

IUserService userService = new UserService(new UserDal(new SQLiteDbContext()));
ICategoryService categoryService = new CategoryService(new CategoryDal(new SQLiteDbContext()));
IBookService bookService = new BookService(new BookDal(new SQLiteDbContext()));
IBorrowService borrowService = new BorrowService(new BorrowDal(new SQLiteDbContext()));




Console.WriteLine("--- Welcome to Library Management System ---");

SelectAChoice();



void SelectAChoice()
{
    _page = 1;
    Console.WriteLine("");
    Console.WriteLine("[1] Kütüphaneye yeni bir kitap ekleyin.\n");
    Console.WriteLine("[2] Kütüphanedeki tüm kitapların listesini görüntüleyin.\n");
    Console.WriteLine("[3] Bir kitabı başlığına veya yazarına göre arayın.\n");
    Console.WriteLine("[4] Bir kitap ödünç alın.\n");
    Console.WriteLine("[5] Bir kitabı iade edin.\n");
    Console.WriteLine("[6] Süresi geçmiş kitaplarla ilgili bilgileri görüntüleyin.\n");
    Console.WriteLine("[7] Ödünç alınan kitapların listesini görüntüleyin.\n");

    ConsoleKey selection = Console.ReadKey().Key;

    Console.Clear();

    switch (selection)
    {
        case ConsoleKey.NumPad1 or ConsoleKey.D1:
            AddNewBookToLibrary();
            break;
        case ConsoleKey.NumPad2 or ConsoleKey.D2:
            ListAllBooks();
            break;
        case ConsoleKey.NumPad3 or ConsoleKey.D3:
            SearchBookWithNameOrAuthor();
            break;
        case ConsoleKey.NumPad4 or ConsoleKey.D4:
            BorrowABook();
            break;
        case ConsoleKey.NumPad5 or ConsoleKey.D5:
            GiveABorrowedBook();
            break;
        case ConsoleKey.NumPad6 or ConsoleKey.D6:
            ListExpiredBooks();
            break;
        case ConsoleKey.NumPad7 or ConsoleKey.D7:
            ListBorrowedBooks();
            break;
        default:
            SelectAChoice();
            break;
    };
}

void ListBorrowedBooks()
{
    Console.WriteLine("[7] Ödünç alınan kitaplar görüntüleniyor.\n");
    //borrowService.GetAllBorrowedBooks().ToList().ForEach(x => Console.WriteLine($"Kitap Adı: {x.Book.Name}\nYazarı: {x.Book.Author}\nISBN: {x.Book.ISBN}\nKitap Kategori: {x.Book.Category.Name}\nKitap Ödünç Alma Tarihi: {x.CreatedDate}\nKitap Ödünç Alma Kodu: {x.Name}\n"));
    List<Borrow> borrows = borrowService.GetAllBorrowedBooks().ToList();
    foreach (var item in borrows)
    {
        Console.WriteLine($"Kitap Adı ve Yazarı: \t\t{item.Book.Name} - {item.Book.Author}\nÖdünç Alan: \t\t\t{item.User.FullName}\nKitap Ödünç Alma Tarihi: \t{item.CreatedDate}\nKitap Ödünç Alma Kodu: \t\t{item.Name}\n");
    }
    Navigation();
}

void ListExpiredBooks()
{
    Console.WriteLine("[6] Süresi geçmiş kitaplarla ilgili bilgiler görüntüleniyor.\n"); // Burada kaldık, sonra da  inputlar için hata kontrolü yapılacak
    List<Borrow> borrows = borrowService.ListExpiredBooks().ToList();

    foreach (var item in borrows)
    {
        Console.WriteLine($"Kitap Adı ve Yazarı: \t{item.Book.Name} - {item.Book.Author}\nÖdünç Alan: \t\t{item.User.FullName}\nÖdünç Alma Tarihi: \t{item.CreatedDate}\nİade Tarihi: \t\t{item.ReturnDate}\nÖdünç Alma Kodu: \t{item.Name}\nGeçen Günler: \t\t{DateOnly.FromDateTime(DateTime.Now).DayNumber - item.ReturnDate.DayNumber}\n");
    }
    Navigation();
}

void GiveABorrowedBook()
{
    Console.WriteLine("[5] Bir kitabı iade edin.\n");
    Console.WriteLine("Kitap adı veya kullanıcı adı veya ISBN girin:");
    string bookNameOrAuthorOrISBN = Console.ReadLine();
    bookNameOrAuthorOrISBN = bookNameOrAuthorOrISBN.ToLower();
    List<Borrow> borrows = borrowService.SearchBookWithNameOrAuthorOrBorrowCode(bookNameOrAuthorOrISBN).ToList();
    for (int i = 0; i < borrows.Count; i++)
    {
        Console.WriteLine($"{i + 1}-)\t Kitap Adı:\t\t {borrows[i].Book.Name}\n\t Yazarı:\t\t {borrows[i].Book.Author}\n\t ISBN:\t\t\t {borrows[i].Book.ISBN}\n\t Ödünç Alma Tarihi:\t {borrows[i].CreatedDate}\n\t Ödünç Alma Kodu:\t {borrows[i].Name}\n");
    }
    if (borrows.Count > 0)
    {
        Console.WriteLine("İade etmek istediğiniz kitabın numarasını giriniz:");
        int selectedBookFromList = Convert.ToInt32(Console.ReadLine());
        Borrow selectedBorrow = borrows[selectedBookFromList - 1];
        borrowService.GiveBackABook(selectedBorrow);
    }
    else
    {
        Console.WriteLine("Aradığınız kriterlere uygun kitap bulunamadı.");
    }
    Navigation();
}

void BorrowABook()
{
    Console.WriteLine("[4] Bir kitap ödünç alın.\n");
    Console.WriteLine("Kitap adı veya yazar adı giriniz:");
    var bookNameOrAuthor = Console.ReadLine();
    while (bookNameOrAuthor.IsNullOrEmpty())
    {
        Console.WriteLine("\nLütfen geçerli kitap adı veya yazar adı giriniz:");
        bookNameOrAuthor = Console.ReadLine();
    }
    bookNameOrAuthor = bookNameOrAuthor.ToLower();
    List<Book> books = bookService.SearchBooksWithNameOrAuthor(bookNameOrAuthor).Where(x => x.CopyCount > 0).ToList();
    for (int i = 0; i < books.Count; i++)
    {
        Console.WriteLine($"{i + 1}-)\t Adı:\t\t {books[i].Name}\n\t Yazarı:\t {books[i].Author}\n\t Kategori:\t {books[i].Category.Name}\n\t ISBN:\t\t {books[i].ISBN}\n\t Copies:\t {books[i].CopyCount}\n");
    }
    if (books.Count > 0)
    {
        Console.WriteLine("Ödünç almak istediğiniz kitabın numarasını giriniz:");
        var selectedBookNumberFromList = Console.ReadLine();
        int bookNumber;
        while (!int.TryParse(selectedBookNumberFromList, out bookNumber) || selectedBookNumberFromList.IsNullOrEmpty() || (bookNumber <= 0 || bookNumber > books.Count))
        {
            Console.WriteLine("\nLütfen geçerli kitap numarası giriniz:");
            selectedBookNumberFromList = Console.ReadLine();
        }
        Book selectedBook = books[bookNumber - 1];
        string borrowCode = GetRandomTicketCode();
        borrowService.Add(new Borrow() { BookId = selectedBook.Id, UserId = 1, Name = borrowCode });
        bookService.DecreaseCopyCountWhenBorrowABook(selectedBook);
        Console.Clear();
        Console.WriteLine($"\n{selectedBook.Name} kitabı {borrowCode} kodu ile ödünç alınmıştır.\nKitabınızı zamanında iade etmeyi unutmayın.\n");
    }
    else
    {
        Console.WriteLine("Aradığınız kriterlere uygun kitap bulunamadı.");
    }

    Navigation();
}

void SearchBookWithNameOrAuthor()
{
    Console.WriteLine("[3] Bir kitabı başlığına veya yazarına göre arayın.\n");
    Console.WriteLine("Kitap adı veya yazar adı giriniz:");
    string bookNameOrAuthor = Console.ReadLine();
    bookNameOrAuthor = bookNameOrAuthor.ToLower();
    Console.WriteLine("Aşağıdaki kitaplar bulundu:");
    foreach (var item in bookService.SearchBooksWithNameOrAuthor(bookNameOrAuthor))
    {
        Console.WriteLine($" Adı:\t\t {item.Name}\n Yazarı:\t {item.Author}\n Kategori:\t {item.Category.Name}\n ISBN:\t\t {item.ISBN}\n");
    }
    Navigation();

}

void ListAllBooks(int page = 1, int listLength = 5)
{
    if (page > 0) page -= 1;
    Console.WriteLine("[2] Kütüphanedeki tüm kitapların listesi görüntüleniyor.\n");

    List<Book> books = bookService.GetBooksWithCategory().Skip(page * listLength).Take(listLength).ToList();

    if (books.Count <= 0)
    {
        Console.WriteLine("-> Listenin sonuna ulaştınız.\n");
        --_page;
    }
    else
    {
        foreach (var item in books)
        {
            Console.WriteLine($" Adı:\t\t {item.Name}\n Yazarı:\t {item.Author}\n Kategori:\t {item.Category.Name}\n ISBN:\t\t {item.ISBN}\n Copies:\t {item.CopyCount}");
            Console.WriteLine("------------");
        }
    }


    Console.WriteLine("[0] Ana menüye dön.");
    if (_page > 1) Console.WriteLine("[1] Bir önceki sayfaya dön.");
    if (books.Count > 0) Console.WriteLine("[2] Bir sonraki sayfaya geç.");
    Console.WriteLine("[3] Yeni kitap ekle.");

    ConsoleKey selection = Console.ReadKey().Key;

    Console.Clear();

    switch (selection)
    {
        case ConsoleKey.NumPad0 or ConsoleKey.D0:
            SelectAChoice();
            break;
        case ConsoleKey.NumPad1 or ConsoleKey.D1:
            if (_page > 1) {
                --_page;
                ListAllBooks(_page);
            };
            break;
        case ConsoleKey.NumPad2 or ConsoleKey.D2:
            if (books.Count > 0) {
                ++_page;
                ListAllBooks(_page);
            } else { Navigation(); }
            break;
        case ConsoleKey.NumPad3 or ConsoleKey.D3:
            AddNewBookToLibrary();
            break;
        default:
            Navigation();
            break;
    };
}

void AddNewBookToLibrary()
{
    Console.WriteLine("[1] Kütüphaneye yeni bir kitap ekleyin.\n");

    Console.WriteLine("\nKitap adı giriniz:");
    var bookName = Console.ReadLine();
    while (bookName.IsNullOrEmpty())
    {
        Console.WriteLine("\nLütfen geçerli kitap adı giriniz:");
        bookName = Console.ReadLine();
    }
    bookName = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(bookName.ToLower());
    Console.WriteLine("\nYazar adı giriniz:");
    var bookAuthor = Console.ReadLine();
    while (bookAuthor.IsNullOrEmpty())
    {
        Console.WriteLine("\nLütfen geçerli yazar adı giriniz:");
        bookAuthor = Console.ReadLine();
    }
    bookAuthor = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(bookAuthor.ToLower());
    Console.WriteLine("\nISBN-10 kodu giriniz:");
    var bookISBN = Console.ReadLine();
    while (bookISBN.IsNullOrEmpty())
    {
        Console.WriteLine("\nLütfen geçerli ISBN-10 kodu giriniz:");
        bookISBN = Console.ReadLine();
    }
    bookISBN = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(bookISBN.ToLower());

    Console.WriteLine("\nKategori numarası seçiniz:");
    foreach (var item in categoryService.GetAll())
    {
        Console.WriteLine($"-> {item.Id} {item.Name}");
    }
    Console.WriteLine("");

    var selectedCategoryId = Console.ReadLine();
    int bookCategoryId;
    while (!int.TryParse(selectedCategoryId, out bookCategoryId) || selectedCategoryId.IsNullOrEmpty())
    {
        Console.WriteLine("\nLütfen geçerli kategori numarası seçiniz:");
        foreach (var item in categoryService.GetAll())
        {
            Console.WriteLine($"-> {item.Id} {item.Name}");
        }
        Console.WriteLine("");
        selectedCategoryId = Console.ReadLine();
    }

    Console.WriteLine("\nKitap kopya sayısı giriniz:");

    var selectedCopyCount = Console.ReadLine();
    int bookCopyCount;
    while (!int.TryParse(selectedCopyCount, out bookCopyCount) || selectedCopyCount.IsNullOrEmpty())
    {
        Console.WriteLine("\nLütfen geçerli kitap kopya sayısı giriniz:");
        selectedCopyCount = Console.ReadLine();
    }

    bookService.Add(new Book { Name = bookName, Author = bookAuthor, CategoryId = bookCategoryId, CopyCount = bookCopyCount, ISBN = bookISBN });
    Console.Clear();
    Console.WriteLine("Kitap başarıyla eklendi:\n");
    Book addedBook = bookService.GetBooksWithCategory().Where(x => x.ISBN == bookISBN).FirstOrDefault();
    Console.WriteLine($" Kitap Adı:\t {addedBook.Name}\n Yazarı:\t {addedBook.Author}\n Kategori:\t {addedBook.Category.Name}\n ISBN:\t\t {addedBook.ISBN}\n Copies:\t\t {addedBook.CopyCount}\n");

    Navigation();
}

void Navigation()
{
    Console.WriteLine("[0] Ana menüye dönmek için basınız.");

    ConsoleKey selection = Console.ReadKey().Key;

    Console.Clear();

    switch (selection)
    {
        case ConsoleKey.NumPad0 or ConsoleKey.D0:
            SelectAChoice();
            break;
        default:
            Navigation();
            break;
    };
}

string GetRandomTicketCode()
{
    string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
    string stringChars = "";
    int codeLength = 6;
    Random random = new Random();

    for (int i = 0; i < codeLength; i++)
    {
        stringChars += chars[random.Next(chars.Length)];
    }

    return stringChars.ToString();
}

Console.ReadLine();






