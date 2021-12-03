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
using AdminDatabaseInteractions;

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
        string m_activeType;
        string m_activeDesc;

        //Temp variables to hold place for database
        //Page list
        List<string> m_majors = new List<string> { "Tech", "Bio", "Math" };
        //editable variables
        List<string> m_titles = new List<string> { "Tech", "Bio", "Math" };
        List<string> m_type = new List<string> { "BS", "BS", "BS" };
        List<string> m_desc = new List<string> { "Tech degree", "Bio degree", "math degree" };
        int m_target = 0;


        public MainWindow()
        {
            InitializeComponent();
            //Temp add the properties panel, later impliment a way that this gets called/changed based on active element
            AddProperties();
            //Query database for editable pages
            test();
            //Add buttons
            AddButtons(m_majors);
        }

        private void test()
        {
            Majors major = new Majors();
            LinkedList<MajorData> temp = major.GetMajors();
            foreach (MajorData m in temp)
            {
                Debug.WriteLine(m.MajorName);
            }
        
        }

        private void AddButtons(List<string> majors)
        {
            //Add buttons for each page we get from the query

            Grid grid = new Grid();

            int btnPos = 0;
            foreach (string major in majors)
            {
                RowDefinition rd = new RowDefinition();
                grid.RowDefinitions.Add(rd);

                Button btn = new Button();
                btn.Content = major;
                btn.Name = major;
                btn.Click += ButtonPressPage;
                Grid.SetRow(btn, btnPos);
                grid.Children.Add(btn);
                btnPos++;
            }

            PageSelect.Children.Add(grid);

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
            tb.Height = 40;
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
            switch (page)
            {
                case "Tech":
                    m_target = 0;
                    break;
                case "Bio":
                    m_target = 1;
                    break;
                case "Math":
                    m_target = 2;
                    break;
            }

            m_activeTitle = m_titles[m_target];
            m_activeType = m_type[m_target];
            m_activeDesc = m_desc[m_target];

        }

        //Upload the updated information to the database
        private void ButtonPressExport(object sender, EventArgs e)
        {
            Debug.WriteLine("Export not implimented");
        }

        //Update the information locally
        private void VolitileSave()
        {
            m_titles[m_target] = m_activeTitle;
            m_type[m_target] = m_activeType;
            m_desc[m_target] = m_activeDesc;
        }

        //Select a specific page to preview
        private void ButtonPressPage(object sender, EventArgs e)
        {
            FrameworkElement page = sender as FrameworkElement;
            if (m_lastSelected != page.Name)
            {
                if (m_lastSelected != "n")
                {
                    VolitileSave();
                    Preview.Children.RemoveAt(1);
                }

                m_lastSelected = page.Name;
                //Query the selected page for its info
                Debug.WriteLine(page.Name);
                QueryPageData(page.Name);
                ShowPreview();
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

                m_properties.Text = temp.Text;
            }
        }

        //When the text in the edit box changes this gets called
        private void TextChangedTest(object sender, TextChangedEventArgs e)
        {
            TextBlock temp = m_activeElement as TextBlock;
            TextBox tb = sender as TextBox;


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
            grid.RowDefinitions.Add(r1);
            grid.RowDefinitions.Add(r2);
            grid.RowDefinitions.Add(r3);

            TextBlock title = new TextBlock();
            title.Text = m_activeTitle;
            title.Name = "title";
            title.MouseDown += ButtonPressSelected;
            Grid.SetRow(title, 0);

            TextBlock type = new TextBlock();
            type.Text = m_activeType;
            type.Name = "type";
            type.MouseDown += ButtonPressSelected;
            Grid.SetRow(type, 1);

            TextBlock desc = new TextBlock();
            desc.Text = m_activeDesc;
            desc.Name = "desc";
            desc.MouseDown += ButtonPressSelected;
            Grid.SetRow(desc, 2);

            grid.Children.Add(title);
            grid.Children.Add(type);
            grid.Children.Add(desc);

            return grid;
        }
    }
}
