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

    /*
     * Author: Destiny Dahlgren
     * Purpose: Holds important data that is needed for the rest of the front end to function
     */
    static class AppData
    {
        public static string s_lastSelected = "n";
        public static bool s_hasChanged = false;

        public static Majors s_major = new Majors();
        public static Professors s_professors = new Professors();
        public static Buildings s_building = new Buildings();

        public static LinkedList<MajorData> s_majorList;
        public static LinkedList<MajorCategories> s_catList;
        public static LinkedList<BuildingData> s_buildingList;
        public static LinkedList<ProfessorData> s_professorList = new LinkedList<ProfessorData>();

        public static List<MajorData> s_changedList = new List<MajorData>();
        public static List<ProfessorData> s_changedProf = new List<ProfessorData>();
        public static List<BuildingData> s_changedBuildingList = new List<BuildingData>();

        public static MajorData s_activeData;
        public static ProfessorData s_activeProfessor;
        public static BuildingData s_activeBuilding;
        public static MajorCategories s_activeCat;

        public static StackPanel s_previewPanel;
        public static StackPanel s_propertiesPanel;
        public static StackPanel s_listPanel;
        public static TextBox s_properties;
    }
}
