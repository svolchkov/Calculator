// Author:      Sergey Volchkov
// Created on:  2016-12-26
// Version:     1.0

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Calculator
{
    class FuncButton : CalcButton
    {
        public Delegate func;

        public FuncButton (Delegate d) : base()
        {
            this.func = d;
        }
    }
}
