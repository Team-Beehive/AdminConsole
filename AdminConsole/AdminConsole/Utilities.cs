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
    static class Utilities
    {
        private static bool m_hasFile = false;
        public static BuildingData NewBuilding()
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
        public static ProfessorData NewProf()
        {
            ProfessorData temp = new ProfessorData();
            temp.professorName = "Professor Name";
            temp.professorDepartment = "Dept.";
            temp.professorEmail = "email@cset.oit.edu";
            temp.professorOffice = "Office";
            temp.professorPhoneNumber = "123-456-7890";

            return temp;
        }

    /*
     * Author: Destiny Dahlgren
     * Purpose: Background repeated logic that the rest of the front end uses
     */

    /*
     * Functions:
     * cleanString(string str)
     * - Because element names can only be one word and contain no special character this function removes all spaces and all special characters
     * QueryPageData(string page)
     * - Iterates through the major list finding a match for the given major name. Sets the active data variable to the match
     * VolitileSave()
     * - Runs UpdateLocal() saving the information so that changes arn't lost when a new page is selected
     * UploadData()
     * - Takes the changes made and uploads it onto the database
     * GetData()
     * - Gets the important data and sets it to the relivent variables
     */
        public static string cleanString(string str)
        {
            //string clean = String.Concat(str.Where(c => !Char.IsWhiteSpace(c)));
            //clean = String.Concat(clean.Where(c => !Char.IsSymbol(c)));
            HashSet<char> set = new HashSet<char>(" !@#$%^&*()_+-=,:;.<>/\\");
            StringBuilder sb = new StringBuilder(str.Length);
            foreach (char x in str.Where(c => !set.Contains(c)))
            {
                sb.Append(x);
            }


            return sb.ToString();
        }


        //public static void QueryPageData(string page)
        public static void QueryPageData(MajorData major)
        {
            /*foreach (MajorData m in AppData.s_majorList)

            {
                if (cleanString(m.MajorName) == page)
                {
                    AppData.s_activeData = m;
                    break;
                }

            }*/

            AppData.s_activeData = major;


            bool AddedToNull = false;

            AppData.s_activeTitle = AppData.s_activeData.MajorName;
            AppData.s_activeDesc = AppData.s_activeData.about[0];
            AppData.s_activeType = AppData.s_activeData.type[0];
            if (AppData.s_activeData.Classes != null)
            {
                AppData.s_activeClasses = AppData.s_activeData.Classes[0];
            }
            else 
            {
                AppData.s_activeData.Classes = new List<string>() { "No Classes" };
                AppData.s_activeClasses = AppData.s_activeData.Classes[0];
                AddedToNull = true;
            }

            if (AddedToNull)
            {
                VolitileSave();
            }
        }

        public static void VolitileSave()
        {
            //AppData.s_activeData.type[0] = AppData.s_activeType;
            //AppData.s_activeData.about[0] = AppData.s_activeDesc;
            //AppData.s_activeData.Classes[0] = AppData.s_activeClasses;

            if (AppData.s_hasChanged)
            {
                AppData.s_major.UpdateLocal();
                if (!AppData.s_changedList.Contains(AppData.s_activeData))
                {
                    AppData.s_changedList.Add(AppData.s_activeData);
                }

                AppData.s_hasChanged = false;
            }
        }

        public static void UploadData()
        {
            /*foreach (MajorData m in AppData.s_changedList)
            {
                AppData.s_major.EditMajor(m);
            }*/
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
        }
        public static void GetConfig()
        {
            //open and check file for existing file path
            //SetDatabaseKey();
        }

        public static bool SetDatabaseKey(string keyPath)
        {
            bool success = false;
            Debug.WriteLine(keyPath);
            try
            {
                AppData.s_database = new Database("oit-kiosk", keyPath);
                GetData();
                success = true;
            }
            catch (Exception ex)
            {
                success = false;
                ErrorWindow error = new ErrorWindow(ex);
                error.Show();
                AppData.s_database = null;
                //Debug.WriteLine(ex);
            }
            return success;
        }


        public static void GetData()
        {
            AppData.s_major = AppData.s_database.Majors;
            AppData.s_building = AppData.s_database.Buildings;
            AppData.s_professors = AppData.s_database.Professors;

            AppData.s_majorList = AppData.s_major.GetMajors();
            AppData.s_professorList = AppData.s_professors.GetProfessors();
            
            //AppData.s_majorList = AppData.s_major.GetMajors();
            AppData.s_buildingList = AppData.s_building.GetBuildings();
            
            //AppData.s_majorList = AppData.s_major.GetMajors();
            AppData.s_catList = AppData.s_major.GetCategories();

        }
        
    }
}
