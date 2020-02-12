using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using Microsoft.Extensions.Logging;
using System.IO;
using Newtonsoft.Json;
using Moment2.Models;
using Microsoft.AspNetCore.Http;

namespace Moment2.Controllers
{
    public class HomeController : Controller
    {
        [HttpGet("/")] //Ändrar grundrouten för Index-sidan
        public IActionResult Index([FromQuery]string Ort) //Tar ev frågesträng med ort som argument
        {
            if (HttpContext.Session.GetString("ort") == null) //Kontrollerar om session är tom
                HttpContext.Session.SetString("ort", "Stockholm"); //Sätter Stockholm som default-ort i session
            if (Ort != null)
                HttpContext.Session.SetString("ort", Ort); //Sätter nytt val av ort som värde i session

            ViewData["ort"] = HttpContext.Session.GetString("ort"); //Hämtar lagrad värde för ort i session och lagrar i ViewData

            return View();
        }
        [HttpGet("/om")] //Ändrar grundrouten för About-sidan
        public IActionResult About([FromQuery]string Ort) //Tar ev frågesträng med ort som argument
        {
            if (HttpContext.Session.GetString("ort") == null) //Kontrollerar om session är tom
                HttpContext.Session.SetString("ort", "Stockholm"); //Sätter Stockholm som default-ort i session
            if (Ort != null)
                HttpContext.Session.SetString("ort", Ort); //Sätter nytt val av ort som värde i session

            ViewData["ort"] = HttpContext.Session.GetString("ort"); //Hämtar lagrad värde för ort i session och lagrar i ViewData

            return View();
        }
        [HttpGet("/meny")] //Ändrar grundrouten för Menu-sidan
        public IActionResult Menu([FromQuery]string Ort) //Tar ev frågesträng med ort som argument
        {
            if (HttpContext.Session.GetString("ort") == null) //Kontrollerar om session är tom
                HttpContext.Session.SetString("ort", "Stockholm"); //Sätter Stockholm som default-ort i session
            if (Ort != null)
                HttpContext.Session.SetString("ort", Ort); //Sätter nytt val av ort som värde i session

            ViewData["ort"] = HttpContext.Session.GetString("ort"); //Hämtar lagrad värde för ort i session och lagrar i ViewData

            var jsonStr = System.IO.File.ReadAllText("menuitems.json"); //Hämtar data ur JSON-fil och lagar i variabel
            var jsonObj = JsonConvert.DeserializeObject<IEnumerable<Menu>>(jsonStr); //Konverterar JSON-sträng till objekt av typ IEnumerable<Model>
            return View(jsonObj); //Skickar objektet till view
        }
        [HttpGet]
        public IActionResult Admin([FromRoute]int Id) //Tar id som skickats som parameter i URL som argument
        {
            var jsonStr = System.IO.File.ReadAllText("menuitems.json"); //Hämtar data i JSON-fil

            var jsonObj = JsonConvert.DeserializeObject<List<Menu>>(jsonStr); //Konverterar JSON-sträng till Lista av typen Model
            for (var i = 0; i < jsonObj.Count; ++i) //Jämför objektens id med id:t som skickats med som argument och tar bort det objekt vars id överensstämmer
            {
                if (jsonObj[i].Id == Id)
                {
                    jsonObj.Remove(jsonObj[i]);
                }
            }
            System.IO.File.WriteAllText("menuitems.json", JsonConvert.SerializeObject(jsonObj, Formatting.Indented)); //Skriver om datan i JSON-filen

            var updJsonStr = System.IO.File.ReadAllText("menuitems.json"); //Hämtar ny version av data i JSON-fil
            var jsonObj2 = JsonConvert.DeserializeObject<IEnumerable<Menu>>(updJsonStr); //Konverterar till objekt av typen IEnumerable<Model>
            ViewBag.menuList = jsonObj2; //Lagrar objekt i VewBag
            return View(new Menu()); //Skickar ny instans av model till view
        }

        [HttpPost]
        public IActionResult Admin(Menu model) //Tar modellen som fyllts med värden från formulär som argument
        {
            if (ModelState.IsValid) //Kontrollerar add modellen validerar
            {
                int id = 0; //Variabel för id-värde
                var jsonStr = System.IO.File.ReadAllText("menuitems.json"); //Hämtar JSON-data
                var jsonObj = JsonConvert.DeserializeObject<IEnumerable<Menu>>(jsonStr); //Konverterar till objekt av typen IEnumerable<Model>
                foreach (var item in jsonObj) //Loopar igenom datat i objektet för att få fram högsta id:t
                {
                    if (item.Id > id)
                        id = item.Id;
                }
                model.Id = id + 1; //Skapar id för det nya modellobjektet som är ett högre än det högsta

                var jsonObj2 = JsonConvert.DeserializeObject<List<Menu>>(jsonStr); //Konverterar JSON-datat till Lista av typen Model
                jsonObj2.Add(model); //Lagrar nytt modellobjekt i listan med resterande data

                System.IO.File.WriteAllText("menuitems.json", JsonConvert.SerializeObject(jsonObj2, Formatting.Indented)); //Uppdaterar data i JSON-fil
                ModelState.Clear(); //Tar bort värden i modell/värden i inputfält
            }
            var updJsonStr = System.IO.File.ReadAllText("menuitems.json"); //Hämtar data i JSON-fil
            var jsonObj3 = JsonConvert.DeserializeObject<IEnumerable<Menu>>(updJsonStr); //Konverterar till objekt av typen IEnumerable<Model>
            ViewBag.menuList = jsonObj3; //Lagrar objekt i VewBag

            return View(new Menu()); //Skickar ny instans av model till view
        }
    }
}