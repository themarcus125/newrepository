﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _1760336_Project1_BatchRename
{
    public delegate string StringProcessor(string origin);

    public interface StringArgs
    {
        string Details {get; }
    }

    public interface StringAction
    {
        bool isChecked { get; set; }
        string Name {get;}
        StringProcessor Processor {get;}
        StringArgs Args {get; set;}
        StringAction Clone();
        void ShowEditDialog();
    }
}
