﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
using System;
using System.Collections.Generic;

namespace vms.entity.models
{
    public partial class Color
    {
        public Color()
        {
            Themes = new HashSet<Theme>();
        }

        public int ColorId { get; set; }
        public string Name { get; set; }
        public string Frist { get; set; }
        public string Sec { get; set; }
        public string Third { get; set; }
        public string Forth { get; set; }

        public virtual ICollection<Theme> Themes { get; set; }
    }
}