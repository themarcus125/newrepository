using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _1760336_Project1_BatchRename
{
    public class NewCaseArgs : StringArgs, INotifyPropertyChanged
    {
        private int _template;

        public int Template
        {
            get => _template;
            set
            {
                _template = value;
                NotifyChange("Template");
                NotifyChange("Details");
            }
        }

        private void NotifyChange(string v)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged.Invoke(this, new PropertyChangedEventArgs(v));
            }
        }

        public string Details => $"Set New Case for file with {Template} template";

        public event PropertyChangedEventHandler PropertyChanged;
    }

    public class NewCaseAction : StringAction
    {
        public bool isChecked { get; set; }

        public string Name => "New case action";

        public StringProcessor Processor => _newcase;

        private string _newcase(string origin)
        {
            var myArgs = Args as NewCaseArgs;
            var template = myArgs.Template;
            string result = " ";
            if (template == 1)
            {
                result = origin.ToUpper();
            }
            if (template == 2)
            {
                result = origin.ToLower();
            }
            if (template == 3)
            {
                result = origin.ToLower();
                char[] a = result.ToCharArray();
                a[0] = char.ToUpper(a[0]);
                result = new string(a);
            }
            if (template == 0)
            {
                result = origin;
            }
            return result;
        }

        public StringArgs Args { get ; set ; }

        public StringAction Clone()
        {
            return new NewCaseAction()
            {
                Args = new NewCaseArgs()
            };
        }

        public void ShowEditDialog()
        {
            var screen = new NewCase.NewCaseArgsDialog(Args as NewCaseArgs);

            if (screen.ShowDialog() == true)
            {
                var myArgs = Args as NewCaseArgs;
                myArgs.Template = screen.Template;
            }
        }
    }
}
