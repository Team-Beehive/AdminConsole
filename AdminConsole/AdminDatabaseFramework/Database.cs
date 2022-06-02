using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Google.Cloud.Firestore;
using Google.Cloud.Firestore.V1;
using Grpc.Auth;
using Google.Cloud.Storage.V1;
using Firebase.Auth;
using Firebase.Database;
using System.Text.Json;
using System.Text.Json.Nodes;
using System.IO;


namespace AdminDatabaseFramework
{
    public class Database
    {
        public Majors Majors { get; set; }
        public Buildings Buildings { get; set; }
        public Professors Professors { get; set; }
        public FirestoreDb db { get; private set; }
        private string m_project;

        public Database(string Project, string envPath)
        {
            m_project = Project;
            AttemptConnection(envPath);
            Majors = new Majors(db);
            Buildings = new Buildings(db);
            Professors = new Professors(db);
        }

        public Database(string envPath)
        {
            m_project = projectFromJson (envPath);
            AttemptConnection(envPath);
            Majors = new Majors (db);
            Buildings = new Buildings(db);
            Professors = new Professors(db);
        }
        public void AttemptConnection(string path)
        {
            try
            {
                Google.Apis.Auth.OAuth2.GoogleCredential cred = Google.Apis.Auth.OAuth2.GoogleCredential.FromFile(path);
                FirestoreClientBuilder clientBuilder = new FirestoreClientBuilder();
                clientBuilder.ChannelCredentials = cred.ToChannelCredentials();
                db = FirestoreDb.Create(m_project, clientBuilder.Build());
            }
            catch (Exception e)
            {
                throw new DatabaseException(e.Message, e);
            }
        }

        public void UpdateConenction(string path)
        {
            AttemptConnection(path);
            updateProject(m_project);
        }

        public void updateProject(string project)
        {
            try
            {
                //Add in a call to Attempt connection that has a variable for the project.
                Majors.updateDB(db);
                Buildings.updateDB(db);
                Professors.updateDB(db);
            }
            catch
            {
                throw new DatabaseException("Unable to connect to database with name " + project + " using provided credentials");
            }
            m_project = project;
        }

        public void updateCreds(string envPath)
        {
            Environment.SetEnvironmentVariable("GOOGLE_APPLICATION_CREDENTIALS", envPath);
            updateProject(m_project);
        }

        public string projectFromJson(string path)
        {
            if (File.Exists(path))
            {
                string Json = File.ReadAllText(path);
                JsonNode document = JsonNode.Parse(Json);
                JsonNode root = document.Root;
                try
                {
                    return root["project_id"].AsValue().ToString();
                }
                catch
                {
                    return "No Name";
                }
            }
            return "No Path";
        }
    }
}

