using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Oman_Public_Library_System.Model
{
    public class Book
    {
        [Key]
        public int BookId { get; set; }
        [Required]
        public string ISBN { get; set; }
        [Required]
        public string Title { get; set; }
        [Required]
        public string Author { get; set; }

        public string IsAvailable { get; set; }  //for checking 


        //BR
        public List<Borrow_Record> borrow_Records = new List<Borrow_Record>();


    }
}
