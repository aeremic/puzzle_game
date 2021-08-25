using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SloziSliku {
    public partial class Form1 : Form {

        private StatesDB statesDB;

        private int[,] values;
        private Bitmap img;
        private Field[,] fields;

        private Color lineColor;
        private readonly int lineWidth;
        private string username;

        private readonly int offsetLeft, offsetRight, offsetTop, offsetBottom;
        private int borderLeft, borderRight, borderTop, borderBottom;
        private int x, y;

        private int rows;
        private int columns;

        private Boolean repaint;
        private Boolean reinitialization;
        private Boolean play;

        private int movesNum;
        private Stopwatch stopWatch;
        private TimeSpan offsetTimeSpan;

        private void btn_newGame_Click(object sender, EventArgs e) {
            if (openFileDialog1.FileName != "") {
                Form3 form3 = new Form3();
                form3.Show();

                form3.FormClosed += delegate {
                    username = form3.getUsername();

                    if (username != "") {
                        play = true;
                        movesNum = 0;
                        label2.Text = "Broj poteza: " + movesNum;
                        stopWatch.Start();
                        Randomize(fields);

                        repaint = true;
                        this.Refresh();
                    } else MessageBox.Show("Ime igraca neuspesno uneto.");
                };
            } else {
                MessageBox.Show("Slika nije ucitana.");
            }
        }

        private void Randomize(Field[,] matrix) {
            Random rnd = new Random();

            Field[,] randomized = new Field[rows, columns];
            int[] shuffledRowIndexes = Enumerable.Range(0, rows).ToArray();
            int[] shuffledColumnIndexes = Enumerable.Range(0, columns).ToArray();

            shuffledRowIndexes = shuffledRowIndexes.OrderBy(x => rnd.Next()).ToArray();
            for (int i = 0; i < rows; i++) {
                shuffledColumnIndexes = shuffledColumnIndexes.OrderBy(x => rnd.Next()).ToArray();
                for (int j = 0; j < columns; j++) {
                    randomized[i, j] = matrix[shuffledRowIndexes.ElementAt(i), shuffledColumnIndexes.ElementAt(j)];
                    // randomized[i, j] = matrix[i, j];
                    randomized[i, j].PositionX = i;
                    randomized[i, j].PositionY = j;
                }

            }

            Boolean empty = false;
            for (int i = 0; i < rows; i++) {
                for (int j = 0; j < columns; j++) {
                    if (randomized[i, j].Filled == false)
                        empty = true;
                }
            }

            if (!empty)
                randomized[rnd.Next(0, rows), rnd.Next(0, columns)].Filled = false;


            fields = randomized;
        }

        private void btn_lineColor_Click(object sender, EventArgs e) {
            if (colorDialog1.ShowDialog() == DialogResult.OK) {
                lineColor = colorDialog1.Color;
                repaint = true;
                this.Refresh();
            }
        }

        public Form1() {
            InitializeComponent();

            statesDB = new StatesDB();

            repaint = false;

            FormBorderStyle = FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;

            openFileDialog1.InitialDirectory = System.AppDomain.CurrentDomain.BaseDirectory;
            openFileDialog1.Filter = "Image files (*.jpg, *.jpeg, *.jpe, *.jfif, *.png, *.bmp) | *.jpg; *.jpeg; *.jpe; *.jfif; *.png; *.bmp";
            openFileDialog1.FilterIndex = 2;
            openFileDialog1.RestoreDirectory = true;
            openFileDialog1.Title = "Open image";
            openFileDialog1.FileName = "";

            label1.Text = "";
            label2.Text = "Broj poteza: " + movesNum;
            stopWatch = new Stopwatch();
            offsetTimeSpan = new TimeSpan();

            rb_rows3.Checked = true;
            rb_column3.Checked = true;

            rb_rows4.Enabled = true;
            rb_rows5.Enabled = false;
            rb_column4.Enabled = true;
            rb_column5.Enabled = false;

            offsetLeft = 9;
            offsetTop = 19;
            offsetRight = 94;
            offsetBottom = 19;

            lineColor = Color.FromArgb(255, 0, 0, 0);
            lineWidth = 1;
        }

        private void btn_loadImage_Click(object sender, EventArgs e) {
            if (openFileDialog1.ShowDialog() == DialogResult.OK) {
                repaint = true;
                reinitialization = true;
                this.Refresh();
                label1.Text = openFileDialog1.SafeFileName;
            }
        }

        protected override void OnPaint(PaintEventArgs e) {

            Rectangle rect = this.ClientRectangle;

            borderLeft = rect.X + offsetLeft;
            borderTop = rect.Y + offsetTop;
            borderRight = rect.Width - offsetRight;
            borderBottom = rect.Height - offsetBottom;

            y = (borderBottom - borderTop) / rows;
            borderBottom = borderTop + rows * y;

            x = (borderRight - borderLeft) / columns;
            borderRight = borderLeft + columns * x;

            Pen pen = new Pen(lineColor, lineWidth);
            for (int i = 0; i <= rows; i++) {
                // horizontal
                e.Graphics.DrawLine(pen, borderLeft, borderTop + i * y, borderRight + lineWidth - 1, borderTop + i * y);
                for (int j = 0; j <= columns; j++) {
                    // vertical
                    e.Graphics.DrawLine(pen, borderLeft + j * x, borderTop, borderLeft + j * x, borderBottom + lineWidth - 1);
                }
            }

            if (repaint && openFileDialog1.FileName != "") {
                if (reinitialization) {
                    int valuesCount = 0;
                    values = new int[rows, columns];
                    for (int i = 0; i < rows; i++) {
                        for (int j = 0; j < columns; j++) {
                            values[i, j] = ++valuesCount;
                        }
                    }

                    fields = new Field[rows, columns];
                    reinitialization = false;
                }

                Graphics graphics = e.Graphics;
                img = new Bitmap(openFileDialog1.FileName);
                img = new Bitmap(img, new Size(695, 410));
                int oneImgHeight = img.Height / rows;
                int oneImgWidth = img.Width / columns;
                int count = 0;

                for (int i = 0; i < rows; i++) {
                    for (int j = 0; j < columns; j++) {
                        if (Object.Equals(fields[i, j], default(Field))) {
                            fields[i, j] = new Field(i, j, oneImgHeight, oneImgWidth, CropImage(img, i, j, oneImgWidth, oneImgHeight), ++count);

                            fields[i, j].Draw(graphics);
                            // graphics.DrawImage(img, new Rectangle((10 + j) + oneImgWidth * j, (20 + i) + oneImgHeight * i, oneImgWidth, oneImgHeight), new Rectangle(j * oneImgWidth, i * oneImgHeight, oneImgWidth, oneImgHeight), GraphicsUnit.Pixel);
                        } else if (fields[i, j].Filled == true) {
                            fields[i, j].Draw(graphics);
                        }
                    }
                }

                repaint = false;
            }
        }

        private Bitmap CropImage(Bitmap source, int i, int j, int oneImgWidth, int oneImgHeight) {
            var bitmap = new Bitmap(oneImgWidth, oneImgWidth);
            using (var g = Graphics.FromImage(bitmap)) {
                g.DrawImage(source, new Rectangle(0, 0, oneImgWidth, oneImgHeight), new Rectangle(j * oneImgWidth, i * oneImgHeight, oneImgWidth, oneImgHeight), GraphicsUnit.Pixel);
                return bitmap;
            }
        }

        private void Form1_MouseDoubleClick(object sender, MouseEventArgs e) {
            if (play && e.Button == MouseButtons.Left && openFileDialog1.FileName != "") {
                Point point = new Point {
                    X = e.X,
                    Y = e.Y
                };

                if (borderLeft < point.X && point.Y < borderRight && borderTop < point.Y && point.Y < borderBottom) {
                    int selectedColumnIndex = (point.X - borderLeft) / x;
                    int selectedRowIndex = (point.Y - borderTop) / y;
                    // MessageBox.Show(selectedRowIndex + " " + selectedColumnIndex);
                    if (selectedRowIndex == 0) {
                        if ((selectedColumnIndex != 0) && (fields[selectedRowIndex, selectedColumnIndex - 1].Filled == false)) {
                            // check right
                            // change
                            fields[selectedRowIndex, selectedColumnIndex].Filled = false;

                            fields[selectedRowIndex, selectedColumnIndex - 1].Image = fields[selectedRowIndex, selectedColumnIndex].Image;
                            int tempValue = fields[selectedRowIndex, selectedColumnIndex].Value;
                            fields[selectedRowIndex, selectedColumnIndex].Value = fields[selectedRowIndex, selectedColumnIndex - 1].Value;
                            fields[selectedRowIndex, selectedColumnIndex - 1].Value = tempValue;

                            fields[selectedRowIndex, selectedColumnIndex - 1].Filled = true;

                            repaint = true;
                            movesNum++;
                            label2.Text = "Broj poteza: " + movesNum;
                            this.Refresh();
                            check();
                        } else if (fields[selectedRowIndex + 1, selectedColumnIndex].Filled == false) {
                            // down check
                            // change
                            fields[selectedRowIndex, selectedColumnIndex].Filled = false;

                            fields[selectedRowIndex + 1, selectedColumnIndex].Image = fields[selectedRowIndex, selectedColumnIndex].Image;
                            int tempValue = fields[selectedRowIndex, selectedColumnIndex].Value;
                            fields[selectedRowIndex, selectedColumnIndex].Value = fields[selectedRowIndex + 1, selectedColumnIndex].Value;
                            fields[selectedRowIndex + 1, selectedColumnIndex].Value = tempValue;

                            fields[selectedRowIndex + 1, selectedColumnIndex].Filled = true;

                            repaint = true;
                            movesNum++;
                            label2.Text = "Broj poteza: " + movesNum;
                            this.Refresh();
                            check();
                        } else if ((selectedColumnIndex != columns - 1) && (fields[selectedRowIndex, selectedColumnIndex + 1].Filled == false)) {
                            // check left
                            // change
                            fields[selectedRowIndex, selectedColumnIndex].Filled = false;

                            fields[selectedRowIndex, selectedColumnIndex + 1].Image = fields[selectedRowIndex, selectedColumnIndex].Image;
                            int tempValue = fields[selectedRowIndex, selectedColumnIndex].Value;
                            fields[selectedRowIndex, selectedColumnIndex].Value = fields[selectedRowIndex, selectedColumnIndex + 1].Value;
                            fields[selectedRowIndex, selectedColumnIndex + 1].Value = tempValue;

                            fields[selectedRowIndex, selectedColumnIndex + 1].Filled = true;

                            repaint = true;
                            movesNum++;
                            label2.Text = "Broj poteza: " + movesNum;
                            this.Refresh();
                            check();
                        }
                    } else if ((selectedColumnIndex == 0 || selectedColumnIndex == columns - 1) && (selectedRowIndex != rows - 1)) {
                        // MessageBox.Show("Sredisnja " + selectedRowIndex + " " + selectedColumnIndex);
                        if (fields[selectedRowIndex - 1, selectedColumnIndex].Filled == false) {
                            // check up
                            // change
                            fields[selectedRowIndex, selectedColumnIndex].Filled = false;

                            fields[selectedRowIndex - 1, selectedColumnIndex].Image = fields[selectedRowIndex, selectedColumnIndex].Image;
                            int tempValue = fields[selectedRowIndex, selectedColumnIndex].Value;
                            fields[selectedRowIndex, selectedColumnIndex].Value = fields[selectedRowIndex - 1, selectedColumnIndex].Value;
                            fields[selectedRowIndex - 1, selectedColumnIndex].Value = tempValue;

                            fields[selectedRowIndex - 1, selectedColumnIndex].Filled = true;

                            repaint = true;
                            movesNum++;
                            label2.Text = "Broj poteza: " + movesNum;
                            this.Refresh();
                            check();
                        } else if (fields[selectedRowIndex + 1, selectedColumnIndex].Filled == false) {
                            // check down
                            // change
                            fields[selectedRowIndex, selectedColumnIndex].Filled = false;

                            fields[selectedRowIndex + 1, selectedColumnIndex].Image = fields[selectedRowIndex, selectedColumnIndex].Image;
                            int tempValue = fields[selectedRowIndex, selectedColumnIndex].Value;
                            fields[selectedRowIndex, selectedColumnIndex].Value = fields[selectedRowIndex + 1, selectedColumnIndex].Value;
                            fields[selectedRowIndex + 1, selectedColumnIndex].Value = tempValue;

                            fields[selectedRowIndex + 1, selectedColumnIndex].Filled = true;

                            repaint = true;
                            movesNum++;
                            label2.Text = "Broj poteza: " + movesNum;
                            this.Refresh();
                            check();
                        } else if ((selectedColumnIndex == 0) && (fields[selectedRowIndex, selectedColumnIndex + 1].Filled == false)) {
                            // check right
                            // change
                            fields[selectedRowIndex, selectedColumnIndex].Filled = false;

                            fields[selectedRowIndex, selectedColumnIndex + 1].Image = fields[selectedRowIndex, selectedColumnIndex].Image;
                            int tempValue = fields[selectedRowIndex, selectedColumnIndex].Value;
                            fields[selectedRowIndex, selectedColumnIndex].Value = fields[selectedRowIndex, selectedColumnIndex + 1].Value;
                            fields[selectedRowIndex, selectedColumnIndex + 1].Value = tempValue;

                            fields[selectedRowIndex, selectedColumnIndex + 1].Filled = true;

                            repaint = true;
                            movesNum++;
                            label2.Text = "Broj poteza: " + movesNum;
                            this.Refresh();
                            check();
                        } else if ((selectedColumnIndex == columns - 1) && (fields[selectedRowIndex, selectedColumnIndex - 1].Filled == false)) {
                            // check left
                            // change
                            fields[selectedRowIndex, selectedColumnIndex].Filled = false;

                            fields[selectedRowIndex, selectedColumnIndex - 1].Image = fields[selectedRowIndex, selectedColumnIndex].Image;
                            int tempValue = fields[selectedRowIndex, selectedColumnIndex].Value;
                            fields[selectedRowIndex, selectedColumnIndex].Value = fields[selectedRowIndex, selectedColumnIndex - 1].Value;
                            fields[selectedRowIndex, selectedColumnIndex - 1].Value = tempValue;

                            fields[selectedRowIndex, selectedColumnIndex - 1].Filled = true;

                            repaint = true;
                            movesNum++;
                            label2.Text = "Broj poteza: " + movesNum;
                            this.Refresh();
                            check();
                        }
                    } else if (selectedRowIndex == rows - 1) {
                        // MessageBox.Show("Donja " + selectedRowIndex + " " + selectedColumnIndex);
                        if ((selectedColumnIndex != 0) && (fields[selectedRowIndex, selectedColumnIndex - 1].Filled == false)) {
                            // check right
                            // change
                            fields[selectedRowIndex, selectedColumnIndex].Filled = false;

                            fields[selectedRowIndex, selectedColumnIndex - 1].Image = fields[selectedRowIndex, selectedColumnIndex].Image;
                            int tempValue = fields[selectedRowIndex, selectedColumnIndex].Value;
                            fields[selectedRowIndex, selectedColumnIndex].Value = fields[selectedRowIndex, selectedColumnIndex - 1].Value;
                            fields[selectedRowIndex, selectedColumnIndex - 1].Value = tempValue;

                            fields[selectedRowIndex, selectedColumnIndex - 1].Filled = true;

                            repaint = true;
                            movesNum++;
                            label2.Text = "Broj poteza: " + movesNum;
                            this.Refresh();
                            check();
                        } else if (fields[selectedRowIndex - 1, selectedColumnIndex].Filled == false) {
                            // up check
                            // change
                            fields[selectedRowIndex, selectedColumnIndex].Filled = false;

                            fields[selectedRowIndex - 1, selectedColumnIndex].Image = fields[selectedRowIndex, selectedColumnIndex].Image;
                            int tempValue = fields[selectedRowIndex, selectedColumnIndex].Value;
                            fields[selectedRowIndex, selectedColumnIndex].Value = fields[selectedRowIndex - 1, selectedColumnIndex].Value;
                            fields[selectedRowIndex - 1, selectedColumnIndex].Value = tempValue;

                            fields[selectedRowIndex - 1, selectedColumnIndex].Filled = true;

                            repaint = true;
                            movesNum++;
                            label2.Text = "Broj poteza: " + movesNum;
                            this.Refresh();
                            check();
                        } else if ((selectedColumnIndex != columns - 1) && (fields[selectedRowIndex, selectedColumnIndex + 1].Filled == false)) {
                            // check left
                            // change
                            fields[selectedRowIndex, selectedColumnIndex].Filled = false;

                            fields[selectedRowIndex, selectedColumnIndex + 1].Image = fields[selectedRowIndex, selectedColumnIndex].Image;
                            int tempValue = fields[selectedRowIndex, selectedColumnIndex].Value;
                            fields[selectedRowIndex, selectedColumnIndex].Value = fields[selectedRowIndex, selectedColumnIndex + 1].Value;
                            fields[selectedRowIndex, selectedColumnIndex + 1].Value = tempValue;

                            fields[selectedRowIndex, selectedColumnIndex + 1].Filled = true;

                            repaint = true;
                            movesNum++;
                            label2.Text = "Broj poteza: " + movesNum;
                            this.Refresh();
                            check();
                        }
                    } else {
                        // MessageBox.Show(selectedRowIndex + " " + selectedColumnIndex);
                        if (fields[selectedRowIndex - 1, selectedColumnIndex].Filled == false) {
                            // check up
                            // change
                            fields[selectedRowIndex, selectedColumnIndex].Filled = false;

                            fields[selectedRowIndex - 1, selectedColumnIndex].Image = fields[selectedRowIndex, selectedColumnIndex].Image;
                            int tempValue = fields[selectedRowIndex, selectedColumnIndex].Value;
                            fields[selectedRowIndex, selectedColumnIndex].Value = fields[selectedRowIndex - 1, selectedColumnIndex].Value;
                            fields[selectedRowIndex - 1, selectedColumnIndex].Value = tempValue;

                            fields[selectedRowIndex - 1, selectedColumnIndex].Filled = true;

                            repaint = true;
                            movesNum++;
                            label2.Text = "Broj poteza: " + movesNum;
                            this.Refresh();
                            check();
                        } else if (fields[selectedRowIndex + 1, selectedColumnIndex].Filled == false) {
                            // check down
                            // change
                            fields[selectedRowIndex, selectedColumnIndex].Filled = false;

                            fields[selectedRowIndex + 1, selectedColumnIndex].Image = fields[selectedRowIndex, selectedColumnIndex].Image;
                            int tempValue = fields[selectedRowIndex, selectedColumnIndex].Value;
                            fields[selectedRowIndex, selectedColumnIndex].Value = fields[selectedRowIndex + 1, selectedColumnIndex].Value;
                            fields[selectedRowIndex + 1, selectedColumnIndex].Value = tempValue;

                            fields[selectedRowIndex + 1, selectedColumnIndex].Filled = true;

                            repaint = true;
                            movesNum++;
                            label2.Text = "Broj poteza: " + movesNum;
                            this.Refresh();
                            check();
                        } else if (fields[selectedRowIndex, selectedColumnIndex - 1].Filled == false) {
                            // check left
                            // change
                            fields[selectedRowIndex, selectedColumnIndex].Filled = false;

                            fields[selectedRowIndex, selectedColumnIndex - 1].Image = fields[selectedRowIndex, selectedColumnIndex].Image;
                            int tempValue = fields[selectedRowIndex, selectedColumnIndex].Value;
                            fields[selectedRowIndex, selectedColumnIndex].Value = fields[selectedRowIndex, selectedColumnIndex - 1].Value;
                            fields[selectedRowIndex, selectedColumnIndex - 1].Value = tempValue;

                            fields[selectedRowIndex, selectedColumnIndex - 1].Filled = true;

                            repaint = true;
                            movesNum++;
                            label2.Text = "Broj poteza: " + movesNum;
                            this.Refresh();
                            check();
                        } else if (fields[selectedRowIndex, selectedColumnIndex + 1].Filled == false) {
                            // check right
                            // change
                            fields[selectedRowIndex, selectedColumnIndex].Filled = false;

                            fields[selectedRowIndex, selectedColumnIndex + 1].Image = fields[selectedRowIndex, selectedColumnIndex].Image;
                            int tempValue = fields[selectedRowIndex, selectedColumnIndex].Value;
                            fields[selectedRowIndex, selectedColumnIndex].Value = fields[selectedRowIndex, selectedColumnIndex + 1].Value;
                            fields[selectedRowIndex, selectedColumnIndex + 1].Value = tempValue;

                            fields[selectedRowIndex, selectedColumnIndex + 1].Filled = true;

                            repaint = true;
                            movesNum++;
                            label2.Text = "Broj poteza: " + movesNum;
                            this.Refresh();
                            check();
                        }
                    }
                }
            }
        }

        private void check() {
            for (int i = 0; i < rows; i++) {
                for (int j = 0; j < columns; j++) {
                    // Console.WriteLine("move " + fields[i, j].Value + " " + values[i, j]);
                    if (fields[i, j].Value != values[i, j])
                        return;
                }
            }

            // MessageBox.Show("Pobeda! Broj potrebnih poteza: " + movesNum);
            play = false;
            stopWatch.Stop();
            statesDB.add(username, offsetTimeSpan.Add(stopWatch.Elapsed), movesNum);

            Form form2 = new Form2();
            form2.Show();
        }

        private void Form1_Load(object sender, EventArgs e) {
            if (File.Exists("states.txt")) {
                IFormatter formatter = new BinaryFormatter();
                Stream stream = new FileStream("states.txt", FileMode.Open, FileAccess.Read);

                username = (string)formatter.Deserialize(stream);
                openFileDialog1.FileName = (string)formatter.Deserialize(stream);
                label1.Text = openFileDialog1.SafeFileName;
                lineColor = (Color)formatter.Deserialize(stream);
                img = new Bitmap(openFileDialog1.FileName);
                img = new Bitmap(img, new Size(695, 410));

                rows = (int)formatter.Deserialize(stream);
                columns = (int)formatter.Deserialize(stream);

                int valuesCount = 0;
                values = new int[rows, columns];
                for (int i = 0; i < rows; i++) {
                    for (int j = 0; j < columns; j++) {
                        values[i, j] = ++valuesCount;
                    }
                }

                fields = new Field[rows, columns];
                reinitialization = false;
                play = true;

                for (int i = 0; i < rows; i++) {
                    for (int j = 0; j < columns; j++) {
                        fields[i, j] = new Field {
                            PositionX = (int)formatter.Deserialize(stream),
                            PositionY = (int)formatter.Deserialize(stream),
                            OneImgHeight = (int)formatter.Deserialize(stream),
                            OneImgWidth = (int)formatter.Deserialize(stream),
                            Image = (Bitmap)formatter.Deserialize(stream),
                            Filled = (Boolean)formatter.Deserialize(stream),
                            Value = (int)formatter.Deserialize(stream)
                        };
                    }
                }
                movesNum = (int)formatter.Deserialize(stream);
                play = (Boolean)formatter.Deserialize(stream);
                offsetTimeSpan = (TimeSpan)formatter.Deserialize(stream);
                stopWatch.Start();

                repaint = true;

                label2.Text = "Broj poteza: " + movesNum;

                stream.Close();
            }
        }

        private void btn_end_Click(object sender, EventArgs e) {
            if (openFileDialog1.FileName != "") {
                BinaryFormatter formatter = new BinaryFormatter();
                Stream stream = new FileStream("states.txt", FileMode.Create, FileAccess.Write);

                formatter.Serialize(stream, username);
                formatter.Serialize(stream, openFileDialog1.FileName);
                formatter.Serialize(stream, lineColor);
                formatter.Serialize(stream, rows);
                formatter.Serialize(stream, columns);
                for (int i = 0; i < rows; i++) {
                    for (int j = 0; j < columns; j++) {
                        formatter.Serialize(stream, fields[i, j].PositionX);
                        formatter.Serialize(stream, fields[i, j].PositionY);
                        formatter.Serialize(stream, fields[i, j].OneImgHeight);
                        formatter.Serialize(stream, fields[i, j].OneImgWidth);
                        formatter.Serialize(stream, fields[i, j].Image);
                        formatter.Serialize(stream, fields[i, j].Filled);
                        formatter.Serialize(stream, fields[i, j].Value);
                    }
                }
                formatter.Serialize(stream, movesNum);
                formatter.Serialize(stream, play);

                stopWatch.Stop();
                formatter.Serialize(stream, stopWatch.Elapsed);
            }

            this.Close();
        }

        private void rb_rows3_CheckedChanged(object sender, EventArgs e) {
            rows = 3;
            repaint = true;
            reinitialization = true;
            this.Refresh();
        }

        private void rb_rows4_CheckedChanged(object sender, EventArgs e) {
            rows = 4;
            repaint = true;
            reinitialization = true;
            this.Refresh();
        }

        private void rb_rows5_CheckedChanged(object sender, EventArgs e) {
            rows = 5;
            repaint = true;
            reinitialization = true;
            this.Refresh();
        }

        private void rb_column3_CheckedChanged(object sender, EventArgs e) {
            columns = 3;
            repaint = true;
            reinitialization = true;
            this.Refresh();
        }

        private void rb_column4_CheckedChanged(object sender, EventArgs e) {
            columns = 4;
            repaint = true;
            reinitialization = true;
            this.Refresh();
        }

        private void rb_column5_CheckedChanged(object sender, EventArgs e) {
            columns = 5;
            repaint = true;
            reinitialization = true;
            this.Refresh();
        }

    }
}

