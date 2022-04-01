using AdminDatabaseFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace AdminConsole
{
    /// <summary>
    /// Interaction logic for CategorySelect.xaml
    /// </summary>
    public partial class CategorySelect : UserControl
    {
        AppData m_data;
        public CategorySelect(AppData data)
        {
            m_data = data;
            InitializeComponent();
            PopulateDropdown();
        }

        private void PopulateDropdown()
        {
            ComboBoxItem unselected = new ComboBoxItem();
            unselected.Content = "Select category";
            CategoryDropdown.Items.Add(unselected);
            CategoryDropdown.SelectedItem = unselected;
            foreach (MajorCategories cat in m_data.s_catList)
            {
                CatDropdownItem catItem = new CatDropdownItem();
                catItem.Content = cat.categoryTitle;
                catItem.cat = cat;

                CategoryDropdown.Items.Add(catItem);
            }
        }

        private void AddRelatedDegree(object sender, EventArgs e)
        {
            CatDropdownItem selected = sender as CatDropdownItem;
            selected.cat.relatedDegrees.Add(m_data.s_activeData);
        }

        private bool HasRelatedCategory()
        {
            foreach (MajorCategories cat in m_data.s_catList)
            {

            }
            return false;
        }
    }
}
