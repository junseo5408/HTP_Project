using Google.Apis.Auth.OAuth2;
using Google.Cloud.Firestore;
using Google.Cloud.Firestore.V1;
using Grpc.Auth;
using Microsoft.Maui.ApplicationModel.Communication;
using Microsoft.VisualBasic;
using System.ComponentModel;
using System.Diagnostics;
using test_app.Model;
using FileSystem = Microsoft.Maui.Storage.FileSystem;


namespace test_app
{
    public class DataBaseConnect
    {
        private FirestoreDb db;
        private string jsonName = "team-npu-firebase-adminsdk-47z6i-de259c450c.json";

        private void CopyFirebaseJson()
        {
            string destinationPath = Path.Combine(FileSystem.AppDataDirectory, jsonName);

            // 이미 파일이 복사되었는지 확인
            if (!File.Exists(destinationPath))
            {
                using var inputStream = FileSystem.OpenAppPackageFileAsync(jsonName).Result;
                using var outputStream = File.Create(destinationPath);
                inputStream.CopyTo(outputStream);
            }
        }


        private async Task ConnectDB()
        {
            CopyFirebaseJson();

            // 경로 확인
            string jsonFilePath = Path.Combine(FileSystem.AppDataDirectory, jsonName);
            Console.WriteLine($"JSON File Path: {jsonFilePath}");

            if (!File.Exists(jsonFilePath))
            {
                throw new FileNotFoundException($"Credential file not found: {jsonFilePath}");
            }

            // Credential 생성
            GoogleCredential credential;
            try
            {
                credential = GoogleCredential.FromFile(jsonFilePath);
                Console.WriteLine("Credential successfully loaded.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error loading credential: {ex.Message}");
                throw;
            }

            // FirestoreClient 생성
            FirestoreClient firestoreClient;
            try
            {
                firestoreClient = new FirestoreClientBuilder
                {
                    ChannelCredentials = credential.ToChannelCredentials()
                }.Build();
                Console.WriteLine("FirestoreClient successfully created.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error creating FirestoreClient: {ex.Message}");
                throw;
            }

            // FirestoreDb 생성
            try
            {
                db = FirestoreDb.Create("team-npu", firestoreClient);
                Console.WriteLine("FirestoreDb successfully created.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error creating FirestoreDb: {ex.Message}");
                throw;
            }
        }


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
                    }
                }
                return true;
            }
            else return false;
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
