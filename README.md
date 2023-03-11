## FiveM Clothes Rename Tool 

### Preview
Streamable (v1.1 latest): [View](https://streamable.com/wy2arh "View")
Streamable (v1 old): [View](https://streamable.com/aoykcv "View")

------------
### Changelogs v1.1
- Implemented an option for the user to rename FiveM clothing files for GTA V Single-Player by removing all characters before the `^` symbol.
- Added an icon to the executable.
- Added `Console.Title()` to the application.
- Made a few other minor fixes.

------------
### About
This is a command-line tool written in C# that allows users to rename GTA V clothes files to FiveM clothing files.

------------
### How it Works
The tool prompts the user to enter the path to the source folder containing the clothes files to be renamed. The user is then asked if they want to create a backup of the source folder. If the user chooses to create a backup, a new folder is created with the suffix "_new" appended to the original folder name. All the files in the source folder with the extensions ".ydd" and ".ytd" are copied to the backup folder.

After creating the backup (if applicable), the user is prompted to enter a prefix to be added to the file names. If the user enters nothing and just presses enter, the subfolder name of each file will be used as the prefix. If the prefix entered by the user does not end with a caret "^" character, the tool will append it automatically.

Finally, the tool searches for all the files with the extensions ".ydd" and ".ytd" in the source folder and its subfolders, and renames them according to the prefix entered by the user. The new file name is constructed by concatenating the prefix and the original file name. If a file with the new name already exists, the tool will overwrite it.

------------
### Prerequisites
.NET v4.7.2