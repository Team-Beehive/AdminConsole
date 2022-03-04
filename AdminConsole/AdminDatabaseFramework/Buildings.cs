using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Google.Cloud.Firestore;

namespace AdminDatabaseFramework
{
    /*--------------
        Class: Buildings
        Purpose: Used to update and read information about buildings from fire store
        Date Created: 1/16/22
        Author: Tucker Nulty

        Ctors:
            -(FirestoreDb)
                -Saves passed in database to local variable

        Local Variables:
            -public LinkedList<BuildingData> BuildingList
                -Used to store the buildings from the database
            -private FirestoreDb db
                -Firestore connection created outside of Buildings
            
        
        Funtions:
            -void updateLocalBuildings()
                -Updates the local list of buildings by calling db_GetBuildingListAsync()
            -LinkedList<BuildingData> GetBuildings()
                -returns BuildingList
            -void CreateBuilding(BuildingData building)
                -Creates/Overrides a building in the database by calling db_CreateBuilding(building)
            -void RemoveBuilding(BuildingData building)
                -Removes passed building if it exists by calling db_DeleteBuilding(building)
            -void UpdateBuilding(BuildingData building)
                -Updates passed building and/or changes name of building
        */
    public class Buildings
    {
        public LinkedList<BuildingData> BuildingList { get; set; }
        private FirestoreDb db { get; set; }
        public Buildings(FirestoreDb m_db)
        {
            try
            {
                db = m_db;
            }
            catch
            {
                throw new DatabaseException("Unable to connect to Firestore");
            }
        }
        public void updateDB(FirestoreDb m_db)
        {
            db = m_db;
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
            try
            {
                CollectionReference buildingRef = db.Collection("pages").Document("Map").Collection("Buildings");
                QuerySnapshot snapshot = await buildingRef.GetSnapshotAsync();
                LinkedList<BuildingData> buildings = new LinkedList<BuildingData>();

                foreach (DocumentSnapshot document in snapshot.Documents)
                {
                    BuildingData temp = new BuildingData();
                    Dictionary<string, object> properties = document.ToDictionary();

                    buildings.AddLast(temp.stringsToBuildingData(document.Id.ToString(), ObjectFunctions.ObjToStr(properties["majors"] as List<object>), properties["name_info"].ToString(), ObjectFunctions.ObjToStr(properties["professors"] as List<object>), ObjectFunctions.ObjToStr(properties["professors"] as List<object>), properties["year"].ToString(), document.Reference, document.Id.ToString()));
                }
                return buildings;
            }
            catch
            {
                throw new DatabaseException("Unable to get buildings");
            }
        }

        private async Task db_CreateBuilding(BuildingData buildingData)
        {
            try
            {
                if (buildingData.BuildingName != null)
                {
                    DocumentReference buildingRef = db.Collection("pages").Document("Map").Collection("Buildings").Document(buildingData.BuildingName);
                    await buildingRef.SetAsync(buildingData.ToDictionary());
                }
                else
                {
                    throw new Exception("BuildingName not set, cannot create document");
                }
            }
            catch
            {
                throw new DatabaseException("Unable to create building");
            }
        }

        private async Task db_RemoveBuilding(BuildingData buildingData)
        {
            if(buildingData.BuildingName != null)
            {
                await db.Collection("pages").Document("Map").Collection("Buildings").Document(buildingData.BuildingName).DeleteAsync();
            }
            else
            {
                throw new Exception("BuildingName not set, cannot delete document");
            }
        }

        private async Task db_UpdateBuilding(BuildingData buildingData)
        {
            try
            {
                if (buildingData.BuildingName != buildingData.oldName && buildingData.BuildingName != null)
                {
                    DocumentReference buildingRef = db.Collection("pages").Document("Map").Collection("Buildings").Document(buildingData.oldName);
                    await buildingRef.DeleteAsync();
                    Task.Run(async () => await db_CreateBuilding(buildingData)).Wait();
                }
                else if (buildingData.BuildingName == buildingData.oldName && buildingData.BuildingName != null)
                {
                    await db.Collection("pages").Document("Map").Collection("Buildings").Document(buildingData.BuildingName).UpdateAsync(buildingData.ToDictionary());
                }
                else
                {
                    throw new Exception("BuildingName not set, cannot delete document");
                }
            }
            catch
            {
                throw new DatabaseException("Unable to update building");
            }
            
        }
       
    }
}
