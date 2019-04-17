using System;
using System.Collections.Generic;
using System.Linq;
using System.ComponentModel;
using System.Text;
using System.Threading.Tasks;
using System.Globalization;
using System.Threading;

namespace _1760336_Project1_BatchRename
{
    public class FullNameNormalizeArgs : StringArgs, INotifyPropertyChanged
    {
        private void NotifyChange(string v)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged.Invoke(this, new PropertyChangedEventArgs(v));
            }
        }

        public string Details => $"Fullname normalize";

        public event PropertyChangedEventHandler PropertyChanged;
    }

    

    public class FullNameNormalizeAction : StringAction
    {
        public bool isChecked { get; set; }

        public string Name => "Fullname Normalize Action";

        public StringProcessor Processor => _FullNameNormalize;

        private string _FullNameNormalize(string origin)
        {
            string result = origin.ToLower();
            string[] tokens = result.Split(' ');
            if (tokens != null)
            {
                result = SentenceCase(tokens[0]);
                result += " ";
                int n = tokens.Length;
                if (n > 0)
                {
                    for (int i = 1; i < n; i++)
                    {
                        if (tokens[i] != "")
                        {
                            result += SentenceCase(tokens[i]);
                            result += " ";
                        }
                    }
                }
            }
            return result;
        }
        private string SentenceCase(string v)
        {
            if (v.Length < 1)
                return v;

            string sentence = v.ToLower();
            return sentence[0].ToString().ToUpper() +
               sentence.Substring(1);
        }

        public StringArgs Args { get; set; }

        public StringAction Clone()
        {
            return new FullNameNormalizeAction()
            {
                Args = new FullNameNormalizeArgs()
            };
        }

        public void ShowEditDialog()
        {
            var screen = new FullNameNormalize.FullNameNormalizeArgsDialog(Args as FullNameNormalizeArgs);
            
            if (screen.ShowDialog() == true)
            {
                var myArgs = Args as FullNameNormalizeArgs;
            }
        }
    }
}
