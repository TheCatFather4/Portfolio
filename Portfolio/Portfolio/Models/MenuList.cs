﻿using Cafe.Core.Entities;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Portfolio.Models
{
    public class MenuList
    {
        public SelectList? Categories { get; set; }
        public int? SelectedCategoryID { get; set; }
        public SelectList? TimesOfDays { get; set; }
        public int? SelectedTimeOfDayID { get; set; }
        public string? Date { get; set; }
        public IEnumerable<Item>? Items { get; set; }
    }
}
