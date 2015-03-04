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
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using www.ptsol.com.au.DAL;
using www.ptsol.com.au.DAL.Interfaces;
using www.ptsol.com.au.Business;
using www.ptsol.com.au.Business.Interfaces;
using www.ptsol.com.au.Utilities;
using www.ptsol.com.au.Models.REST;
using www.ptsol.com.au.Utilities.SendEmail;


namespace www.ptsol.com.au.Controllers.REST
{
    [RoutePrefix("REST/REGISTRATION")]
    public class RegistrationController : Base
    {
        private UserManager<ptsolUser> manager;

        public RegistrationController(ILibraries libraries)
            : base(libraries)
        {
            manager = new UserManager<ptsolUser>(new UserStore<ptsolUser>(this.libraries.Context));
        }

        [HttpPost]
        [Route("RESET")]
        public HttpResponseMessage Reset(inResetForm model)
        {
            HttpResponseMessage response;
            if (Captcha.isValidGUID(model.guid))
            {
                Dictionary<string, string> cachecontent = HttpRuntime.Cache[model.guid] as Dictionary<string, string>;
                string answer = cachecontent["Answer"];
                if (Captcha.isValidAnswer(model.guid, model.answer))
                {
                    if (libraries.Users.CheckUsername(model.username))
                    {
                        try
                        {
                            string token = libraries.Users.ResetPassword(model.username);
                            SendResetPassword mail = new SendResetPassword(this.libraries, model.username, token);
                            mail.Send();
                            response = Request.CreateResponse(HttpStatusCode.OK);
                            return response;
                        }
                        catch (SmtpException ex)
                        {
                            response = Request.CreateResponse(HttpStatusCode.ServiceUnavailable, ConfigurationManager.AppSettings["smtpException"].ToString());
                            return response;
                        }
                        catch (Exception ex)
                        {
                            response = Request.CreateResponse(HttpStatusCode.InternalServerError, ConfigurationManager.AppSettings["generalException"].ToString());
                            return response;
                        }
                    }
                    else
                    {
                        response = Request.CreateResponse(HttpStatusCode.InternalServerError, new { ScopeID = "username_e", Message = "Invalid username." });
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

        [HttpPost]
        [Route("SUBMIT")]
        public HttpResponseMessage Submit(inRegistrationForm model)
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
                        if (!libraries.Users.CheckEmail(model.email))
                        {
                            if (!libraries.Users.CheckUsername(model.username))
                            {
                                try
                                {
                                    ptsolUser user = new ptsolUser(model);
                                    IdentityResult result = manager.Create(user);
                                    manager.AddToRole(user.Id, "Customer");

                                    SendActivationLink mail = new SendActivationLink(libraries, model.username, user.SecurityStamp);
                                    mail.Send();

                                    response = Request.CreateResponse(HttpStatusCode.OK);
                                    return response;
                                }
                                catch (SmtpException ex)
                                {
                                    response = Request.CreateResponse(HttpStatusCode.ServiceUnavailable, ConfigurationManager.AppSettings["smtpException"].ToString());
                                    return response;
                                }
                                catch (Exception ex)
                                {
                                    response = Request.CreateResponse(HttpStatusCode.InternalServerError, ConfigurationManager.AppSettings["generalException"].ToString());
                                    return response;
                                }
                            }
                            else
                            {
                                response = Request.CreateResponse(HttpStatusCode.InternalServerError, new { ScopeID = "username_e", Message = "This username has been used. Try another one." });
                                return response;
                            }
                        }
                        else
                        {
                            response = Request.CreateResponse(HttpStatusCode.InternalServerError, new { ScopeID = "email_e", Message = "This email has been used. Try another one." });
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


        [HttpPost]
        [Route("NEW-PASSWORD/CONFIRM")]
        public HttpResponseMessage ConfirmPassword(inNewPasswordForm model)
        {
            HttpResponseMessage response;

            if (Captcha.isValidGUID(model.guid))
            {
                Dictionary<string, string> cachecontent = HttpRuntime.Cache[model.guid] as Dictionary<string, string>;
                string answer = cachecontent["Answer"];
                if (Captcha.isValidAnswer(model.guid, model.answer))
                {

                    if (libraries.Users.CheckUsername(model.username))
                    {
                        ptsolUser user = manager.FindByName(model.username);
                        if (user.SecurityStamp.ToLower().Trim() == model.token.ToLower().Trim())
                        {
                            try
                            {
                                PasswordHasher hasher = new PasswordHasher();
                                user.PasswordHash = hasher.HashPassword(model.password);
                                manager.Update(user);

                                response = Request.CreateResponse(HttpStatusCode.OK);
                                return response;
                            }
                            catch (SmtpException ex)
                            {
                                response = Request.CreateResponse(HttpStatusCode.ServiceUnavailable, ConfigurationManager.AppSettings["smtpException"].ToString());
                                return response;
                            }
                            catch (Exception ex)
                            {
                                response = Request.CreateResponse(HttpStatusCode.InternalServerError, ConfigurationManager.AppSettings["generalException"].ToString());
                                return response;
                            }

                        }
                        else
                        {
                            response = Request.CreateResponse(HttpStatusCode.InternalServerError, new { ScopeID = "token_e", Message = "Invalid token." });
                            return response;
                        }
                    }
                    else
                    {
                        response = Request.CreateResponse(HttpStatusCode.NotFound, "Username " + model.username + " can not be found.");
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
