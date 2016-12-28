// Author:      Sergey Volchkov
// Created on:  2016-12-26
// Version:     1.0

using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using System.Reflection;
using MyMathLib;

namespace Calculator
{
    public partial class MainForm : Form
    {
        // code to create rounded corners of the form
        [DllImport("Gdi32.dll", EntryPoint = "CreateRoundRectRgn")]
        private static extern IntPtr CreateRoundRectRgn
        (
        int nLeftRect, // x-coordinate of upper-left corner
        int nTopRect, // y-coordinate of upper-left corner
        int nRightRect, // x-coordinate of lower-right corner
        int nBottomRect, // y-coordinate of lower-right corner
        int nWidthEllipse, // height of ellipse
        int nHeightEllipse // width of ellipse
        );

        // code to allow moving the form on the screen
        public const int WM_NCLBUTTONDOWN = 0xA1;
        public const int HT_CAPTION = 0x2;

        [DllImportAttribute("user32.dll")]
        public static extern int SendMessage(IntPtr hWnd, int Msg, int wParam, int lParam);
        [DllImportAttribute("user32.dll")]
        public static extern bool ReleaseCapture();


        Dictionary<string, Keys> keyDict = new Dictionary<string, Keys>(); // links key names to keys
        Dictionary<Keys, CalcButton> keysToButtons = new Dictionary<Keys, CalcButton>(); // links keys to buttons
        string tempString; // temporary storage of the value entered by user as string
        double previousValue; // previous value entered by user as double
        double currentValue; // current value entered by user as double
        Func<double, double, double> currentFunc = null; // arithmetical operation being currently performed
        Dictionary<String, Func<double, double, double>> arOps = new Dictionary<string, Func<double, double, double>>()
        {
          {"+", Arithmetic.Add },
          {"-", Arithmetic.Subtract },
          {"*", Arithmetic.Multiply },
          {"/", Arithmetic.Divide }
        };//links button text to the corresponding arithmetical operation
        Dictionary<String, Func<double, double>> advOps = new Dictionary<string, Func<double, double>>()
        {
          {"x\xB2", Arithmetic.Square},
          {"sqrt", Math.Sqrt },
          {"crt", Arithmetic.CubicRoot },
          {"1/x", Arithmetic.Inverse },
          {"sin", Arithmetic.Sin },
          {"cos", Arithmetic.Cos },
          {"tan", Arithmetic.Tan }
        };//links button text to the corresponding advanced operation
        List<string> arOpsList = new List<string> { "+", "-", "*", "/" }; //list of arithmetical buttons
        //buttons are to be placed on the calculator in this order
        List<string> advOpList = new List<string> { "x\xB2", "sqrt","crt","1/x","sin","cos","tan" };
        // list of advanced operation buttons
        bool newCalcStarted; // has the user started a new calculation?

