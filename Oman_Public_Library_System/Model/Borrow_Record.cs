using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Oman_Public_Library_System.Model
{
    public class Borrow_Record
    {
        public int Borrow_RecordId { get; set; }
        public DateTime BorrowDate { get; set; }
        public DateTime ReturnDate { get; set; }

        public string Status { get; set; }

        //memberfk

        [ForeignKey("MemberId")]
        public Member Member { get; set; } 

        public int MemberId { get; set; }

        //bookfk

        [ForeignKey("BookId")]
        public Book Book { get; set; }

        public int BookId { get; set; }


        public bool IsOverdue()
        {
            return Status == "Borrowed" && BorrowDate.AddDays(14) < DateTime.Now;
        }

        public void MarkAsReturned(DateTime returnDate)
        {
            ReturnDate = returnDate;
            Status = "Returned";
        }

    }
}
