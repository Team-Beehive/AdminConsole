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
            Task.Run(() => db_CreateBuilding(building)).Wait();
        }

        public void RemoveBuilding(BuildingData building)
        {
           Task.Run(() => db_RemoveBuilding(building)).Wait();
        }

        public void UpdateBuilding(BuildingData building)
        {
            Task.Run(() => db_UpdateBuilding(building)).Wait();
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

                buildings.AddLast(temp.stringsToBuildingData(document.Id.ToString(), ObjectFunctions.ObjToStr(properties["majors"] as List<object>), properties["name_info"].ToString(),ObjectFunctions.ObjToStr(properties["professors"] as List<object>), ObjectFunctions.ObjToStr(properties["professors"] as List<object>), properties["year"].ToString(), document.Reference, document.Id.ToString()));
            }
            return buildings;
        }

        private async Task db_CreateBuilding(BuildingData buildingData)
        {
            if(buildingData.BuildingName != null)
            {
                DocumentReference buildingRef = firestoreDb.Collection("pages").Document("Map").Collection("Buildings").Document(buildingData.BuildingName);
                await buildingRef.SetAsync(buildingData.ToDictionary());
            }
            else
            {
                throw new Exception("BuildingName not set, cannot create document");
            }
        }

        private async Task db_RemoveBuilding(BuildingData buildingData)
        {
            if(buildingData.BuildingName != null)
            {
                await firestoreDb.Collection("pages").Document("Map").Collection("Buildings").Document(buildingData.BuildingName).DeleteAsync();
            }
            else
            {
                throw new Exception("BuildingName not set, cannot delete document");
            }
        }

        private async Task db_UpdateBuilding(BuildingData buildingData)
        {
            if (buildingData.BuildingName != buildingData.oldName)
            {
                DocumentReference buildingRef = firestoreDb.Collection("pages").Document("Map").Collection("Buildings").Document(buildingData.oldName);
                await buildingRef.DeleteAsync();
                Task.Run(async () => await db_CreateBuilding(buildingData)).Wait();
            }
            else if (buildingData.BuildingName == buildingData.oldName)
            {
                await firestoreDb.Collection("pages").Document("Map").Collection("Buildings").Document(buildingData.BuildingName).UpdateAsync(buildingData.ToDictionary());
            }
        }
       
    }
}
