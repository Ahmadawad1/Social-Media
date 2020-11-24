using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
   public class Messages
    {
        [Key]
        public int ID { set; get; }
        public int Sender { set; get; }
        public int Receiver { set; get; }
        public string Content { set; get; }


        public DateTime Date { set; get; }
    }
}
