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
using System.Xaml;

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



    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            AppData.s_previewPanel = Preview;
            AppData.s_propertiesPanel = Properties;

            Utilities.GetData();
            PageSelect.Children.Add(CreateElements.CreateMajorButtons(AppData.s_majorList));
            //PageSelect.Children.Add(CreateElements.CreateSectionButtons());
            


            tb_status.Text = "";
        }

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
