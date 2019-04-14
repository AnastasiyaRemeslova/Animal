using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Threading;

namespace Animal
{
    public partial class Form1 : Form
    {

        private System.Windows.Forms.TrackBar trackBar1, trackBar2, trackBar3, trackBar4, trackBar5;
        private System.Windows.Forms.TextBox textBox1, textBox2, textBox3, textBox4, textBox5;
        private System.Drawing.SolidBrush myBrush;
        Graphics formGraphics, g;
        Enviroment enviroment;
        private int width, height, x0, y0, n ,m, numberOfFishes, numberOfBirds, numberOfMammals, k;
        Thread mythread;
        Font aFont = new Font("Tahoma", 10, FontStyle.Regular);

        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            
            e.Graphics.DrawString("Количество клеток по горизонтали:", aFont, Brushes.Black, 10, 25);
            e.Graphics.DrawString("Количество клеток по вертикали:", aFont, Brushes.Black, 10, 95);
            e.Graphics.DrawString("Количество животных:", aFont, Brushes.Black, 10, 165);
            e.Graphics.DrawString("Рыбы:", aFont, Brushes.Black, 10, 210);
            e.Graphics.DrawString("Птицы:", aFont, Brushes.Black, 10, 260);
            e.Graphics.DrawString("Млекопитающие:", aFont, Brushes.Black, 10, 310);
        }


        public Form1()
        {
            this.SetStyle(ControlStyles.AllPaintingInWmPaint | ControlStyles.UserPaint, true);
            this.SetStyle(ControlStyles.OptimizedDoubleBuffer, true);
            this.DoubleBuffered = true;
            width = 700;
            height = 700;
            x0 = 255;
            y0 = 25;
            InitializeComponent();
            g = this.CreateGraphics();
            
            this.textBox1 = new TextBox();
            this.trackBar1 = new TrackBar();

            this.textBox1.Location = new Point(210, 50);
            this.textBox1.Size = new Size(30, 30);

            this.Controls.AddRange(new Control[] { this.textBox1, this.trackBar1 });

            this.trackBar1.Location = new Point(10, 50);
            this.trackBar1.Size = new Size(200, 30);
            this.trackBar1.Scroll += new EventHandler(trackBar1_Scroll);

            trackBar1.Minimum = 10;
            trackBar1.Maximum = 80;
            trackBar1.TickFrequency = 10;
            trackBar1.LargeChange = 5;
            trackBar1.SmallChange = 1;
            trackBar1.Value = trackBar1.Minimum;

            this.textBox2 = new TextBox();
            this.trackBar2 = new TrackBar();

            this.textBox2.Location = new Point(210, 120);
            this.textBox2.Size = new Size(30, 30);

            this.Controls.AddRange(new Control[] { this.textBox2, this.trackBar2 });

            this.trackBar2.Location = new Point(10, 120);
            this.trackBar2.Size = new Size(200, 30);
            this.trackBar2.Scroll += new EventHandler(trackBar2_Scroll);

            trackBar2.Minimum = 10;
            trackBar2.Maximum = 80;
            trackBar2.TickFrequency = 10;
            trackBar2.LargeChange = 5;
            trackBar2.SmallChange = 1;
            trackBar2.Value = trackBar2.Minimum;

            this.textBox3 = new TextBox();
            this.trackBar3 = new TrackBar();

            this.textBox3.Location = new Point(210, 210);
            this.textBox3.Size = new Size(30, 30);

            this.Controls.AddRange(new Control[] { this.textBox3, this.trackBar3 });

            this.trackBar3.Location = new Point(110, 210);
            this.trackBar3.Size = new Size(100, 30);
            this.trackBar3.Scroll += new EventHandler(trackBar3_Scroll);

            trackBar3.Minimum = 5;
            trackBar3.Maximum = 50;
            trackBar3.TickFrequency = 10;
            trackBar3.LargeChange = 5;
            trackBar3.SmallChange = 1;
            trackBar3.Value = trackBar3.Minimum;

            this.textBox4 = new TextBox();
            this.trackBar4 = new TrackBar();

            this.textBox4.Location = new Point(210, 260);
            this.textBox4.Size = new Size(30, 30);

            this.Controls.AddRange(new Control[] { this.textBox4, this.trackBar4 });

            this.trackBar4.Location = new Point(110, 260);
            this.trackBar4.Size = new Size(100, 30);
            this.trackBar4.Scroll += new EventHandler(trackBar4_Scroll);

            trackBar4.Minimum = 5;
            trackBar4.Maximum = 50;
            trackBar4.TickFrequency = 10;
            trackBar4.LargeChange = 5;
            trackBar4.SmallChange = 1;
            trackBar4.Value = trackBar4.Minimum;

            this.textBox5 = new TextBox();
            this.trackBar5 = new TrackBar();

            this.textBox5.Location = new Point(210, 310);
            this.textBox5.Size = new Size(30, 30);

            this.Controls.AddRange(new Control[] { this.textBox5, this.trackBar5 });

            this.trackBar5.Location = new Point(110, 310);
            this.trackBar5.Size = new Size(100, 30);
            this.trackBar5.Scroll += new EventHandler(trackBar5_Scroll);

            trackBar5.Minimum = 5;
            trackBar5.Maximum = 50;
            trackBar5.TickFrequency = 10;
            trackBar5.LargeChange = 5;
            trackBar5.SmallChange = 1;
            trackBar5.Value = trackBar5.Minimum;
        }

