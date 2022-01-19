using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Google.Cloud.Firestore;

namespace AdminDatabaseFramework
{
    public class ProfessorData
    {
        public string professorName { get; set; }
        public string professorDepartment { get; set; }
        public string professorOffice { get; set; }
        public string professorEmail { get; set; }
        public string professorPhoneNumber { get; set; }
        public DocumentReference DocumentReferenceSelf { get; set; }
        public string oldTitle { get; set; }

        public Dictionary<string, object> ToDictionary()
        {
            return new Dictionary<string, object>
            {
                { "department", professorDepartment },
                { "email", professorEmail },
                {"office", professorOffice },
                {"phone_number", professorPhoneNumber }
            };
        }
    }
    public class Professors
    {
        private FirestoreDb db;
        public string project = "oit-kiosk";
        public LinkedList<ProfessorData> LocalProfessors;

        public Professors()
        {
            Environment.SetEnvironmentVariable("GOOGLE_APPLICATION_CREDENTIALS", "F:\\CSTCode\\JP\\Database\\oit-kiosk-firebase-adminsdk-u24sq-8f7958c50f.json");
            db = FirestoreDb.Create(project);
        }

        public void UpdateLocal()
        {
            LocalProfessors = Task<LinkedList<ProfessorData>>.Run(() => db_GetProfessors()).Result;
        }

        public void CreateProfessor(ProfessorData professor)
        {
            Task.Run(() => db_CreateProfessor(professor)).Wait();
        }

        public void RemoveProfessor(ProfessorData professor)
        {
            Task.Run(() => db_RemoveProfessor(professor)).Wait();
        }

        public async void UpdateProfessor(ProfessorData professor)
        {
            if (professor.professorName != professor.oldTitle)
            {
                DocumentReference documentReference = db.Collection("pages").Document("Professors").Collection("Professors").Document(professor.oldTitle);
                await documentReference.DeleteAsync();
                Task.Run(() => db_CreateProfessor(professor)).Wait();
            }
            else
            {
                await db.Collection("pages").Document("Professors").Collection("Professors").Document(professor.professorName).CreateAsync(professor.ToDictionary());
            }

        }

        private async Task<LinkedList<ProfessorData>> db_GetProfessors()
        {
            LinkedList<ProfessorData> professorDatas = new LinkedList<ProfessorData>();
            CollectionReference reference = db.Collection("pages").Document("Professors").Collection("Professors");
            QuerySnapshot documents = await reference.GetSnapshotAsync();
            foreach(DocumentSnapshot document in documents)
            {
                Dictionary<string, object> data = document.ToDictionary();

                ProfessorData temp = new ProfessorData();
                temp.professorDepartment = data["department"].ToString();
                temp.professorEmail = data["email"].ToString();
                temp.professorOffice = data["office"].ToString();
                temp.professorPhoneNumber = data["phone_number"].ToString();
                temp.professorName = document.Id.ToString();
                temp.oldTitle = temp.professorName;
                temp.DocumentReferenceSelf = document.Reference;
                professorDatas.AddLast(temp);
            }
            return professorDatas;
        }

        private async Task db_RemoveProfessor(ProfessorData professor)
        {
            await db.Collection("pages").Document("Professors").Collection("Professors").Document(professor.professorName).DeleteAsync();
        }

        private async Task db_CreateProfessor(ProfessorData professor)
        {
            DocumentReference document = db.Collection("pages").Document("Professors").Collection("Professors").Document(professor.professorName);
            await document.SetAsync(professor.ToDictionary());
        }
    }
}
