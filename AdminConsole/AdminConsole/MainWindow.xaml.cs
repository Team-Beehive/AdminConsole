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
using Microsoft.Win32;

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
        private bool isConnected = false;
        public MainWindow()
        {
            InitializeComponent();
            AppData.s_previewPanel = Preview;
            AppData.s_propertiesPanel = Properties;
            AppData.s_listPanel = PageSelect;
            //Utilities.GetData();
            tb_status.Text = "";

        }

        private void ButtonPressOpenKey(object sender, EventArgs e)
        {
            OpenFileDialog openFile = new OpenFileDialog();
            openFile.Filter = "Firebase key (JSON)|*.json";
            if (openFile.ShowDialog() == true)
            {
                isConnected = Utilities.SetDatabaseKey(openFile.FileName);
                //isConnected = true;
            }
        }

        private void ButtonPressMajors(object sender, EventArgs e)
        {
            if (isConnected)
            { 
                RemovePageSelect();
                PageSelect.Children.Add(CreateElements.CreateMajorButtons(AppData.s_majorList));
            }
        }

        private void ButtonPressCategory(object sender, EventArgs e)
        {
            if(isConnected)
            {
                CreateElements.AddCatButtons(AppData.s_catList);

            }
        }

        private void ButtonPressBuilding(object sender, EventArgs e)
        {
            if (isConnected)
            { 
                RemovePageSelect();
                PageSelect.Children.Add(CreateElements.CreateBuildingButtons());
            }
        }

        private void ButtonPressProf(object sender, EventArgs e)
        {
            if (isConnected)
            { 
                RemovePageSelect();
                PageSelect.Children.Add(CreateElements.CreateProfButtons());
            }
        }

        private void ButtonPressNewCat(object sender, EventArgs e)
        {
            Debug.WriteLine("New Cat!");
        }

        private void RemovePageSelect()
        {
            if (PageSelect.Children.Count > 1)
            {
                PageSelect.Children.RemoveAt(1);
            }
        }

        //Upload the updated information to the database
        private void ButtonPressExport(object sender, EventArgs e)
        {
            if (isConnected)
            { 
                tb_status.Text = "";
                Utilities.VolitileSave();
                Utilities.UploadData();
                tb_status.Text = "Database Updated";
            }

        }
    }
}
