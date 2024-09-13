using Firebase.Storage;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FSU.SPORTIDY.Service.Utils.Common.Upload
{
    public static class UploadToFirebase
    {
        public static async Task<string> UploadImageOrVideo(IFormFile fileRoot, string nameOfDirection)
        {
            string videoFileName = Path.GetFileName(fileRoot.FileName);
            var firebaseStorage = new FirebaseStorage(FirebaseConfig.STORAGE_BUCKET);
            await firebaseStorage.Child(nameOfDirection).Child(videoFileName).PutAsync(fileRoot.OpenReadStream());
            var downloadVideoUrl = await firebaseStorage.Child(nameOfDirection).Child(videoFileName).GetDownloadUrlAsync();
            return downloadVideoUrl; 
        }
    }
}
