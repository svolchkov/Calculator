namespace Calculator
{
    partial class MainForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.tbOutput = new System.Windows.Forms.TextBox();
            this.lbExit = new System.Windows.Forms.Label();
            this.tlpNumericKeys = new System.Windows.Forms.TableLayoutPanel();
            this.tlpArithmetics = new System.Windows.Forms.TableLayoutPanel();
            this.lbCalcHistory = new System.Windows.Forms.Label();
            this.tlpAdvanced = new System.Windows.Forms.TableLayoutPanel();
            this.SuspendLayout();
            // 
            // tbOutput
            // 
            this.tbOutput.BackColor = System.Drawing.Color.Black;
            this.tbOutput.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.tbOutput.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tbOutput.ForeColor = System.Drawing.Color.White;
            this.tbOutput.Location = new System.Drawing.Point(22, 31);
            this.tbOutput.Name = "tbOutput";
            this.tbOutput.ReadOnly = true;
            this.tbOutput.Size = new System.Drawing.Size(297, 41);
            this.tbOutput.TabIndex = 0;
            this.tbOutput.Text = "0";
            this.tbOutput.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // lbExit
            // 
            this.lbExit.AutoSize = true;
            this.lbExit.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbExit.ForeColor = System.Drawing.Color.White;
            this.lbExit.Location = new System.Drawing.Point(482, 9);
            this.lbExit.Name = "lbExit";
            this.lbExit.Size = new System.Drawing.Size(21, 20);
            this.lbExit.TabIndex = 1;
            this.lbExit.Text = "X";
            this.lbExit.Click += new System.EventHandler(this.lbExit_Click);
            // 
            // numKeyPanel
            // 
            this.tlpNumericKeys.ColumnCount = 3;
            this.tlpNumericKeys.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.3322F));
            this.tlpNumericKeys.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.3289F));
            this.tlpNumericKeys.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.3389F));
            this.tlpNumericKeys.Location = new System.Drawing.Point(40, 103);
            this.tlpNumericKeys.Name = "numKeyPanel";
            this.tlpNumericKeys.RowCount = 4;
            this.tlpNumericKeys.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tlpNumericKeys.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tlpNumericKeys.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tlpNumericKeys.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tlpNumericKeys.Size = new System.Drawing.Size(260, 214);
            this.tlpNumericKeys.TabIndex = 2;
            // 
            // tlpArithmetics
            // 
            this.tlpArithmetics.ColumnCount = 1;
            this.tlpArithmetics.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlpArithmetics.Location = new System.Drawing.Point(345, 31);
            this.tlpArithmetics.Name = "tlpArithmetics";
            this.tlpArithmetics.RowCount = 6;
            this.tlpArithmetics.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 14.28F));
            this.tlpArithmetics.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 14.28F));
            this.tlpArithmetics.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 14.28F));
            this.tlpArithmetics.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 14.28F));
            this.tlpArithmetics.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 14.28F));
            this.tlpArithmetics.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 28.6F));
            this.tlpArithmetics.Size = new System.Drawing.Size(50, 286);
            this.tlpArithmetics.TabIndex = 3;
            // 
            // lbPrevValue
            // 
            this.lbCalcHistory.AutoSize = true;
            this.lbCalcHistory.ForeColor = System.Drawing.Color.White;
            this.lbCalcHistory.Location = new System.Drawing.Point(83, 9);
            this.lbCalcHistory.Name = "lbPrevValue";
            this.lbCalcHistory.Size = new System.Drawing.Size(0, 17);
            this.lbCalcHistory.TabIndex = 4;
            // 
            // tlpAdvanced
            // 
            this.tlpAdvanced.ColumnCount = 1;
            this.tlpAdvanced.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlpAdvanced.Location = new System.Drawing.Point(431, 31);
            this.tlpAdvanced.Name = "tlpAdvanced";
            this.tlpAdvanced.RowCount = 7;
            this.tlpAdvanced.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 14.28F));
            this.tlpAdvanced.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 14.28F));
            this.tlpAdvanced.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 14.28F));
            this.tlpAdvanced.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 14.28F));
            this.tlpAdvanced.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 14.28F));
            this.tlpAdvanced.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 14.28F));
            this.tlpAdvanced.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 14.32F));
            this.tlpAdvanced.Size = new System.Drawing.Size(50, 286);
            this.tlpAdvanced.TabIndex = 5;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Black;
            this.ClientSize = new System.Drawing.Size(515, 352);
            this.Controls.Add(this.tlpAdvanced);
            this.Controls.Add(this.lbCalcHistory);
            this.Controls.Add(this.tlpArithmetics);
            this.Controls.Add(this.tlpNumericKeys);
            this.Controls.Add(this.lbExit);
            this.Controls.Add(this.tbOutput);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "MainForm";
            this.Text = "Form1";
            this.Shown += new System.EventHandler(this.Form1_Shown);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.Form1_KeyDown);
            this.MouseDown += new System.Windows.Forms.MouseEventHandler(this.Form1_MouseDown);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox tbOutput;
        private System.Windows.Forms.Label lbExit;
        private System.Windows.Forms.TableLayoutPanel tlpNumericKeys;
        private System.Windows.Forms.TableLayoutPanel tlpArithmetics;
        private System.Windows.Forms.Label lbCalcHistory;
        private System.Windows.Forms.TableLayoutPanel tlpAdvanced;
    }
}

