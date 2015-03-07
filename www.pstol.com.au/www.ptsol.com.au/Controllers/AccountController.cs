using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Owin.Security;
using www.ptsol.com.au.DAL;
using www.ptsol.com.au.DAL.Interfaces;
using www.ptsol.com.au.Business;
using www.ptsol.com.au.Business.Interfaces;
using www.ptsol.com.au.Models;
using www.ptsol.com.au.Utilities;
using www.ptsol.com.au.Utilities.SendEmail;

namespace www.ptsol.com.au.Controllers
{
    [Authorize]
    [RoutePrefix("Account")]
    public class AccountController : Controller
    {
        private ILibraries libraries;
        private UserManager<ptsolUser> userManager;
        private RoleManager<IdentityRole> roleManager;
        public AccountController(ILibraries libraries)
            : this(libraries,new UserManager<ptsolUser>(new UserStore<ptsolUser>(new ptsolDBContext())),new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(new ptsolDBContext())))
        {
            
        }

        public AccountController(ILibraries libraries,UserManager<ptsolUser> userManager,RoleManager<IdentityRole> roleManager)
        {
            this.libraries = libraries;
            this.userManager = userManager;
            this.roleManager = roleManager;
        }

        //public UserManager<ptsolUser> UserManager { get; private set; }

        //
        // GET: /Account/Login
        [AllowAnonymous]
        public ActionResult Login(string returnUrl)
        {
            //ViewBag.ReturnUrl = returnUrl;
            //return View();
            LoginViewModel model = new LoginViewModel();
            model.Guid = Guid.NewGuid().ToString();
            ViewBag.ReturnUrl = returnUrl;
            return View(model);
        }

        //
        // POST: /Account/Login
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Login(LoginViewModel model, string returnUrl)
        {
            if (ModelState.IsValid)
            {
                if (Captcha.isValidGUID(model.Guid))
                {
                    if (Captcha.isValidAnswer(model.Guid, model.Answer))
                    {
                        
                        ptsolUser user = this.userManager.Find(model.UserName, model.Password);
                        if (user != null)
                        {
                            if(this.libraries.Users.CheckConfirmation(user.UserName))
                            {
                                await SignInAsync(user, model.RememberMe);
                                return RedirectToLocal(returnUrl);
                            }
                            else
                            {
                                SendActivationLink email = new SendActivationLink(libraries, user.UserName, user.SecurityStamp);
                                email.Send();

                                ModelState.AddModelError("", "You haven't confirmed your registration. Please check your email now.");
                            }
                        }
                        else
                        {
                            ModelState.AddModelError("", "Invalid username or password.");
                        }
                    }
                    else
                    {
                        ModelState.AddModelError("", "Invalid answer.");
                    }
                }
                else
                {
                    ModelState.AddModelError("", "Invalid GUID.");
                }
            }

            return View(model);
        }


        [AllowAnonymous]
        [HttpGet]
        [Route("NEW-PASSWORD/CONFIRM/{username}/{token}")]
        public ActionResult ConfirmedPassword(string username, string token)
        {
            ViewBag.GUID = Guid.NewGuid().ToString();
            ViewBag.Username = username;
            ViewBag.Token = token;
            return View();
        }


        [AllowAnonymous]
        [Route("ACTIVATION/{username}/{token}")]
        public ActionResult Activation(string username,string token)
        {
            ptsolUser user = this.userManager.FindByName(username);
            if (user != null)
            { 
                if(!user.IsConfirmed)
                {
                    if (user.SecurityStamp.Trim().ToLower() == token.Trim().ToLower())
                    {
                        user.IsConfirmed = true;
                        this.userManager.Update(user);

                        ViewBag.Message = "Your account has been activated.";
                    }
                    else
                    {
                        ViewBag.Message = "Invalid Token.";
                    }
                }
                else
                {
                    ViewBag.Message = "This account had been confirmed before.";
                }
            }
            else
            {
                ViewBag.Message = "Invalid Username";
            }

            return View();
        }

        [AllowAnonymous]
        [Route("RESET-PASSWORD")]
        public ActionResult Reset()
        {
            ViewBag.GUID = Guid.NewGuid().ToString();
            return View();
        }

        [Route("Your-Profile")]
        public ActionResult YourProfile()
        {
            ViewBag.Username = User.Identity.Name;

            ptsolUser user = this.userManager.FindByName(User.Identity.Name);
            if (user != null)
            {
                ViewBag.Title = user.Title;
                ViewBag.Firstname = user.Firstname;
                ViewBag.Lastname = user.Lastname;
                ViewBag.Email = user.Email;
            }

            return View();
        }

