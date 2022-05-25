using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Configuration;

namespace Global
{
    public class File
    {
        public static string Backslash = "\\";

        /// <summary>
        /// Build FilePath
        /// </summary>
        /// <param name="transactionID"></param>
        /// <param name="fileName"></param>
        /// <returns></returns>
        public static string GetFilePath(int transactionID, string fileName,int folderPath)
        {
            string filepath = GetFolderPath(folderPath) + Convert.ToString(transactionID) + Backslash + fileName;
            return filepath;
        }

        /// <summary>
        /// Build FilePath
        /// </summary>
        /// <param name="transactionID"></param>
        /// <param name="fileName"></param>
        /// <returns></returns>
        public static string GetFilePathTemplates(int documentVersionID, string fileName, int folderPath)
        {
            string filepath = GetFolderPath(folderPath) + Convert.ToString(documentVersionID) + Backslash + fileName;
            return filepath;
        }

        /// <summary>
        /// Build FilePath
        /// </summary>
        /// <param name="transactionID"></param>
        /// <param name="fileName"></param>
        /// <returns></returns>
        public static string GetFilePathDocuments(int dealID,int documentID, string fileName, int folderPath)
        {
            string filepath = GetFolderPath(folderPath) + Convert.ToString(dealID) + Backslash + Convert.ToString(documentID) + Backslash + fileName;
            return filepath;
        }

        /// <summary>
        /// Get Physical path
        /// </summary>
        /// <returns></returns>
        public static string GetFolderPath(int folderPath)
        {
            switch (folderPath)
            {
                case (int)Enums.FolderPath.Actions:
                    return ConfigurationManager.AppSettings["ActionsFolderPath"].ToString();
                case (int)Enums.FolderPath.Templates:
                    return ConfigurationManager.AppSettings["TemplatesFolderPath"].ToString();
                case (int)Enums.FolderPath.Documents:
                    return ConfigurationManager.AppSettings["DocumentsFolderPath"].ToString();
                case (int)Enums.FolderPath.Profile:
                    return ConfigurationManager.AppSettings["ProfileFolderPath"].ToString();
                case (int)Enums.FolderPath.Correspondence:
                    return ConfigurationManager.AppSettings["CorrespondenceFolderPath"].ToString();
                case (int)Enums.FolderPath.Swift:
                    return ConfigurationManager.AppSettings["SwiftFolderPath"].ToString();
                default:
                    return ConfigurationManager.AppSettings["ActionsFolderPath"].ToString();
            }
        }

        /// <summary>
        /// Create folder if not exists
        /// </summary>
        /// <param name="transactionID"></param>
        public static void CreateNewDirectoryActions(int folderName,int folderPath)
        {
            string FolderDirectory = GetFolderPath(folderPath) + Convert.ToString(folderName);

            // If the destination directory does not exist, create it.
            if (!Directory.Exists(FolderDirectory))
            {
                Directory.CreateDirectory(FolderDirectory);
            }

        }

        /// <summary>
        /// Create folder if not exists
        /// </summary>
        /// <param name="transactionID"></param>
        public static void CreateNewDirectoryProfile(int folderName, int folderPath)
        {
            string FolderDirectory = GetFolderPath(folderPath) + Convert.ToString(folderName);

            // If the destination directory does not exist, create it.
            if (!Directory.Exists(FolderDirectory))
            {
                Directory.CreateDirectory(FolderDirectory);
            }

        }

        /// <summary>
        /// Create folder if not exists
        /// </summary>
        /// <param name="transactionID"></param>
        public static void CreateNewDirectoryTemplates(int folderName, int folderPath)
        {
            string FolderDirectory = GetFolderPath(folderPath) + Convert.ToString(folderName);

            // If the destination directory does not exist, create it.
            if (!Directory.Exists(FolderDirectory))
            {
                Directory.CreateDirectory(FolderDirectory);
            }

        }

        /// <summary>
        /// Create folder if not exists
        /// </summary>
        /// <param name="transactionID"></param>
        public static void CreateNewDirectoryDocuments(int dealID,int documentID, int folderPath)
        {
            string FolderDirectory = GetFolderPath(folderPath) + Convert.ToString(dealID) + Backslash + Convert.ToString(documentID);

            // If the destination directory does not exist, create it.
            if (!Directory.Exists(FolderDirectory))
            {
                Directory.CreateDirectory(FolderDirectory);
            }

        }

        /// <summary>
        /// Copy all files from one folder to another
        /// </summary>
        /// <param name="sourceDirName"></param>
        /// <param name="destDirName"></param>
        /// <param name="copySubDirs"></param>
        private static void FolderCopy(string sourceDirName, string destDirName, bool copySubDirs)
        {
            DirectoryInfo dir = new DirectoryInfo(sourceDirName);
            DirectoryInfo[] dirs = dir.GetDirectories();

            // If the source directory does not exist, throw an exception.
            if (!dir.Exists)
            {
                throw new DirectoryNotFoundException(
                    "Source directory does not exist or could not be found: "
                    + sourceDirName);
            }

            // If the destination directory does not exist, create it.
            if (!Directory.Exists(destDirName))
            {
                Directory.CreateDirectory(destDirName);
            }


            // Get the file contents of the directory to copy.
            FileInfo[] files = dir.GetFiles();

            foreach (FileInfo file in files)
            {
                // Create the path to the new copy of the file.
                string temppath = Path.Combine(destDirName, file.Name);

                // Copy the file.
                file.CopyTo(temppath, false);
            }

            // If copySubDirs is true, copy the subdirectories.
            if (copySubDirs)
            {

                foreach (DirectoryInfo subdir in dirs)
                {
                    // Create the subdirectory.
                    string temppath = Path.Combine(destDirName, subdir.Name);

                    // Copy the subdirectories.
                    FolderCopy(subdir.FullName, temppath, copySubDirs);
                }
            }
        }

