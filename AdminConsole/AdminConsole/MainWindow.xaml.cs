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
using System.Resources;
using System.Reflection;
using System.Collections;

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
            //isConnected = false;
            tb_status.Text = "";
            LoadDbFromSavedString();
        }

        private void LoadDbFromSavedString()
        {
            ResXResourceReader rsxr;
            IDictionaryEnumerator dict;
            try
            {
                rsxr = new ResXResourceReader("Resources.resx");
                dict = rsxr.GetEnumerator();
            }
            catch
            {
                using (ResXResourceWriter resx = new ResXResourceWriter(@".\Resources.resx"))
                {
                    resx.Generate();
                    resx.Close();
                }
                rsxr = new ResXResourceReader("Resources.resx");
                dict = rsxr.GetEnumerator();
            }
            while (dict.MoveNext())
            {
                if (dict.Key.ToString() == "EnvPath" && dict.Value != null)
                {
                    LoadingIndicator.Show();
                    try
                    {
                    isConnected = util.SetDatabaseKey(dict.Value.ToString());
                    GetData(isConnected);
                    }
                    catch
                    {
                        using (ResXResourceWriter resx = new ResXResourceWriter(@".\Resources.resx"))
                        {
                            resx.AddResource("EnvPath", dict.Value.ToString());
                            resx.Generate();
                            resx.Close();
                        }
                        ErrorWindow error = new ErrorWindow(new DatabaseException("Saved credentials did failed, please upload new credentials"));
                        error.Show();
                    }

                    if (isConnected)
                    {
                        ell_ConnectionIndicator.Fill = new SolidColorBrush(Color.FromRgb(90, 245, 66));
                        ell_ConnectionIndicator.ToolTip = "Connected";
                        LoadingIndicator.Hide();

                    }
                    else
                    {
                        ell_ConnectionIndicator.Fill = new SolidColorBrush(Color.FromRgb(255, 0, 0));
                        ell_ConnectionIndicator.ToolTip = "Not Connected";
                        LoadingIndicator.Hide();
                    }
                }
            }
        }
        private void ButtonPressOpen(object sender, EventArgs e)
        {
            OpenFileDialog openFile = new OpenFileDialog();
            openFile.Filter = "Firebase credentials (JSON)|*.json";
            LoadingIndicator.Show();
            if (openFile.ShowDialog() == true)
            {
                isConnected = util.SetDatabaseKey(openFile.FileName);
                GetData(isConnected);
            }
            if(isConnected)
            {
                ell_ConnectionIndicator.Fill= new SolidColorBrush(Color.FromRgb(90, 245, 66));
                ell_ConnectionIndicator.ToolTip = "Connected";
                LoadingIndicator.Hide();
                using (ResXResourceWriter resx = new ResXResourceWriter(@".\Resources.resx"))
                {
                    resx.AddResource("EnvPath", openFile.FileName);
                    resx.Generate();
                    resx.Close();
                }
            }    
            else
            {
                ell_ConnectionIndicator.Fill = new SolidColorBrush(Color.FromRgb(255, 0, 0));
                ell_ConnectionIndicator.ToolTip = "Not Connected";
                LoadingIndicator.Hide();
            }
        }

        private void ButtonPressMajors(object sender, EventArgs e)
        {
            if (isConnected)
            { 
                RemovePageSelect();
                //PageSelect.Children.Add(elements.CreateMajorButtons(data.s_majorList));
                PageSelect.Children.Add(data.majorButtonList);
                util.ClearProperties();
            }
        }

        private void ButtonPressCategory(object sender, EventArgs e)
        {
            if (isConnected)
            { 
                elements.AddCatButtons(data.s_catList);
                util.ClearProperties();

            }
        }

        private void ButtonPressBuilding(object sender, EventArgs e)
        {
            if (isConnected)
            { 
                RemovePageSelect();
                //PageSelect.Children.Add(elements.CreateBuildingButtons(data.s_buildingList));
                PageSelect.Children.Add(data.buildingButtonList);
                util.ClearProperties();
            }
        }

        private void ButtonPressProf(object sender, EventArgs e)
        {
            if (isConnected)
            { 
                RemovePageSelect();
                PageSelect.Children.Add(data.profButtonList);
                util.ClearProperties();
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
            foreach (AppData.changeMajorCat c in data.catsToUpdate)
            {
                //data.s_major.RemoveMajorFromCat(c.oldCat, c.major);
                //data.s_major.AddMajorToCat(c.newCat, c.major);
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
                data.s_catList = data.s_major.GetCategories();

                data.s_majorList = new Dictionary<string, MajorData>();
                LinkedList<MajorData> tempList = data.s_major.GetMajors();
                foreach (MajorData major in tempList)
                {
                    data.s_majorList.Add(major.MajorName, major);
                }
                //data.s_majorList = data.s_major.GetMajors();

                data.majorButtonList = elements.CreateMajorButtonsByCat(data.s_majorList, data.s_catList, data.s_major, data.s_relatedCategories);
                data.profButtonList = elements.CreateProfButtons(data.s_professorList);
                data.buildingButtonList = elements.CreateBuildingButtons(data.s_buildingList);
            }
        }
    }
}

