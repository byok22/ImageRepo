using Domain.Entities;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace ApplicationsA.Functions
{
    public class FileOperations
    {
        public string getDateStringFromFileName(string fileName)
        {
            string dateString = Regex.Match(fileName, @"\d{8}").Value;
            return dateString;
        }

        /// <summary>
        /// upLoadAllImagesFromPath
        /// </summary>
        /// <param name="rootPath"></param>
        /// <param name="destinationPath"></param>
        /// <returns></returns>
        public List<ImageRepositoryModel> upLoadAllImagesFromPath(string rootPath, string destinationPath)
        {
            List<ImageRepositoryModel> imageRepositories = new List<ImageRepositoryModel>();
            List<string> files = Directory.GetFiles(rootPath, "*.*", SearchOption.AllDirectories).ToList();
            Parallel.ForEach(files, file =>
            {
                UploadFile(destinationPath, file, imageRepositories);
            });
            return imageRepositories;
        }

        private void UploadFile(string destinationPath, string file, List<ImageRepositoryModel> imageRepositories)
        {
            string serialNumber = SerialNumbers.getValidSerialNumberFromPath(file);
            string dateString = getDateStringFromFileName(file);
            if (serialNumber.Length > 0 && dateString.Length > 0)
            {
                var serialAndPath = createFoldersAndUpdateImageFromPath(file, destinationPath);
                imageRepositories.Add(new ImageRepositoryModel
                {
                    SerialNumber = serialAndPath.Item1,
                    Path = serialAndPath.Item2
                });
            }
        }
        public List<string> getFilesFromPath(string rootPath)
        {
            List<string> files = Directory.GetFiles(rootPath, "*.*", SearchOption.AllDirectories).ToList();
            return files;
        }

        private Tuple<string, string> createFoldersAndUpdateImageFromPath(string sourcePath, string newRootPath)
        {

            string imageFileName = Path.GetFileName(sourcePath);
            string serialNumber = getDateStringFromFileName(imageFileName);
            string date = getDateStringFromFileName(imageFileName);
            string year = date.Substring(0, 4);
            string month = date.Substring(4, 2);
            string day = date.Substring(6, 2);

            string newPath = Path.Combine(newRootPath, year, month, day);
            int counter = 0;
        Chek:

            if (!Directory.Exists(newPath))
            {
                Directory.CreateDirectory(newPath);

                //Sleep 3

                if (counter > 5)
                    return new Tuple<string, string>(null, null);

                counter++;

                goto Chek;
            }

            //copy image to new path
            string newImagePath = Path.Combine(newPath, imageFileName);
            //force to overwrite if exists
            File.Move(sourcePath, newImagePath);
            return new Tuple<string, string>(serialNumber, newImagePath);
        }
    }
}
