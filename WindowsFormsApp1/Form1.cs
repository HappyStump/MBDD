using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics.Eventing.Reader;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            Rectangle rectangle = Screen.PrimaryScreen.Bounds;
            map = new Bitmap(rectangle.Width, rectangle.Height);
            graphics = Graphics.FromImage(map);

            map1 = new Bitmap(rectangle.Width, rectangle.Height);
            graphics1 = Graphics.FromImage(map1);
            Start();
        }

        Bitmap map = new Bitmap(100, 100);
        Graphics graphics;

        Bitmap map1 = new Bitmap(100, 100);
        Graphics graphics1;

        int ship = 1;
        bool poz = true;
        bool play = false;
        bool turn = true;
        bool can = true;
        bool endgame = false;
        bool bot = false;
        bool cat = false;
        bool kill = false;
        bool kakHochesh = true;

        int shotPlan = 0;
        int shotX = 0;
        int shotY = 0;
        int shotLastX = 0;
        int shotLastY = 0;
        


        int[] shipsarray = { 4, 3, 2, 1 };
        int[] shipsarray2 = { 4, 3, 2, 1 };

        
        int[,] box1 = new int[12, 12];
        
        int[,] box2 = new int[12, 12];

        int[] boxBot = new int[100];

        int[,] array = new int[10, 5];
        int[,] array2 = new int[10, 5];



        Pen pen = new Pen(Color.Black, 3f);
        Pen pen1 = new Pen(Color.Green, 3f);
        Pen pen2 = new Pen(Color.Gray, 3f);
        Pen pen3 = new Pen(Color.Red, 3f);
        Pen pen4 = new Pen(Color.Blue, 3f);

        private void Start()
        {
            for (int i = 0; i < 100; i++)
            {
                boxBot[i] = i;
            }
            for(int i = 0; i < 12; i++)
            {
                for(int j = 0; j < 12; j++)
                {
                    box1[i, j] = 0;
                    box2[i, j] = 0;
                }
            }
            /*box1[1, 1] = 1;
            box1[1, 2] = 2;
            box1[1, 3] = 3;
            box1[1, 4] = 4;
            box2[1, 1] = 1;
            box2[1, 2] = 2;
            box2[1, 3] = 3;
            box2[1, 4] = 4;*/
           
            for (int x = 0; x<= 400; x += 40)
            {
                graphics.DrawLine(pen, x, 0, x, 400);
            }

            for (int y = 0; y<= 400; y += 40)
            {
                graphics.DrawLine(pen, 0, y, 400, y);
            }
            pictureBox1.Image = map;

            for (int x = 0; x <= 400; x += 40)
            {
                graphics1.DrawLine(pen, x, 0, x, 400);
            }

            for (int y = 0; y <= 400; y += 40)
            {
                graphics1.DrawLine(pen, 0, y, 400, y);
            }
            pictureBox2.Image = map1;
            
        }

        private void pictureBox1_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            //Task.Delay(300).Wait();
            for (int i = 1; i < 11; i++)
            {
                for (int j = 1; j < 11; j++)
                {
                    if (box1[i, j] == 1 && kakHochesh && (turn || bot)) //Корабль
                    {
                        g.DrawRectangle(pen1, (i - 1) * 40 + 5, (j - 1) * 40 + 5, 30, 30);
                        pictureBox1.Image = map;
                    }
                    if (box1[i, j] == 2 && kakHochesh && (turn || bot))//вокруг корабля
                    {
                        g.DrawRectangle(pen2, (i - 1) * 40 + 5, (j - 1) * 40 + 5, 30, 30);
                        pictureBox1.Image = map;
                    }
                    if (box1[i, j] == 3)//мимо
                    {
                        g.DrawEllipse(pen, (i - 1) * 40 + 18, (j - 1) * 40 + 18, 4, 4);
                        pictureBox1.Image = map;
                    }
                    if (box1[i, j] == 4)//попадание
                    {
                        g.DrawRectangle(pen1, (i - 1) * 40 + 5, (j - 1) * 40 + 5, 30, 30);
                        g.DrawLine(pen3, (i - 1) * 40, (j - 1) * 40, i * 40, j * 40);
                        g.DrawLine(pen3, i * 40, (j - 1) * 40, (i - 1) * 40, j * 40);
                        pictureBox1.Image = map;
                    }
                    if (box1[i, j] == 5)//корабль уничтожен
                    {
                        g.DrawRectangle(pen1, (i - 1) * 40 + 5, (j - 1) * 40 + 5, 30, 30);
                        g.DrawLine(pen4, (i - 1) * 40, (j - 1) * 40, i * 40, j * 40);
                        g.DrawLine(pen4, i * 40, (j - 1) * 40, (i - 1) * 40, j * 40);
                        pictureBox1.Image = map;
                    }
                }
            }
            //g.Clear(pictureBox1.BackColor);
            //pictureBox1.Image = map;
        }

        private void pictureBox1_MouseUp(object sender, MouseEventArgs e)
        {

        }

        private bool Check1()
        {
            bool flag = true;

            for(int i = 0; i < 12; i++)
            {
                for (int j = 0; j < 12; j++)
                {
                    if (box1[i, j] == 1)
                    {
                        return false;
                    }
                }
            }
            return flag;
        }

        private bool Check2()
        {
            bool flag = true;

            for (int i = 0; i < 12; i++)
            {
                for (int j = 0; j < 12; j++)
                {
                    if (box2[i, j] == 1)
                    {
                        return false;
                    }
                }
            }
            return flag;
        }

        private void pictureBox1_MouseDown(object sender, MouseEventArgs e)
        {
            int x = e.X / 40 + 1;
            int y = e.Y / 40 + 1;

            bool flag = true;

            if (play)
            {
                if (can && !turn && !endgame && !bot)
                {
                    
                    if (box1[x, y] == 0 || box1[x, y] == 2)
                    {
                        box1[x, y] = 3;
                        can = false;
                    }
                    if (box1[x, y] == 1)
                    {
                        box1[x, y] = 4;
                    }

                    bool broke = true;
                    //check if ship are gone in paint
                    for (int i = 0; i < 10; i++)
                    {
                        broke = true;
                        //delete ship
                        for (int n = array[i, 0]; n <= array[i, 2]; n++)
                        {
                            for (int m = array[i, 1]; m <= array[i, 3]; m++)
                            {
                                if (box1[n, m] != 4)
                                {
                                    broke = false;
                                    break;
                                }
                            }
                        }

                        if (broke)
                        {
                            //delete ship
                            for (int n = array[i, 0]; n <= array[i, 2]; n++)
                            {
                                for (int m = array[i, 1]; m <= array[i, 3]; m++)
                                {
                                    box1[n, m] = 5; //ship are gone
                                    pictureBox1.Refresh();
                                }
                            }
                            pictureBox1.Refresh();
                            break;
                        }
                    }

                    if (Check1())
                    {
                        endgame = true;
                        label1.Text = "Игрок 2 победил!";
                    }

                    pictureBox1.Refresh();
                    Task.Delay(300).Wait();
                }
                else
                {
                    label1.Text = "Вы не можете стрелять в данный момент";
                    //MessageBox.Show("Вы не можете стрелять в данный момент");
                }
                pictureBox1.Refresh();
            }
            else if(turn)
            { 
                if (poz)
                {
                    if (x + ship > 11)
                    {
                        flag = false;

                    }
                    else
                    {
                        for (int i = x; i < x + ship; i++)
                        {
                            if (box1[i, y] != 0)
                            {
                                flag = false;
                                break;
                            }
                        }
                    }


                }
                else
                {
                    if (y + ship > 11)
                    {
                        flag = false;

                    }
                    else
                    {
                        for (int i = y; i < y + ship; i++)
                        {
                            if (box1[x, i] != 0)
                            {
                                flag = false;
                                break;
                            }
                        }
                    }

                }


                if (flag && shipsarray[ship - 1] > 0)
                {

                    int vv = 0;
                    if (ship == 1) { vv = 4; }
                    if (ship == 2) { vv = 7; }
                    if (ship == 3) { vv = 9; }
                    if (ship == 4) { vv = 10; }

                    array[vv - shipsarray[ship - 1], 0] = x;
                    array[vv - shipsarray[ship - 1], 1] = y;

                    if (poz)
                    {
                        array[vv - shipsarray[ship - 1], 2] = x + ship - 1;
                        array[vv - shipsarray[ship - 1], 3] = y;
                        array[vv - shipsarray[ship - 1], 4] = 1;
                    }

                    else
                    {
                        array[vv - shipsarray[ship - 1], 2] = x;
                        array[vv - shipsarray[ship - 1], 3] = y + ship - 1;
                        array[vv - shipsarray[ship - 1], 4] = 1;
                    }

                    shipsarray[ship - 1] -= 1;

                    if (poz)
                    {
                        for (int i = x; i < x + ship; i++)
                        {
                            box1[i, y] = 1;
                        }

                        for (int i = x - 1; i <= x + ship; i++)
                        {
                            for (int j = y - 1; j <= y + 1; j++)
                            {
                                if (box1[i, j] != 1)
                                {
                                    box1[i, j] = 2;
                                }
                            }
                        }
                        pictureBox1.Refresh();
                    }
                    else
                    {
                        for (int i = y; i < y + ship; i++)
                        {
                            box1[x, i] = 1;
                        }

                        for (int i = x - 1; i <= x + 1; i++)
                        {
                            for (int j = y - 1; j <= y + ship; j++)
                            {
                                if (box1[i, j] != 1)
                                {
                                    box1[i, j] = 2;
                                }
                            }
                        }
                        pictureBox1.Refresh();
                        
                    }
                    pictureBox1.Refresh();
                }
                else
                {
                    label1.Text = "Вы не можете поставить корабль";
                    //MessageBox.Show("Вы не можете поставить корабль");
                }
            }
        }

        private void pictureBox2_Paint(object sender, PaintEventArgs e)
        {
            Graphics g2 = e.Graphics;
            for (int i = 1; i < 11; i++)
            {
                for (int j = 1; j < 11; j++)
                {
                    if (box2[i, j] == 1 && kakHochesh && !turn) //Корабль
                    {
                        g2.DrawRectangle(pen1, (i - 1) * 40 + 5, (j - 1) * 40 + 5, 30, 30);
                        pictureBox2.Image = map1;
                    }
                    if (box2[i, j] == 2 && kakHochesh && !turn)//вокруг корабля
                    {
                        g2.DrawRectangle(pen2, (i - 1) * 40 + 5, (j - 1) * 40 + 5, 30, 30);
                        pictureBox2.Image = map1;
                    }
                    if (box2[i, j] == 3)//мимо
                    {
                        g2.DrawEllipse(pen, (i - 1) * 40 + 18, (j - 1) * 40 + 18, 4, 4);
                        pictureBox2.Image = map1;
                    }
                    if (box2[i, j] == 4)//попадание
                    {
                        g2.DrawRectangle(pen1, (i - 1) * 40 + 5, (j - 1) * 40 + 5, 30, 30);
                        g2.DrawLine(pen3, (i - 1) * 40, (j - 1) * 40, i * 40, j * 40);
                        g2.DrawLine(pen3, i * 40, (j - 1) * 40, (i - 1) * 40, j * 40);
                        pictureBox2.Image = map1;
                    }
                    if (box2[i, j] == 5)//корабль уничтожен
                    {
                        g2.DrawRectangle(pen1, (i - 1) * 40 + 5, (j - 1) * 40 + 5, 30, 30);
                        g2.DrawLine(pen4, (i - 1) * 40, (j - 1) * 40, i * 40, j * 40);
                        g2.DrawLine(pen4, i * 40, (j - 1) * 40, (i - 1) * 40, j * 40);
                        pictureBox2.Image = map1;
                    }
                }
            }
        }

        private void pictureBox2_MouseUp(object sender, MouseEventArgs e)
        {

        }

        private void pictureBox2_MouseDown(object sender, MouseEventArgs e)
        {
            
            int x = e.X / 40 + 1;
            int y = e.Y / 40 + 1;


            bool flag = true;

            if (play)
            {
                if (turn && can && !endgame)
                {
                    if (box2[x, y] == 0 || box2[x, y] == 2)
                    {
                        box2[x, y] = 3;
                        can = false;
                    }
                    if (box2[x, y] == 1)
                    {
                        box2[x, y] = 4;
                    }

                    bool broke = true;
                    //check if ship are gone in paint
                    for (int i = 0; i < 10; i++)
                    {
                        broke = true;
                        //delete ship
                        for (int n = array2[i, 0]; n <= array2[i, 2]; n++)
                        {
                            for (int m = array2[i, 1]; m <= array2[i, 3]; m++)
                            {
                                if (box2[n, m] != 4)
                                {
                                    broke = false;
                                    break;
                                }
                            }
                        }

                        if (broke)
                        {
                            //delete ship
                            for (int n = array2[i, 0]; n <= array2[i, 2]; n++)
                            {
                                for (int m = array2[i, 1]; m <= array2[i, 3]; m++)
                                {
                                    box2[n, m] = 5; //ship are gone
                                    pictureBox2.Refresh();
                                }
                            }
                            pictureBox2.Refresh();
                            break;
                        }
                    }

                    if (Check2())
                    {
                        endgame = true;
                        label1.Text = "Игрок 1 победил!";
                    }
                    pictureBox2.Refresh();
                    Task.Delay(300).Wait();
                }
                else
                {
                    label1.Text = "Вы не можете стрелять в данный момент";
                    //MessageBox.Show("Вы не можете стрелять в данный момент");
                }
                pictureBox2.Refresh();
            }
            else if(!turn)
            {
                if (poz)
                {
                    if (x + ship > 11)
                    {
                        flag = false;
                    }
                    else
                    {
                        for (int i = x; i < x + ship; i++)
                        {
                            if (box2[i, y] != 0)
                            {
                                flag = false;
                                break;
                            }
                        }
                    }
                }
                else
                {
                    if (y + ship > 11)
                    {
                        flag = false;

                    }
                    else
                    {
                        for (int i = y; i < y + ship; i++)
                        {
                            if (box2[x, i] != 0)
                            {
                                flag = false;
                                break;
                            }
                        }
                    }
                }


                if (flag && shipsarray2[ship - 1] > 0)
                {

                    int vv = 0;
                    if (ship == 1) { vv = 4; }
                    if (ship == 2) { vv = 7; }
                    if (ship == 3) { vv = 9; }
                    if (ship == 4) { vv = 10; }

                    array2[vv - shipsarray2[ship - 1], 0] = x;
                    array2[vv - shipsarray2[ship - 1], 1] = y;

                    if (poz)
                    {
                        array2[vv - shipsarray2[ship - 1], 2] = x + ship - 1;
                        array2[vv - shipsarray2[ship - 1], 3] = y;
                        array2[vv - shipsarray2[ship - 1], 4] = 1;
                    }

                    else
                    {
                        array2[vv - shipsarray2[ship - 1], 2] = x;
                        array2[vv - shipsarray2[ship - 1], 3] = y + ship - 1;
                        array2[vv - shipsarray2[ship - 1], 4] = 1;
                    }

                    shipsarray2[ship - 1] -= 1;

                    if (poz)
                    {
                        for (int i = x; i < x + ship; i++)
                        {
                            box2[i, y] = 1;
                        }

                        for (int i = x - 1; i <= x + ship; i++)
                        {
                            for (int j = y - 1; j <= y + 1; j++)
                            {
                                if (box2[i, j] != 1)
                                {
                                    box2[i, j] = 2;
                                }
                            }
                        }
                        pictureBox2.Refresh();
                    }
                    else
                    {
                        for (int i = y; i < y + ship; i++)
                        {
                            box2[x, i] = 1;
                        }

                        for (int i = x - 1; i <= x + 1; i++)
                        {
                            for (int j = y - 1; j <= y + ship; j++)
                            {
                                if (box2[i, j] != 1)
                                {
                                    box2[i, j] = 2;
                                }
                            }
                        }
                        pictureBox2.Refresh();
                    }
                    pictureBox2.Refresh();
                }
                else
                {
                    label1.Text = "Вы не можете поставить корабль";
                    //MessageBox.Show("Вы не можете поставить корабль");
                }
            }
            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            ship = 1;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            ship = 2;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            ship = 3;
        }

        private void button4_Click(object sender, EventArgs e)
        {
            ship = 4;
        }

        private void button9_Click(object sender, EventArgs e)
        {
            poz = !poz;
        }

        private void button8_Click(object sender, EventArgs e)
        {
            label1.Text = " ";
            if (shipsarray.Sum() == 0 && shipsarray2.Sum() == 0)
            {
                play = true;
                Task.Delay(100);
                label1.Text = "Игра началась";
                turn = true;
            }
            else
            {
                label1.Text = "Не все корабли расставлены";
                //MessageBox.Show("Не все корабли расставлены");
            }
            
        }

        private void button7_Click(object sender, EventArgs e)
        {
            if (shipsarray.Sum() == 0)
            {
                for (int i = 0; i < 12; i++)
                {
                    for (int j = 0; j < 12; j++)
                    {
                        box2[i, j] = 0;
                    }
                }
                shipsarray2[3] = 0;
                shipsarray2[2] = 0;
                shipsarray2[1] = 0;
                shipsarray2[0] = 0;
                RandomShipPlacement2(4, 9);
                RandomShipPlacement2(3, 8);
                RandomShipPlacement2(3, 7);
                RandomShipPlacement2(2, 6);
                RandomShipPlacement2(2, 5);
                RandomShipPlacement2(2, 4);
                RandomShipPlacement2(1, 3);
                RandomShipPlacement2(1, 2);
                RandomShipPlacement2(1, 1);
                RandomShipPlacement2(1, 0);

                play = true;
                bot = true;
                Task.Delay(1000);
                label1.Text = "Игра началась";
                turn = true;
                for (int i = 0; i < 12; i++)
                {
                    for (int j = 0; j < 12; j++)
                    {
                        if (i == 0 || j == 0 || i == 11 || j == 11)
                        {
                            box1[i, j] = 3;
                        }
                    }
                }
            }
            else
            {
                label1.Text = "Не все корабли расставлены";
                //MessageBox.Show("Не все корабли расставлены");
            }
        }

        private void button6_Click(object sender, EventArgs e)
        {
            can = true;
            if(!bot)
            {
                kakHochesh = false;
                pictureBox1.Refresh();
                pictureBox2.Refresh();
                turn = !turn;
                Task.Delay(1000).Wait();
                kakHochesh = true;
                if (turn)
                {
                    label1.Text = "Ход игрока 1";
                }
                else
                {
                    label1.Text = "Ход игрока 2";
                }
                
            }
            else
            {
                label1.Text = "Ход игрока 2";
                BotPlay();
            }
            
            
            pictureBox1.Refresh();
            pictureBox2.Refresh();

        }

        private void button5_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < 12; i++)
            {
                for (int j = 0; j < 12; j++)
                {
                    box1[i, j] = 0;
                }
            }
            shipsarray[3] = 0;
            shipsarray[2] = 0;
            shipsarray[1] = 0;
            shipsarray[0] = 0;
            RandomShipPlacement(4, 9);
            RandomShipPlacement(3, 8);
            RandomShipPlacement(3, 7);
            RandomShipPlacement(2, 6);
            RandomShipPlacement(2, 5);
            RandomShipPlacement(2, 4);
            RandomShipPlacement(1, 3); 
            RandomShipPlacement(1, 2);
            RandomShipPlacement(1, 1);
            RandomShipPlacement(1, 0);
        }

        private void BotPlay()
        {
            while(true)
            {
                pictureBox1.Refresh();
                //Task.Delay(1000).Wait();
                if (Check1())
                {
                    endgame = true;
                    label1.Text = "Игрок 2 победил!";
                    return;
                }
                Random random1 = new Random();
                if (shotPlan == 0)
                {
                    shotX = random1.Next(10) + 1;
                    shotY = random1.Next(10) + 1;
                    //int r = random1.Next(100);
                    //shotX = r / 10 + 1;
                    //shotY = r % 10 + 1;
                    /* int[] numbers = { 1, 3, 4, 9, 2 };
                    var numbersList = numbers.ToList();
                    numbersList.Remove(4);
                    var numbers1 = numbersList.ToArray();*/

                    if (box1[shotX, shotY] == 1)
                    {
                        if (box1[shotX, shotY - 1] != 1 && box1[shotX - 1, shotY] != 1 && box1[shotX, shotY + 1] != 1 && box1[shotX + 1, shotY] != 1) // однопалубник
                        {
                            box1[shotX, shotY] = 4;
                            shotPlan = 0;  
                            continue;
                        }
                        else
                        {
                            box1[shotX, shotY] = 4;
                            shotPlan = 1;
                            continue;
                        }
                        
                    }
                    else if (box1[shotX, shotY] != 3 && box1[shotX, shotY] != 4)
                    {
                        bool bigBob = true;
                        for(int i = shotX - 1; i <= shotX + 1; i++)
                        {
                            for (int j = shotY - 1; j <= shotY + 1; j++)
                            {
                                if(box1[i, j] == 4)
                                {
                                    bigBob = false;
                                }
                            }
                        }
                        if(bigBob)
                        {
                            box1[shotX, shotY] = 3;
                            break;
                        }
                        else
                        {
                            continue;
                        }
                    }
                    else
                    {
                        continue;
                    }
                }

                if (shotPlan > 0)
                {
                    int newX = shotX;
                    int newY = shotY;
                    if (shotPlan == 1) { newY--; }
                    if (shotPlan == 2) { newX--; }
                    if (shotPlan == 3) { newY++; }
                    if (shotPlan == 4) { newX++; }


                    bool damage = box1[newX, newY] == 1; // попал
                    bool wallmiss = box1[newX, newY] == 3; // есть стена(и *)

                    if (!cat && !kill) //нахождение направления корабля
                    {
                        if (damage) // попал
                        {
                            cat = true;
                            box1[newX, newY] = 4;
                            //сохранение точки начала
                            shotLastX = shotX;
                            shotLastY = shotY;
                            // data to next cycle
                            shotX = newX;
                            shotY = newY;
                            continue;
                        }
                        else //есть стена(и *) или мимо
                        {
                            if (shotPlan < 4) { shotPlan++; }
                            else if (shotPlan == 4) { shotPlan = 1; }
                            box1[newX, newY] = 3;
                            if (wallmiss) { continue; } //стена
                            else { break; } //нет стены
                        }
                    }
                    else if (!damage) //не попал
                    {
                        if (!kill) //найдено направление
                        {
                            kill = true;
                            if (shotPlan == 1) { shotPlan = 3; }
                            else if (shotPlan == 2) { shotPlan = 4; }
                            else if (shotPlan == 3) { shotPlan = 1; }
                            else if (shotPlan == 4) { shotPlan = 2; }
                            
                            
                            int shittyX = shotLastX;
                            int shittyY = shotLastY;
                            if (shotPlan == 1) { shittyY--; }
                            if (shotPlan == 2) { shittyX--; }
                            if (shotPlan == 3) { shittyY++; }
                            if (shotPlan == 4) { shittyX++; }

                            if (box1[shittyX, shittyY] != 1)
                            {
                                cat = false;
                                kill = false;
                                shotPlan = 0;
                                continue;
                            }
                            else
                            {
                                box1[newX, newY] = 3;
                                shotX = shotLastX;
                                shotY = shotLastY;
                                if (wallmiss) { continue; } //стена
                                else { break; } //нет стены
                            }
                            
                        }
                        else //найден конец
                        {
                            shotPlan = 0; //убил корабль
                            cat = false;
                            kill = false;
                            continue;
                        }
                    }
                    else //попал
                    {
                        box1[newX, newY] = 4;
                        // data to next cycle
                        shotX = newX;
                        shotY = newY;
                        continue;
                    }
                }

                //В конце после while true и в начале файл тру pic refresh
                //All wall need to be miss(3)

                 
            }
            pictureBox1.Refresh();
            label1.Text = "Ход игрока 1";
            
            //turn = !turn;
        }

        private void RandomShipPlacement(int size, int key)
        {
            Random random = new Random();
            bool edit = true;
            Point p; // координата носа
            Point t; // определяем ориентацию корабля
            do
            {
                edit = true; // смогли поставить корабль
                if (random.Next(0, 2) == 0) t = new Point(1, 0); // горизонтально
                else t = new Point(0, 1); // вертикально
                p = new Point(random.Next(1, 11 - t.X * (size - 1)),
                              random.Next(1, 11 - t.Y * (size - 1)));
                for (var i = - 1; i < size + 1; i++)
                {
                    if (
                        box1[i * t.X + p.X, i * t.Y + p.Y] == 1 ||
                        box1[i * t.X + p.X + t.Y, i * t.Y + p.Y + t.X] == 1 ||
                        box1[i * t.X + p.X - t.Y, i * t.Y + p.Y - t.X] == 1
                    ) // проверка соседних клеток
                    {
                        edit = false; // не смогли поставить корабль
                        break;
                    }
                }
            } while (!edit);

            array[key, 0] = p.X;
            array[key, 1] = p.Y;
            array[key, 2] = p.X + (size - 1) * t.X;
            array[key, 3] = p.Y + (size - 1) * t.Y;
            array[key, 4] = 1;

            for (var i = - 1; i < size + 1; i++) // установка корабля
            {
                box1[i * t.X + p.X, i * t.Y + p.Y] = 2;
                box1[i * t.X + p.X + t.Y, i * t.Y + p.Y + t.X] = 2;
                box1[i * t.X + p.X - t.Y, i * t.Y + p.Y - t.X] = 2;

            }

            for (var i = 0; i < size; i++) // установка корабля
            {
                box1[i * t.X + p.X, i * t.Y + p.Y] = 1;

            }
            pictureBox1.Refresh();

        }

        private void RandomShipPlacement2(int size, int key)
        {
            Random random = new Random();
            bool edit = true;
            Point p; // координата носа
            Point t; // определяем ориентацию корабля
            do
            {
                edit = true; // смогли поставить корабль
                if (random.Next(0, 2) == 0) t = new Point(1, 0); // горизонтально
                else t = new Point(0, 1); // вертикально
                p = new Point(random.Next(1, 11 - t.X * (size - 1)),
                              random.Next(1, 11 - t.Y * (size - 1)));
                for (var i = -1; i < size + 1; i++)
                {
                    if (
                        box2[i * t.X + p.X, i * t.Y + p.Y] == 1 ||
                        box2[i * t.X + p.X + t.Y, i * t.Y + p.Y + t.X] == 1 ||
                        box2[i * t.X + p.X - t.Y, i * t.Y + p.Y - t.X] == 1
                    ) // проверка соседних клеток
                    {
                        edit = false; // не смогли поставить корабль
                        break;
                    }
                }
            } while (!edit);

            array2[key, 0] = p.X;
            array2[key, 1] = p.Y;
            array2[key, 2] = p.X + (size - 1) * t.X;
            array2[key, 3] = p.Y + (size - 1) * t.Y;
            array2[key, 4] = 1;

            for (var i = -1; i < size + 1; i++) // установка корабля
            {
                box2[i * t.X + p.X, i * t.Y + p.Y] = 2;
                box2[i * t.X + p.X + t.Y, i * t.Y + p.Y + t.X] = 2;
                box2[i * t.X + p.X - t.Y, i * t.Y + p.Y - t.X] = 2;

            }

            for (var i = 0; i < size; i++) // установка корабля
            {
                box2[i * t.X + p.X, i * t.Y + p.Y] = 1;

            }
            pictureBox2.Refresh();

        }

        private void button10_Click(object sender, EventArgs e)
        {
            for(int i = 0; i < 12;i++)
            {
                for(int j = 0; j < 12 ; j++)
                {
                    box2[i, j] = 0;
                }
            }
            shipsarray2[3] = 0;
            shipsarray2[2] = 0;
            shipsarray2[1] = 0;
            shipsarray2[0] = 0;
            RandomShipPlacement2(4, 9);
            RandomShipPlacement2(3, 8);
            RandomShipPlacement2(3, 7);
            RandomShipPlacement2(2, 6);
            RandomShipPlacement2(2, 5);
            RandomShipPlacement2(2, 4);
            RandomShipPlacement2(1, 3);
            RandomShipPlacement2(1, 2);
            RandomShipPlacement2(1, 1);
            RandomShipPlacement2(1, 0);
        }

        private void label3_Click(object sender, EventArgs e)
        {

        }
    }
}
