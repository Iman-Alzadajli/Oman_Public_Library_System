using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Oman_Public_Library_System.Model
{
    public class Member
    {
        [Key]
        public int MemberId { get; set; }
        [Required]
        public string Name { get; set; }
        
        public DateTime Join_Date { get; set; }

        //BR

        public List<Borrow_Record> borrow_Records = new List<Borrow_Record>();

        public List<Borrow_Record> GetCurrentBorrowedBooks()
        {
            return borrow_Records.Where(br => br.Status == "Borrowed").ToList();
        }



    }
}
