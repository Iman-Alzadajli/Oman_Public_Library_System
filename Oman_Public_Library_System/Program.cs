using Oman_Public_Library_System.Context;
using Oman_Public_Library_System.Model;

namespace Oman_Public_Library_System
{
    internal class Program
    {
        static void Main(string[] args)
        {
           
            bool flag1chooice = true;

            while (flag1chooice)
            {

                using ApplicationDbContext _db = new ApplicationDbContext(); //to do stuff in database :)



                Console.WriteLine("===========================================\n" +
                    "Sohar Public Library - System\n" +
                    "===========================================\n\n" +
                    "1. Register New Member\n" +
                    "2. Add New Book\n" +
                    "3. Borrow Book\n" +
                    "4. Return Book\n" +
                    "5. View Member Borrowed Books\n" +
                    "6. Show Overdue Books\n" +
                    "7. Exit");


                Console.Write("Select an option: ");
                int chooice = Convert.ToInt32(Console.ReadLine());

                switch (chooice)
                {

                    case 1:
                        Console.WriteLine("-- Register New Member --");
                        Console.Write("Enter Member Name: ");
                        string memberName = Console.ReadLine();

                        Console.Write("Enter Join Date (dd/mm/yyyy): ");
                        DateTime joinDate = DateTime.ParseExact(Console.ReadLine(), "dd/MM/yyyy", null);

                        //insert

                        Member member = new Member()
                        {
                            Name = memberName,
                            Join_Date = joinDate
                        };

                        _db.Members.Add(member);
                        _db.SaveChanges();



                        Console.WriteLine($"Member registered successfully");
                        Console.WriteLine("-------------------------------------------");

                        break;

                    case 2:
                        Console.WriteLine("-- Add New Book --");
                        Console.Write("Enter ISBN: ");
                        string isbn = Console.ReadLine();

                        Console.Write("Enter Title: ");
                        string title = Console.ReadLine();

                        Console.Write("Enter Author: ");
                        string author = Console.ReadLine();


                        //insert

                        Book book = new Book()
                        {
                            ISBN = isbn,
                            Title = title,
                            Author = author,
                            IsAvailable = "Yes"  // نعتبره متاح عند الإضافة
                        };

                        _db.Books.Add(book);
                        _db.SaveChanges();

                        Console.WriteLine("Book added successfully.");
                        Console.WriteLine("-------------------------------------------");

                        break;

                    case 3:

                        Console.WriteLine("-- Borrow Book --");

                        Console.Write("Enter Member ID: ");
                        int memberId = Convert.ToInt32(Console.ReadLine());

                        Console.Write("Enter ISBN: ");
                        string checkisbncase3 = Console.ReadLine();

                        Console.Write("Enter Borrow Date (dd/mm/yyyy): ");
                        DateTime borrowDate = DateTime.ParseExact(Console.ReadLine(), "dd/MM/yyyy", null);


                        Member Checkmember = _db.Members.FirstOrDefault(m => m.MemberId == memberId);
                        if (Checkmember == null)
                        {
                            Console.WriteLine("Member not found ! ");
                            break;
                        }

                         Book CheckBook = _db.Books.FirstOrDefault(b => b.ISBN == checkisbncase3);
                        if (CheckBook == null)
                        {
                            Console.WriteLine("Book not found ! ");
                            break;
                        }

                        if (CheckBook.IsAvailable != "Yes")
                        {
                            Console.WriteLine("Book is not available At moment ");
                            break;
                        }



                        //insert 
                        Borrow_Record borrow_Record = new Borrow_Record()
                        {
                            MemberId = memberId,
                            BookId = CheckBook.BookId,
                            BorrowDate = borrowDate,
                            Status = "Borrowed"
                        };

                        _db.Borrow_Records.Add(borrow_Record); //Deset
                        CheckBook.IsAvailable = "No";

                        _db.SaveChanges();


                        Console.WriteLine("Book borrowed successfully ");
                        Console.WriteLine("-------------------------------------------");

                        break;


                    case 4:
                        Console.WriteLine("-- Return Book --");
                        Console.Write("Enter Member ID: ");
                        int memberId2 = Convert.ToInt32(Console.ReadLine());

                        Console.Write("Enter ISBN: ");
                        string checkisbncase4 = Console.ReadLine();

                        Console.Write("Enter Return Date (dd/mm/yyyy): ");
                        DateTime returnDate = DateTime.ParseExact(Console.ReadLine(), "dd/MM/yyyy", null);

                        Member Checkmember2 = _db.Members.FirstOrDefault(m => m.MemberId == memberId2);
                        if (Checkmember2 == null)
                        {
                            Console.WriteLine("Member not found ! ");
                            break;
                        }

                        Book CheckBook2 = _db.Books.FirstOrDefault(b => b.ISBN == checkisbncase4);
                        if (CheckBook2 == null)
                        {
                            Console.WriteLine("Book not found ! ");
                            break;
                        }



                             var borrowRecord = _db.Borrow_Records
                             .FirstOrDefault(br => br.MemberId == Checkmember2.MemberId && 
                             br.BookId == CheckBook2.BookId &&
                             br.Status == "Borrowed");

                        if (borrowRecord == null)
                        {
                            Console.WriteLine(" No borrow record found !");
                            break;
                        }

                        borrowRecord.ReturnDate = returnDate;
                        borrowRecord.Status = "Returned";
                        CheckBook2.IsAvailable = "Yes";

                        _db.SaveChanges();


                        Console.WriteLine("successfully returned ");
                        Console.WriteLine("-------------------------------------------");


                        break;

                    case 5:

                        Console.WriteLine("-- View Borrowed Books --");

                        Console.Write("Enter Member ID: ");
                        int memberId3 = Convert.ToInt32(Console.ReadLine());

                        Member viewMember = _db.Members.FirstOrDefault(m => m.MemberId == memberId3);
                        if (viewMember == null)
                        {
                            Console.WriteLine(" Member not found ! ");
                            break;
                        }

                        var borrowedBooks = _db.Borrow_Records
                       .Where(br => br.MemberId == memberId3 && br.Status == "Borrowed")
                       .Join(_db.Books,
                        br => br.BookId,
                        b => b.BookId,
                        (br, b) => new { b.Title, b.Author, br.BorrowDate })
                       .ToList();

                       Console.WriteLine($"Borrowed Books for Member {viewMember.MemberId} - {viewMember.Name}:");

                        if (borrowedBooks.Count == 0)
                        {
                            Console.WriteLine("No books currently borrowed ");
                        }
                        else
                        {
                            foreach (var Book in borrowedBooks)
                            {
                                Console.WriteLine($"Title: {Book.Title} | Borrowed On: {Book.BorrowDate.ToString("dd/MM/yyyy")}");
                            }
                        }


                        break;

                    case 6:
                        Console.WriteLine("-- Overdue Books --");

                        var borrowedRecords = _db.Borrow_Records
                            .Where(br => br.Status == "Borrowed")
                            .ToList(); 

                        
                        var overdueRecords = borrowedRecords
                            .Where(br => br.IsOverdue())
                            .Join(_db.Books,
                                  br => br.BookId,
                                  b => b.BookId,
                                  (br, b) => new { b.Title, b.Author, br.BorrowDate, br.MemberId })
                            .Join(_db.Members,
                                  br => br.MemberId,
                                  m => m.MemberId,
                                  (br, m) => new { br.Title, br.Author, br.BorrowDate, m.Name })
                            .ToList();

                        if (overdueRecords.Count == 0)
                        {
                            Console.WriteLine("No overdue books found ! "); // it will check the current day with borrow day  
                        }

                        else
                        {
                            foreach (var record in overdueRecords)
                            {
                                Console.WriteLine($"Title: {record.Title} | Borrowed On: {record.BorrowDate.ToString("dd/MM/yyyy")} Member Name: {record.Name} ");

                            }
                        }


                        break;

                    case 7:
                        Console.WriteLine("Thank you for using Sohar Library System. Goodbye!");
                        flag1chooice=false;

                        break;

                    default:
                        Console.WriteLine("Wrong Choice Try again");
                        break;



                }



            }


        }
    }
}
