using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Newtonsoft.Json;


namespace _1760336_Project1_BatchRename
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    /// 

    public partial class MainWindow : Window
    {

        List<StringAction> _protypes = null;



        public MainWindow()
        {
            InitializeComponent();

        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            //string origin = "unsplash.com Anatoly - New dawn - 1920x1080.jpg";
            //var action = new FullNameNormalizeAction()
            //{
            //    Args = new FullNameNormalizeArgs()
            //};
            //var result = action.Processor.Invoke(origin);

            //MessageBox.Show(result);

            _protypes = new List<StringAction>()
            {
                new ReplaceAction(),
                new NewCaseAction(),
                new FullNameNormalizeAction(),
                new MoveAction(),
                new UniqueNameAction()
            };

            ActionComboBox.ItemsSource = _protypes;

            foldersListView.ItemsSource = folderSource;

            filesListView.ItemsSource = fileSource;

            actionsListBox.ItemsSource = actionSource;
        }

        List<myFile> fileSource = new List<myFile>();

        public class myFile : IEquatable<myFile>
        {
            public string originalFileName { get; set; }
            public string newFileName { get; set; }
            public string filePath { get; set; }
            public string fileError { get; set; }
            public bool Equals(myFile other)
            {
                if (this.filePath == other.filePath)
                    return true;
                return false;
            }
        }

        List<StringAction> actionSource = new List<StringAction>();

        private void AddActionButton_Click(object sender, RoutedEventArgs e)
        {
            var selectedAction = ActionComboBox.SelectedItem as StringAction;
            var instance = selectedAction.Clone();
            instance.ShowEditDialog();
            instance.isChecked = true;
            actionSource.Add(instance);
            getPreviewFiles();
            getPreviewFolders();
            actionsListBox.Items.Refresh();
        }

        private void MenuItemSetting_Click(object sender, RoutedEventArgs e)
        {
            var action = actionsListBox.SelectedItem as StringAction;
            action.ShowEditDialog();
        }

        private void MenuItemMoveUp_Click(object sender, RoutedEventArgs e)
        {
            MoveItem(-1);
        }

        private void MenuItemMoveDown_Click(object sender, RoutedEventArgs e)
        {
            MoveItem(1);

        }

        private void MenuItemMoveTop_Click(object sender, RoutedEventArgs e)
        {
            StringAction selected = actionsListBox.SelectedItem as StringAction;
            //actionsListBox.Items.Remove(selected);
            //actionsListBox.Items.Insert(0, selected);
            //actionsListBox.SelectedIndex = 0;
            actionSource.Remove(selected);
            actionSource.Insert(0, selected);
            actionsListBox.SelectedIndex = 0;
            actionsListBox.Items.Refresh();
            getPreviewFiles();
            getPreviewFolders();
        }

        private void MenuItemMoveBottom_Click(object sender, RoutedEventArgs e)
        {
            StringAction selected = actionsListBox.SelectedItem as StringAction;
            //actionsListBox.Items.Remove(selected);
            //actionsListBox.Items.Add(selected);
            //actionsListBox.SelectedIndex = actionsListBox.Items.Count - 1;
            actionSource.Remove(selected);
            actionSource.Add(selected);
            actionsListBox.SelectedIndex = actionSource.Count - 1;
            actionsListBox.Items.Refresh();
            getPreviewFiles();
            getPreviewFolders();
        }

        public string getPreview(string origin)
        {
            string result = origin;
            foreach (StringAction action in actionSource)
            {
                if (action.isChecked == true)
                    result = action.Processor.Invoke(result);
            }
            return result;
        }

        public void getPreviewFiles()
        {
            foreach (myFile file in filesListView.Items)
            {
                file.newFileName = getPreview(System.IO.Path.GetFileName(file.filePath));
            }
            filesListView.Items.Refresh();
        }

        public void getPreviewFolders()
        {
            foreach (myFolder folder in foldersListView.Items)
            {
                folder.newFolderName = getPreview(System.IO.Path.GetFileName(folder.folderPath));
            }
            foldersListView.Items.Refresh();
        }

        

        public void MoveItem(int direction)
        {
            if (actionsListBox.SelectedItem == null || actionsListBox.SelectedIndex < 0) return;

            int newIndex = actionsListBox.SelectedIndex + direction;

            if (newIndex < 0 || newIndex >= actionSource.Count) return;

            StringAction selected = actionsListBox.SelectedItem as StringAction;
            actionSource.Remove(selected);
            actionSource.Insert(newIndex, selected);
            actionsListBox.SelectedIndex = newIndex;
            actionsListBox.Items.Refresh();
            getPreviewFiles();
            getPreviewFolders();
        }

        private void MenuItemRemove_Click(object sender, RoutedEventArgs e)
        {
            StringAction selected = actionsListBox.SelectedItem as StringAction;
            actionSource.Remove(selected);
            actionsListBox.Items.Refresh();
            getPreviewFiles();
            getPreviewFolders();
        }

        private void StartBatchButton_Click(object sender, RoutedEventArgs e)
        {
            foreach (myFile file in fileSource)
            {
                //string result = System.IO.Path.GetFileName(file.filePath);
                string result = file.newFileName;
                int countAction = 0;
                foreach (StringAction action in actionsListBox.Items)
                {
                    if (action.isChecked == true)
                    {
                        //result = action.Processor.Invoke(result);
                        countAction++;
                    }
                }

                if (countAction == 0)
                {
                    MessageBox.Show("Please have at least one activated action to continue.", "Error");
                    return;
                }

                try
                {
                    var newFile = new FileInfo(file.filePath);
                    var resultDir = System.IO.Path.GetDirectoryName(file.filePath) + System.IO.Path.DirectorySeparatorChar + result;
                    if (file.filePath.ToLowerInvariant() == result.ToLowerInvariant())
                    {
                        string tempName = resultDir + "tmp";
                        newFile.MoveTo(tempName);
                        var tempFile = new FileInfo(tempName);
                        tempFile.MoveTo(resultDir);
                    }
                    else newFile.MoveTo(resultDir);
                    file.newFileName = System.IO.Path.GetFileName(resultDir);
                    file.fileError = "Successfully renamed.";
                    filesListView.Items.Refresh();
                }
                catch (FileNotFoundException)
                {
                    file.fileError = "File not found. Check your files / actions' settings.";
                    filesListView.Items.Refresh();
                }
                catch (IOException)
                {
                    if (keepDupRadioButton.IsChecked == true)
                    {
                        file.fileError = "File duplicated in selected directory. Old name remains.";
                        refreshFolderListView();
                    }
                    if (numDupRadioButton.IsChecked == true)
                    {
                        int count = 1;
                        string newPath = "";
                        string[] subfiles = Directory.GetFiles(System.IO.Path.GetDirectoryName(file.filePath));
                        foreach (var subfile in subfiles)
                        {
                            if (System.IO.Path.GetFileName(subfile) == file.newFileName)
                            {
                                newPath = System.IO.Path.GetDirectoryName(file.filePath) + System.IO.Path.DirectorySeparatorChar + System.IO.Path.GetFileNameWithoutExtension(file.newFileName) + "_" + count + System.IO.Path.GetExtension(file.newFileName);
                                count++;
                            }
                        }
                        System.IO.Directory.Move(file.filePath, newPath);
                        file.newFileName = System.IO.Path.GetFileName(newPath);
                        file.filePath = "Successfully renamed.";
                        filesListView.Items.Refresh();
                    }
                }
                catch (UnauthorizedAccessException)
                {
                    file.fileError = "Access denied. Please run the program with Administrator priviledge.";
                    filesListView.Items.Refresh();
                }
            }
        }

        private void AddFileButton_Click(object sender, RoutedEventArgs e)
        {
            var screen = new System.Windows.Forms.FolderBrowserDialog();

            if (screen.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                foreach (var file in Directory.GetFiles(screen.SelectedPath))
                {
                    var file1 = new myFile()
                    {
                        originalFileName = System.IO.Path.GetFileName(file),
                        filePath = file,
                        newFileName = getPreview(System.IO.Path.GetFileName(file))
                    };
                    if (!fileSource.Contains(file1))
                    {
                        fileSource.Add(file1);
                        filesListView.Items.Refresh();
                    }
                    else MessageBox.Show("File already exists in list.", "Error");
                }
            }
        }

        private void UpFileButton_Click(object sender, RoutedEventArgs e)
        {
            MoveFileItem(-1);
        }

        private void TopFileButton_Click(object sender, RoutedEventArgs e)
        {
            myFile selected = filesListView.SelectedItem as myFile;
            fileSource.Remove(selected);
            fileSource.Insert(0, selected);
            filesListView.SelectedIndex = 0;
            filesListView.Items.Refresh();
        }

        private void DownFileButton_Click(object sender, RoutedEventArgs e)
        {
            MoveFileItem(1);
        }

        private void BottomFileButton_Click(object sender, RoutedEventArgs e)
        {
            myFile selected = filesListView.SelectedItem as myFile;
            fileSource.Remove(selected);
            fileSource.Add(selected);
            filesListView.SelectedIndex = filesListView.Items.Count - 1;
            filesListView.Items.Refresh();

        }

        private void DeleteFileButton_Click(object sender, RoutedEventArgs e)
        {
            myFile selected = filesListView.SelectedItem as myFile;
            fileSource.Remove(selected);
            filesListView.Items.Refresh();
        }

        public void MoveFileItem(int direction)
        {
            if (filesListView.SelectedItem == null || filesListView.SelectedIndex < 0) return;

            int newIndex = filesListView.SelectedIndex + direction;

            if (newIndex < 0 || newIndex >= fileSource.Count) return;

            myFile selected = filesListView.SelectedItem as myFile;
            fileSource.Remove(selected);
            fileSource.Insert(newIndex, selected);
            filesListView.SelectedIndex = newIndex;
            filesListView.Items.Refresh();
        }

        public class myFolder : IEquatable<myFolder>
        {
            public string originalFolderName { get; set; }
            public string newFolderName { get; set; }
            public string folderPath { get; set; }
            public string folderError { get; set; }
            public bool Equals(myFolder other)
            {
                if (this.folderPath == other.folderPath)
                    return true;
                return false;
            }
        }

        List<myFolder> folderSource = new List<myFolder>();

        private void refreshFolderListView()
        {
            ICollectionView view = CollectionViewSource.GetDefaultView(folderSource);
            view.Refresh();
        }

        private void AddFolderButton_Click(object sender, RoutedEventArgs e)
        {
            
            var screen = new System.Windows.Forms.FolderBrowserDialog();

            if (screen.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                var path = screen.SelectedPath;
                var folder1 = new myFolder()
                {
                    folderPath = path,
                    originalFolderName = System.IO.Path.GetFileName(path),
                    newFolderName = getPreview(System.IO.Path.GetFileName(path))
                };

                //if (!foldersListView.Items.Contains(folder1))
                //    foldersListView.Items.Add(folder1);

                //else MessageBox.Show("Folder already exists in list.", "Error");
                if (!folderSource.Contains(folder1))
                {
                    folderSource.Add(folder1);
                    refreshFolderListView();
                }
                else MessageBox.Show("Folder already exists in list.", "Error");
            }

        }

        private void UpFolderButton_Click(object sender, RoutedEventArgs e)
        {
            MoveFolderItem(-1);
            refreshFolderListView();

        }

        private void TopFolderButton_Click(object sender, RoutedEventArgs e)
        {
            myFolder selected = foldersListView.SelectedItem as myFolder;
            folderSource.Remove(selected);
            //foldersListView.Items.Insert(0, selected);
            //foldersListView.SelectedIndex = 0;
            folderSource.Insert(0, selected);
            foldersListView.SelectedIndex = 0;
            refreshFolderListView();

        }

        private void DownFolderButton_Click(object sender, RoutedEventArgs e)
        {
            MoveFolderItem(1);
            refreshFolderListView();

        }

        private void BottomFolderButton_Click(object sender, RoutedEventArgs e)
        {
            myFolder selected = foldersListView.SelectedItem as myFolder;
            folderSource.Remove(selected);
            folderSource.Add(selected);
            foldersListView.SelectedIndex = folderSource.Count - 1;
            refreshFolderListView();

        }

        private void DeleteFolderButton_Click(object sender, RoutedEventArgs e)
        {
            myFolder selected = foldersListView.SelectedItem as myFolder;
            folderSource.Remove(selected);
            refreshFolderListView();
        }

        public void MoveFolderItem(int direction)
        {
            if (foldersListView.SelectedItem == null || foldersListView.SelectedIndex < 0) return;

            int newIndex = foldersListView.SelectedIndex + direction;

            if (newIndex < 0 || newIndex >= foldersListView.Items.Count) return;

            myFolder selected = foldersListView.SelectedItem as myFolder;
            //foldersListView.Items.Remove(selected);
            //foldersListView.Items.Insert(newIndex, selected);
            folderSource.Remove(selected);
            folderSource.Insert(newIndex, selected);
            foldersListView.SelectedIndex = newIndex;
        }

        private void StartBatchButton1_Click(object sender, RoutedEventArgs e)
        {
            
            foreach (myFolder folder in folderSource)
            {
                //string result = System.IO.Path.GetFileName(folder.folderPath);
                int countAction = 0;
                foreach (StringAction action in actionsListBox.Items)
                {
                    //result = action.Processor.Invoke(result);
                    countAction++;
                }
                if (countAction == 0)
                {
                    MessageBox.Show("Please have at least one activated action to continue.", "Error");
                    return;
                }

                try
                {
                    string sourcePath = folder.folderPath;
                    string newPath = System.IO.Path.GetDirectoryName(sourcePath) + System.IO.Path.DirectorySeparatorChar + folder.newFolderName;
                    if (sourcePath.ToLowerInvariant() == newPath.ToLowerInvariant())
                    {
                        string tempName = newPath + "temp";
                        System.IO.File.Move(sourcePath, tempName);
                        System.IO.File.Move(tempName, newPath);
                    }
                    else System.IO.Directory.Move(sourcePath, newPath);
                    folder.newFolderName = System.IO.Path.GetFileName(newPath);
                    folder.folderError = "Successfully renamed.";
                    refreshFolderListView();

                }
                catch (FileNotFoundException)
                {
                    folder.folderError = "File not found. Check your files / actions' settings.";
                    refreshFolderListView();


                }
                catch (IOException)
                {
                    if (keepDupRadioButton.IsChecked == true)
                    {
                        folder.folderError = "Folder duplicated in selected directory. Old name remains.";
                        refreshFolderListView();
                    }
                    if (numDupRadioButton.IsChecked == true)
                    {
                        int count = 1;
                        string newPath = "";
                        string[] subfolders = Directory.GetDirectories(System.IO.Path.GetDirectoryName(folder.folderPath));
                        foreach (var subfolder in subfolders)
                        {
                            if (System.IO.Path.GetFileName(subfolder) == folder.newFolderName)
                            {
                                newPath = System.IO.Path.GetDirectoryName(folder.folderPath) + System.IO.Path.DirectorySeparatorChar + folder.newFolderName + "_" + count;
                                count++;
                            }
                        }
                        System.IO.Directory.Move(folder.folderPath, newPath);
                        folder.newFolderName = System.IO.Path.GetFileName(newPath);
                        folder.folderError = "Successfully renamed.";
                        refreshFolderListView();
                    }
                }
                catch (UnauthorizedAccessException)
                {
                    folder.folderError = "Access denied.Please run the program with Administrator priviledge.";
                    refreshFolderListView();


                }
            }
        }

        private void IsActivate_Click(object sender, RoutedEventArgs e)
        {
            getPreviewFiles();
            getPreviewFolders();
        }

        private void OpenPreset_Click(object sender, RoutedEventArgs e)
        {
            throw new NotImplementedException();
        }

        private void SavePreset_Click(object sender, RoutedEventArgs e)
        {
            var screen = new SaveFileDialog();

            if (screen.ShowDialog() == true)
            {
                var fpath = System.IO.Path.GetFullPath(screen.FileName);
                Presets myPreset = new Presets()
                {
                    Name = screen.FileName,
                    actions = new List<StringAction>(actionSource)
                };

                string json = JsonConvert.SerializeObject(myPreset, Formatting.Indented);
                System.IO.File.WriteAllText(fpath, json);
            }

        }
    }
}
