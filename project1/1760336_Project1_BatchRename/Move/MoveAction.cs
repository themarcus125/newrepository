
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _1760336_Project1_BatchRename
{
    public class MoveArgs : StringArgs, INotifyPropertyChanged
    {
        private int _startIndex;
        private int _length;
        private int _insertIndex;

        public int StartIndex
        {
            get => _startIndex;
            set
            {
                _startIndex = value;
                NotifyChange("StartIndex");
                NotifyChange("Details");
            }
        }

        public int Length
        {
            get => _length;
            set
            {
                _length = value;
                NotifyChange("Length");
                NotifyChange("Details");
            }
        }

        public int InsertIndex
        {
            get => _insertIndex;
            set
            {
                _insertIndex = value;
                NotifyChange("InsertIndex");
                NotifyChange("Details");
            }
        }

        public string Details => $"Move substring ";

        private void NotifyChange(string v)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged.Invoke(this, new PropertyChangedEventArgs(v));
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
    }

    public class MoveAction : StringAction
    {
        public bool isChecked { get; set; }

        public string Name => "Move action";

        public StringProcessor Processor => _move;

        private string _move(string origin)
        {
            var myArgs = Args as MoveArgs;
            var startIndex = myArgs.StartIndex;
            var length = myArgs.Length;
            var insertIndex = myArgs.InsertIndex;
            var result = origin;
            var resultStringBuilder = new StringBuilder(result);
            string token = resultStringBuilder.ToString(startIndex, length);
            resultStringBuilder.Remove(startIndex, length);
            resultStringBuilder.Insert(insertIndex, token);
            result = resultStringBuilder.ToString();
            return result;
        }

        public StringArgs Args { get; set; }

        public StringAction Clone()
        {
            return new MoveAction()
            {
                Args = new MoveArgs()
            };
        }

        public void ShowEditDialog()
        {
            var screen = new Move.MoveArgsDialog(Args as MoveArgs);

            if (screen.ShowDialog() == true)
            {
                var myArgs = Args as MoveArgs;
                myArgs.StartIndex = screen.StartIndex;
                myArgs.Length = screen.Length;
                myArgs.InsertIndex = screen.InsertIndex;
            }
        
        
        }
    }
}
