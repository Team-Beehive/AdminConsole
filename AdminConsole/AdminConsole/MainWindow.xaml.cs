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
        

        public MainWindow()
        {
            InitializeComponent();
            //Temp add the properties panel, later impliment a way that this gets called/changed based on active element
            //AddProperties();
            //Query database for editable pages
            //test();
            GetData();
            //Add buttons
            //AddButtons(m_majors);
            AddButtons(m_majorList);
            tb_status.Text = "";
        }



        private void GetData()
        {
            m_majorList = m_major.GetMajors();
        }

        //private void AddButtons(List<string> majors)
        private void AddButtons(LinkedList<MajorData> majors)
        {
            //Add buttons for each page we get from the query

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

        }

        private string cleanString(string str)
        {
            //string clean = String.Concat(str.Where(c => !Char.IsWhiteSpace(c)));
            //clean = String.Concat(clean.Where(c => !Char.IsSymbol(c)));
            HashSet<char> set = new HashSet<char>(" !@#$%^&*()_+-=,:;<>");
            StringBuilder sb = new StringBuilder(str.Length);
            foreach (char x in str.Where(c => !set.Contains(c)))
            {
                sb.Append(x);
            }
            
            
            return sb.ToString();
        }

        //This function creates and adds a properties panel to the page
        private void AddProperties()
        {
            //Temp for text editing properties
            Grid grid = TextProperties();
            Properties.Children.Add(grid);
        }

        //Create a preview of the page for clicking and editing
        private void ShowPreview()
        {
            Grid grid = GetPageFormat();
            Preview.Children.Add(grid);
        }

        //Select the page to edit
        private Grid GetPageFormat()
        {
            //Select the correct template and get the relivent information
            //GetDatabaseInfo()
            Grid grid = MajorsTemplate();
            return grid;
        }

        //Properties panel for changing text
        private Grid TextProperties()
        {
            //Currently it can only change what text is there
            Grid grid = new Grid();
            TextBox tb = new TextBox();
            tb.Height = 80;
            tb.TextWrapping = TextWrapping.Wrap;
            tb.TextChanged += TextChangedTest;
            //tb.Name = "TextBoxProperties";
            m_properties = tb;
            Grid.SetRow(tb, 0);
            grid.Children.Add(tb);

            return grid;
        }

        //Query a specific page and get its data
        //Later change the perameter to a better way to target the page
        private void QueryPageData(string page)
        {
            //Replace all of this for a query
            //MajorData temp = null;
            foreach (MajorData m in m_majorList)
            {
                if (cleanString(m.MajorName) == page)
                {
                    m_activeData = m;
                    break;
                }
            }

            //Debug.WriteLine("You pressed: " + temp.MajorName);

            //try
            //{
            bool AddedToNull = false;

            m_activeTitle = m_activeData.MajorName;
            //if (m_activeData.about != null)
            //{
                m_activeDesc = m_activeData.about[0];
            //}
            //if (m_activeData.type != null)
            //{
                m_activeType = m_activeData.type[0];
            //}
            //m_activeProfessors = m_activeData.Professors[0];
            m_activeCampus = m_activeData.campuses[0];

            if (m_activeData.Classes != null)
            {
                m_activeClasses = m_activeData.Classes[0];
            }
            else
            {
                m_activeData.Classes = new List<string>() { "No Classes" };
                m_activeClasses = m_activeData.Classes[0];
                AddedToNull = true;
            }

            if (AddedToNull)
            {
                VolitileSave();
            }

            //}
            //catch (Exception e)
            //{
            //    Debug.WriteLine(e);
            //}


        }

        //Upload the updated information to the database
        private void ButtonPressExport(object sender, EventArgs e)
        {
            tb_status.Text = "";
            VolitileSave();
            UploadData();
            tb_status.Text = "Database Updated";
        }

        //Update the information locally
        private void VolitileSave()
        {
            m_activeData.type[0] = m_activeType;
            m_activeData.about[0] = m_activeDesc;
            m_activeData.Classes[0] = m_activeClasses;
            if (m_hasChanged)
            {
                m_major.UpdateLocal();
                if (!m_changedList.Contains(m_activeData))
                {
                    m_changedList.Add(m_activeData);
                }
                m_hasChanged = false;
            }

        }

        //Select a specific page to preview
        private void ButtonPressPage(object sender, EventArgs e)
        {
            FrameworkElement page = sender as FrameworkElement;
            if (m_lastSelected != page.Name)
            {
                AddProperties();
                if (m_lastSelected != "n")
                {
                    VolitileSave();
                    Preview.Children.RemoveAt(1);
                    //clear text in properties
                    if (Properties.Children.Count > 1)
                    {
                        Properties.Children.RemoveAt(1);
                    }
                }

                m_lastSelected = page.Name;
                QueryPageData(page.Name);
                ShowPreview();
            }
        }

        private void UploadData()
        {
            //tb_status.Text = "Uploading...";
            foreach (MajorData m in m_changedList)
            {
                m_major.EditMajor(m);
            }
        }

        //Select a specific element to edit
        public void ButtonPressSelected(object sender, EventArgs e)
        {
            //Currently it is only set up to select and change text blocks
            m_activeElement = sender;
            TextBlock temp = sender as TextBlock;
            if (temp != null)
            {
                //AddProperties();
                m_properties.Text = temp.Text;
            }
        }

        //When the text in the edit box changes this gets called
        private void TextChangedTest(object sender, TextChangedEventArgs e)
        {
            TextBlock temp = m_activeElement as TextBlock;
            TextBox tb = sender as TextBox;

            m_hasChanged = true;

            if (temp != null)
            {
                switch (temp.Name)
                {
                    case "title":
                        m_activeTitle = tb.Text;
                        break;
                    case "type":
                        m_activeType = tb.Text;
                        break;
                    case "desc":
                        m_activeDesc = tb.Text;
                        break;
                    case "classes":
                        m_activeClasses = tb.Text;
                        break;
                    case "prof":
                        m_activeProfessors = tb.Text;
                        break;
                    case "camp":
                        m_activeCampus = tb.Text;
                        break;
                }
                temp.Text = tb.Text;
            }

        }

        //Temporary template for the majors page
        private Grid MajorsTemplate()
        {
            Grid grid = new Grid();

            //In WPF you absolutly HAVE to create row/collum definitions to work
            RowDefinition r1 = new RowDefinition();
            RowDefinition r2 = new RowDefinition();
            RowDefinition r3 = new RowDefinition();
            RowDefinition r4 = new RowDefinition();
            RowDefinition r5 = new RowDefinition();
            RowDefinition r6 = new RowDefinition();
            grid.RowDefinitions.Add(r1);
            grid.RowDefinitions.Add(r2);
            grid.RowDefinitions.Add(r3);
            grid.RowDefinitions.Add(r4);
            grid.RowDefinitions.Add(r5);
            grid.RowDefinitions.Add(r6);

            TextBlock title = new TextBlock();
            title.Text = m_activeTitle;
            title.Name = "title";
            title.MouseDown += ButtonPressSelected;
            Grid.SetRow(title, 0);

            TextBlock classes = new TextBlock();
            classes.Text = m_activeClasses;
            classes.Name = "classes";
            classes.MouseDown += ButtonPressSelected;
            Grid.SetRow(classes, 1);

            //TextBlock prof = new TextBlock();
            //prof.Text = m_activeProfessors;
            //prof.Name = "prof";
            //prof.MouseDown += ButtonPressSelected;
            //Grid.SetRow(prof, 2);

            TextBlock camp = new TextBlock();
            camp.Text = m_activeCampus;
            camp.Name = "camp";
            camp.MouseDown += ButtonPressSelected;
            Grid.SetRow(camp, 2);

            TextBlock type = new TextBlock();
            type.Text = m_activeType;
            type.Name = "type";
            type.MouseDown += ButtonPressSelected;
            //Grid.SetRow(type, 4);
            Grid.SetRow(type, 3);

            TextBlock desc = new TextBlock();
            desc.Text = m_activeDesc;
            desc.Name = "desc";
            desc.MouseDown += ButtonPressSelected;
            desc.TextWrapping = TextWrapping.Wrap;
            //Grid.SetRow(desc, 5);
            Grid.SetRow(desc, 4);

            grid.Children.Add(title);
            //grid.Children.Add(classes);
            //grid.Children.Add(prof);
            grid.Children.Add(camp);
            grid.Children.Add(type);
            grid.Children.Add(desc);

            return grid;
        }
    }
}
