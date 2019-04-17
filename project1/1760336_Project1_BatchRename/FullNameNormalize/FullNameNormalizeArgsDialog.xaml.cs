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

namespace _1760336_Project1_BatchRename.FullNameNormalize
{
    /// <summary>
    /// Interaction logic for FullNameNormalizeArgsDialog.xaml
    /// </summary>
    public partial class FullNameNormalizeArgsDialog : Window
    {
        public FullNameNormalizeArgsDialog(FullNameNormalizeArgs fullNameNormalizeArgs)
        {
            InitializeComponent();
        }

        private void OkButton_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = true;
        }
    }
}
