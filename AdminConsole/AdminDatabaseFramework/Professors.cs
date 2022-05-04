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
        public LinkedList<ProfessorData> LocalProfessors { get; set; }

        public Professors(FirestoreDb m_db)
        {
            db = m_db;
        }

        public void updateDB(FirestoreDb m_db)
        {
            db = m_db;
        }
        public LinkedList<ProfessorData> GetProfessors()
        {
            UpdateLocal();
            return LocalProfessors;
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

        public void UpdateProfessor(ProfessorData professor)
        {
            Task.Run(() => db_UpdateProfessor(professor)).Wait();
        }

        private async Task db_UpdateProfessor(ProfessorData professor)
        {
            try
            {
                if (professor.professorName != professor.oldTitle)
                {
                    DocumentReference documentReference = db.Collection("pages").Document("Professors").Collection("Professors").Document(professor.oldTitle);
                    await documentReference.DeleteAsync();
                    Task.Run(() => db_CreateProfessor(professor)).Wait();
                    professor.oldTitle = professor.professorName;
                }
                else if (professor.professorName == professor.oldTitle)
                {
                    await db.Collection("pages").Document("Professors").Collection("Professors").Document(professor.professorName).UpdateAsync(professor.ToDictionary());
                }
                else
                {
                    await db.Collection("pages").Document("Professors").Collection("Professors").Document(professor.professorName).CreateAsync(professor.ToDictionary());
                }
            }
            catch
            {
                throw new DatabaseException("Could not update professor");
            }
        }
        private async Task<LinkedList<ProfessorData>> db_GetProfessors()
        {
            try
            {
                LinkedList<ProfessorData> professorDatas = new LinkedList<ProfessorData>();
                CollectionReference reference = db.Collection("pages").Document("Professors").Collection("Professors");
                QuerySnapshot documents = await reference.GetSnapshotAsync();
                foreach (DocumentSnapshot document in documents)
                {
                    Dictionary<string, object> data = document.ToDictionary();

                    ProfessorData temp = new ProfessorData();
                    temp.professorDepartment = data.ContainsKey("department") ? data["department"].ToString() : null;
                    temp.professorEmail = data.ContainsKey("email") ? data["email"].ToString() : null;
                    temp.professorOffice = data.ContainsKey("office") ? data["office"].ToString() : null;
                    temp.professorPhoneNumber = data.ContainsKey("phone_number") ? data["phone_number"].ToString() : null;
                    temp.professorName = document.Id.ToString();
                    temp.oldTitle = temp.professorName;
                    temp.DocumentReferenceSelf = document.Reference;
                    professorDatas.AddLast(temp);
                }
                return professorDatas;
            }
            catch
            {
                throw new DatabaseException("Could not get professors");
            }
        }

        private async Task db_RemoveProfessor(ProfessorData professor)
        {
            try
            {
                await db.Collection("pages").Document("Professors").Collection("Professors").Document(professor.professorName).DeleteAsync();
            }
            catch
            {
                throw new DatabaseException("Could not remove professor");
            }
        }

        private async Task db_CreateProfessor(ProfessorData professor)
        {
            try
            {
                DocumentReference document = db.Collection("pages").Document("Professors").Collection("Professors").Document(professor.professorName);
                await document.SetAsync(professor.ToDictionary());
            }
            catch
            {
                throw new DatabaseException("Could not create professor");
            }
        }
    }
}
