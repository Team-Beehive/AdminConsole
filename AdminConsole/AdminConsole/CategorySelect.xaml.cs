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
            if (m_data.s_relatedCategories.ContainsKey(m_major.MajorName))
            {
                List<MajorCategories> relatedCat = m_data.s_relatedCategories[m_major.MajorName];

                int pos = 0;
                foreach (MajorCategories catSet in relatedCat)
                {
                    ComboBox catBox = new ComboBox();
                    ComboBoxItem unselected = new ComboBoxItem();
                    unselected.Content = "Select category";
                    //catBox.Items.Add(unselected);
                    //catBox.SelectedItem = unselected;
                    foreach (MajorCategories cat in m_data.s_catList)
                    {
                        CatDropdownItem catItem = new CatDropdownItem();
                        catItem.Content = cat.categoryTitle;
                        catItem.cat = cat;
                        catItem.listIndedx = pos;

                        catBox.Items.Add(catItem);
                        if (cat.categoryTitle != null)
                        {
                            if (cat.categoryTitle == catSet.categoryTitle)
                            {
                                catBox.SelectedItem = catItem;
                            }
                        }
                    }
                    catBox.SelectionChanged += SelectionChanged;
                    RowDefinition rd = new RowDefinition();
                    CatGrid.RowDefinitions.Add(rd);
                    Grid.SetRow(catBox, pos);
                    CatGrid.Children.Add(catBox);
                    pos++;
                }
            }
            else
            {
                m_data.s_relatedCategories.Add(m_major.MajorName, new List<MajorCategories>());

                ComboBox catBox = new ComboBox();
                ComboBoxItem unselected = new ComboBoxItem();
                unselected.Content = "Select category";
                catBox.Items.Add(unselected);
                catBox.SelectedItem = unselected;
                int pos = 0;
                foreach (MajorCategories cat in m_data.s_catList)
                {
                    CatDropdownItem catItem = new CatDropdownItem();
                    catItem.Content = cat.categoryTitle;
                    catItem.cat = cat;
                    catItem.listIndedx = pos;

                    catBox.Items.Add(catItem);
                    
                }
                catBox.SelectionChanged += SelectionChanged;
                RowDefinition rd = new RowDefinition();
                CatGrid.RowDefinitions.Add(rd);
                Grid.SetRow(catBox, pos);
                CatGrid.Children.Add(catBox);
                
            }
        }

        private void AddRelatedDegree(object sender, EventArgs e)
        {
            CatDropdownItem selected = sender as CatDropdownItem;
            selected.cat.relatedDegrees.Add(m_data.s_activeData);
        }

        private void SelectionChanged(object sender, EventArgs e)
        {
            AppData.changeMajorCat change = new AppData.changeMajorCat();
            change.major = m_major;
            ComboBox box = sender as ComboBox;
            List<MajorCategories> catList = m_data.s_relatedCategories[m_major.MajorName];
            CatDropdownItem selected = box.SelectedItem as CatDropdownItem;
            m_data.s_database.Majors.AddMajorToCat(selected.cat, m_major);
            if (catList.Count < selected.listIndedx)
            { 
                change.oldCat = catList[selected.listIndedx];
                catList.RemoveAt(selected.listIndedx);
            }
            catList.Insert(selected.listIndedx, selected.cat);
            change.newCat = selected.cat;
            m_data.catsToUpdate.Add(change);
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