        public MainForm()
        {
            InitializeComponent();

            //   .select keys from Enum.Keys that will be added to the calculator
            //    this is to allow the calculator to be operated from the NumPad
            foreach (Keys k in Enum.GetValues(typeof(Keys)))
            {
               // Console.WriteLine(k.ToString());
                if (k.ToString().Contains("NumPad") 
                    || arOps.Values.Select(t => t.Method.Name).Contains(k.ToString())
                    || k.ToString() == "Decimal"
                    || k.ToString() == "Return"
                    || k.ToString() == "Back"
                    || k.ToString() == "Delete")
                    keyDict[k.ToString()] = k;
            }
            newCalcStarted = true;
            previousValue = 0;
            List<CalcButton>calcBtns = new List<CalcButton>();

            // create rounded corners of the calculator window
            Region = System.Drawing.Region.FromHrgn(CreateRoundRectRgn(0, 0, Width, Height, 20, 20));

            // populate the numeric key panel with buttons
            int counter = 1;
            for (int row = 0; row < tlpNumericKeys.RowCount; row++)
            {
                for (int column = 0; column < tlpNumericKeys.ColumnCount; column++)
                {
                    string keyVal = (counter % 10).ToString();
                    string keyName = "NumPad" + keyVal;
                    Keys btnKey = keyDict[keyName];
                    CalcButton btn = new CalcButton(btnKey);
                    keysToButtons[btnKey] = btn;
                    tlpNumericKeys.Controls.Add(btn, column, row);
                    btn.Text = keyVal;
                    calcBtns.Add(btn);
                    counter++;
                    if (counter > 10)
                        break;
                }
            }
            
            // add callback functions to numeric keys
            foreach (CalcButton c in calcBtns)
            {
                c.Click += (s, e) =>
                {
                    if (newCalcStarted)
                    {
                        lbCalcHistory.Text = "";
                        tbOutput.Text = c.Text;
                        newCalcStarted = false;
                    }
                    else
                    {
                        if (tbOutput.Text == "0")
                        {
                            tbOutput.Text = c.Text;
                        }
                        else
                        {
                            tempString = tbOutput.Text + c.Text;
                            if (Double.TryParse(tempString, out currentValue))
                            {
                                //tbOutput.Text = currentValue.ToString();
                                tbOutput.Text = tempString;
                            }
                        }

                    }

                };
            }

            // add decimal point button and create callback
            CalcButton dotBtn = new CalcButton(keyDict["Decimal"]);
            keysToButtons[keyDict["Decimal"]] = dotBtn;
            tlpNumericKeys.Controls.Add(dotBtn, 1, 3);
            dotBtn.Text = ".";
            //calcBtns.Add(dotBtn);
            dotBtn.Click += (s, e) =>
            {
                if (newCalcStarted)
                {
                    tbOutput.Text = "0.";
                    lbCalcHistory.Text = "";
                    newCalcStarted = false;
                }
                else if (!tbOutput.Text.Contains("."))
                {
                    tbOutput.Text += ".";
                }
                
            };

            // add backspace button and create callback
            CalcButton erase = new CalcButton(keyDict["Back"]);
            keysToButtons[keyDict["Back"]] = erase;
            tlpNumericKeys.Controls.Add(erase, 2, 3);
            erase.Text = "\u2190";
            //calcBtns.Add(dotBtn);
            erase.Click += (s, e) =>
            {
                if (Double.TryParse(tbOutput.Text, out currentValue) 
                    && (Double.IsInfinity(currentValue) || Double.IsNaN(currentValue)))
                {
                    lbCalcHistory.Text = "";
                    tbOutput.Text = "0";
                }
                else
                {
                    if (tbOutput.Text.Length == 1)
                    {
                        tbOutput.Text = "0";
                    }
                    else
                    {
                        tbOutput.Text = tbOutput.Text.Substring(0, tbOutput.Text.Length - 1);
                        if (newCalcStarted)
                        {
                            lbCalcHistory.Text = "";
                            newCalcStarted = false;
                        }
                    }
                         
                }

            };

            AddArithmeticFunctions();
            AddAdvancedFunctions();

            // add event handler to each button to handle key presses
            foreach (Control c in this.Controls)
            {
                c.KeyDown += new KeyEventHandler(keypressed);
                foreach(Control cc in c.Controls)
                {
                    cc.KeyDown += new KeyEventHandler(keypressed);
                }
            }
        }

        // Each key press is propagated to the form level and suppressed at the control level
        private void keypressed(Object o, KeyEventArgs e)
        {
            e.Handled = true;
            Form1_KeyDown(o, e);
            e.SuppressKeyPress = true;
        }

        // The form is borderless so no standard red cross in the top right corner. User can exit
        // by clicking on the exit label
        private void lbExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        // Add arithmetical functions, erase all and equals buttons and corresponding callbacks
        private void AddArithmeticFunctions()
        {
            CalcButton eraseAll = new CalcButton();
            eraseAll.key = keyDict["Delete"];
            keysToButtons[keyDict["Delete"]] = eraseAll;
            tlpArithmetics.Controls.Add(eraseAll, 0, 0);
            eraseAll.Text = "CE";
            eraseAll.Click += (sender, e) =>
            {
                currentFunc = null;
                previousValue = 0;
                currentValue = 0;
                lbCalcHistory.Text = "";
                newCalcStarted = true;
                tbOutput.Text = "0";
            };

            int rowIdx = 1;
            foreach (string s in arOpsList)
            {
                FuncButton btn = new FuncButton(arOps[s]);
                if (keyDict.Keys.Contains(btn.func.Method.Name))
                {
                    Keys k = keyDict[btn.func.Method.Name];
                    keysToButtons[k] = btn; 
                }
                tlpArithmetics.Controls.Add(btn, 0, rowIdx);
                btn.Text = s;
                rowIdx++;
                // The minus button is to be handled differently from the others to allow entry
                // of negative numbers
                if (s == "-")
                {
                    btn.Click += (sender, e) =>
                    {
                        // user entering negative number
                        if (tbOutput.Text == "0" || newCalcStarted)
                        {
                            if (newCalcStarted) lbCalcHistory.Text = "";
                            tbOutput.Text = "-";
                            newCalcStarted = false;
                        }
                        else if (Double.TryParse(tbOutput.Text, out currentValue))
                        {
                            //if (!(Double.IsNaN(currentValue))) performOp(s, btn);
                            setCurrentFunc(s, btn);
                        }

                    };
                }
                else
                {
                    btn.Click += (sender, e) =>
                    {
                        if (Double.TryParse(tbOutput.Text, out currentValue))
                        {
                            //if (!(Double.IsNaN(currentValue))) performOp(s, btn);
                            setCurrentFunc(s, btn);
                        }
                    };
                }
            }
            CalcButton equals = new CalcButton();
            tlpArithmetics.Controls.Add(equals, 0, rowIdx);
            equals.key = keyDict["Return"];
            keysToButtons[keyDict["Return"]] = equals;
            equals.Text = "=";
            equals.Click += (sender, e) =>
            {
                if (currentFunc != null)
                {
                    if (Double.TryParse(tbOutput.Text, out currentValue))
                    {
                        try
                        {
                            double result = (double)currentFunc.DynamicInvoke(previousValue, currentValue);
                            lbCalcHistory.Text += (" " + tbOutput.Text);
                            tbOutput.Text = result.ToString();
                            previousValue = result;
                            currentFunc = null;
                        }
                        catch (TargetInvocationException ex)
                        {
                            if (ex.InnerException is DivideByZeroException)
                            {
                                lbCalcHistory.Text += (" " + tbOutput.Text);
                                tbOutput.Text = "INF";
                                previousValue = 0;
                                currentFunc = null;
                            }

                        }
                        newCalcStarted = true;
                    }
                }
            };
        }

