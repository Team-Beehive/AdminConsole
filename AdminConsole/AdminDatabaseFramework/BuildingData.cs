using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdminDatabaseFramework
{
    public class BuildingData
    {
        public string BuildingName { get; set; }
        public List<string> BuildingMajors { get; set; }
        public string BuildingName_Info { get; set; }
        public List<string> BuildingRoom_Type { get; set; }
        public string BuildingConstructionYear { get; set; }

        public BuildingData stringsToBuildingData(string name, List<string> majors, string info, List<string> roomTypes, string construction)
        {
            this.BuildingName = name;
            this.BuildingMajors = majors;
            this.BuildingName_Info = info;
            this.BuildingRoom_Type = roomTypes;
            this.BuildingConstructionYear = construction;
            return this;
        }
    }
}
