using Microsoft.WindowsAzure.Storage.Auth;
using Microsoft.WindowsAzure.Storage.Blob;
using PayBay.Model;
using PayBay.Utilities.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Windows.ApplicationModel.Email;
using Windows.Devices.Geolocation;
using Windows.Storage;
using Windows.Storage.Pickers;
using Windows.UI.Popups;
using Windows.UI.Xaml.Media.Imaging;

namespace PayBay.Utilities.Common
{
    public enum TYPEGET
    {
        START = 0,
        MORE = 1
    };

    public enum TYPE
    {
        OLD = 0,
        NEW = 1
    };
        
    public class Functions
    {                
        private static Functions m_Instance = null;

        public static bool isDiscover = false;
        
        static Regex ValidEmailRegex = CreateValidEmailRegex();
               
        public static Functions Instance
        {
            get
            {
                return m_Instance ?? (m_Instance = new Functions());
            }

            protected set
            {
                m_Instance = value;
            }
        }
                
        public static byte[] GetBytes(string str)
        {
            byte[] bytes = System.Text.Encoding.UTF8.GetBytes(str);
            //byte[] bytes = new byte[str.Length * sizeof(char)];
            //System.Buffer.BlockCopy(str.ToCharArray(), 0, bytes, 0, bytes.Length);
            return bytes;
        }

        public static string GetString(byte[] bytes)
        {
            string str = System.Text.Encoding.UTF8.GetString(bytes, 0, bytes.Length);
            //char[] chars = new char[bytes.Length / sizeof(char)];
            //System.Buffer.BlockCopy(bytes, 0, chars, 0, bytes.Length);
            //return new string(chars);
            return str;
        }

        public static async Task<StorageFile> GetPhotoFromGallery()
        {            
            FileOpenPicker filePicker = new FileOpenPicker();
            //filePicker.SuggestedStartLocation = PickerLocationId.PicturesLibrary;
            filePicker.ViewMode = PickerViewMode.Thumbnail;

            // Filter to include a sample subset of file types
            filePicker.FileTypeFilter.Clear();
            filePicker.FileTypeFilter.Add(".bmp");
            filePicker.FileTypeFilter.Add(".png");
            filePicker.FileTypeFilter.Add(".jpeg");
            filePicker.FileTypeFilter.Add(".jpg");

            StorageFile media = await filePicker.PickSingleFileAsync();
                        
            return media;
        }

        public async Task<bool> UploadImageToBlob(string containerName, string image, string SasQuery, StorageFile media)
        {
            try
            {
                if (NetworkHelper.Instance.HasInternetConnection)
                {
                    //If we have a returned SAS, then upload the blob.
                    if (!string.IsNullOrEmpty(SasQuery))
                    {
                        // Get the URI generated that contains the SAS 
                        // and extract the storage credentials.
                        StorageCredentials cred = new StorageCredentials(SasQuery);
                        var imageUri = new Uri(image);

                        // Instantiate a Blob store container based on the info in the returned item.
                        CloudBlobContainer container = new CloudBlobContainer(
                            new Uri(string.Format("https://{0}/{1}",
                                imageUri.Host, containerName)), cred);

                        string resourceName = image.Substring(image.LastIndexOf('/') + 1);

                        // Get the new image as a stream.
                        using (var inputStream = await media.OpenReadAsync())
                        {
                            // Upload the new image as a BLOB from the stream.
                            CloudBlockBlob blobFromSASCredential =
                                container.GetBlockBlobReference(resourceName);
                            await blobFromSASCredential.UploadFromStreamAsync(inputStream);
                        }
                    }
                }                
            }
            catch (Exception ex)
            {
                return false;
            }
            return true;
        }

