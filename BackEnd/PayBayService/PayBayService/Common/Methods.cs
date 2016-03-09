using Microsoft.WindowsAzure.Mobile.Service;
using Microsoft.WindowsAzure.Storage.Auth;
using Microsoft.WindowsAzure.Storage.Blob;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using PayBayService.Models;
using PayBayService.Models.Accounts;
using PayBayService.Models.BlobStorage;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;

namespace PayBayService.Common
{

    public enum TYPE
    {
        OLD = 0,
        NEW = 1
    };

    public class Methods
    {
        private static string connectionString = ConfigurationManager.ConnectionStrings["MS_TableConnectionString"].ToString();

        //SqlConnection cnn;
        //SqlCommand cmd;
        //SqlDataAdapter da;        

        public static string err = "";

        static string StorageAccoutName = ConfigurationManager.AppSettings["STORAGE_ACCOUNT_NAME"].ToString();
        static string StorageAccountKey = ConfigurationManager.AppSettings["STORAGE_ACCOUNT_ACCESS_KEY"].ToString();
                  
        private static Methods m_Instance = null;

        public static Methods GetInstance()
        {
            if (m_Instance == null)
                return m_Instance = new Methods();
            return m_Instance;
        }

        private Methods()
        {            
        }

        /// <summary>
        /// Return a json object
        /// </summary>
        /// <param name="code">ErrCode</param>
        /// <param name="message">ErrMsg</param>
        /// <returns></returns>
        public static JObject CustomResponseMessage(int code, string message)
        {
            JObject response = new JObject();
            response.Add("ErrCode", code);
            response.Add("ErrMsg", message);

            return response;
        }

        /// <summary>
        /// Execute Non Query with no data
        /// </summary>
        /// <param name="sql">String query or procedure name</param>
        /// <param name="ct">Text or Store Procedure</param>
        /// <param name="err">Error</param>
        /// <param name="param">Parameters</param>
        /// <returns></returns>
        public bool ExecNonQuery(string sql, CommandType ct, ref string err, params SqlParameter[] param)
        {
            bool result = false;
            using (SqlConnection cnn = new SqlConnection(connectionString))
            {
                try
                {
                    if (cnn.State == ConnectionState.Open)
                        cnn.Close();
                    cnn.Open();
                    SqlCommand cmd = new SqlCommand(sql, cnn);
                    cmd.CommandType = ct;
                    cmd.CommandTimeout = 6000;
                    cmd.Parameters.Clear();
                    if (param != null)
                    {
                        foreach (SqlParameter p in param)
                        {
                            cmd.Parameters.Add(p);
                        }
                    }

                    cmd.ExecuteNonQuery();

                    result = true;
                }
                catch (Exception ex)
                {
                    err = ex.Message;
                    result = false;
                }
                finally
                {
                    cnn.Close();
                }
            }
            return result;
        }

        /// <summary>
        /// Excute query with data list from database
        /// </summary>
        /// <param name="sql">String query or procedure name</param>
        /// <param name="ct">Text or Store Procedure</param>
        /// <param name="err">Error</param>
        /// <param name="param">Parameters</param>
        /// <returns></returns>
        public JArray ExecQueryWithResult(string sql, CommandType ct, ref string err, params SqlParameter[] param)
        {
            DataTable dt = new DataTable();
            JArray result = new JArray();
            string json = "";
            using (SqlConnection cnn = new SqlConnection(connectionString))
            {
                try
                {
                    if (cnn.State == ConnectionState.Open)
                        cnn.Close();
                    cnn.Open();
                    SqlCommand cmd = new SqlCommand(sql, cnn);
                    cmd.CommandType = ct;
                    cmd.CommandTimeout = 6000;
                    cmd.Parameters.Clear();
                    if (param != null)
                    {
                        foreach (SqlParameter p in param)
                            cmd.Parameters.Add(p);
                    }

                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    da.Fill(dt);
                    json = JsonConvert.SerializeObject(dt);
                    result = JArray.Parse(json);
                }
                catch (SqlException ex)
                {
                    JObject message = CustomResponseMessage(0, ex.Message.ToString());
                    result.Add(message);
                    err = ex.Message;
                }
                finally
                {
                    cnn.Close();
                }
            }
            return result;
        }
                
        /// <summary>
        /// Get a value from specific column
        /// </summary>
        /// <param name="sql">query or procedure</param>
        /// <param name="ct">Text or Store Procedure</param>
        /// <param name="err">Storage error</param>
        /// <param name="param">List parameters</param>
        /// <returns></returns>
        public object GetValue(string sql, CommandType ct, ref string err, params SqlParameter[] param)
        {
            object result = null;
            using (SqlConnection cnn = new SqlConnection(connectionString))
            {
                try
                {
                    if (cnn.State == ConnectionState.Open)
                        cnn.Close();
                    cnn.Open();
                    SqlCommand cmd = new SqlCommand(sql, cnn);
                    cmd.CommandType = ct;
                    cmd.CommandTimeout = 6000;
                    cmd.Parameters.Clear();
                    if (param != null)
                    {
                        foreach (SqlParameter p in param)
                        {
                            cmd.Parameters.Add(p);
                        }
                    }
                    result = cmd.ExecuteScalar();
                }
                catch (Exception ex)
                {
                    err = ex.Message;
                }
                finally
                {
                    cnn.Close();
                }
            }
            return result;

        }

