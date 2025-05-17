using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Oman_Public_Library_System.Model
{
    public class Stuff
    {
        [Key]
        public int StuffId { get; set; }
        [Required]
        [StringLength(50)]
        public string Name { get; set; }
        [EmailAddress(ErrorMessage ="Wrong Email")]
        [Required]
        public string Email { get; set; }
        [Required]
        public string Password { get; set; }


    }
}
