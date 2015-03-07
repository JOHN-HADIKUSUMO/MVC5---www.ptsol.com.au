using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace www.ptsol.com.au.DAL.Models
{
    public class Testimonial
    {
        [Key]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public int TestimonialId { get; set; }
        public string UserId { get; set; }
        public string Comment { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
        public Nullable<bool> IsDeleted { get; set; }
        public Nullable<System.DateTime> DeletedDate { get; set; }
        [ForeignKey("UserId")]
        public virtual ptsolUser User { get; set; }

        public Testimonial()
        {
            this.CreatedDate = DateTime.Now;
        }

        public Testimonial(string userid,string comment)
        {
            this.CreatedDate = DateTime.Now;
            this.UserId = userid;
            this.Comment = comment;
        }

    }
}