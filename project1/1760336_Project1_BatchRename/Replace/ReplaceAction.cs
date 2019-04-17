using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _1760336_Project1_BatchRename
{
    public class ReplaceArgs: StringArgs, INotifyPropertyChanged
    {
        private string _finder;
        private string _replacer;

        public string Finder {
            get => _finder;
            set 
            {
                _finder = value;
                NotifyChange("Finder");
                NotifyChange("Details");
            }
        }

        public string Replacer{
            get => _replacer;
            set
            {
                _replacer = value;
                NotifyChange("Replacer");
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

        public string Details => $"Replace {Finder} with {Replacer}";

        public event PropertyChangedEventHandler PropertyChanged;
    }

    public class ReplaceAction : StringAction
    {
        public bool isChecked { get; set; }

        public string Name => "Replace action";

        public StringProcessor Processor => _replace;

        private string _replace(string origin)
        {
            var myArgs = Args as ReplaceArgs;
            var finder = myArgs.Finder;
            var replacer = myArgs.Replacer;

            string result = origin.Replace(finder, replacer);

            return result;
        }

        public StringArgs Args {get; set; }

        public StringAction Clone()
        {
            return new ReplaceAction()
            {
                Args = new ReplaceArgs()
            };
        }

        public void ShowEditDialog()
        {
            var screen = new Replace.ReplaceArgsDialog(Args as ReplaceArgs);

            if (screen.ShowDialog() == true)
            {
                var myArgs = Args as ReplaceArgs;
                myArgs.Finder = screen.Finder;
                myArgs.Replacer = screen.Replacer;
            }
        }
    
    }
}
