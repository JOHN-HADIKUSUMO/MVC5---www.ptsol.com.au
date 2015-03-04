using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Web;
using System.Net;
using System.Net.Mail;
using System.Net.Http;
using System.Web.Http;
using System.Text;
using System.Configuration;
using www.ptsol.com.au.Business;
using www.ptsol.com.au.Business.Interfaces;
using www.ptsol.com.au.Utilities;
using www.ptsol.com.au.Models.REST;
using www.ptsol.com.au.Utilities.SendEmail;


namespace www.ptsol.com.au.Controllers.REST
{
    [RoutePrefix("REST/CONTACT-US")]
    public class ContactUsController : Base
    {
        public ContactUsController(ILibraries libraries):base(libraries)
        {

        }

        [HttpPost]
        [Route("SUBMIT")]
        public HttpResponseMessage Submit(inContactUsForm model)
        {
            HttpResponseMessage response;

            if (Captcha.isValidGUID(model.guid))
            {
                Dictionary<string, string> cachecontent = HttpRuntime.Cache[model.guid] as Dictionary<string, string>;
                string answer = cachecontent["Answer"];
                if (Captcha.isValidAnswer(model.guid, model.answer))
                {
                    if (RegEx.isValidEmail(model.email))
                    {
                        if (RegEx.isValidTelephone(model.mobile))
                        {
                            try
                            {
                                SendContactUs sendemail = new SendContactUs(model);
                                sendemail.Send();

                                response = Request.CreateResponse(HttpStatusCode.OK);
                                return response;
                            }
                            catch (SmtpException ex)
                            {
                                response = Request.CreateResponse(HttpStatusCode.ServiceUnavailable);
                                return response;
                            }
                        }
                        else
                        {
                            response = Request.CreateResponse(HttpStatusCode.InternalServerError, new { ScopeID = "mobile_e", Message = "Invalid mobile format." });
                            return response;
                        }
                    }
                    else
                    {
                        response = Request.CreateResponse(HttpStatusCode.InternalServerError, new { ScopeID = "email_e", Message = "Invalid email format." });
                        return response;
                    }
                }
                else
                {
                    response = Request.CreateResponse(HttpStatusCode.InternalServerError, new { ScopeID = "answer_e", Message = "Invalid answer." });
                    return response;
                }
            }
            else
            {
                response = Request.CreateResponse(HttpStatusCode.InternalServerError, new { ScopeID = "answer_e", Message = "Invalid guid." });
                return response;
            }

        }

    }
}