        public async Task<bool> DeleteImageInBlob(string containerName, string image, string SasQuery)
        {
            try
            {
                if (NetworkHelper.Instance.HasInternetConnection)
                {
                    //If we have a returned SAS, then upload the blob.
                    if (!string.IsNullOrEmpty(SasQuery))
                    {
                        // Get the URI generated that contains the SAS 
                        // and extract the storage credentials.
                        StorageCredentials cred = new StorageCredentials(SasQuery);
                        var imageUri = new Uri(image);

                        // Instantiate a Blob store container based on the info in the returned item.
                        CloudBlobContainer container = new CloudBlobContainer(
                            new Uri(string.Format("https://{0}/{1}",
                                imageUri.Host, containerName)), cred);

                        string resourceName = image.Substring(image.LastIndexOf('/') + 1);
                                                                       
                        // Upload the new image as a BLOB from the stream.
                        CloudBlockBlob blobFromSASCredential = container.GetBlockBlobReference(resourceName);
                        await blobFromSASCredential.DeleteIfExistsAsync();                       
                    }
                }
            }
            catch (Exception ex)
            {
                return false;
            }
            return true;
        }

        private static Regex CreateValidEmailRegex()
        {
            string validEmailPattern = @"^(?!\.)(""([^""\r\\]|\\[""\r\\])*""|"
                + @"([-a-z0-9!#$%&'*+/=?^_`{|}~]|(?<!\.)\.)*)(?<!\.)"
                + @"@[a-z0-9][\w\.-]*[a-z0-9]\.[a-z][a-z\.]*[a-z]$";

            return new Regex(validEmailPattern, RegexOptions.IgnoreCase);
        }

        internal static bool EmailIsValid(string emailAddress)
        {
            bool isValid = ValidEmailRegex.IsMatch(emailAddress);

            return isValid;
        }

        public async Task ComposeSms(Windows.ApplicationModel.Contacts.Contact recipient,
                                        string messageBody,
                                        StorageFile attachmentFile,
                                        string mimeType)
        {
            var chatMessage = new Windows.ApplicationModel.Chat.ChatMessage();
            chatMessage.Body = messageBody;

            if (attachmentFile != null)
            {
                var stream = Windows.Storage.Streams.RandomAccessStreamReference.CreateFromFile(attachmentFile);

                var attachment = new Windows.ApplicationModel.Chat.ChatMessageAttachment(
                    mimeType,
                    stream);

                chatMessage.Attachments.Add(attachment);
            }

            var phone = recipient.Phones.FirstOrDefault<Windows.ApplicationModel.Contacts.ContactPhone>();
            if (phone != null)
            {
                chatMessage.Recipients.Add(phone.Number);
            }
            await Windows.ApplicationModel.Chat.ChatMessageManager.ShowComposeSmsMessageAsync(chatMessage);
        }

        public static bool CheckExpiredDateOfSasQuery(string sasQuery)
        {
            int startIndex = sasQuery.LastIndexOf("&se=");
            int length = 10;
            string expireDate = sasQuery.Substring(startIndex, length);
            DateTime expiredDate = Convert.ToDateTime(expireDate);
            if (expiredDate >= DateTime.UtcNow)
                return false;

            return true;
        }

        public static async Task<Coordinate> TrackLocationOfUser()
        {
            var accessStatus = await Geolocator.RequestAccessAsync();
            Coordinate coor = new Coordinate();
            switch (accessStatus)
            {
                case GeolocationAccessStatus.Allowed:                    
                    Geolocator geolocator = new Geolocator() { DesiredAccuracyInMeters = 0 };
                    Geoposition pos = await geolocator.GetGeopositionAsync(
                                            maximumAge: TimeSpan.FromMinutes(5),
                                            timeout: TimeSpan.FromSeconds(10));
                    coor.Latitute = pos.Coordinate.Point.Position.Latitude;
                    coor.Longitute = pos.Coordinate.Point.Position.Longitude;                    
                    break;
                case GeolocationAccessStatus.Denied:
                    await new MessageDialog("Access to location is denied.", "Notification!").ShowAsync();
                    break;
                case GeolocationAccessStatus.Unspecified:
                    await new MessageDialog("Unspecified error.", "Notification!").ShowAsync();
                    break;
            }
            return coor;
        }
                
    }
}
