using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataModelLayer
{
    public class Users
    {
        public int User_ID { get; set; }
        public String FirstName { get; set; }
        public String LastName { get; set; }
        public String Phone { get; set; }
        [Required(ErrorMessage = "Please provide Email")]
        public String Email { get; set; }
        public int Permission_ID { get; set; }
        [Required(ErrorMessage = "Please provide password" )]
        [DataType(DataType.Password)]
        public String Pwd { get; set; }
    }
}
