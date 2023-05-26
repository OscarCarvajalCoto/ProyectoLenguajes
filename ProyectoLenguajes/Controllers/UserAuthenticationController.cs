using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ProyectoLenguajes.Models.DTO;
using ProyectoLenguajes.Repositories.Abstract;
using System.Net.Mail;

namespace ProyectoLenguajes.Controllers
{
    public class UserAuthenticationController : Controller
    {
        private readonly IUserAuthenticationService _service;
        public UserAuthenticationController(IUserAuthenticationService service)
        {
            this._service = service;
        }

        public IActionResult Registration()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Registration(RegistrationModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            model.Role = "client";
            var result = await _service.RegistrationAsync(model);
            TempData["msg"] = result.Message;
            string subject = "Welcome Dear " + model.Name;
            string body =   "You have registered successfully in our database, here you have your profile information <br>" +
                            "Name: " + model.Name + "<br>" +
                            "Username: " + model.UserName + "<br>" +
                            "Password: " + model.Password + "<br>" +
                            "Role: " + model.Role + "<br>";
            SendEmail(model.Email, subject, body);
            return RedirectToAction(nameof(Registration));
        }

        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            var result = await _service.LoginAsync(model);
            if (result.StatusCode == 1)
            {
                return RedirectToAction("Index", "Home");
            }
            else
            {
                TempData["msg"] = result.Message;
                return RedirectToAction(nameof(Login));
            }
        }

        [Authorize]
        public async Task<IActionResult> Logout()
        {
            await _service.LogoutAsync();
            return RedirectToAction(nameof(Login));
        }

        public void SendEmail(string destinationEmail, string subject, string message)
        {
            try
            {
                MailMessage email = new MailMessage();
                email.From = new MailAddress("application.assistant.bmw.11@gmail.com");
                email.To.Add(destinationEmail);
                email.Subject = subject;
                email.Body = message;
                email.IsBodyHtml = true;
                email.Priority = MailPriority.Normal;

                //In case of sending files (Search Code)

                SmtpClient smtp = new SmtpClient();
                smtp.Host = "smtp.gmail.com";
                smtp.Port = 25;
                smtp.EnableSsl = true;
                smtp.UseDefaultCredentials = false;
                string senderEmail = "application.assistant.bmw.11@gmail.com";
                string senderEmailPass = "phibuntqvaziimgs";
                smtp.Credentials = new System.Net.NetworkCredential(senderEmail, senderEmailPass);

                smtp.Send(email);
                Console.WriteLine("Email sended successful");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }

        //public async Task<IActionResult> Reg()
        //{
        //    var model = new RegistrationModel
        //    {
        //        UserName = "super_admin_w",
        //        Name = "Wilson-Mata",
        //        Email = "wilsonbm@gmail.com",
        //        Password = "SuperAdmin2023*",
        //    };
        //    model.Role = "superadmin";
        //    var result = await _service.RegistrationAsync(model);
        //    return Ok(result);

        //}

    }
}
