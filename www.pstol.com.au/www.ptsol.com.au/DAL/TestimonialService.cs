using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using www.ptsol.com.au.DAL;
using www.ptsol.com.au.DAL.Models;
using www.ptsol.com.au.DAL.Interfaces;
using www.ptsol.com.au.Business;
using www.ptsol.com.au.Business.Interfaces;


namespace www.ptsol.com.au.DAL
{
    public class TestimonialService : Base, ITestimonialService
    {
        public TestimonialService(IDataContext datacontext)
            : base(datacontext)
        {

        }
        public TestimonialService()
            : base()
        {

        }

        public int Create(Testimonial model)
        {
            entity.Testimonials.Add(model);
            entity.SaveChanges();
            return model.TestimonialId;
        }

        public Testimonial Read(int id)
        {
            return entity.Testimonials.Where(w => w.TestimonialId == id && (w.IsDeleted == null || w.IsDeleted == false)).FirstOrDefault();
        }

        public IQueryable<Testimonial> ReadAll()
        {
            return entity.Testimonials.Where(w => w.IsDeleted == null || w.IsDeleted == false).AsQueryable<Testimonial>();
        }

        public bool Update(Testimonial model)
        {
            Testimonial tempTestimonial = entity.Testimonials.Where(w => w.TestimonialId == model.TestimonialId && (w.IsDeleted == null || w.IsDeleted == false)).FirstOrDefault();
            if (tempTestimonial == null) return false;
            tempTestimonial.Comment = model.Comment;
            tempTestimonial.CreatedDate = DateTime.Now;
            entity.SaveChanges();
            return true;
        }

        public bool Delete(int id)
        {
            Testimonial tempTestimonial = entity.Testimonials.Where(w => w.TestimonialId == id && (w.IsDeleted == null || w.IsDeleted == false)).FirstOrDefault();
            if (tempTestimonial == null) return false;
            tempTestimonial.IsDeleted = true;
            tempTestimonial.DeletedDate = DateTime.Now;
            entity.SaveChanges();
            return true;
        }

        public List<Testimonial> GetTopTwo()
        {
            return entity.Testimonials.OrderByDescending(o => o.CreatedDate).Take(2).ToList();
        }
    }
}