        [Route("Change-Password")]
        public ActionResult ChangePassword()
        {
            ptsolUser user = this.userManager.FindByName(User.Identity.Name);
            if(user != null)
            {
                ViewBag.Username = user.UserName;
                ViewBag.Stamp = user.SecurityStamp;
            }
            return View();
        }

        [Route("Your-Invoices")]
        public ActionResult Invoices()
        {
            ViewBag.GUID = Guid.NewGuid().ToString();
            return View();
        }

        [AllowAnonymous]
        public ActionResult Register()
        {
            ViewBag.GUID = Guid.NewGuid().ToString();
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Disassociate(string loginProvider, string providerKey)
        {
            ManageMessageId? message = null;
            IdentityResult result = await this.userManager.RemoveLoginAsync(User.Identity.GetUserId(), new UserLoginInfo(loginProvider, providerKey));
            if (result.Succeeded)
            {
                message = ManageMessageId.RemoveLoginSuccess;
            }
            else
            {
                message = ManageMessageId.Error;
            }
            return RedirectToAction("Manage", new { Message = message });
        }

        //
        // GET: /Account/Manage
        public ActionResult Manage(ManageMessageId? message)
        {
            ViewBag.StatusMessage =
                message == ManageMessageId.ChangePasswordSuccess ? "Your password has been changed."
                : message == ManageMessageId.SetPasswordSuccess ? "Your password has been set."
                : message == ManageMessageId.RemoveLoginSuccess ? "The external login was removed."
                : message == ManageMessageId.Error ? "An error has occurred."
                : "";
            ViewBag.HasLocalPassword = HasPassword();
            ViewBag.ReturnUrl = Url.Action("Manage");
            return View();
        }

        //
        // POST: /Account/Manage
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Manage(ManageUserViewModel model)
        {
            bool hasPassword = HasPassword();
            ViewBag.HasLocalPassword = hasPassword;
            ViewBag.ReturnUrl = Url.Action("Manage");
            if (hasPassword)
            {
                if (ModelState.IsValid)
                {
                    IdentityResult result = await this.userManager.ChangePasswordAsync(User.Identity.GetUserId(), model.OldPassword, model.NewPassword);
                    if (result.Succeeded)
                    {
                        return RedirectToAction("Manage", new { Message = ManageMessageId.ChangePasswordSuccess });
                    }
                    else
                    {
                        AddErrors(result);
                    }
                }
            }
            else
            {
                // User does not have a password so remove any validation errors caused by a missing OldPassword field
                ModelState state = ModelState["OldPassword"];
                if (state != null)
                {
                    state.Errors.Clear();
                }

                if (ModelState.IsValid)
                {
                    IdentityResult result = await this.userManager.AddPasswordAsync(User.Identity.GetUserId(), model.NewPassword);
                    if (result.Succeeded)
                    {
                        return RedirectToAction("Manage", new { Message = ManageMessageId.SetPasswordSuccess });
                    }
                    else
                    {
                        AddErrors(result);
                    }
                }
            }

            // If we got this far, something failed, redisplay form
            return View(model);
        }

        //
        // POST: /Account/ExternalLogin
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult ExternalLogin(string provider, string returnUrl)
        {
            // Request a redirect to the external login provider
            return new ChallengeResult(provider, Url.Action("ExternalLoginCallback", "Account", new { ReturnUrl = returnUrl }));
        }

        //
        // GET: /Account/ExternalLoginCallback
        [AllowAnonymous]
        public async Task<ActionResult> ExternalLoginCallback(string returnUrl)
        {
            var loginInfo = await AuthenticationManager.GetExternalLoginInfoAsync();
            if (loginInfo == null)
            {
                return RedirectToAction("Login");
            }

            // Sign in the user with this external login provider if the user already has a login
            var user = await this.userManager.FindAsync(loginInfo.Login);
            if (user != null)
            {
                await SignInAsync(user, isPersistent: false);
                return RedirectToLocal(returnUrl);
            }
            else
            {
                // If the user does not have an account, then prompt the user to create an account
                ViewBag.ReturnUrl = returnUrl;
                ViewBag.LoginProvider = loginInfo.Login.LoginProvider;
                return View("ExternalLoginConfirmation", new ExternalLoginConfirmationViewModel { UserName = loginInfo.DefaultUserName });
            }
        }

        //
        // POST: /Account/LinkLogin
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult LinkLogin(string provider)
        {
            // Request a redirect to the external login provider to link a login for the current user
            return new ChallengeResult(provider, Url.Action("LinkLoginCallback", "Account"), User.Identity.GetUserId());
        }

        //
        // GET: /Account/LinkLoginCallback
        public async Task<ActionResult> LinkLoginCallback()
        {
            var loginInfo = await AuthenticationManager.GetExternalLoginInfoAsync(XsrfKey, User.Identity.GetUserId());
            if (loginInfo == null)
            {
                return RedirectToAction("Manage", new { Message = ManageMessageId.Error });
            }
            var result = await this.userManager.AddLoginAsync(User.Identity.GetUserId(), loginInfo.Login);
            if (result.Succeeded)
            {
                return RedirectToAction("Manage");
            }
            return RedirectToAction("Manage", new { Message = ManageMessageId.Error });
        }

        //
        // POST: /Account/ExternalLoginConfirmation
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> ExternalLoginConfirmation(ExternalLoginConfirmationViewModel model, string returnUrl)
        {
            if (User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Manage");
            }

            if (ModelState.IsValid)
            {
                // Get the information about the user from the external login provider
                var info = await AuthenticationManager.GetExternalLoginInfoAsync();
                if (info == null)
                {
                    return View("ExternalLoginFailure");
                }
                var user = new ptsolUser() { UserName = model.UserName };
                var result = await this.userManager.CreateAsync(user);
                if (result.Succeeded)
                {
                    result = await this.userManager.AddLoginAsync(user.Id, info.Login);
                    if (result.Succeeded)
                    {
                        await SignInAsync(user, isPersistent: false);
                        return RedirectToLocal(returnUrl);
                    }
                }
                AddErrors(result);
            }

            ViewBag.ReturnUrl = returnUrl;
            return View(model);
        }

        //
        // POST: /Account/LogOff
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult LogOff()
        {
            AuthenticationManager.SignOut();
            return RedirectToAction("Index", "Home");
        }

        //
        // GET: /Account/ExternalLoginFailure
        [AllowAnonymous]
        public ActionResult ExternalLoginFailure()
        {
            return View();
        }

        [ChildActionOnly]
        public ActionResult RemoveAccountList()
        {
            var linkedAccounts = this.userManager.GetLogins(User.Identity.GetUserId());
            ViewBag.ShowRemoveButton = HasPassword() || linkedAccounts.Count > 1;
            return (ActionResult)PartialView("_RemoveAccountPartial", linkedAccounts);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing && this.userManager != null)
            {
                this.userManager.Dispose();
                this.userManager = null;
            }
            base.Dispose(disposing);
        }

