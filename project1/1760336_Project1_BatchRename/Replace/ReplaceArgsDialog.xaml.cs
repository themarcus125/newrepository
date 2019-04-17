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

namespace _1760336_Project1_BatchRename.Replace
{
    /// <summary>
    /// Interaction logic for ReplaceArgsDialog.xaml
    /// </summary>
    public partial class ReplaceArgsDialog : Window
    {
        public string Finder;
        public string Replacer;

        public ReplaceArgsDialog(ReplaceArgs args)
        {
            InitializeComponent();
            finderTextBox.Text = args.Finder;
            replacerTextBox.Text = args.Replacer;
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            Finder = finderTextBox.Text;
            Replacer = replacerTextBox.Text;

            this.DialogResult = true;
        }
    }
}
