using System;
using System.Collections.Generic;
using System.Text;
using Google.Cloud.Firestore;

namespace AdminDatabaseFramework
{
    public class MajorData
    {
        public string MajorName { get; set; }
        public string OldName { get; set; }
        public List<string> type { get; set; }
        public List<string> Classes { get; set; }
        public List<string> Professors { get; set; }
        public List<string> campuses { get; set; }
        public List<string> about { get; set; }

        public DocumentReference DocumentReferenceSelf { get; set; }

        public Dictionary<string, object> ToDictionary
        {
            get
            {
                return new Dictionary<string, object>()
                {
                    {"about", about },
                    {"campuses", campuses },
                    {"type", type },
                    {"classes", Classes }
                };
            }
        }

    }

    public class MajorCategories
    {
        public string categoryTitle { get; set; }
        public string oldTitle { get; set; }
        public List<Object> relatedDegrees { get; set; }

        public Dictionary<string, object> ToDictionary
        {
            get
            {
                return new Dictionary<string, object>()
            {
                {"categoryTitle", categoryTitle },
                {"relatedDegrees", relatedDegrees }
            };
            }
        }
    }
}
