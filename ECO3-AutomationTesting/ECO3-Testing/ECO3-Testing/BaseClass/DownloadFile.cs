using ECO3_Testing.ComponentHelpers;
using System;
using System.IO;
using System.Linq;

namespace ECO3_Testing.BaseClass
{
    public class DownloadFile
    {
        private string GetDownloadPath()
        {
            string userPath = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile);
            return Path.Combine(userPath, "Downloads");
        }
        public int DownloadIsComplete()
        {
            Directory.CreateDirectory(GetDownloadPath());
            GenericHelper.HardWait(7);
            return GetCurrentCSVFilesInDownloadFolder();
        }
        
        public int GetCurrentCSVFilesInDownloadFolder()
        {
            return Directory.GetFiles(GetDownloadPath(), "*.csv*", SearchOption.AllDirectories).Length;
        }

        public string GetLastDownloadedFile()
        {
            DirectoryInfo directory = new DirectoryInfo(GetDownloadPath());
            string fileDownloaded =  directory.GetFiles("*.csv")
                .OrderByDescending(f => (f == null ? DateTime.MinValue : f.LastWriteTime))
                .FirstOrDefault().ToString();
            return Path.Combine(GetDownloadPath(), fileDownloaded);
        }
        public void DeleteFile()
        { 
            string fileToDelete = Path.Combine(GetDownloadPath(), GetLastDownloadedFile());
            if (File.Exists(fileToDelete))
            {
                File.Delete(fileToDelete);
            }
        }
    }
}
