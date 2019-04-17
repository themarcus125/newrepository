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

namespace _1760336_Project1_BatchRename.Move
{
    /// <summary>
    /// Interaction logic for MoveArgsDialog.xaml
    /// </summary>
    public partial class MoveArgsDialog : Window
    {
        public int StartIndex;
        public int Length;
        public int InsertIndex;

        public MoveArgsDialog(MoveArgs args)
        {
            InitializeComponent();
            startIndexTextBox.Text = Convert.ToString(args.StartIndex);
            lengthTextBox.Text = Convert.ToString(args.Length);
            insertIndexTextBox.Text = Convert.ToString(args.InsertIndex);

        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            StartIndex = Convert.ToInt32(startIndexTextBox.Text);
            Length = Convert.ToInt32(lengthTextBox.Text);
            InsertIndex = Convert.ToInt32(insertIndexTextBox.Text);

            this.DialogResult = true;
        }

        private void StartIndexTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (System.Text.RegularExpressions.Regex.IsMatch(startIndexTextBox.Text, "[^0-9]"))
            {
                MessageBox.Show("Please enter only numbers.");
                startIndexTextBox.Text = startIndexTextBox.Text.Remove(startIndexTextBox.Text.Length - 1);
            }
        }

        private void LengthTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (System.Text.RegularExpressions.Regex.IsMatch(lengthTextBox.Text, "[^0-9]"))
            {
                MessageBox.Show("Please enter only numbers.");
                lengthTextBox.Text = startIndexTextBox.Text.Remove(lengthTextBox.Text.Length - 1);
            }
        }

        private void InsertIndexTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (System.Text.RegularExpressions.Regex.IsMatch(insertIndexTextBox.Text, "[^0-9]"))
            {
                MessageBox.Show("Please enter only numbers.");
                insertIndexTextBox.Text = insertIndexTextBox.Text.Remove(insertIndexTextBox.Text.Length - 1);
            }
        }

       
    }
}
