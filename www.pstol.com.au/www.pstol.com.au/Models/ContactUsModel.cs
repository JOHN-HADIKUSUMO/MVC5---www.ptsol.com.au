using System;
using System.ComponentModel.DataAnnotations;

namespace www.pstol.com.au.Models
{
    public class ContactUsModel
    {
        public ContactUsModel()
        {
            Name = "";
            Email = "";
            Mobile = "";
            Subject = "";
            Message = "";
            Captcha = "";
            SendEmail = false;
            GUID = Guid.NewGuid().ToString();
        }

        [Required]
        [DataType(DataType.Text)]
        public string Name { get; set; }

        [Required]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.PhoneNumber)]
        public string Mobile { get; set; }

        [Required]
        [DataType(DataType.Text)]
        public string Subject { get; set; }

        [Required]
        [DataType(DataType.Text)]
        public string Message { get; set; }

        [Required]
        [DataType(DataType.Text)]
        public string Captcha { get; set; }

        public bool SendEmail { get; set; }

        public string GUID { get; set; }
    }
}