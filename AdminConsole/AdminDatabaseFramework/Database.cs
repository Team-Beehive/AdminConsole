﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Google.Cloud.Firestore;

namespace AdminDatabaseFramework
{
    public class Database
    {
        public Majors Majors { get; set; }
        public Buildings Buildings { get; set; }
        public Professors Professors { get; set; }
        public FirestoreDb db;
        private string m_project;

        public Database(string Project, string envPath)
        {
            Environment.SetEnvironmentVariable("GOOGLE_APPLICATION_CREDENTIALS", envPath);
            db = FirestoreDb.Create(Project);
            Majors = new Majors(db);
            Buildings = new Buildings(db);
            Professors = new Professors(db);
            m_project = Project;
        }

        public void updateProject(string project)
        {
            db = FirestoreDb.Create(project);
            Majors.updateDB(db);
            Buildings.updateDB(db);
            Professors.updateDB(db);
            m_project = project;
        }

        public void updateCreds(string envPath)
        {
            Environment.SetEnvironmentVariable("GOOGLE_APPLICATION_CREDENTIALS", envPath);
            updateProject(m_project);
        }
    }
}