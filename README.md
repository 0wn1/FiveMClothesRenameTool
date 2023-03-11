## FiveM Clothes Rename Tool

### Update v1.1
This update now allows the user to rename FiveM clothing files back to GTA V single-player.

------------
### Changelogs v1.1
- Added icon to the executable.
- Added Console.Title() to the application.
- Implemented an option for the user to rename FiveM clothing files for GTA V Single-Player by removing all characters before the ^ symbol.
- Made a few other minor fixes.

------------
This is a command-line tool written in C# that allows users to rename GTA V clothes files to FiveM clothing files.

------------
### Prerequisites
.NET v4.7.2

------------
### How it Works
The tool prompts the user to enter the path to the source folder containing the clothes files to be renamed. The user is then asked if they want to create a backup of the source folder. If the user chooses to create a backup, a new folder is created with the suffix "_new" appended to the original folder name. All the files in the source folder with the extensions ".ydd" and ".ytd" are copied to the backup folder.

After creating the backup (if applicable), the user is prompted to enter a prefix to be added to the file names. If the user enters nothing and just presses enter, the subfolder name of each file will be used as the prefix. If the prefix entered by the user does not end with a caret "^" character, the tool will append it automatically.

Finally, the tool searches for all the files with the extensions ".ydd" and ".ytd" in the source folder and its subfolders, and renames them according to the prefix entered by the user. The new file name is constructed by concatenating the prefix and the original file name. If a file with the new name already exists, the tool will overwrite it.

------------
### Usage
To use the tool, simply compile the code into an executable file and run it from the command line. Make sure to pass any necessary arguments, such as the path to the source folder.

Note that this tool was designed specifically for use with FiveM clothes files and may not work properly with other types of files. Also, be sure to use it responsibly and make backups of your files before using it.

------------
### Preview
Streamable: [View](https://streamable.com/aoykcv "View")