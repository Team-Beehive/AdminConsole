﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using AdminDatabaseFramework;

namespace AdminConsole.Elements
{
    class CatMenuItem : MenuItem
    {
        public MajorCategories data { get; set; }
    }
}
