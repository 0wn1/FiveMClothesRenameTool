using System;
using System.IO;

namespace FiveMClothesRenameTool
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Welcome to FiveM Clothes Rename Tool!");

            string sourceFolderPath = string.Empty;
            while (string.IsNullOrEmpty(sourceFolderPath))
            {
                Console.Write("Please enter the path of the source folder: ");
                sourceFolderPath = Console.ReadLine();
            }

            string backupOption = string.Empty;
            bool backup = false;

            while (string.IsNullOrEmpty(backupOption))
            {
                Console.Write("Do you want to create a backup of the source folder? (Y/N): ");
                backupOption = Console.ReadLine().Trim().ToUpper();
                if (backupOption == "Y")
                {
                    backup = true;
                }
                else if (backupOption == "N")
                {
                    backup = false;
                }
                else
                {
                    backupOption = string.Empty;
                }
            }

            string backupFolderPath = string.Empty;
            if (backup)
            {
                backupFolderPath = sourceFolderPath + "_new";
                try
                {
                    Directory.CreateDirectory(backupFolderPath);
                    string[] extensions = { ".ydd", ".ytd" };
                    string[] filePaths = Directory.GetFiles(sourceFolderPath, "*.*", SearchOption.AllDirectories);
                    int count = 0;
                    foreach (string filePath in filePaths)
                    {
                        string extension = Path.GetExtension(filePath);
                        if (Array.IndexOf(extensions, extension) >= 0)
                        {
                            string subFolder = Path.GetDirectoryName(filePath).Replace(sourceFolderPath, "").TrimStart('\\');
                            string fileName = Path.GetFileName(filePath);
                            string newSubFolderPath = Path.Combine(backupFolderPath, subFolder);
                            Directory.CreateDirectory(newSubFolderPath);
                            string newFilePath = Path.Combine(newSubFolderPath, fileName);
                            File.Copy(filePath, newFilePath, true);
                            count++;
                        }
                    }
                    Console.WriteLine("Backup created successfully.");
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Failed to create backup: " + ex.Message);
                    Console.ReadLine();
                    return;
                }
                sourceFolderPath = backupFolderPath;
            }

            Console.Write("Please enter the prefix to be added to the file names (press enter to use subfolder name as prefix): ");
            string prefix = Console.ReadLine().Trim();
            if (string.IsNullOrEmpty(prefix))
            {
                prefix = "{0}^";
            }
            else if (!prefix.EndsWith("^"))
            {
                prefix += "^";
            }

            string[] extensionsToRename = { ".ydd", ".ytd" };
            string[] filePathsToRename = Directory.GetFiles(sourceFolderPath, "*.*", SearchOption.AllDirectories);
            int renamedFileCount = 0;
            foreach (string filePath in filePathsToRename)
            {
                string extension = Path.GetExtension(filePath);
                if (Array.IndexOf(extensionsToRename, extension) >= 0)
                {
                    string subFolder = Path.GetDirectoryName(filePath).Replace(sourceFolderPath, "").TrimStart('\\');
                    string fileName = string.Format(prefix, subFolder) + Path.GetFileName(filePath);
                    string newFilePath = Path.Combine(Path.GetDirectoryName(filePath), fileName);
                    try
                    {
                        File.Move(filePath, newFilePath);
                        Console.WriteLine("Renamed file: {0} -> {1}", filePath, newFilePath);
                        renamedFileCount++;
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine("Failed to rename file: {0} -> {1}. {2}", filePath, newFilePath, ex.Message);
                    }
                }
            }

            Console.WriteLine("Renamed {0} files.", renamedFileCount);
            Console.ReadLine();
        }
    }
}