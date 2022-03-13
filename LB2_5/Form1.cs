// This is a personal academic project. Dear PVS-Studio, please check it.

// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: https://pvs-studio.com
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace LB2_5
{
    public partial class Form1 : Form
    {
        private Color Bcolor = Color.White;
        private Color color;
        Bitmap picture;
        Bitmap picture1;
        string mod;
        int x1, y1;
        int x3, y4;
        public Form1()
        {   //инициализация переменных
            InitializeComponent();
            Timer timer = new Timer();
            timer.Enabled = true;
            timer.Tick += new EventHandler(timer1_Tick);
            timer = new Timer() { Interval = 1000 };
            timer.Tick += timer1_Tick;
            timer.Start();
            picture = new Bitmap(800,800);
            picture1 = new Bitmap(800,800);
            x1 = y1 = 0;
            mod = "Линия";
            pictureBox1.BackColor = Bcolor;
        }

        private void оПрограммеToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //обращение к окну вывода информации
            MessageBox.Show("Программа: Графический редактор.\nРазработал студент группы ЭИСБ-24: Ивашевский И.И.");
        }

        private void выходToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Close(); //закрытие приложения
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            //вывод даты и времени в строку Strip
            toolStripStatusLabel2.Text = DateTime.Now.ToShortDateString() + ", " + DateTime.Now.ToLongTimeString();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //работа с цветом пера, зависит от выбранной кнопки и цвет этой кнопки присваивается Button5 
            //для отображения выбранного цвета
            Button b = (Button)sender;
            button5.BackColor = b.BackColor;
        }

        private void сохранитьToolStripMenuItem_Click(object sender, EventArgs e)
        {   //сохранение PictureBox
            if (openFileDialog1.FileName != "openFileDialog1")
                picture.Save(openFileDialog1.FileName);
            else
               MessageBox.Show("Вы не выбрали файл, воспользуйтесь функцией Сохранить как...");
        }

        private void сохранитьКакToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //сохранение PictureBox
            saveFileDialog1.ShowDialog(); //вызов модального окна
            //если файл был изменён
            if (saveFileDialog1.FileName != "")
            //то сохраняем picture
            picture.Save(saveFileDialog1.FileName);
        }

        private void открытьToolStripMenuItem_Click(object sender, EventArgs e)
        {   //открытие файла
            openFileDialog1.ShowDialog(); //вызов модального окна
            //если файл не пустой, то открываем его
            if (openFileDialog1.FileName != "")
            {   //оба метода получают дескриптор изображения через аргумент 
                //вернет суперкласс Image, а первый просто вернет Bitmap, чтобы вы могли избежать приведения.
                picture = (Bitmap)Image.FromFile(openFileDialog1.FileName); 
                pictureBox1.Image = picture;
            }
        }

        private void pictureBox1_MouseUp(object sender, MouseEventArgs e)
        {   
            Pen p;
            p = new Pen(button5.BackColor, trackBar1.Value);
            Graphics g;
            //статичный метод для создания объекта Graphics на основе картинки
            g = Graphics.FromImage(picture); 

            //блок который производит работу с рисованием "Окружности"
            if (mod == "Окружность")
            {   //эллипс создается как геометрическая фигура, вписанная в прямоугольник
                g.DrawEllipse(p, x3, y4, e.X - x3, e.Y - y4);
                if (checkBox1.Checked)
                {   //метод для залифки фигур
                    SolidBrush myBrush = new SolidBrush(button5.BackColor);
                    //Заполняет внутреннюю часть эллипса, определяемого ограничивающим прямоугольником, который задан структурой Rectangle.
                    g.FillEllipse(myBrush, x3, y4, Math.Abs(e.X - x3), Math.Abs(e.Y - y4));
                }
            }
            //блок который производит работу с рисованием "Прямоугольника"
            if (mod == "Прямоугольник")
            {   //создание прямоугольника
                g.DrawRectangle(p, x3, y4, e.X - x3, e.Y - y4);

                    if (checkBox1.Checked)
                    {   //метод для залифки фигур
                        SolidBrush myBrush = new SolidBrush(button5.BackColor);
                        //Заполняет внутреннюю часть прямоугольника
                        g.FillRectangle(myBrush, x3, y4, Math.Abs(e.X - x3), Math.Abs(e.Y - y4));
                    };

            }
        }
        //присвоение "Окружности" специальной кнопки, для дальнейшей работы
        private void button6_Click(object sender, EventArgs e)
        {
            mod = "Окружность";
        }
        //присвоение "Прямоугольнику" специальной кнопки, для дальнейшей работы
        private void button7_Click(object sender, EventArgs e)
        {
            mod = "Прямоугольник";
        }
        //присвоение "Линии" специальной кнопки, для дальнейшей работы
        private void button8_Click(object sender, EventArgs e)
        {
            mod = "Линия";
        }
        //присвоение перу начального цвета, через radioButton1
        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            button5.BackColor = Color.Black;
        }
        //объявление координат, данная функция срабатывает, при нажатии на кнопку мыши
        private void pictureBox1_MouseDown(object sender, MouseEventArgs e)
        {
            x3 = e.X;
            y4 = e.Y;
        }
        //очищение PictureBox1
        private void button10_Click(object sender, EventArgs e)
        { 
            using (var g = Graphics.FromImage(picture))
                g.Clear(Color.White);

            pictureBox1.Image = picture;

        }
        public static int point (UInt64 x)
        {
            int po = unchecked((int)(((uint)x) + (uint)(x >> 32)));
            if (x <= 0)
            {
                return unchecked(-po);
            }
            return po;
        }

        private void Form1_Move(object sender, EventArgs e)
        {

        }

            //функция по работе с рисованием линий и фигур
            private bool pictureBox1_MouseMove(object sender, MouseEventArgs e)
        {
            Pen p;
            int j = 100;
            p = new Pen(button5.BackColor, trackBar1.Value);
            p.StartCap = System.Drawing.Drawing2D.LineCap.Round;
            //cвойства Pen класса StartCap и EndCap определяют, как линия рисует свои концевые колпачки.
            p.EndCap = System.Drawing.Drawing2D.LineCap.Round;
            Graphics g; 
            g = Graphics.FromImage(picture); //создаёт новый объект Grafics
            Graphics g1;
            g1 = Graphics.FromImage(picture1); //создаёт новый объект Grafics
            if (e.Button == MouseButtons.Left) //если задеёствована левая кнопка мыши, то рисуем
            {
                //блок который производит работу с рисованием "Окружности"
                if (mod == "Окружность")
                {
                    
                    g1.Clear(Color.White);
                    g1.DrawEllipse(p, x3, y4, Math.Abs(e.X - x3), Math.Abs (e.Y - y4));
                    if (checkBox1.Checked)
                    {
                        SolidBrush myBrush = new SolidBrush(button5.BackColor);
                        g1.FillEllipse(myBrush, x3, y4, Math.Abs(e.X - x3), Math.Abs(e.Y - y4));
                    }
                }
                
                //блок который производит работу с рисованием "Прямоугольника"
                if (mod == "Прямоугольник")
                {
                    int x, y, z;
                    x = x3;
                    y = y4;
                    x = x > e.X ? e.X : x;
                    y = y > e.Y ? e.Y : y;
                    g1.Clear(Color.White);
                    g1.DrawRectangle(p, x, y, Math.Abs(e.X - x3), Math.Abs(e.Y - y4));
                    if (checkBox1.Checked)
                    {
                        SolidBrush myBrush = new SolidBrush(button5.BackColor);
                        g1.FillRectangle(myBrush, x, y, Math.Abs(e.X - x3), Math.Abs(e.Y - y4));
                    }

                }

                 g1.DrawImage(picture, 0, 0);
                //блок который производит работу с рисованием "Линии"
                if (mod == "Линия")
                {
                    g.DrawLine(p, x1, y1, e.X, e.Y);
                    if (checkBox1.Checked)
                    {
                        g1.DrawLine(p, x1, y1, e.X, e.Y);
                    }
                }
                return false;
                
            }
            return true;
        }
        //функция по выбору цвета и присвоением этого цвета Button5

        private void button9_Click(object sender, EventArgs e)
        {
            bool _isPointXValid;
            bool _isPointYValid;
            if (colorDialog1.ShowDialog() == DialogResult.Cancel)
                return;
            if (colorDialog1.ShowDialog() == DialogResult.Cancel)
            { }
            color = colorDialog1.Color;
            button5.BackColor = color;
            bool isPointValid()
            {
                return _isPointXValid && _isPointXValid;
            };
        }
    }
}
