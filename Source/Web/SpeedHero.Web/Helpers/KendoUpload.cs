namespace SpeedHero.Web.Helpers
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Web;

    public class KendoUpload
    {
        public static bool CheckIsFileAnImage(HttpPostedFileBase file)
        {
            if (file == null)
            {
                throw new ArgumentNullException("No file");
            }

            var allowedFileTypes = new List<string> { "image/jpeg", "image/png" };

            foreach (var type in allowedFileTypes)
            {
                if (file.ContentType == type)
                {
                    return true;
                }
            }

            return false;
        }

        public static void SaveCoverPhoto(HttpPostedFileBase coverPhoto, string path, HttpServerUtilityBase server)
        {
            if (coverPhoto == null)
            {
                throw new ArgumentNullException("No cover photo");
            }

            if (string.IsNullOrEmpty(path))
            {
                throw new ArgumentNullException("No path in which to save the files");
            }

            // Some browsers send file names with full path. We only care about the file name.
            var coverPhotoName = Path.GetFileName(coverPhoto.FileName);
            var destinationPath = Path.Combine(server.MapPath(path), coverPhotoName);
            coverPhoto.SaveAs(destinationPath);
        }
    }
}