        // Add advanced functions and corresponding callbacks
        private void AddAdvancedFunctions()
        {

            int rowIdx = 0;
            foreach (string s in advOpList)
            {
                FuncButton btn = new FuncButton(advOps[s]);
                tlpAdvanced.Controls.Add(btn, 0, rowIdx);
                btn.Text = s;
                rowIdx++;
                btn.Click += (sender, e) =>
                {
                    if (Double.TryParse(tbOutput.Text, out currentValue))
                    {
                        if (currentFunc != null)
                        {
                            try
                            {
                                currentValue = (double)currentFunc.DynamicInvoke(previousValue, currentValue);

                            }
                            catch (TargetInvocationException ex)
                            {
                                if (ex.InnerException is DivideByZeroException)
                                {
                                    lbCalcHistory.Text += (" " + tbOutput.Text);
                                    tbOutput.Text = "INF";
                                    previousValue = 0;
                                    currentFunc = null;
                                    return;
                                }
                            }
                        }
                        if (btn.Text == "1/x")
                        {
                            lbCalcHistory.Text = ("1/" + currentValue);
                        } 
                        // square
                        else if (btn.Text == "x\xB2")
                        {
                            lbCalcHistory.Text = String.Format("{0} * {0}", currentValue.ToString());
                        }
                        else
                        {
                            lbCalcHistory.Text = String.Format("{0}({1})", btn.Text, currentValue);
                        }
                            
                        try
                        {
                            currentValue = (double)btn.func.DynamicInvoke(currentValue);
                            tbOutput.Text = currentValue.ToString();
                        }
                        catch (TargetInvocationException ex)
                        {
                            if (ex.InnerException is DivideByZeroException)
                            {
                                lbCalcHistory.Text += (" " + tbOutput.Text);
                                tbOutput.Text = "INF";
                                previousValue = 0;
                                currentFunc = null;
                                return;
                            }
                        }
                        newCalcStarted = true;
                        currentFunc = null;
                    }
                };
            }
        }

        // Sets current function in response to function key press
        private void setCurrentFunc(string s, FuncButton btn)
        {
            if (currentFunc != null)
            {
                try
                {
                    previousValue = (double)currentFunc.DynamicInvoke(previousValue, currentValue);
                    lbCalcHistory.Text = previousValue.ToString() + " " + s;
                    tbOutput.Text = "0";
                }
                catch (TargetInvocationException ex)
                {
                    if (ex.InnerException is DivideByZeroException)
                    {
                        lbCalcHistory.Text += (" " + tbOutput.Text);
                        tbOutput.Text = "INF";
                        previousValue = 0;
                        currentFunc = null;
                    }
                }
            }
            else
            {
                previousValue = currentValue;
                lbCalcHistory.Text = tbOutput.Text + " " + s;
                tbOutput.Text = "0";
            }
            currentFunc = (Func<double, double, double>)btn.func;
            newCalcStarted = false;
    }

        // Deselect textbox when the form is shown
        private void Form1_Shown(object sender, EventArgs e)
        {
            tbOutput.SelectionLength = 0;
            lbExit.Focus();
        }

        // This code is for moving the borderless form around on the screen
        private void Form1_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                ReleaseCapture();
                SendMessage(Handle, WM_NCLBUTTONDOWN, HT_CAPTION, 0);
            }
        }

        // The main window is to drop shadow on the screen
        // similar to standard Win calculator
        protected override CreateParams CreateParams
        {
            get
            {
                const int CS_DROPSHADOW = 0x20000;
                CreateParams cp = base.CreateParams;
                cp.ClassStyle |= CS_DROPSHADOW;
                return cp;
            }
        }

        // This is to prevent the default behaviour of the Enter key
        // which is to activate the form control currently having focus. 
        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if (keyData == (Keys.Enter))
            {
                // Alt+F pressed
                keysToButtons[keyDict["Return"]].PerformClick();
                lbExit.Focus();
                return true;
            }
            return base.ProcessCmdKey(ref msg, keyData);
        }

        // Handle NumPad keypress
        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            //e.Handled = true;
            if (keysToButtons.Keys.Contains(e.KeyCode))
            {
                keysToButtons[e.KeyCode].PerformClick();
            }
        }
    }
}