        private void create_Click(object sender, EventArgs e)
        {
            n = this.trackBar2.Value;
            m = this.trackBar1.Value;
            numberOfFishes = this.trackBar3.Value;
            numberOfBirds = this.trackBar4.Value;
            numberOfMammals = this.trackBar5.Value;
            Console.WriteLine(n + " " + m);
            enviroment = Enviroment.getInstance(n, m, numberOfFishes, numberOfBirds, numberOfMammals);
            if (Enviroment.N >= Enviroment.M)
            {
                k = width / Enviroment.N;
            }
            else
            {
                k = height / Enviroment.M;
            }
            Draw();
            Thread mythread = new Thread(MyThread);
            mythread.IsBackground = true;
            mythread.Start();
        }

        void MyThread()
        {
            enviroment.Process(this);
        }

        public void Draw()
        {
            Pen myPen = new Pen(SystemColors.Highlight, 2);
            g.DrawRectangle(myPen, new Rectangle(x0 - 5, y0 -5 , Enviroment.M * k + 10, Enviroment.N*k + 10));
            int typeOfCell;
                for (int i = 1; i <= Enviroment.N; i++)
                {
                    for (int j = 1; j <= Enviroment.M; j++)
                    {
                        typeOfCell = Enviroment.GetCellByCoords(i, j).Type;
                        switch (typeOfCell)
                        {
                            case 0:
                                myBrush = new System.Drawing.SolidBrush(System.Drawing.Color.FromArgb(185, 150, 130));
                                //myBrush = new System.Drawing.SolidBrush(System.Drawing.Color.FromArgb(135, 65, 0));
                                g.FillRectangle(myBrush, new Rectangle(x0+(j-1) * k, y0+(i-1) * k, k, k));
                                break;
                            case 1:
                                myBrush = new System.Drawing.SolidBrush(System.Drawing.Color.FromArgb(155, 230, 150));
                                //myBrush = new System.Drawing.SolidBrush(System.Drawing.Color.FromArgb(20, 150, 0));      
                                g.FillRectangle(myBrush, new Rectangle(x0 + (j - 1) * k, y0 + (i - 1) * k, k, k));
                                break;
                            case 2:
                                myBrush = new System.Drawing.SolidBrush(System.Drawing.Color.FromArgb(170, 215, 240));
                                //myBrush = new System.Drawing.SolidBrush(System.Drawing.Color.FromArgb(0, 10, 220));
                                g.FillRectangle(myBrush, new Rectangle(x0 + (j - 1) * k, y0 + (i - 1) * k, k, k));
                                break;
                    }
                    }
                }

            List<Animal> fishes = Enviroment.GetAnimals(0);

            String s = "" + Enviroment.NumberOfFishes;
            myBrush = new System.Drawing.SolidBrush(System.Drawing.Color.WhiteSmoke);
            g.FillRectangle(myBrush, new Rectangle(10, 400, 20, 420));
            g.DrawString(s, aFont, Brushes.Black, 10, 400);
            for (int i = 0; i < Enviroment.NumberOfFishes; i++)
            {
                Fish fish = (Fish)fishes.ElementAt(i);
                int x = fish.PositionX, y = fish.PositionY;
                if(fish.IsPredator) myBrush = new System.Drawing.SolidBrush(System.Drawing.Color.FromArgb(0, 50, 50));
                else myBrush = new System.Drawing.SolidBrush(System.Drawing.Color.FromArgb(0, 0, 50));
                g.FillEllipse(myBrush, x0 + (x-1) * k + 1, y0 + (y-1) * k +1, k/3, k/3);
            }

            List<Animal> birds = Enviroment.GetAnimals(1);

            String s2 = "" + Enviroment.NumberOfBirds;
            myBrush = new System.Drawing.SolidBrush(System.Drawing.Color.WhiteSmoke);
            g.FillRectangle(myBrush, new Rectangle(10, 420, 20, 450));
            g.DrawString(s2, aFont, Brushes.Black, 10, 420);
            for (int i = 0; i < Enviroment.NumberOfBirds; i++)
            {
                Bird bird = (Bird)birds.ElementAt(i);
                int x = bird.PositionX, y = bird.PositionY;
                myBrush = new System.Drawing.SolidBrush(System.Drawing.Color.FromArgb(255, 105, 0));
                g.FillEllipse(myBrush, x0 + (x - 1) * k + k / 3 + 1, y0 + (y - 1) * k + k / 3 + 1, k / 3, k / 3);

            }

            List<Animal> mammals = Enviroment.GetAnimals(2);

            String s3 = "" + Enviroment.NumberOfMammals;
            myBrush = new System.Drawing.SolidBrush(System.Drawing.Color.WhiteSmoke);
            g.FillRectangle(myBrush, new Rectangle(10, 440, 20, 460));
            g.DrawString(s3, aFont, Brushes.Black, 10, 440);
            for (int i = 0; i < Enviroment.NumberOfMammals; i++)
            {
                Mammal mammal = (Mammal)mammals.ElementAt(i);
                int x = mammal.PositionX, y = mammal.PositionY;
                myBrush = new System.Drawing.SolidBrush(System.Drawing.Color.FromArgb(210, 0, 0));
                g.FillEllipse(myBrush, x0 + (x - 1) * k + k / 3 + 1, y0 + (y - 1) * k + 1, k / 3, k / 3);

            }

        }

        private void trackBar1_Scroll(object sender, System.EventArgs e)
        {
            textBox1.Text = "" + trackBar1.Value;
        }
        private void trackBar2_Scroll(object sender, System.EventArgs e)
        {
            textBox2.Text = "" + trackBar2.Value;
        }
        private void trackBar3_Scroll(object sender, System.EventArgs e)
        {
            textBox3.Text = "" + trackBar3.Value;
        }
        private void trackBar4_Scroll(object sender, System.EventArgs e)
        {
            textBox4.Text = "" + trackBar4.Value;
        }
        private void trackBar5_Scroll(object sender, System.EventArgs e)
        {
            textBox5.Text = "" + trackBar5.Value;
        }



    }
}
