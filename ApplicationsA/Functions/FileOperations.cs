using ApplicationsA.Helpers;
using Domain.Common;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Security.Principal;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace ApplicationsA.Functions
{
    public class FileOperations
    {

        public int DeleteFilesFromPath(string path)
        {
            int count = 0;           
            File.Delete(path);
            //Verify if file was deleted
            FileInfo fileInfo = new FileInfo(path);
            if (!fileInfo.Exists)
            {
                count++;
            }
            return count;                        
            
        }
        /// <summary>
        /// GetDateStringFromFileName
        /// </summary>
        /// <param name="fileName"></param>
        /// <returns> DateString </returns>

        public string GetDateStringFromFileName(string fileName)
        {
            //example for filename : 20230104132136-20230104132136.JPG
            //get datetime with hour minuts and secons from file name
            string dateString = Regex.Match(fileName, @"\d{14}").Value;           
            //string dateString = Regex.Match(fileName, @"\d{8}").Value;
            return dateString;
        }

        /// <summary>
        /// upLoadAllImagesFromPath
        /// </summary>
        /// <param name="rootPath"></param>
        /// <param name="destinationPath"></param>
        /// <returns></returns>
        public List<ImageRepositoryModel> UpLoadAllImagesFromPath(string rootPath, string destinationPath)
        {
            List<ImageRepositoryModel> imageRepositories = new List<ImageRepositoryModel>();
            List<string> files = Directory.GetFiles(rootPath, "*.*", SearchOption.AllDirectories).ToList();
            Parallel.ForEach(files, file =>
            {
                UploadFile(destinationPath, file, imageRepositories);
            });
            return imageRepositories;
        }
        /// <summary>
        /// UploadFile
        /// </summary>
        /// <param name="destinationPath"></param>
        /// <param name="file"></param> 
        /// <param name="imageRepositories"></param>
        /// <returns></returns>

        private void UploadFile(string destinationPath, string file, List<ImageRepositoryModel> imageRepositories)
        {
            string serialNumber = SerialNumbers.GetValidSerialNumberFromPath(file);
            string dateString = GetDateStringFromFileName(file);
            if (serialNumber.Length > 0 && dateString.Length > 0)
            {
                var serialAndPath = CreateFoldersAndUpdateImageFromPath(file, destinationPath);
                imageRepositories.Add(new ImageRepositoryModel
                {
                    SerialNumber = serialAndPath.Item1,
                    Path = serialAndPath.Item2
                });
            }
        }
        /// <summary>
        /// GetFilesFromPath
        /// </summary>
        /// <param name="rootPath"></param>
        /// <returns>List Of Files</returns>
        
        public List<string> GetFilesFromPath(string rootPath)
        {
            var ext = new List<string> { "jpg", "tif" };
            List<string> files = Directory.GetFiles(rootPath,"*.*",SearchOption.AllDirectories).Where(s => ext.Contains(Path.GetExtension(s).TrimStart('.').ToLowerInvariant())).ToList();
            return files;
        }
        /// <summary>
        /// CreateFoldersAndUpdateImageFromPath
        /// </summary>
        /// <param name="sourcePath"></param>
        /// <param name="newRootPath"></param>
        /// <returns>SerialNumer, New Path</returns>

        public Tuple<string, string> CreateFoldersAndUpdateImageFromPath(string sourcePath, string newRootPath, bool createSerialFolder = false)
        {

            string imageFileName = Path.GetFileName(sourcePath);
            string serialNumber = SerialNumbers.GetValidSeriaFromPath(imageFileName);            
            string date = GetDateStringFromFileName(imageFileName);

            //If image name does not contain date, then get date from file creation date
            if (date.Length == 0)
            {
                 //get datetime with hour minuts and secons from dateString
                date = File.GetCreationTime(sourcePath).ToString("yyyyMMddHHmmss");
                imageFileName = $"{date}-{imageFileName}";
            }
            string year = date.Substring(0, 4);
            string month = date.Substring(4, 2);
            string day = date.Substring(6, 2);
            string newPath = createSerialFolder? Path.Combine(newRootPath, year, month, day,serialNumber):Path.Combine(newRootPath, year, month, day);
            string virtualPath = createSerialFolder ? Path.Combine(year, month, day, serialNumber, imageFileName):  Path.Combine(year, month, day, imageFileName);
            int counter = 0;
      
           //Connect to Red Folder with credentials and check if folder exists
            string userName = "SVCGDL_WEBAPPS_SWENG";
            string password = "Jabil@1234";
            string domain = "jabil";
            
            //save sourcePath  image in memory
            Byte[] byteSource = File.ReadAllBytes(sourcePath);

            //convert byteSource to file stream            
            if (userName != null && userName != "" && password != null && password != "" && domain != null && domain != "")
            {
                try
                {                   
                    ImpersonationHelper.Impersonate(domain, userName, password, delegate
                    {
                    Chek:
                        //copy files to red folder
                        if (!Directory.Exists(newPath))
                        {
                            Directory.CreateDirectory(newPath);

                            //Sleep 3
                            System.Threading.Thread.Sleep(3000);

                            if (counter > 5)
                                return;
                            counter++;
                            goto Chek;
                            //copy image to new path                            
                        }
                        if (counter <= 5)
                        {
                            string newImagePath = Path.Combine(newPath, imageFileName);
                            //force to overwrite if exists                           
                            File.WriteAllBytes(newImagePath, byteSource);                           
                        }
                    });
                    if (counter > 3)
                    {
                        return new Tuple<string, string>(null, null);
                    }
                  
                    return new Tuple<string, string>(serialNumber, virtualPath);
                }
                catch (Exception ex) { 
                     LoggerImage.WriteLog(ex.Message, "CreateFoldersAndUpdateImageFromPath");
                     return new Tuple<string, string>(null, null); 
                     }
               
            }
            return new Tuple<string, string>(null, null);
        }
    }
}
