using Google.Cloud.Firestore;
using Microsoft.Maui.ApplicationModel.Communication;
using System.ComponentModel;
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
            ConnectDB();
            DocumentReference ADMIN = db.Collection("Login").Document(username);
            Dictionary<string, string> datal = new Dictionary<string, string>()
        {
            {"Email", email},
            {"UserName", username},
            {"Password", pwd}
        };
            ADMIN.SetAsync(datal);
        }

        public async Task<bool> LoginCheck(string email, string pwd)
        {
            ConnectDB();
            bool isEmpty = false ;
            Query qref = db.Collection("Login").WhereEqualTo("Email", email).WhereEqualTo("Password", pwd);
            QuerySnapshot snap = await qref.GetSnapshotAsync();

            foreach (DocumentSnapshot docsnap in snap)
            {
                FirebaseProperty fp = docsnap.ConvertTo<FirebaseProperty>();

                if (docsnap.Exists)
                {
                    UserData.Name = fp.UserName;
                    UserData.Email = fp.Email;
                }
                else
                    isEmpty = true;
            }
            return isEmpty;
        }

        public async Task<bool> EmailCheck(string email)
        {
            ConnectDB();
            bool isCheck = false;
            Query qref = db.Collection("Login").WhereEqualTo("Email", email);
            QuerySnapshot snap = await qref.GetSnapshotAsync();

            foreach (DocumentSnapshot docsnap in snap)
            {
                FirebaseProperty fp = docsnap.ConvertTo<FirebaseProperty>();

                if (docsnap.Exists)
                {
                    isCheck = false;
                }
                else
                    isCheck = true;
                    //Add_User(email, name, password);
            }
            return isCheck;
        }
    }
}
