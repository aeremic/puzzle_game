
namespace SloziSliku {
    partial class Form1 {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing) {
            if (disposing && (components != null)) {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent() {
            this.btn_loadImage = new System.Windows.Forms.Button();
            this.btn_newGame = new System.Windows.Forms.Button();
            this.lbl_moves = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.rb_column5 = new System.Windows.Forms.RadioButton();
            this.rb_column4 = new System.Windows.Forms.RadioButton();
            this.rb_column3 = new System.Windows.Forms.RadioButton();
            this.btn_lineColor = new System.Windows.Forms.Button();
            this.btn_end = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.colorDialog1 = new System.Windows.Forms.ColorDialog();
            this.rb_rows3 = new System.Windows.Forms.RadioButton();
            this.rb_rows4 = new System.Windows.Forms.RadioButton();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.rb_rows5 = new System.Windows.Forms.RadioButton();
            this.label2 = new System.Windows.Forms.Label();
            this.groupBox2.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // btn_loadImage
            // 
            this.btn_loadImage.Location = new System.Drawing.Point(713, 22);
            this.btn_loadImage.Name = "btn_loadImage";
            this.btn_loadImage.Size = new System.Drawing.Size(75, 23);
            this.btn_loadImage.TabIndex = 0;
            this.btn_loadImage.Text = "Ucitaj sliku";
            this.btn_loadImage.UseVisualStyleBackColor = true;
            this.btn_loadImage.Click += new System.EventHandler(this.btn_loadImage_Click);
            // 
            // btn_newGame
            // 
            this.btn_newGame.Location = new System.Drawing.Point(713, 64);
            this.btn_newGame.Name = "btn_newGame";
            this.btn_newGame.Size = new System.Drawing.Size(75, 23);
            this.btn_newGame.TabIndex = 1;
            this.btn_newGame.Text = "Nova igra";
            this.btn_newGame.UseVisualStyleBackColor = true;
            this.btn_newGame.Click += new System.EventHandler(this.btn_newGame_Click);
            // 
            // lbl_moves
            // 
            this.lbl_moves.AutoSize = true;
            this.lbl_moves.Location = new System.Drawing.Point(735, 96);
            this.lbl_moves.Name = "lbl_moves";
            this.lbl_moves.Size = new System.Drawing.Size(0, 13);
            this.lbl_moves.TabIndex = 2;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.rb_column5);
            this.groupBox2.Controls.Add(this.rb_column4);
            this.groupBox2.Controls.Add(this.rb_column3);
            this.groupBox2.Location = new System.Drawing.Point(713, 232);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(75, 93);
            this.groupBox2.TabIndex = 4;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Broj kolona";
            // 
            // rb_column5
            // 
            this.rb_column5.AutoSize = true;
            this.rb_column5.Location = new System.Drawing.Point(25, 69);
            this.rb_column5.Name = "rb_column5";
            this.rb_column5.Size = new System.Drawing.Size(31, 17);
            this.rb_column5.TabIndex = 8;
            this.rb_column5.TabStop = true;
            this.rb_column5.Text = "5";
            this.rb_column5.UseVisualStyleBackColor = true;
            this.rb_column5.CheckedChanged += new System.EventHandler(this.rb_column5_CheckedChanged);
            // 
            // rb_column4
            // 
            this.rb_column4.AutoSize = true;
            this.rb_column4.Location = new System.Drawing.Point(25, 46);
            this.rb_column4.Name = "rb_column4";
            this.rb_column4.Size = new System.Drawing.Size(31, 17);
            this.rb_column4.TabIndex = 7;
            this.rb_column4.TabStop = true;
            this.rb_column4.Text = "4";
            this.rb_column4.UseVisualStyleBackColor = true;
            this.rb_column4.CheckedChanged += new System.EventHandler(this.rb_column4_CheckedChanged);
            // 
            // rb_column3
            // 
            this.rb_column3.AutoSize = true;
            this.rb_column3.Location = new System.Drawing.Point(25, 23);
            this.rb_column3.Name = "rb_column3";
            this.rb_column3.Size = new System.Drawing.Size(31, 17);
            this.rb_column3.TabIndex = 6;
            this.rb_column3.TabStop = true;
            this.rb_column3.Text = "3";
            this.rb_column3.UseVisualStyleBackColor = true;
            this.rb_column3.CheckedChanged += new System.EventHandler(this.rb_column3_CheckedChanged);
            // 
            // btn_lineColor
            // 
            this.btn_lineColor.Location = new System.Drawing.Point(713, 331);
            this.btn_lineColor.Name = "btn_lineColor";
            this.btn_lineColor.Size = new System.Drawing.Size(75, 23);
            this.btn_lineColor.TabIndex = 5;
            this.btn_lineColor.Text = "Boja linija";
            this.btn_lineColor.UseVisualStyleBackColor = true;
            this.btn_lineColor.Click += new System.EventHandler(this.btn_lineColor_Click);
            // 
            // btn_end
            // 
            this.btn_end.Location = new System.Drawing.Point(713, 411);
            this.btn_end.Name = "btn_end";
            this.btn_end.Size = new System.Drawing.Size(75, 23);
            this.btn_end.TabIndex = 6;
            this.btn_end.Text = "Snimi i zavrsi";
            this.btn_end.UseVisualStyleBackColor = true;
            this.btn_end.Click += new System.EventHandler(this.btn_end_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(710, 48);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(35, 13);
            this.label1.TabIndex = 7;
            this.label1.Text = "label1";
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            // 
            // rb_rows3
            // 
            this.rb_rows3.AutoSize = true;
            this.rb_rows3.Location = new System.Drawing.Point(25, 21);
            this.rb_rows3.Name = "rb_rows3";
            this.rb_rows3.Size = new System.Drawing.Size(31, 17);
            this.rb_rows3.TabIndex = 0;
            this.rb_rows3.TabStop = true;
            this.rb_rows3.Text = "3";
            this.rb_rows3.UseVisualStyleBackColor = true;
            this.rb_rows3.CheckedChanged += new System.EventHandler(this.rb_rows3_CheckedChanged);
            // 
            // rb_rows4
            // 
            this.rb_rows4.AutoSize = true;
            this.rb_rows4.Location = new System.Drawing.Point(25, 44);
            this.rb_rows4.Name = "rb_rows4";
            this.rb_rows4.Size = new System.Drawing.Size(31, 17);
            this.rb_rows4.TabIndex = 1;
            this.rb_rows4.TabStop = true;
            this.rb_rows4.Text = "4";
            this.rb_rows4.UseVisualStyleBackColor = true;
            this.rb_rows4.CheckedChanged += new System.EventHandler(this.rb_rows4_CheckedChanged);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.rb_rows5);
            this.groupBox1.Controls.Add(this.rb_rows4);
            this.groupBox1.Controls.Add(this.rb_rows3);
            this.groupBox1.Location = new System.Drawing.Point(713, 122);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(75, 93);
            this.groupBox1.TabIndex = 3;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Broj vrsta";
            // 
            // rb_rows5
            // 
            this.rb_rows5.AutoSize = true;
            this.rb_rows5.Location = new System.Drawing.Point(25, 67);
            this.rb_rows5.Name = "rb_rows5";
            this.rb_rows5.Size = new System.Drawing.Size(31, 17);
            this.rb_rows5.TabIndex = 5;
            this.rb_rows5.TabStop = true;
            this.rb_rows5.Text = "5";
            this.rb_rows5.UseVisualStyleBackColor = true;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(718, 90);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(35, 13);
            this.label2.TabIndex = 8;
            this.label2.Text = "label2";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btn_end);
            this.Controls.Add(this.btn_lineColor);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.lbl_moves);
            this.Controls.Add(this.btn_newGame);
            this.Controls.Add(this.btn_loadImage);
            this.Name = "Form1";
            this.Text = "Slozi sliku";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.Form1_MouseDoubleClick);
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btn_loadImage;
        private System.Windows.Forms.Button btn_newGame;
        private System.Windows.Forms.Label lbl_moves;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Button btn_lineColor;
        private System.Windows.Forms.Button btn_end;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.RadioButton rb_column5;
        private System.Windows.Forms.RadioButton rb_column4;
        private System.Windows.Forms.RadioButton rb_column3;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.ColorDialog colorDialog1;
        private System.Windows.Forms.RadioButton rb_rows3;
        private System.Windows.Forms.RadioButton rb_rows4;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.RadioButton rb_rows5;
        private System.Windows.Forms.Label label2;
    }
}

