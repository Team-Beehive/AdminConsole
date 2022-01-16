using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Google.Cloud.Firestore;

namespace AdminDatabaseFramework
{
    public class BuildingData
    {
        public string BuildingName { get; set; }
        public List<string> BuildingMajors { get; set; }
        public string BuildingName_Info { get; set; }
        public List<string> BuildingProfessors { get; set; }
        public List<string> BuildingRoom_Type { get; set; }
        public string BuildingConstructionYear { get; set; }
        public DocumentReference DocumentReferenceSelf { get; set; }
        public string oldName { get; set; }

        public BuildingData stringsToBuildingData(string name, List<string> majors, string info, List<string> profs, List<string> roomTypes, string construction, DocumentReference self, string oldName)
        {
            this.BuildingName = name;
            this.BuildingMajors = majors;
            this.BuildingName_Info = info;
            this.BuildingProfessors = profs;
            this.BuildingRoom_Type = roomTypes;
            this.BuildingConstructionYear = construction;
            this.DocumentReferenceSelf = self;
            this.oldName = oldName;
            return this;
        }

        public Dictionary<string, object> ToDictionary()
        {
            return new Dictionary<string, object>
            {
                {"majors",  BuildingMajors},
                {"name_info", BuildingName_Info},
                {"professors", BuildingProfessors},
                {"room_types", BuildingRoom_Type },
                {"year", BuildingConstructionYear }
            };
        }

    }
}
