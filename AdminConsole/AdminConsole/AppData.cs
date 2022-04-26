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
    public class AppData
    {
        public struct changeMajorCat
        {
            public MajorCategories oldCat;
            public MajorCategories newCat;
            public MajorData major;
        }

        public List<changeMajorCat> catsToUpdate = new List<changeMajorCat>();

        public Database s_database;

        public Majors s_major; //= new Majors();
        public Professors s_professors; //= new Professors();
        public Buildings s_building; //= new Buildings();

        public Dictionary<string, List<MajorCategories>> s_relatedCategories = new Dictionary<string, List<MajorCategories>>();

        //public LinkedList<MajorData> s_majorList;
        public Dictionary<string, MajorData> s_majorList;
        public LinkedList<MajorCategories> s_catList;
        public LinkedList<BuildingData> s_buildingList;
        public LinkedList<ProfessorData> s_professorList = new LinkedList<ProfessorData>();

        public List<MajorData> s_changedList = new List<MajorData>();
        public List<ProfessorData> s_changedProf = new List<ProfessorData>();
        public List<BuildingData> s_changedBuildingList = new List<BuildingData>();

        public List<BuildingData> s_addedBuildingList = new List<BuildingData>();
        public List<MajorData> s_addedMajorList = new List<MajorData>();

        public MajorData s_activeData;
        public ProfessorData s_activeProfessor;
        public BuildingData s_activeBuilding;
        public MajorCategories s_activeCat;

        public Grid majorButtonList;
        public Grid profButtonList;
        public Grid catButtonList;
        public Grid buildingButtonList;

        //public static StackPanel s_previewPanel;
        //public static StackPanel s_propertiesPanel;
        //public static StackPanel s_listPanel;
        //public static TextBox s_properties;
    }
}
