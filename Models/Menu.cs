using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Moment2.Models
{
    public class Menu
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Obligatorisk")]
        public string DishName { get; set; }
        public string DishDescription { get; set; }
        [Required(ErrorMessage = "Obligatorisk")]
        public double? Price { get; set; }
        public string Vego { get; set; } = "Nej";
        public string[] VegoCtrl = new[] { "Nej", "Ja" }; //Värden för radiobuttons
        public string DishType { get; set; }
        public List<SelectListItem> DishTypes { get; } = new List<SelectListItem>
        {   //Options till select-lista
            new SelectListItem { Value = "Förrätt", Text = "Förrätt" },
            new SelectListItem { Value = "Varmrätt", Text = "Varmrätt" },
            new SelectListItem { Value = "Efterrätt", Text = "Efterrätt"  },
        };
        public Menu()
        {

        }
    }
}
