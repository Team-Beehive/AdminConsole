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
        private Utilities util;
        private AppData data;
        private CreateElements elements;
        private AppEvents events;
        public MainWindow()
        {
            InitializeComponent();
            data = new AppData();
            util = new Utilities(Preview, Properties, PageSelect, data);
            elements = new CreateElements(util);
            events = new AppEvents(data, util);
            elements.SetEvents(events);
            events.SetCreateElements(elements);
            isConnected = false;
            tb_status.Text = "";

        }

        private void ButtonPressOpen(object sender, EventArgs e)
        {
            OpenFileDialog openFile = new OpenFileDialog();
            openFile.Filter = "Firebase credentials (JSON)|*.json";
            if (openFile.ShowDialog() == true)
            {
                isConnected = util.SetDatabaseKey(openFile.FileName);
                GetData(isConnected);
            }
        }

        private void ButtonPressMajors(object sender, EventArgs e)
        {
            if (isConnected)
            { 
                RemovePageSelect();
                PageSelect.Children.Add(elements.CreateMajorButtons(data.s_majorList));
            }
        }

        private void ButtonPressCategory(object sender, EventArgs e)
        {
            if (isConnected)
            { 
                elements.AddCatButtons(data.s_catList);
            }
        }

        private void ButtonPressBuilding(object sender, EventArgs e)
        {
            if (isConnected)
            { 
                RemovePageSelect();
                PageSelect.Children.Add(elements.CreateBuildingButtons(data.s_buildingList));
            }
        }

        private void ButtonPressProf(object sender, EventArgs e)
        {
            if (isConnected)
            { 
                RemovePageSelect();
                PageSelect.Children.Add(elements.CreateProfButtons(data.s_professorList));
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
            tb_status.Text = "";

            //Utilities.VolitileSave();
            //util.UploadData();

            foreach (ProfessorData p in data.s_changedProf)
            {
                data.s_professors.UpdateProfessor(p);
            }
            foreach (BuildingData b in data.s_changedBuildingList)
            {
                data.s_building.UpdateBuilding(b);
            }
            foreach (MajorData m in data.s_changedList)
            {
                data.s_major.EditMajor(m);
            }

            tb_status.Text = "Database Updated";
        }

        private void GetData(bool connected)
        {
            if (connected)
            {
                data.s_major = data.s_database.Majors;
                data.s_building = data.s_database.Buildings;
                data.s_professors = data.s_database.Professors;

                data.s_professorList = data.s_professors.GetProfessors();
                data.s_buildingList = data.s_building.GetBuildings();
                data.s_majorList = data.s_major.GetMajors();
                data.s_catList = data.s_major.GetCategories();
            }
        }
    }
}
