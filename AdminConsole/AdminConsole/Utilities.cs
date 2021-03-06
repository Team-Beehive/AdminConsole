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
        private AppData m_data;
        
        public Utilities(StackPanel preview, StackPanel properties, StackPanel list, AppData data)
        {
            m_preview = preview;
            m_properties = properties;
            m_list = list;
            m_data = data;
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
                for (int i = 1; i < m_properties.Children.Count; i++)
                {
                    m_properties.Children.RemoveAt(i);
                }
            }
        }
        public void ClearPropertiesKeepCat()
        {
            if (m_properties.Children.Count > 2)
            {
                for (int i = 2; i < m_properties.Children.Count; i++)
                {
                    m_properties.Children.RemoveAt(i);
                }
            }
        }
        public void SetProperties(UIElement element, bool clear = true)
        {
            if (clear)
            { 
                ClearProperties();
            }
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
        
        public MajorData NewMajor()
        {
            MajorData temp = new MajorData();
            temp.OldName = "New Major";
            temp.MajorName = "New Major";
            temp.type = new List<string>();
            temp.type.Add("Set Type");
            temp.Classes = new List<string>();
            temp.Classes.Add("Set Class");
            temp.Professors = new List<string>();
            temp.Professors.Add("Set Professors");
            temp.campuses = new List<string>();
            temp.campuses.Add("Set Campuses");
            temp.about = new List<string>();
            temp.about.Add("Set About");

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

        public bool SetDatabaseKey(string key)
        {
            bool success = false;
            if (m_data.s_database == null)
            {
                try
                {
                    m_data.s_database = new Database(key);
                    //GetData();
                    success = true;
                }
                catch (DatabaseException ex)
                {
                    success = false;
                    ErrorWindow error = new ErrorWindow(ex);
                    error.Show();
                }
            }
            else
            {
                try
                {
                    m_data.s_database.UpdateConenction(key);
                    success = true;
                }
                catch (DatabaseException ex)
                {
                    success=false;
                    ErrorWindow error = new ErrorWindow(ex);
                    error.Show();
                }
            }
            return success;
        }
    }
}
