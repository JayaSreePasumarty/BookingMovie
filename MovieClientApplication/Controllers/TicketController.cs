using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MovieClientApplication.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace MovieClientApplication.Controllers
{
    public class TicketController : Controller
    {

         [HttpGet]
        public IActionResult Index()
        {
            return View();
        }
        public async Task<IActionResult> Index(Ticket b)
        {
            if (HttpContext.Session.GetString("token") == null)
            {

                return RedirectToAction("Login", "Login");

            }
            else
            {
                using (var client = new HttpClient())
                {
                    StringContent content1 = new StringContent(JsonConvert.SerializeObject(b), Encoding.UTF8, "application/json");
                    var contentType = new MediaTypeWithQualityHeaderValue("application/json");
                    client.DefaultRequestHeaders.Accept.Add(contentType);

                    client.DefaultRequestHeaders.Authorization =
            new AuthenticationHeaderValue("Bearer", HttpContext.Session.GetString("token"));

                    using (var response = await client.PostAsync("https://localhost:44325/api/Ticket", content1))
                    {
                        

                        if (response.IsSuccessStatusCode)
                        {
                            return RedirectToAction("Index", "Booking");
                        }
                        else
                        {
                            return RedirectToAction("Login", "Login");
                        }
                        //string apiResponse1 = await response.Content.ReadAsStringAsync();

                    }
                }
                // return RedirectToAction("Index");
            }

        }
    }
}
