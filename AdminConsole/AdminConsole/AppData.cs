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
    static class AppData
    {
        public static object s_activeElement;
        public static string s_lastSelected = "n";
        public static string s_activeTitle;
        public static string s_activeClasses;
        public static string s_activeCampus;
        public static string s_activeType;
        public static string s_activeDesc;
        public static bool s_hasChanged = false;

        public static MajorData s_activeData;
        public static Majors s_major = new Majors();
        public static LinkedList<MajorData> s_majorList;
        public static List<MajorData> s_changedList = new List<MajorData>();
        public static Buildings s_building = new Buildings();
        public static LinkedList<BuildingData> s_buildingList;
        public static BuildingData s_activeBuilding;
        public static List<BuildingData> s_changedBuildingList = new List<BuildingData>();

        public static StackPanel s_previewPanel;
        public static StackPanel s_propertiesPanel;
        public static TextBox s_properties;
    }
}
