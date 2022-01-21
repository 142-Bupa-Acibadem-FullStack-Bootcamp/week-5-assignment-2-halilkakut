using Northwind.Entity.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Northwind.Entity.Models
{
    [Table("Users", Schema ="dbo")]
    public class User : EntityBase
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Required(ErrorMessage ="User Id required")]
        public int UserID { get; set; }

        [MaxLength(30)]
        public string UserName { get; set; }

        [MaxLength(30)]
        public string UserLastName { get; set; }
        
        [MaxLength(40)]
        [Required(ErrorMessage = "User Code is Required")]
        public string UserCode { get; set; }

        [MaxLength(60)]
        [Required(ErrorMessage = "Password is Required")]
        public string Password { get; set; }
    }
}
