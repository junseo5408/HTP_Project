using Google.Cloud.Firestore;
using test_app.Model;


namespace test_app
{
    public class DataBaseConnect
    {
        FirestoreDb db;
        string path = AppDomain.CurrentDomain.BaseDirectory + @"team-npu-firebase-adminsdk-47z6i-de259c450c.json";

        private void ConnectDB()
        {
            Environment.SetEnvironmentVariable("GOOGLE_APPLICATION_CREDENTIALS", path);
            db = FirestoreDb.Create("(default)");
        }

        public void Add_User(string email, string username, string pwd)
        {
            DocumentReference ADMIN = db.Collection("Login").Document(username);
            Dictionary<string, string> datal = new Dictionary<string, string>()
        {
            {"E-mail", email},
            {"UserName", username},
            {"Password", pwd}
        };
            ADMIN.SetAsync(datal);
        }

        public async void CheckUserData(string email, string pwd)
        {
            Query qref = db.Collection("Login").WhereEqualTo("E-mail", email).WhereEqualTo("Password", pwd);
            QuerySnapshot snap = await qref.GetSnapshotAsync();

            foreach (DocumentSnapshot docsnap in snap)
            {
                FirebaseProperty fp = docsnap.ConvertTo<FirebaseProperty>();

                if (docsnap.Exists)
                {
                    //richTextBox1.Text += "[Doc Name: " + docsnap.Id + "]\n";
                    //richTextBox1.Text += fp.Name + "\n";
                    //richTextBox1.Text += fp.PhoneNo + "\n\n";

                }
            }
        }
    }
}
