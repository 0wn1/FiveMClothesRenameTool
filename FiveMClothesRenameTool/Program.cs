using System;
using System.IO;

namespace FiveMClothesRenameTool
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.Title = "FiveMClothesRenameTool v1.1";
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

            bool renameToSinglePlayer = false;

            while (true)
            {
                Console.Write("Do you want to rename all files to Single Player format? (Y/N): ");
                string userInput = Console.ReadLine().Trim().ToUpper();
                if (userInput == "Y")
                {
                    renameToSinglePlayer = true;
                    break;
                }
                else if (userInput == "N")
                {
                    renameToSinglePlayer = false;
                    break;
                }
                else
                {
                    Console.WriteLine("Invalid input. Please enter Y or N.");
                }
            }

            if (renameToSinglePlayer)
            {
                foreach (string filePath in Directory.GetFiles(sourceFolderPath, "*.*", SearchOption.AllDirectories))
                {
                    string fileName = Path.GetFileName(filePath);
                    string newFileName = fileName.Substring(fileName.LastIndexOf("^") + 1);

                    string newFilePath = Path.Combine(Path.GetDirectoryName(filePath), newFileName);

                    try
                    {
                        File.Move(filePath, newFilePath);
                        Console.WriteLine("Renamed file: {0} -> {1}", filePath, newFilePath);
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine("Failed to rename file: {0} -> {1}. {2}", filePath, newFilePath, ex.Message);
                    }
                }
                Console.WriteLine("Task completed. Please press Enter to exit.");
                Console.ReadLine();
                Environment.Exit(0);
            }

            Console.Write("Please enter the prefix to be added to the file names (press enter to use subfolder name as prefix): ");
            string prefix = Console.ReadLine().Trim();

            string childFolderName = new DirectoryInfo(sourceFolderPath).Name;
            string defaultPrefix = $"{childFolderName}^";

            if (!string.IsNullOrEmpty(prefix))
            {
                if (prefix.EndsWith("^"))
                {
                    prefix = prefix.Substring(0, prefix.Length - 1);
                }
                defaultPrefix = prefix;
            }

            string[] extensionsToRename = { ".ydd", ".ytd" };
            int renamedFileCount = 0;

            foreach (string filePath in Directory.GetFiles(sourceFolderPath, "*.*", SearchOption.AllDirectories))
            {
                string extension = Path.GetExtension(filePath);

                if (Array.IndexOf(extensionsToRename, extension) >= 0)
                {
                    string fileName = Path.GetFileName(filePath);
                    string subFolderName = new DirectoryInfo(Path.GetDirectoryName(filePath)).Name;

                    if (subFolderName.Contains("_new"))
                    {
                        subFolderName = subFolderName.Substring(0, subFolderName.IndexOf("_new"));
                    }
                    else
                    {
                        childFolderName += "^";
                    }

                    string filePrefix = subFolderName == childFolderName ? defaultPrefix : $"{subFolderName}^";

                    if (!string.IsNullOrEmpty(prefix))
                    {
                        if (subFolderName != childFolderName)
                        {
                            filePrefix = $"{prefix}^";
                        }
                        else
                        {
                            filePrefix = prefix;
                        }
                    }

                    fileName = $"{filePrefix}{fileName}";
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