using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace www.ptsol.com.au.DAL.Models
{
    public class Captcha
    {
        [Key]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public int CaptchaId { get; set; }
        public string Filename { get; set; }
        public string Answer { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
        public Nullable<bool> IsDeleted { get; set; }
        public Nullable<System.DateTime> DeletedDate { get; set; }

        public Captcha(string filename,string answer)
        {
            Filename = filename;
            Answer = answer;
            CreatedDate = DateTime.Now;
        }

        public Captcha()
        {

        }

    }
}