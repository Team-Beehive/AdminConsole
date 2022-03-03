using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using AdminDatabaseFramework;

namespace AdminConsole
{
    public class Utilities
    {
        private StackPanel m_preview, m_properties, m_list;
        
        public Utilities(StackPanel preview, StackPanel properties, StackPanel list)
        {
            m_preview = preview;
            m_properties = properties;
            m_list = list;
        }

        public void ClearPreview()
        {
            if (m_preview.Children.Count > 1)
            {
                m_preview.Children.RemoveAt(1);
            }
        }
        public void SetPreview(UIElement element)
        {
            ClearPreview();
            m_preview.Children.Add(element);
        }

        public void ClearProperties()
        {
            if (m_properties.Children.Count > 1)
            {
                m_properties.Children.RemoveAt(1);
            }
        }
        public void SetProperties(UIElement element)
        {
            ClearProperties();
            m_properties.Children.Add(element);
        }

        public void ClearList()
        {
            if (m_list.Children.Count > 1)
            {
                m_list.Children.RemoveAt(1);
            }
        }
        public void SetList(UIElement element)
        {
            ClearList();
            m_list.Children.Add(element);
        }

        public BuildingData NewBuilding()
        {
            BuildingData temp = new BuildingData();
            temp.BuildingName = "New Building";
            temp.BuildingMajors = new List<string>();
            temp.BuildingName_Info = "Lorium Ipsum";
            temp.BuildingProfessors = new List<string>();
            temp.BuildingRoom_Type = new List<string>();
            temp.BuildingConstructionYear = "9999";

            return temp;
        }
        public ProfessorData NewProf()
        {
            ProfessorData temp = new ProfessorData();
            temp.professorName = "Professor Name";
            temp.professorDepartment = "Dept.";
            temp.professorEmail = "email@cset.oit.edu";
            temp.professorOffice = "Office";
            temp.professorPhoneNumber = "123-456-7890";

            return temp;
        }

        public string cleanString(string str)
        {
            HashSet<char> set = new HashSet<char>(" !@#$%^&*()_+-=,:;.<>/\\");
            StringBuilder sb = new StringBuilder(str.Length);
            foreach (char x in str.Where(c => !set.Contains(c)))
            {
                sb.Append(x);
            }


            return sb.ToString();
        }

        /*public void UploadData()
        {
            foreach (ProfessorData p in AppData.s_changedProf)
            {
                AppData.s_professors.UpdateProfessor(p);
            }
            foreach (BuildingData b in AppData.s_changedBuildingList)
            {
                AppData.s_building.UpdateBuilding(b);
            }
            foreach (MajorData m in AppData.s_changedList)
            {
                AppData.s_major.EditMajor(m);
            }
        }*/

        /*public void GetData(ref bool connected)
        {
            if (connected)
            {
                AppData.s_professorList = AppData.s_professors.GetProfessors();

                AppData.s_buildingList = AppData.s_building.GetBuildings();

                AppData.s_majorList = AppData.s_major.GetMajors();
                AppData.s_catList = AppData.s_major.GetCategories();
            }
            else
            {
                MessageBox.Show("Is not connected to server");
            }

        }*/
        
    }
}
