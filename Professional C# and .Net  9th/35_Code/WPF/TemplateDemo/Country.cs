﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TemplateDemo
{
    public class Country
    {
        public string Name { get; set; }
        public string ImagePath { get; set; }

        public override string ToString()
        {
            return Name;
        }
    }

}
