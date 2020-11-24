using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
  public  class Likes
    {
        [Key]
        public int ID { set; get; }
        public int Liker { set; get; }
        public int Poster { set; get; }
        public int PostID { set; get; }

    }
}
