using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using www.ptsol.com.au.DAL.Models;

namespace www.ptsol.com.au.DAL.Interfaces
{
    public interface ITestimonialService : ICRUD<int, int, bool, Testimonial>
    {
        List<Testimonial> GetTopTwo();
    }
}
