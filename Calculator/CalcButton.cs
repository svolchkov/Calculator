// Author:      Sergey Volchkov
// Created on:  2016-12-26
// Version:     1.0

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Calculator
{
    public partial class CalcButton : Button
    {
        
       public Keys key { get; set; }
        
        public CalcButton(Keys k = default(Keys))
        {
            InitializeComponent();
            this.BackColor = Color.Black;
            this.ForeColor = Color.White;
            this.Dock = DockStyle.Fill;
            this.Margin = new Padding(1);
            this.key = k;
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.CalcButton_KeyDown);
            this.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.CalcButton_KeyPress);
        }

        private void CalcButton_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Return)
            {
                e.Handled = true;
                e.SuppressKeyPress = true;
            }
        }
        private void CalcButton_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                e.Handled = true;
                //e.SuppressKeyPress = true;
            }
        }

    }
}
