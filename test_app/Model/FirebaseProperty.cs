using Google.Cloud.Firestore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace test_app.Model
{
    public class FirebaseProperty
    {
        [FirestoreProperty]
        public string Email { get; set; }

        [FirestoreProperty]
        public string UserName { get; set; }

        [FirestoreProperty]
        public string Password { get; set; }
    }
}
