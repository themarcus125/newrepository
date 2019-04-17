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

namespace _1760336_Project1_BatchRename.NewCase
{
    /// <summary>
    /// Interaction logic for NewCaseArgsDialog.xaml
    /// </summary>
    public partial class NewCaseArgsDialog : Window
    {
        public int Template;

        public NewCaseArgsDialog(NewCaseArgs args)
        {
            InitializeComponent();
            if (args.Template == 1)
            {
                template1RadioButton.IsChecked = true;
            }
            else if (args.Template == 2)
            {
                template3RadioButton.IsChecked = true;
            }
            else if (args.Template == 3)
            {
                template3RadioButton.IsChecked = true;
            }
            else
            {
                template0RadioButton.IsChecked = true;
            }
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            if (template1RadioButton.IsChecked == true)
            {
                Template = 1;
            }
            else if (template2RadioButton.IsChecked == true)
            {
                Template = 2;
            }
            else if (template3RadioButton.IsChecked == true)
            {
                Template = 3;
            }
            else if (template0RadioButton.IsChecked == true)
            {
                Template = 0;
            }
            this.DialogResult = true;
        }
    }
}
