using Google.Cloud.Firestore;

namespace test_app.Model
{
    public class FirebaseProperty
    {
        public class HTP_Property
        {
            [FirestoreProperty]
            public DateTime DateTime { get; set; }

            [FirestoreProperty]
            public string House { get; set; }

            [FirestoreProperty]
            public string Person { get; set; }

            [FirestoreProperty]
            public string Result { get; set; }

            [FirestoreProperty]
            public string Tree { get; set; }

            [FirestoreProperty]
            public string UserEmail { get; set; }
        }

        public class Ror_Property
        {
            [FirestoreProperty]
            public DateTime DateTime { get; set; }

            [FirestoreProperty]
            public string Result { get; set; }

            [FirestoreProperty]
            public string UserEmail { get; set; }
        }
    }
}
