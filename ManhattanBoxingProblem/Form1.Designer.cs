namespace ManhattanBoxingProblem
{
    partial class Form1
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
            this.canvas = new System.Windows.Forms.PictureBox();
            this.btnClear = new System.Windows.Forms.Button();
            this.btnBox = new System.Windows.Forms.Button();
            this.iDelayMs = new System.Windows.Forms.NumericUpDown();
            this.label1 = new System.Windows.Forms.Label();
            this.solverDropdown = new System.Windows.Forms.ComboBox();
            ((System.ComponentModel.ISupportInitialize)(this.canvas)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.iDelayMs)).BeginInit();
            this.SuspendLayout();
            // 
            // canvas
            // 
            this.canvas.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.canvas.Location = new System.Drawing.Point(13, 13);
            this.canvas.Name = "canvas";
            this.canvas.Size = new System.Drawing.Size(512, 512);
            this.canvas.TabIndex = 0;
            this.canvas.TabStop = false;
            // 
            // btnClear
            // 
            this.btnClear.Location = new System.Drawing.Point(12, 531);
            this.btnClear.Name = "btnClear";
            this.btnClear.Size = new System.Drawing.Size(75, 23);
            this.btnClear.TabIndex = 1;
            this.btnClear.Text = "Clear Field";
            this.btnClear.UseVisualStyleBackColor = true;
            // 
            // btnBox
            // 
            this.btnBox.Location = new System.Drawing.Point(93, 531);
            this.btnBox.Name = "btnBox";
            this.btnBox.Size = new System.Drawing.Size(105, 23);
            this.btnBox.TabIndex = 2;
            this.btnBox.Text = "Create Boxes";
            this.btnBox.UseVisualStyleBackColor = true;
            // 
            // iDelayMs
            // 
            this.iDelayMs.Increment = new decimal(new int[] {
            100,
            0,
            0,
            0});
            this.iDelayMs.Location = new System.Drawing.Point(204, 534);
            this.iDelayMs.Maximum = new decimal(new int[] {
            10000,
            0,
            0,
            0});
            this.iDelayMs.Name = "iDelayMs";
            this.iDelayMs.Size = new System.Drawing.Size(68, 20);
            this.iDelayMs.TabIndex = 3;
            this.iDelayMs.Value = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(278, 536);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(50, 13);
            this.label1.TabIndex = 4;
            this.label1.Text = "ms Delay";
            // 
            // solverDropdown
            // 
            this.solverDropdown.FormattingEnabled = true;
            this.solverDropdown.Location = new System.Drawing.Point(334, 533);
            this.solverDropdown.Name = "solverDropdown";
            this.solverDropdown.Size = new System.Drawing.Size(191, 21);
            this.solverDropdown.TabIndex = 5;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(535, 620);
            this.Controls.Add(this.solverDropdown);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.iDelayMs);
            this.Controls.Add(this.btnBox);
            this.Controls.Add(this.btnClear);
            this.Controls.Add(this.canvas);
            this.Name = "Form1";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.canvas)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.iDelayMs)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        public System.Windows.Forms.PictureBox canvas;
        private System.Windows.Forms.Label label1;
        public System.Windows.Forms.Button btnClear;
        public System.Windows.Forms.Button btnBox;
        public System.Windows.Forms.NumericUpDown iDelayMs;
        public System.Windows.Forms.ComboBox solverDropdown;
    }
}

