using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace _1760336_Project1_BatchRename
{
    public class UniqueNameArgs : StringArgs, INotifyPropertyChanged
    {
        private int _counter;

        public int Counter { get; set; }

        private void NotifyChange(string v)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged.Invoke(this, new PropertyChangedEventArgs(v));
            }
        }

        public string Details => $"Set unique name";

        public event PropertyChangedEventHandler PropertyChanged;
    }

    public class UniqueNameAction : StringAction
    {

        public bool isChecked { get; set; }

        public string Name => "Unique Name action";

        public StringProcessor Processor => _uniqueName;

        private string _uniqueName(string origin)
        {
            var myArgs = Args as UniqueNameArgs;
            string result = Convert.ToString(myArgs.Counter) + System.IO.Path.GetExtension(origin);
            myArgs.Counter++;
            return result;

        }

        public StringArgs Args { get; set; }

        public StringAction Clone()
        {
            return new UniqueNameAction()
            {
                Args = new UniqueNameArgs()
            };
        }

        public void ShowEditDialog()
        {
            var screen = new UniqueName.UniqueNameArgsDialog(Args as UniqueNameArgs);

            if (screen.ShowDialog() == true)
            {
                var myArgs = Args as UniqueNameArgs;
                myArgs.Counter = screen.Counter;
            }
        }
    }
}