        public string GetImageFromByteArray(byte[] f, string fileName)
        {
            try
            {
                MemoryStream ms = new MemoryStream(f);

                string path = System.Web.Hosting.HostingEnvironment.MapPath("~/StorageImage/") + fileName + ".png";

                FileStream fs = new FileStream(path, FileMode.Create);

                ms.WriteTo(fs);

                ms.Close();
                fs.Close();
                fs.Dispose();

                return path;
            }
            catch (Exception ex)
            {
                // return the error message if the operation fails
                return ex.Message.ToString();
            }
        }

        /// <summary>
        /// Get Sas Query to write on Blob storage and get image uri
        /// </summary>
        /// <param name="containnerName">Directory</param>
        /// <param name="resourceName">File name</param>
        /// <param name="objectId">File id avoid same name</param>
        /// <returns>Model Blob</returns>
        public async Task<ModelBlob> GetSasAndImageUriFromBlob(string containnerName, string resourceName, int objectId)
        {
            ModelBlob blob = new ModelBlob();
            
            // Set the URI for the Blob Storage service.
            Uri blobEndpoint = new Uri(string.Format("https://{0}.blob.core.windows.net", StorageAccoutName));

            // Create the BLOB service client.
            CloudBlobClient blobClient = new CloudBlobClient(blobEndpoint,
                new StorageCredentials(StorageAccoutName, StorageAccountKey));
                        
            // Set the BLOB store container name on the item, which must be lowercase.
            string _resname = resourceName.ToLower();
            _resname = (_resname.IndexOf(" ") != -1) ? _resname.Replace(" ", "") : _resname;

            // Create a container, if it doesn't already exist.
            CloudBlobContainer container = blobClient.GetContainerReference(containnerName);
            await container.CreateIfNotExistsAsync();

            // Create a shared access permission policy. 
            BlobContainerPermissions containerPermissions = new BlobContainerPermissions();

            // Enable anonymous read access to BLOBs.
            containerPermissions.PublicAccess = BlobContainerPublicAccessType.Blob;
            container.SetPermissions(containerPermissions);

            // Define a policy that gives write access to the container for 5 minutes.                                   
            SharedAccessBlobPolicy sasPolicy = new SharedAccessBlobPolicy()
            {
                SharedAccessExpiryTime = DateTime.UtcNow.AddHours(24),
                Permissions = SharedAccessBlobPermissions.Write | SharedAccessBlobPermissions.Read
            };

            // Get the SAS as a string.
            blob.SasQuery = container.GetSharedAccessSignature(sasPolicy);

            // Set the URL used to store the image.
            blob.ImageUri = string.Format("{0}{1}/{2}", blobEndpoint.ToString(),
                containnerName, _resname + objectId + ".jpg");

            return blob;            
        }

        public async Task<bool> SendMail(string email,string newPwd)
        {
            var body = "<p>Email From: {0} ({1})</p><p>Message:</p><p>{2}</p>";
            string msg = string.Format("<p>HI {0}!!!</p><p>Your new password is: {1}</p><p>Thank you for using our app!</p>", email, newPwd);
            var message = new MailMessage();
            message.To.Add(new MailAddress(email));  // replace with valid value 
            message.From = new MailAddress("paybayservice@outlook.com.vn");  // replace with valid value
            message.Subject = "Reset Your Password";
            message.Body = string.Format(body, "Paybay Group", "paybayservice@outlook.com.vn", msg);
            message.IsBodyHtml = true;
            message.Priority = MailPriority.High;
            try {
                using (var smtp = new SmtpClient())
                {
                    var credential = new NetworkCredential
                    {
                        UserName = "paybayservice@outlook.com.vn",  // replace with valid value
                        Password = "paybayteam@"  // replace with valid value
                    };
                    smtp.UseDefaultCredentials = false;
                    smtp.Credentials = credential;
                    smtp.Host = "smtp-mail.outlook.com";
                    smtp.Port = 587;
                    smtp.EnableSsl = true;                    
                    await smtp.SendMailAsync(message);
                }
            }
            catch (Exception ex)
            {
                return false;
            }
            return true;
        }

        public async Task<bool> UserSendMail(AccountMail mail)
        {
            string Host = "";
            var body = "<p>Email From: {0} ({1})</p><p>Message:</p><p>{2}</p>";
            string msg = string.Format("<p>{0}</p>", mail.Content);
            var message = new MailMessage();
            message.To.Add(new MailAddress("paybayservice@outlook.com.vn"));  // replace with valid value 
            message.From = new MailAddress(mail.Email);  // replace with valid value
            message.Subject = mail.Title;
            message.Body = string.Format(body, mail.Email, "paybayservice@outlook.com.vn", msg);
            message.IsBodyHtml = true;
            message.Priority = MailPriority.High;
            try
            {
                using (var smtp = new SmtpClient())
                {
                    var credential = new NetworkCredential
                    {
                        UserName = mail.Email,  // replace with valid value
                        Password = mail.Pass  // replace with valid value
                    };
                    smtp.UseDefaultCredentials = false;
                    smtp.Credentials = credential;
                    Host = mail.Email.Substring(mail.Email.IndexOf("@") + 1);
                    if (mail.Email.IndexOf("outlook.com") != -1)
                    {
                        Host = "smtp-mail." + Host;
                    }
                    else
                        Host = "smtp." + Host;            
                    smtp.Host = Host;
                    smtp.Port = 587;
                    smtp.EnableSsl = true;
                    await smtp.SendMailAsync(message);
                }
            }
            catch (Exception ex)
            {
                return false;
            }
            return true;
        }

    }
}