using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Google.Cloud.Firestore;

namespace AdminDatabaseFramework
{
    public class Buildings
    {
        public LinkedList<BuildingData> BuildingList { get; set; }
        private FirestoreDb firestoreDb { get; set; }
        public string project = "oit-kiosk";
        public Buildings()
        {
            Environment.SetEnvironmentVariable("GOOGLE_APPLICATION_CREDENTIALS", "F:\\CSTCode\\JP\\Database\\oit-kiosk-firebase-adminsdk-u24sq-8f7958c50f.json");
            firestoreDb = FirestoreDb.Create(project);
        }

        public void updateLocalBuildings()
        {
            BuildingList = Task<LinkedList<BuildingData>>.Run(() => db_GetBuildingListAsync()).Result;
        }
        public LinkedList<BuildingData> GetBuildings()
        {
            updateLocalBuildings();
            return BuildingList;
        }

        public void CreateBuilding(BuildingData building)
        {

        }

        public void RemoveBuilding(BuildingData building)
        {

        }

        public void UpdateBuilding(BuildingData building)
        {

        }

        private async Task<LinkedList<BuildingData>> db_GetBuildingListAsync()
        {
            CollectionReference buildingRef = firestoreDb.Collection("pages").Document("Map").Collection("Buildings");
            QuerySnapshot snapshot = await buildingRef.GetSnapshotAsync();
            LinkedList<BuildingData> buildings = new LinkedList<BuildingData>();

            foreach(DocumentSnapshot document in snapshot.Documents)
            {
                BuildingData temp = new BuildingData();
                Dictionary<string, object> properties = document.ToDictionary();
                
                
                buildings.AddLast(temp.stringsToBuildingData(document.Id.ToString(), ObjToStr(properties["majors"] as List<object>), properties["name_info"].ToString(), ObjToStr(properties["professors"] as List<object>), properties["year"].ToString()));
            }
            return buildings;
        }

        private List<string> ObjToStr(List<object> objList)
        {
            List<string> tempList = new List<string>(objList.Count);

            foreach (object obj in objList)
            {
                tempList.Add(obj.ToString());
            }

            return tempList;
        }
    }
}
