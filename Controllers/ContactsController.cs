using System.Net.Http;
using System.Text;
using FirstProject.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace FirstProject.Controllers
{
    public class ContactsController : Controller
    {

        Uri baseAddress = new Uri("https://localhost:7097/api");
        HttpClient httpClient;

        public ContactsController()
        {
            httpClient = new HttpClient();
            httpClient.BaseAddress = baseAddress;
        }
        public async Task<IActionResult> Index()
        {
            List<Contact> modelList = new List<Contact>();
            HttpResponseMessage response = httpClient.GetAsync(httpClient.BaseAddress + "/Contacts").Result;
            if (response.IsSuccessStatusCode)
            {
                string data = response.Content.ReadAsStringAsync().Result;
                modelList = JsonConvert.DeserializeObject<List<Contact>>(data);
            }
            return View(modelList);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(Contact contact)
        {
            string data = JsonConvert.SerializeObject(contact);
            StringContent content = new StringContent(data, Encoding.UTF8, "application/json");
            HttpResponseMessage response = httpClient.PostAsync(httpClient.BaseAddress + "/Contacts", content).Result;

            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("Index");
            }
            return View();
        }

        public async Task<IActionResult> Edit(Guid id)
        {
            Contact model = new Contact();
            HttpResponseMessage response = httpClient.GetAsync(httpClient.BaseAddress + "/Contacts/" + id).Result;
            if (response.IsSuccessStatusCode)
            {
                string data = response.Content.ReadAsStringAsync().Result;
                model = JsonConvert.DeserializeObject<Contact>(data);
            }
            return View("Edit", model);
        }

        [HttpPatch]
        public async Task<IActionResult> Edit(Contact contact)
        {
            string data = JsonConvert.SerializeObject(contact);
            StringContent content = new StringContent(data, Encoding.UTF8, "application/json");
            HttpResponseMessage response = httpClient.PatchAsync(httpClient.BaseAddress + "/Contacts/" + contact.Id, content).Result;
            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("Index");
            }
            return View("Edit", contact);
        }

        public async Task<IActionResult> Delete(Guid id)
        {
            Contact model = new Contact();
            HttpResponseMessage response = httpClient.GetAsync(httpClient.BaseAddress + "/Contacts/" + id).Result;
            if (response.IsSuccessStatusCode)
            {
                string data = response.Content.ReadAsStringAsync().Result;
                model = JsonConvert.DeserializeObject<Contact>(data);
            }
            return View("Delete", model);
        }

        [HttpDelete, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(Contact contact)
        {
            HttpResponseMessage response = httpClient.DeleteAsync(httpClient.BaseAddress + "/Contacts/" + contact.Id).Result;

            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("Index");
            }
            return View("Delete", contact);
        }
    }
}
