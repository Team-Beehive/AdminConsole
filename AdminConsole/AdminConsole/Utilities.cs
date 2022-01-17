using System;
using System.Collections.Generic;
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
        public static string cleanString(string str)
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

        public static void QueryPageData(string page)
        {
            foreach (MajorData m in AppData.s_majorList)
            {
                if (cleanString(m.MajorName) == page)
                {
                    AppData.s_activeData = m;
                    break;
                }
            }

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
            AppData.s_activeData.type[0] = AppData.s_activeType;
            AppData.s_activeData.about[0] = AppData.s_activeDesc;
            AppData.s_activeData.Classes[0] = AppData.s_activeClasses;

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
            foreach (MajorData m in AppData.s_changedList)
            {
                AppData.s_major.EditMajor(m);
            }
        }

        public static void GetData()
        {
            AppData.s_majorList = AppData.s_major.GetMajors();
            AppData.s_buildingList = AppData.s_building.GetBuildings();
        }
        
    }
}
