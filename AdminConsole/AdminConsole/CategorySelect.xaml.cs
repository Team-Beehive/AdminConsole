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
        MajorData m_major;
        public CategorySelect(AppData data, MajorData major)
        {
            m_data = data;
            m_major = major;
            InitializeComponent();
            PopulateDropdown();
        }

        private void PopulateDropdown()
        {
            List<MajorCategories> relatedCat = m_data.s_relatedCategories[m_major.MajorName];
            int pos = 0;
            foreach (MajorCategories catSet in relatedCat)
            { 
                ComboBox catBox = new ComboBox();
                ComboBoxItem unselected = new ComboBoxItem();
                unselected.Content = "Select category";
                catBox.Items.Add(unselected);
                catBox.SelectedItem = unselected;
                foreach (MajorCategories cat in m_data.s_catList)
                {
                    CatDropdownItem catItem = new CatDropdownItem();
                    catItem.Content = cat.categoryTitle;
                    catItem.cat = cat;

                    catBox.Items.Add(catItem);
                    if (cat.categoryTitle == catSet.categoryTitle)
                    {
                        catBox.SelectedItem = catItem;
                    }
                }
                RowDefinition rd = new RowDefinition();
                CatGrid.RowDefinitions.Add(rd);
                Grid.SetRow(catBox, pos);
                CatGrid.Children.Add(catBox);
                pos++;
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