        /// <summary>
        /// Move all files from old TransactionID to new 
        /// </summary>
        /// <param name="oldTransactionID"></param>
        /// <param name="newTransactionID"></param>
        public static void ArchiveTransactionFiles(int oldTransactionID,int newTransactionID,int folderPath)
        {
            try
            {
                string oldfolderpath = GetFolderPath(folderPath) + Convert.ToString(oldTransactionID);
                string newfolderpath = GetFolderPath(folderPath) + Convert.ToString(newTransactionID);

                // Move old files to New folder => New TransactionID
                FolderCopy(oldfolderpath, newfolderpath, false);//prevent to overite the file if false

                // overwrite the destination file if it already exists.
                //FolderCopy(oldfolderpath, newfolderpath, true);
            }
            catch (DirectoryNotFoundException DirErr)
            {
                // Do not archive old folder, only create Destination Folder!

                string newfolderpath = GetFolderPath(folderPath) + Convert.ToString(newTransactionID);
                if (!Directory.Exists(newfolderpath))
                {
                    Directory.CreateDirectory(newfolderpath);
                }
            }
            catch (IOException err)
            {
                throw;
            }

        }

        /// <summary>
        /// Move all files from old DocumentID to new 
        /// </summary>
        /// <param name="oldTransactionID"></param>
        /// <param name="newTransactionID"></param>
        public static void ArchiveDocumentFiles(int dealID, int oldDocumentID, int newDocumentID, int folderPath)
        {
            try
            {
                string oldfolderpath = GetFolderPath(folderPath) + Convert.ToString(dealID) + Backslash + Convert.ToString(oldDocumentID);
                string newfolderpath = GetFolderPath(folderPath) + Convert.ToString(dealID) + Backslash + Convert.ToString(newDocumentID);

                // Move old files to New folder => New TransactionID
                FolderCopy(oldfolderpath, newfolderpath, false);

            }
            catch (DirectoryNotFoundException DirErr)
            {
                // Do not archive old folder, only create Destination Folder!

                string newfolderpath = GetFolderPath(folderPath) + Convert.ToString(dealID) + Backslash + Convert.ToString(newDocumentID);
                if (!Directory.Exists(newfolderpath))
                {
                    Directory.CreateDirectory(newfolderpath);
                }
            }
            catch (IOException err)
            {
                throw;
            }

        }

        /// <summary>
        /// Move all files from old TransactionID to new 
        /// </summary>
        /// <param name="oldTransactionID"></param>
        /// <param name="newTransactionID"></param>
        public static void ArchiveTemplatesFiles(int oldVersionID, int newVersionID, int folderPath)
        {
            try
            {
                string oldfolderpath = GetFolderPath(folderPath) + Convert.ToString(oldVersionID);
                string newfolderpath = GetFolderPath(folderPath) + Convert.ToString(newVersionID);

                // Move old files to New folder => New TransactionID
                FolderCopy(oldfolderpath, newfolderpath, false);

            }
            catch (IOException err)
            {
                throw;
            }

        }

        /// <summary>
        /// return true if folder path exist
        /// </summary>
        /// <param name="oldVersionID"></param>
        /// <param name="newVersionID"></param>
        /// <param name="folderPath"></param>
        /// <returns></returns>
        public static bool CheckFolderPathExist(int oldVersionID, int folderPath)
        {
            try
            {
                string oldfolderpath = GetFolderPath(folderPath) + Convert.ToString(oldVersionID);
                return Directory.Exists(oldfolderpath);//chk if file exist          
            }
            catch (IOException err)
            {
                throw;
            }
        }


        /// <summary>
        /// Create File
        /// </summary>
        /// <param name="path"></param>
        /// <param name="fileName"></param>
        /// <param name="fileContent"></param>
        public static void WriteFiletoPath(string path, string fileName, byte[] fileContent)
        {
            try
            {
               // Create File
                FileInfo fileInfo = new FileInfo(path);
                FileStream fileStream = fileInfo.Open(FileMode.Create,
                    FileAccess.Write);
                
                // Write fileContent to path
                fileStream.Write(fileContent,
                    0, fileContent.Length);
                fileStream.Flush();
                fileStream.Close();

            }
            catch (IOException err)
            {
                throw ;
            }

        }

        /// <summary>
        /// Get Stored File
        /// </summary>
        /// <param name="transactionID"></param>
        /// <param name="fileName"></param>
        /// <returns></returns>
        public static byte[] GetFile(int folderID, string fileName,int folderPath)
        {
            string filepath = GetFilePath(folderID, fileName,folderPath);
            return System.IO.File.ReadAllBytes(filepath);
        }

        /// <summary>
        /// Get Stored File
        /// </summary>
        /// <param name="transactionID"></param>
        /// <param name="fileName"></param>
        /// <returns></returns>
        public static byte[] GetFileDocuments(int dealID,int folderID, string fileName, int folderPath)
        {
            string filepath = GetFilePathDocuments(dealID,folderID, fileName, folderPath);
            return System.IO.File.ReadAllBytes(filepath);
        }

    }
}
