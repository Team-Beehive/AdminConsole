using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using AdminDatabaseFramework;
using System.Drawing;

namespace AdminConsole
{
    class CatMenuItem : MenuItem
    {
        /*public CatMenuItem()
        {
            this.Icon = 
        }*/
        public MajorCategories data { get; set; }
    }
}
