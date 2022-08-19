using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc.Rendering;
using WebTest.Models;

namespace WebTest.ViewModel
{
    public class OrderViewModel
    {
        public int Id { get; set; }

        public List<SelectListItem> Customers { get; set; }

        public List<SelectListItem> Products { get; set; }

        public decimal Amount { get; set; }

        public DateTime OrderDate { get; set; }

    }
}
