using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using AdminDatabaseFramework;

namespace AdminConsole 
{
    class CatDropdownItem : ComboBoxItem
    {
        public MajorCategories cat { get; set; }
    }
}
