using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
   public class Comments
    {
        [Key]
        public int ID { set; get; }
        public string Comment { set; get; }
       

       
        public int CommenterID { set; get; }
        public DateTime Date { set; get; }
        public int PostID { set; get; }
    }
}
