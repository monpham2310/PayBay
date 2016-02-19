using Microsoft.WindowsAzure.Storage.Auth;
using Microsoft.WindowsAzure.Storage.Blob;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Storage;
using Windows.Storage.Pickers;
using Windows.UI.Xaml.Media.Imaging;

namespace PayBay.Utilities.Common
{
    public class Functions
    {

        private static Functions m_Instance = null;

        public static Functions GetInstance()
        {
            if(m_Instance == null)
            {
                return new Functions();
            }
            return m_Instance;
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

        public async Task<bool> UploadImageToBlob(string containerName, string resourceName, int objectId, string image, string SasQuery, StorageFile media)
        {
            try
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

                    // Get the new image as a stream.
                    using (var inputStream = await media.OpenReadAsync())
                    {
                        // Upload the new image as a BLOB from the stream.
                        CloudBlockBlob blobFromSASCredential =
                            container.GetBlockBlobReference(resourceName + objectId + ".jpg");
                        await blobFromSASCredential.UploadFromStreamAsync(inputStream);
                    }                                                           
                }
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

    }
}
