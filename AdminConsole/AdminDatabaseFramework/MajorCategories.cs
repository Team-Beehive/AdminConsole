using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Google.Cloud.Firestore;

namespace AdminDatabaseFramework
{
    public class MajorCategories
    {
        public string categoryTitle { get; set; }
        public List<Object> relatedDegrees { get; set; }
    }
}
