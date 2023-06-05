using SudokuProject;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SudokuGUI
{
    public partial class Form1 : Form
    {
        int buttonWidth = 35;
        int buttonHeight = 35;
        
        public static ArrayList timesThread5 = new ArrayList();
        public static ArrayList timesThread10 = new ArrayList();


        Button[][] button = new Button[21][];
        char[][] veriler = DataAccess.Read();

        char[,] sudoku1 = new char[9, 9];
        char[,] sudoku1_n = new char[9, 9];
        char[,] sudoku1_t = new char[9, 9];

        char[,] sudoku2 = new char[9, 9];
        char[,] sudoku2_n = new char[9, 9];
        char[,] sudoku2_t = new char[9, 9];

        char[,] sudoku3 = new char[9, 9];
        char[,] sudoku3_n = new char[9, 9];
        char[,] sudoku3_t = new char[9, 9];

        char[,] sudoku4 = new char[9, 9];
        char[,] sudoku4_n = new char[9, 9];
        char[,] sudoku4_t = new char[9, 9];

        char[,] sudoku5 = new char[9, 9];
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            Control.CheckForIllegalCrossThreadCalls = false;
            Veriler();
            DataAccess.Write();
            Arayuz(buttonHeight,buttonWidth);
        }

        private void Veriler()
        {
            veriler = DataAccess.Read();

            for (int i = 0; i < 9; i++)
            {
                for (int j = 0; j < 9; j++)
                {
                    sudoku1_n[i, j] = veriler[i][j];
                    sudoku1[i, j] = veriler[i][j];
                }
            }

            for (int i = 9; i < 18; i++)
            {
                for (int j = 0; j < 9; j++)
                {
                    sudoku2_n[i % 9, j] = veriler[i + 3][j];
                    sudoku2[i % 9, j] = veriler[i + 3][j];
                }
            }

            for (int i = 0; i < 9; i++)
            {
                for (int j = 9; j < 18; j++)
                {
                    if (i < 6)
                    {
                        sudoku3_n[i % 9, j % 9] = veriler[i][j];
                        sudoku3[i % 9, j % 9] = veriler[i][j];
                    }
                    else
                    {
                        sudoku3_n[i % 9, j % 9] = veriler[i][j + 3];
                        sudoku3[i % 9, j % 9] = veriler[i][j + 3];
                    }

                }
            }

            for (int i = 12; i < 21; i++)
            {
                for (int j = 9; j < 18; j++)
                {
                    if (i >= 15)
                    {
                        sudoku4_n[i - 12, j % 9] = veriler[i][j];
                        sudoku4[i - 12, j % 9] = veriler[i][j];
                    }
                    else
                    {
                        sudoku4_n[i - 12, j % 9] = veriler[i][j + 3];
                        sudoku4[i - 12, j % 9] = veriler[i][j + 3];
                    }

                }
            }

            for (int i = 6; i < 15; i++)
            {
                for (int j = 6; j < 15; j++)
                {
                    if (i < 9 || i >= 12)
                    {
                        sudoku5[i - 6, j - 6] = veriler[i][j];
                    }
                    else
                        sudoku5[i - 6, j - 6] = veriler[i][j - 6];
                }
            }
        }

        void Arayuz(int top, int left)
        {
            for (int i = 0; i < veriler.Length; i++)
            {
                button[i] = new Button[veriler[i].Length];
            }

            for (int i = 0; i <= button.GetUpperBound(0); i++)
            {
                for (int j = 0; j < button[i].Length; j++)
                {
                    button[i][j] = new Button();
                    button[i][j].Font = new Font(button[i][j].Font.FontFamily, 13);
                    button[i][j].BackColor = Color.White;
                    button[i][j].ForeColor = Color.Black;
                    button[i][j].FlatStyle = FlatStyle.Flat;
                    button[i][j].FlatAppearance.BorderColor = Color.Gray;
                    button[i][j].FlatAppearance.BorderSize = 1;
                    if (button[i].Length == 18)
                    {
                        if (j == 9)
                        {
                            left += buttonWidth * 3;
                        }
                        button[i][j].Text = veriler[i][j].ToString();
                        button[i][j].Width = buttonWidth;
                        button[i][j].Height = buttonHeight;
                        button[i][j].Top = top;
                        button[i][j].Left = left;
                        this.Controls.Add(button[i][j]);
                        left += buttonWidth;
                    }

                    else if (button[i].Length == 9)
                    {
                        if (j == 0)
                        {
                            left += 6 * buttonWidth;
                        }
                        button[i][j].Text = veriler[i][j].ToString();
                        button[i][j].Width = buttonWidth;
                        button[i][j].Height = buttonHeight;
                        button[i][j].Top = top;
                        button[i][j].Left = left;
                        this.Controls.Add(button[i][j]);
                        left += buttonWidth;
                    }

                    else
                    {
                        button[i][j].Text = veriler[i][j].ToString();
                        button[i][j].Width = buttonWidth;
                        button[i][j].Height = buttonHeight;
                        button[i][j].Top = top;
                        button[i][j].Left = left;
                        this.Controls.Add(button[i][j]);
                        left += buttonWidth;
                    }
                }
                left = buttonWidth;
                top += buttonHeight;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Solver solver = new Solver();
            solver.SolveForCenter(sudoku5, sudoku1, sudoku2, sudoku3, sudoku4, sudoku1_n, sudoku2_n, sudoku3_n, sudoku4_n);

            for (int i = 0; i < 9; i++)
            {
                for (int j = 0; j < 9; j++)
                {
                    veriler[i][j] = sudoku1[i, j];
                    veriler[i + 12][j] = sudoku2[i, j];
                    if (i < 6)
                    {
                        veriler[i][j + 9] = sudoku3[i, j];
                    }
                    else if (i >= 6 && i < 9)
                    {
                        veriler[i][j + 12] = sudoku3[i, j];
                    }

                    if (i < 3)
                    {
                        veriler[i + 12][j + 12] = sudoku4[i, j];
                    }
                    else if (i >= 3 && i < 9)
                    {
                        veriler[i + 12][j + 9] = sudoku4[i, j];
                    }
                    if (i < 3 || (i >= 6))
                    {
                        veriler[i + 6][j + 6] = sudoku5[i, j];
                    }
                    else
                    {
                        veriler[i + 6][j] = sudoku5[i, j];
                    }
                }
            }

            for (int i = 0; i <= button.GetUpperBound(0); i++)
            {
                for (int j = 0; j < button[i].Length; j++)
                {
                    button[i][j].Text = veriler[i][j].ToString();
                    
                }
            }
            this.Refresh();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Solver solver = new Solver();
            solver.SolveForCenter(sudoku5, sudoku1, sudoku2, sudoku3, sudoku4, sudoku1_n, sudoku2_n, sudoku3_n, sudoku4_n, button, this);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Solver solver = new Solver();
            solver.SolveForCenter(sudoku5, sudoku1, sudoku2, sudoku3, sudoku4, sudoku1_n, sudoku2_n, sudoku3_n, sudoku4_n,sudoku1_t, sudoku2_t, sudoku3_t, sudoku4_t , button, this);

        }

        private void button4_Click(object sender, EventArgs e)
        {
            Solver solver = new Solver();
            Solver.stopwatch.Start();
            solver.SolveForCenterG(sudoku5, sudoku1, sudoku2, sudoku3, sudoku4, sudoku1_n, sudoku2_n, sudoku3_n, sudoku4_n);
            Solver.stopwatch.Stop();

            for (int i = 0; i < Solver.times.Count; i++)
            {
                if(Solver.times[i] != null)
                {
                    timesThread5.Add(Solver.times[i]);
                }
            }

            Solver.times.Clear();
            for (int i = 0; i < 9; i++)
            {
                for (int j = 0; j < 9; j++)
                {
                    veriler[i][j] = sudoku1[i, j];
                    veriler[i + 12][j] = sudoku2[i, j];
                    if (i < 6)
                    {
                        veriler[i][j + 9] = sudoku3[i, j];
                    }
                    else if (i >= 6 && i < 9)
                    {
                        veriler[i][j + 12] = sudoku3[i, j];
                    }

                    if (i < 3)
                    {
                        veriler[i + 12][j + 12] = sudoku4[i, j];
                    }
                    else if (i >= 3 && i < 9)
                    {
                        veriler[i + 12][j + 9] = sudoku4[i, j];
                    }
                    if (i < 3 || (i >= 6))
                    {
                        veriler[i + 6][j + 6] = sudoku5[i, j];
                    }
                    else
                    {
                        veriler[i + 6][j] = sudoku5[i, j];
                    }
                }
            }

            for (int i = 0; i <= button.GetUpperBound(0); i++)
            {
                for (int j = 0; j < button[i].Length; j++)
                {
                    button[i][j].Text = veriler[i][j].ToString();

                }
            }

            this.Refresh();
            Thread.Sleep(1000);
           
            Veriler();
            for (int i = 0; i <= button.GetUpperBound(0); i++)
            {
                for (int j = 0; j < button[i].Length; j++)
                {
                    button[i][j].Text = veriler[i][j].ToString();

                }
            }
            this.Refresh();
            Thread.Sleep(1000);

            Solver.stopwatch.Reset();

            Solver.stopwatch.Start();
            solver.SolveForCenterG(sudoku5, sudoku1, sudoku2, sudoku3, sudoku4, sudoku1_n, sudoku2_n, sudoku3_n, sudoku4_n, sudoku1_t, sudoku2_t, sudoku3_t, sudoku4_t);
            Solver.stopwatch.Stop();

            for (int i = 0; i < Solver.times.Count; i++)
            {
                if (Solver.times[i] != null)
                {
                    timesThread10.Add(Solver.times[i]);
                }
            }

            for (int i = 0; i < 9; i++)
            {
                for (int j = 0; j < 9; j++)
                {
                    veriler[i][j] = sudoku1[i, j];
                    veriler[i + 12][j] = sudoku2[i, j];
                    if (i < 6)
                    {
                        veriler[i][j + 9] = sudoku3[i, j];
                    }
                    else if (i >= 6 && i < 9)
                    {
                        veriler[i][j + 12] = sudoku3[i, j];
                    }

                    if (i < 3)
                    {
                        veriler[i + 12][j + 12] = sudoku4[i, j];
                    }
                    else if (i >= 3 && i < 9)
                    {
                        veriler[i + 12][j + 9] = sudoku4[i, j];
                    }
                    if (i < 3 || (i >= 6))
                    {
                        veriler[i + 6][j + 6] = sudoku5[i, j];
                    }
                    else
                    {
                        veriler[i + 6][j] = sudoku5[i, j];
                    }
                }
            }

            for (int i = 0; i <= button.GetUpperBound(0); i++)
            {
                for (int j = 0; j < button[i].Length; j++)
                {
                    button[i][j].Text = veriler[i][j].ToString();

                }
            }

            this.Refresh();
            Thread.Sleep(1000);

            Form2 form2 = new Form2();
            form2.Show();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            Veriler();
            for (int i = 0; i <= button.GetUpperBound(0); i++)
            {
                for (int j = 0; j < button[i].Length; j++)
                {
                    button[i][j].Text = veriler[i][j].ToString();

                }
            }
            this.Refresh();
        }
    }
}