        #region Helpers
        // Used for XSRF protection when adding external logins
        private const string XsrfKey = "XsrfId";

        private IAuthenticationManager AuthenticationManager
        {
            get
            {
                return HttpContext.GetOwinContext().Authentication;
            }
        }

        private void SignIn(ptsolUser user, bool isPersistent)
        {
            AuthenticationManager.SignOut(DefaultAuthenticationTypes.ExternalCookie);
            var identity = this.userManager.CreateIdentity(user, DefaultAuthenticationTypes.ApplicationCookie);
            AuthenticationManager.SignIn(new AuthenticationProperties() { IsPersistent = isPersistent }, identity);
        }

        private async Task SignInAsync(ptsolUser user, bool isPersistent)
        {
            AuthenticationManager.SignOut(DefaultAuthenticationTypes.ExternalCookie);
            var identity = await this.userManager.CreateIdentityAsync(user, DefaultAuthenticationTypes.ApplicationCookie);
            AuthenticationManager.SignIn(new AuthenticationProperties() { IsPersistent = isPersistent }, identity);
        }

        private void AddErrors(IdentityResult result)
        {
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError("", error);
            }
        }

        private bool HasPassword()
        {
            var user = this.userManager.FindById(User.Identity.GetUserId());
            if (user != null)
            {
                return user.PasswordHash != null;
            }
            return false;
        }

        public enum ManageMessageId
        {
            ChangePasswordSuccess,
            SetPasswordSuccess,
            RemoveLoginSuccess,
            Error
        }

        private ActionResult RedirectToLocal(string returnUrl)
        {
            if (Url.IsLocalUrl(returnUrl))
            {
                return Redirect(returnUrl);
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }

        private class ChallengeResult : HttpUnauthorizedResult
        {
            public ChallengeResult(string provider, string redirectUri) : this(provider, redirectUri, null)
            {
            }

            public ChallengeResult(string provider, string redirectUri, string userId)
            {
                LoginProvider = provider;
                RedirectUri = redirectUri;
                UserId = userId;
            }

            public string LoginProvider { get; set; }
            public string RedirectUri { get; set; }
            public string UserId { get; set; }

            public override void ExecuteResult(ControllerContext context)
            {
                var properties = new AuthenticationProperties() { RedirectUri = RedirectUri };
                if (UserId != null)
                {
                    properties.Dictionary[XsrfKey] = UserId;
                }
                context.HttpContext.GetOwinContext().Authentication.Challenge(properties, LoginProvider);
            }
        }
        #endregion
    }
}