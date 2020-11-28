using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MovieClientApplication.Models;
using Newtonsoft.Json;

namespace MovieClientApplication.Controllers
{
    public class LoginController : Controller
    {
        //static readonly log4net.ILog _log4net = log4net.LogManager.GetLogger(typeof(LoginController));

        public IActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(User user)
        {

            //  _log4net.Info("User Login");
            User Item = new User();
            using (var httpClient = new HttpClient())
            {
                StringContent content1 = new StringContent(JsonConvert.SerializeObject(user), Encoding.UTF8, "application/json");
                using (var response1 = await httpClient.PostAsync("https://localhost:44339/api/Auth/Login", content1))
                {
                    if (response1.StatusCode == System.Net.HttpStatusCode.OK)
                    {

                        string apiResponse1 = await response1.Content.ReadAsStringAsync();

                        string stringJWT = response1.Content.ReadAsStringAsync().Result;

                        JWT jwt = JsonConvert.DeserializeObject<JWT>(stringJWT);

                        HttpContext.Session.SetString("token", jwt.Token);
                        HttpContext.Session.SetString("user", JsonConvert.SerializeObject(user));
                        HttpContext.Session.SetInt32("Userid", user.User_Id);
                        HttpContext.Session.SetString("Username", user.User_Name);
                        ViewBag.Message = "User logged in successfully!";

                        return RedirectToAction("Index", "Booking");
                        
                    }
                    else
                    {
                        return View(user);
                    }


                }
            }
        }
        public ActionResult Logout()
        {
            // _log4net.Info("User Log Out");
            HttpContext.Session.Remove("token");
            // HttpContext.Session.SetString("user", null);

            return View("Login");
        }
    }
}