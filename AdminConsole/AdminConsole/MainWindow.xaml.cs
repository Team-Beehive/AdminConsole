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
using System.Diagnostics;
using AdminDatabaseFramework;

namespace AdminConsole
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    /// 

    /*
     * Author: Destiny (Destin) Dahlgren
     * Purpose: This is the logic for the UI of the admin program
     */



    /*TODO: Use a dictionary to keep track of the relationship between items and "active" variables
     *          title text -> m_active title
     *          This would allow the page to scale better and not have to deal with the switch statment to figure out
     *          what varable the selected text should go to
     *      PRIORITY: HIGH
     *          
     *TODO: Pull certain functions out and make them into their own class
     *          Create a class responsible for creating visual elements
     *          Create a class that is responsible for handling input
     *      PRIORITY: LOW
    */


    public partial class MainWindow : Window
    {
        //NOTE: in the main branch these are removed
        object m_activeElement;
        TextBox m_properties;
        string m_lastSelected = "n";
        string m_activeTitle;
        string m_activeClasses;
        string m_activeProfessors;
        string m_activeCampus;
        string m_activeType;
        string m_activeDesc;
        bool m_hasChanged = false;

        List<MajorData> m_changedList = new List<MajorData>();
        MajorData m_activeData;
        Majors m_major = new Majors();
        LinkedList<MajorData> m_majorList;

        //New Branch related items
        LinkedList<MajorCategories> m_catList;
        MajorCategories m_activeCat;

        public MainWindow()
        {
            InitializeComponent();
            AppData.s_previewPanel = Preview;
            AppData.s_propertiesPanel = Properties;
            AppData.s_listPanel = PageSelect;
            Utilities.GetData();
            tb_status.Text = "";
        }

        //Functions added to this branch =========================================

        private void ButtonPressMajors(object sender, EventArgs e)
        {
            //AddButtons(m_majorList);
            //PageSelect.Children.Add(CreateElements.CreateMajorButtons(AppData.s_majorList));
            CreateElements.CreateMajorButtons(AppData.s_majorList);
        }

        private void ButtonPressCategory(object sender, EventArgs e)
        {
            //AddCatButtons(m_catList);
            CreateElements.AddCatButtons(AppData.s_catList);
        }

        /*private void ButtonPressCatProp(object sender, EventArgs e)
        {
            Button temp = sender as Button;
            
            foreach (MajorCategories m in m_catList)
            {
                if (m.categoryTitle == temp.Content.ToString())
                {
                    m.oldTitle = m.categoryTitle;
                    m_activeCat = m;
                    break;
                }
            }
            AddCatProp();
            m_properties.Text = temp.Content.ToString();
        }*/

        private void ButtonPressNewCat(object sender, EventArgs e)
        {
            Debug.WriteLine("New Cat!");
        }

        /*private void ButtonPressUpdateCat(object sender, EventArgs e)
        {
            m_major.EditMajorCatagoryTitle(m_activeCat);
            CreateElements.AddCatButtons(m_catList);
        }*/

        /*private void TextBoxCatName(object sender, TextChangedEventArgs e)
        {
            TextBox tb = sender as TextBox;
            m_activeCat.categoryTitle = tb.Text;
            Debug.WriteLine(m_activeCat.categoryTitle);
        }*/

        /*private void AddCatButtons(LinkedList<MajorCategories> catList)
        {
            Debug.WriteLine("Generating buttons");
            if (PageSelect.Children.Count > 1)
            {
                PageSelect.Children.RemoveAt(1);
            }

            Grid grid = new Grid();

            int btnPos = 0;
            foreach (MajorCategories cat in catList)
            {
                RowDefinition rd = new RowDefinition();
                grid.RowDefinitions.Add(rd);
                string cleanName = cleanString(cat.categoryTitle);

                Button btn = new Button();
                btn.Content = cat.categoryTitle;
                btn.Name = cleanName;
                btn.Click += ButtonPressCatProp;
                Grid.SetRow(btn, btnPos);
                grid.Children.Add(btn);
                btnPos++;
            }

            Button addbtn = new Button();
            addbtn.Content = "Add new category";
            addbtn.Name = "AddCat";
            addbtn.Click += ButtonPressNewCat;
            Grid.SetRow(addbtn, btnPos);
            grid.Children.Add(addbtn);

            PageSelect.Children.Add(grid);
        }*/

        /*private void AddCatProp()
        {
            if (Properties.Children.Count > 1)
            {
                Properties.Children.RemoveAt(1);
            }

            Grid grid = new Grid();
            RowDefinition rd0 = new RowDefinition();
            RowDefinition rd1 = new RowDefinition();
            grid.RowDefinitions.Add(rd0);
            grid.RowDefinitions.Add(rd1);
            TextBox tb = new TextBox();
            tb.Height = 80;
            tb.TextWrapping = TextWrapping.Wrap;
            tb.TextChanged += TextBoxCatName;
            m_properties = tb;
            Grid.SetRow(tb, 0);
            grid.Children.Add(tb);

            Button btn = new Button();
            btn.Content = "Update";
            btn.Click += ButtonPressUpdateCat;
            Grid.SetRow(btn, 1);
            grid.Children.Add(btn);

            Properties.Children.Add(grid);
        }*/

        //Functions changed for this branch =============================================
        /*private void GetData()
        {
            m_majorList = m_major.GetMajors();
            m_catList = m_major.GetCategories();
        }*/

        //private void AddButtons(List<string> majors)
        /*private void AddButtons(LinkedList<MajorData> majors)
        {
            //Add buttons for each page we get from the query

            if (PageSelect.Children.Count > 1)
            {
                PageSelect.Children.RemoveAt(1);
            }


            //TODO: get a scroll bar for this working
            Grid grid = new Grid();
            ScrollViewer scroll = new ScrollViewer();
            scroll.VerticalScrollBarVisibility = ScrollBarVisibility.Auto;


            int btnPos = 0;
            foreach (MajorData major in majors)
            {
                RowDefinition rd = new RowDefinition();
                grid.RowDefinitions.Add(rd);
                string cleanName = cleanString(major.MajorName);

                Button btn = new Button();
                btn.Content = major.MajorName;
                btn.Name = cleanName;
                btn.Click += ButtonPressPage;
                Grid.SetRow(btn, btnPos);
                grid.Children.Add(btn);
                btnPos++;
            }

            scroll.Content = grid;

            //PageSelect.Children.Add(grid);
            PageSelect.Children.Add(scroll);

        }*/


        //Upload the updated information to the database
        private void ButtonPressExport(object sender, EventArgs e)
        {
            tb_status.Text = "";
            Utilities.VolitileSave();
            Utilities.UploadData();
            tb_status.Text = "Database Updated";
        }


    }
}
