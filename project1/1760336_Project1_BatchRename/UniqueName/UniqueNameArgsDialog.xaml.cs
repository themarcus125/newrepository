using System;
using System.Collections.Generic;
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
using System.Windows.Shapes;

namespace _1760336_Project1_BatchRename.UniqueName
{
    /// <summary>
    /// Interaction logic for UniqueNameArgsDialog.xaml
    /// </summary>
    public partial class UniqueNameArgsDialog : Window
    {
        public int Counter; 

        public UniqueNameArgsDialog(UniqueNameArgs args)
        {
            InitializeComponent();
            indexTextBox.Text = Convert.ToString(0);
        }
        

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            Counter = Convert.ToInt32(indexTextBox.Text);
            this.DialogResult = true;
        }

        private void StartIndexTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (System.Text.RegularExpressions.Regex.IsMatch(indexTextBox.Text, "[^0-9]"))
            {
                MessageBox.Show("Please enter only numbers.");
                indexTextBox.Text = indexTextBox.Text.Remove(indexTextBox.Text.Length - 1);
            }
        }
    }
}
