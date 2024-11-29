using Google.Cloud.Firestore;
using Google.Cloud.Firestore.V1;
using System.Collections.Generic;
using System.Diagnostics;
using test_app.Model;
using static test_app.Model.UserData;
using FileSystem = Microsoft.Maui.Storage.FileSystem;


namespace test_app
{
    public class DataBaseConnect
    {
        private FirestoreDb db;
        private string jsonName = "team-npu-firebase-adminsdk-47z6i-de259c450c.json";

        private async Task initFirestore()
        {
            try
            {
                var stream = await FileSystem.OpenAppPackageFileAsync(jsonName);
                var reader = new StreamReader(stream);
                var contents = reader.ReadToEnd();

                FirestoreClientBuilder fbc = new FirestoreClientBuilder { JsonCredentials = contents };
                db = FirestoreDb.Create("team-npu", fbc.Build());
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task Add_HTP_Result(string h, string t, string p, string result)
        {
            await initFirestore();
            DocumentReference ADMIN = db.Collection("HTP_Result").Document();
            Dictionary<string, object> datal = new Dictionary<string, object>()
            {
                {"DateTime", DateTime.Now.Year},
                {"House", h},
                {"Person", p},
                {"Result", result},
                {"Tree", t},
                {"UserEmail", Model.UserData.Email}
            };
            await ADMIN.SetAsync(datal);
        }

        public async Task Add_RorResult(string result)
        {
            await initFirestore();
            DocumentReference ADMIN = db.Collection("Rorschach_Result").Document();
            Dictionary<string, object> datal = new Dictionary<string, object>()
            {
                {"DateTime", DateTime.Now.Year},
                {"Result", result},
                {"UserEmail", Model.UserData.Email}
            };
            await ADMIN.SetAsync(datal);
        }

        public async Task Add_User(string email, string username, string pwd)
        {
            await initFirestore();
            DocumentReference ADMIN = db.Collection("Users").Document(username);
            Dictionary<string, string> datal = new Dictionary<string, string>()
            {
                {"Email", email},
                {"UserName", username},
                {"Password", pwd}
            };
            await ADMIN.SetAsync(datal);
        }

        public async Task<bool> LoginCheck(string email, string pwd)
        {
            await initFirestore();
            Query qref = db.Collection("Users").WhereEqualTo("Email", email).WhereEqualTo("Password", pwd);
            QuerySnapshot snap = await qref.GetSnapshotAsync();

            if (snap.Count == 1)
            {
                foreach (DocumentSnapshot docsnap in snap)
                {
                    foreach (var field in docsnap.ToDictionary())
                    {
                        Debug.WriteLine($"Field: {field.Key}, Value: {field.Value}");
                        if (field.Key == "Email")
                            Model.UserData.Email = field.Value.ToString();
                        else if(field.Key == "UserName")
                            Model.UserData.Name = field.Value.ToString();
                    }
                }
                return true;
            }
            else return false;
        }

        public async Task<List<ResultHTP_Data>> LoadHTP_Data()
        {
            var list = new List < ResultHTP_Data >();
            try
            {
                await initFirestore();
                Query qref = db.Collection("HTP_Result").WhereEqualTo("UserEmail", Model.UserData.Email);
                QuerySnapshot snap = await qref.GetSnapshotAsync();

                if (snap.Count > 0)
                {
                    foreach (DocumentSnapshot doc in snap.Documents)
                    {
                        if (doc.Exists)
                        {
                            FirebaseProperty.HTP_Property data = doc.ConvertTo<FirebaseProperty.HTP_Property>();
                            //Model.HTP_Data.Htp_List.Add(data);
                            list.Add(new ResultHTP_Data
                            {
                                house = data.House,
                                tree = data.Tree,
                                person = data.Person,
                                result = data.Result
                            });
                        }
                    }
                }
            }
            catch
            {
                await Application.Current.MainPage.DisplayAlert("경고", "HTP데이터 불러오기 실패.", "확인");
            }
            return list;
        }


        public async Task<List<ResultRor_Data>> LoadRorData()
        {
            var list = new List<ResultRor_Data>();
            try
            {
                await initFirestore();
                Query qref = db.Collection("Rorschach_Result").WhereEqualTo("UserEmail", Model.UserData.Email);
                QuerySnapshot snap = await qref.GetSnapshotAsync();

                if (snap.Count == 1)
                {
                    foreach (DocumentSnapshot docsnap in snap)
                    {
                        foreach (var field in docsnap.ToDictionary())
                        {
                            if (field.Key == "Result")
                                list.Add(new ResultRor_Data
                                {
                                    Result = field.Value.ToString()
                                });
                        }
                    }
                }
            }
            catch
            {
                await Application.Current.MainPage.DisplayAlert("경고", "로르샤흐데이터 불러오기 실패.", "확인");
            }
            return list;
        }

        public async Task<bool> EmailCheck(string email)
        {
            await initFirestore();
            Query qref = db.Collection("Users").WhereEqualTo("Email", email);
            QuerySnapshot snap = await qref.GetSnapshotAsync();

            if (snap.Count == 0)
                return true;
            else
            {
                await Application.Current.MainPage.DisplayAlert("경고", "이미 존재하는 계정입니다.", "확인");
                return false;
            }
        }
    }
}
