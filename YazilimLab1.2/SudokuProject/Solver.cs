using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SudokuProject
{

    public class Solver
    {
        private Object thisLock = new Object();
        public static ArrayList times = new ArrayList();
        static public Stopwatch stopwatch = new Stopwatch();
        public bool isValid(char[,] board, int row, int col, char c)
        {
            for (int i = 0; i < 9; i++)
            {
                //check row  
                if (board[i, col] != ' ' && board[i, col] == c)
                    return false;
                //check column  
                if (board[row, i] != ' ' && board[row, i] == c)
                    return false;
                //check 3*3 block  
                if (board[3 * (row / 3) + i / 3, 3 * (col / 3) + i % 3] != ' ' && board[3 * (row / 3) + i / 3, 3 * (col / 3) + i % 3] == c)
                    return false;
            }
            return true;
        }

        public bool isValidForCenter(char[,] board, int row, int col, char c, char[,] sudoku1, char[,] sudoku2, char[,] sudoku3, char[,] sudoku4)
        {
            for (int i = 0; i < 9; i++)
            {
                //check row  
                if (board[i, col] != ' ' && board[i, col] == c)
                    return false;
                //check column  
                if (board[row, i] != ' ' && board[row, i] == c)
                    return false;
                //check 3*3 block  
                if (board[3 * (row / 3) + i / 3, 3 * (col / 3) + i % 3] != ' ' && board[3 * (row / 3) + i / 3, 3 * (col / 3) + i % 3] == c)
                    return false;

                if (row < 3 && col < 3) //Sudoku 1 kontrol
                {
                    if (sudoku1[i, col + 6] != ' ' && sudoku1[i, col + 6] == c)
                    {
                        return false;
                    }

                    if (sudoku1[row + 6, i] != ' ' && sudoku1[row + 6, i] == c)
                    {
                        return false;
                    }
                }

                if (row >= 6 && col < 3)//Sudoku 2 kontrol
                {
                    if (sudoku2[i, col + 6] != ' ' && sudoku2[i, col + 6] == c)
                    {
                        return false;
                    }

                    if (sudoku2[row % 6, i] != ' ' && sudoku2[row % 6, i] == c)
                    {
                        return false;
                    }
                }

                if (row < 3 && col >= 6)//Sudoku 3 kontrol
                {
                    if (sudoku3[i, col - 6] != ' ' && sudoku3[i, col - 6] == c)
                    {
                        return false;
                    }

                    if (sudoku3[row + 6, i] != ' ' && sudoku3[row + 6, i] == c)
                    {
                        return false;
                    }
                }

                if (row >= 6 && col >= 6)//Sudoku 4 kontrol
                {
                    if (sudoku4[i, col % 6] != ' ' && sudoku4[i, col % 6] == c)
                    {
                        return false;
                    }

                    if (sudoku4[row % 6, i] != ' ' && sudoku4[row % 6, i] == c)
                    {
                        return false;
                    }
                }

            }

            return true;
        }

        public void Print(char[,] board)
        {
            for (int i = 0; i <= board.GetUpperBound(0); i++)
            {
                for (int j = 0; j <= board.GetUpperBound(1); j++)
                {
                    Console.Write(board[i, j] + " ");
                }
                Console.WriteLine();

            }
        }
        public bool Solve(char[,] board, Form form, Button[][] button, int indis)
        {
            for (int i = 0; i < 9; i++)
            {
                for (int j = 0; j < 9; j++)
                {
                    if (board[i, j] == ' ')
                    {
                        for (char c = '1'; c <= '9'; c++)
                        {
                            if (isValid(board, i, j, c))
                            {

                                board[i, j] = c;

                                if (indis == 1)
                                {
                                    DataAccess.streamWriter.WriteLine("Sudoku 1'in " + i + ". satirin " + j + ". sutununa " + c + " degeri eklendi.");
                                    button[i][j].Text = c.ToString();
                                }

                                else if (indis == 2)
                                {
                                    DataAccess.streamWriter.WriteLine("Sudoku 2'nin " + i + ". satirin " + j + ". sutununa " + c + " degeri eklendi.");
                                    button[i + 12][j].Text = c.ToString();
                                }

                                else if (indis == 3)
                                {
                                    if (i >= 6 && i < 9)
                                    {
                                        DataAccess.streamWriter.WriteLine("Sudoku 3'ün " + i + ". satirin " + j + ". sutununa " + c + " degeri eklendi.");
                                        button[i][j + 12].Text = c.ToString();
                                    }
                                    else
                                    {
                                        DataAccess.streamWriter.WriteLine("Sudoku 3'ün " + i + ". satirin " + j + ". sutununa " + c + " degeri eklendi.");
                                        button[i][j + 9].Text = c.ToString();
                                    }
                                }

                                else if (indis == 4)
                                {
                                    if (i >= 0 && i < 3)
                                    {
                                        DataAccess.streamWriter.WriteLine("Sudoku 4'ün " + i + ". satirin " + j + ". sutununa " + c + " degeri eklendi.");
                                        button[i + 12][j + 12].Text = c.ToString();
                                    }

                                    else
                                    {
                                        DataAccess.streamWriter.WriteLine("Sudoku 4'ün " + i + ". satirin " + j + ". sutununa " + c + " degeri eklendi.");
                                        button[i + 12][j + 9].Text = c.ToString();
                                    }
                                }

                                if (Solve(board, form, button, indis))
                                {
                                    //form.Refresh();
                                    return true;
                                }

                                else
                                {
                                    board[i, j] = ' ';
                                    if (indis == 1)
                                    {
                                        DataAccess.streamWriter.WriteLine("Sudoku 1'in " + i + ". satirin " + j + ". sutunundaki deger silindi.");
                                        button[i][j].Text = " ";
                                    }

                                    else if (indis == 2)
                                    {
                                        DataAccess.streamWriter.WriteLine("Sudoku 2'nin " + i + ". satirin " + j + ". sutunundaki deger silindi.");
                                        button[i + 12][j].Text = " ";
                                    }

                                    else if (indis == 3)
                                    {
                                        if (i >= 6 && i < 9)
                                        {
                                            DataAccess.streamWriter.WriteLine("Sudoku 3'ün " + i + ". satirin " + j + ". sutunundaki deger silindi.");
                                            button[i][j + 12].Text = " ";
                                        }
                                        else
                                        {
                                            DataAccess.streamWriter.WriteLine("Sudoku 3'ün " + i + ". satirin " + j + ". sutunundaki deger silindi.");
                                            button[i][j + 9].Text = " ";
                                        }
                                    }

                                    else if (indis == 4)
                                    {
                                        if (i >= 0 && i < 3)
                                        {
                                            DataAccess.streamWriter.WriteLine("Sudoku 4'ün " + i + ". satirin " + j + ". sutunundaki deger silindi.");
                                            button[i + 12][j + 12].Text = " ";
                                        }

                                        else
                                        {
                                            DataAccess.streamWriter.WriteLine("Sudoku 4'ün " + i + ". satirin " + j + ". sutunundaki deger silindi.");
                                            button[i + 12][j + 9].Text = " ";
                                        }
                                    }
                                }
                                //form.Refresh();
                            }
                        }
                        return false;
                    }
                }
            }
            return true;

        }
        public bool Solve(char[,] board, Form form, Button[][] button, int indis,int start,int finish)
        {
            for (int i = start; i >= finish; i--)
            {
                for (int j = start; j >= finish; j--)
                {
                    if (board[i, j] == ' ')
                    {
                        for (char c = '1'; c <= '9'; c++)
                        {
                            if (isValid(board, i, j, c))
                            {

                                board[i, j] = c;

                                if (indis == 1)
                                {
                                    DataAccess.streamWriter.WriteLine("Sudoku 1'in "+i + ". satirin " + j + ". sutununa " + c + " degeri eklendi.");
                                    button[i][j].Text = c.ToString();
                                }

                                else if (indis == 2)
                                {
                                    DataAccess.streamWriter.WriteLine("Sudoku 2'nin "+i + ". satirin " + j + ". sutununa " + c + " degeri eklendi.");
                                    button[i + 12][j].Text = c.ToString();
                                }

                                else if (indis == 3)
                                {
                                    if (i >= 6 && i < 9)
                                    {
                                        DataAccess.streamWriter.WriteLine("Sudoku 3'ün " + i + ". satirin " + j + ". sutununa " + c + " degeri eklendi.");
                                        button[i][j + 12].Text = c.ToString();
                                    }
                                    else
                                    {
                                        DataAccess.streamWriter.WriteLine("Sudoku 3'ün " + i + ". satirin " + j + ". sutununa " + c + " degeri eklendi.");
                                        button[i][j + 9].Text = c.ToString();
                                    }
                                }

                                else if (indis == 4)
                                {
                                    if (i >= 0 && i < 3)
                                    {
                                        DataAccess.streamWriter.WriteLine("Sudoku 4'ün " + i + ". satirin " + j + ". sutununa " + c + " degeri eklendi.");
                                        button[i + 12][j + 12].Text = c.ToString();
                                    }

                                    else
                                    {
                                        DataAccess.streamWriter.WriteLine("Sudoku 4'ün " + i + ". satirin " + j + ". sutununa " + c + " degeri eklendi.");
                                        button[i + 12][j + 9].Text = c.ToString();
                                    }
                                }

                                if (Solve(board, form, button, indis,start,finish))
                                {
                                    //form.Refresh();
                                    return true;
                                }

                                else
                                {
                                    board[i, j] = ' ';
                                    if (indis == 1)
                                    {
                                        DataAccess.streamWriter.WriteLine("Sudoku 1'in "+ i + ". satirin " + j + ". sutunundaki deger silindi.");
                                        button[i][j].Text = " ";
                                    }

                                    else if (indis == 2)
                                    {
                                        DataAccess.streamWriter.WriteLine("Sudoku 2'nin "+ i + ". satirin " + j + ". sutunundaki deger silindi.");
                                        button[i + 12][j].Text = " ";
                                    }

                                    else if (indis == 3)
                                    {
                                        if (i >= 6 && i < 9)
                                        {
                                            DataAccess.streamWriter.WriteLine("Sudoku 3'ün "+ i + ". satirin " + j + ". sutunundaki deger silindi.");
                                            button[i][j + 12].Text = " ";
                                        }
                                        else
                                        {
                                            DataAccess.streamWriter.WriteLine("Sudoku 3'ün "+ i + ". satirin " + j + ". sutunundaki deger silindi.");
                                            button[i][j + 9].Text = " ";
                                        }
                                    }

                                    else if (indis == 4)
                                    {
                                        if (i >= 0 && i < 3)
                                        {
                                            DataAccess.streamWriter.WriteLine("Sudoku 4'ün "+ i + ". satirin " + j + ". sutunundaki deger silindi.");
                                            button[i + 12][j + 12].Text = " ";
                                        }

                                        else
                                        {
                                            DataAccess.streamWriter.WriteLine("Sudoku 4'ün "+ i + ". satirin " + j + ". sutunundaki deger silindi.");
                                            button[i + 12][j + 9].Text = " ";
                                        }
                                    }
                                }
                                //form.Refresh();
                            }
                        }
                        return false;
                    }
                }
            }
            return true;

        }
        public bool Solve(char[,] board)
        {
            for (int i = 0; i < 9; i++)
            {
                for (int j = 0; j < 9; j++)
                {
                    if (board[i, j] == ' ')
                    {
                        for (char c = '1'; c <= '9'; c++)
                        {
                            if (isValid(board, i, j, c))
                            {
                                times.Add(stopwatch.Elapsed);
                                //DataAccess.streamWriter.WriteLine(i + ". satirin " + j + ". sutununa " + c + " degeri eklendi.");
                                board[i, j] = c;
                                if (Solve(board))
                                    return true;

                                else
                                {
                                    //DataAccess.streamWriter.WriteLine(i + ". satirin " + j + ". sutunundaki deger silindi.");
                                    board[i, j] = ' ';
                                }
                            }
                        }
                        return false;
                    }
                }
            }
            return true;
        }

        public bool Solve(char[,] board,StreamWriter sw)
        {

            for (int i = 0; i < 9; i++)
            {
                for (int j = 0; j < 9; j++)
                {
                    if (board[i, j] == ' ')
                    {
                        for (char c = '1'; c <= '9'; c++)
                        {
                            if (isValid(board, i, j, c))
                            {
                                sw.WriteLine(i + ". satirin " + j + ". sutununa " + c + " degeri eklendi.");
                                
                                board[i, j] = c;
                                /*lock (thisLock)
                                {
                                    Print(board);
                                    Console.WriteLine();
                                }*/
                                if (Solve(board,sw))
                                    return true;

                                else
                                {
                                    board[i, j] = ' ';
                                    sw.WriteLine(i + ". satirin " + j + ". sutunundaki deger silindi.");
                                }
                            }
                        }
                        return false;
                    }
                }
            }
            return true;
        }

        public bool Solve(char[,] board,int start,int finish)
        {
            
            for (int i = start; i >= finish; i--)
            {
                for (int j = start; j >= finish; j--)
                {
                    if (board[i, j] == ' ')
                    {
                        for (char c = '1'; c <= '9'; c++)
                        {
                            if (isValid(board, i, j, c))
                            {

                                board[i, j] = c;
                                times.Add(stopwatch.Elapsed);
                               /* lock (thisLock)
                                {
                                    Print(board);
                                    Console.WriteLine();
                                }*/

                                if (Solve(board,start,finish))
                                    return true;

                                else
                                {
                                    board[i, j] = ' ';
                                }
                            }
                        }
                        return false;
                    }
                }
            }
            return true;
        }

        public bool SolveForCenter(char[,] board, char[,] sudoku1, char[,] sudoku2, char[,] sudoku3, char[,] sudoku4, char[,] sudoku1_n, char[,] sudoku2_n, char[,] sudoku3_n, char[,] sudoku4_n, Button[][] buttons, Form form)
        {
            for (int i = 0; i < 9; i++)
            {
                for (int j = 0; j < 9; j++)
                {
                    if (board[i, j] == ' ')
                    {
                        for (char c = '1'; c <= '9'; c++)
                        {
                            if (isValidForCenter(board, i, j, c, sudoku1_n, sudoku2_n, sudoku3_n, sudoku4_n))
                            {
                                
                                board[i, j] = c;

                                if (SolveForCenter(board, sudoku1, sudoku2, sudoku3, sudoku4, sudoku1_n, sudoku2_n, sudoku3_n, sudoku4_n, buttons, form))
                                {
                                    for (int k = 0; k < 9; k++)
                                    {
                                        for (int l = 0; l < 9; l++)
                                        {
                                            if (k < 3)
                                            {
                                                buttons[k + 6][l + 6].Text = board[k, l].ToString();
                                            }

                                            else if (3 <= k && k < 6)
                                            {
                                                buttons[k + 6][l].Text = board[k, l].ToString();
                                            }

                                            else
                                            {
                                                buttons[k + 6][l + 6].Text = board[k, l].ToString();
                                            }

                                        }
                                    }
                                    //form.Refresh();

                                    for (int k = 0; k < 3; k++)
                                    {
                                        for (int l = 0; l < 3; l++)
                                        {
                                            sudoku1[k + 6, l + 6] = board[k, l];
                                            sudoku2[k, l + 6] = board[k + 6, l];
                                            sudoku3[k + 6, l] = board[k, l + 6];
                                            sudoku4[k, l] = board[k + 6, l + 6];
                                        }
                                    }

                                    bool val1 = false, val2 = false, val3 = false, val4 = false;
                                    Thread thread1 = new Thread(() => { val1 = Solve(sudoku1, form, buttons, 1); });
                                    thread1.Start();

                                    Thread thread2 = new Thread(() => { val2 = Solve(sudoku2, form, buttons, 2); });
                                    thread2.Start();

                                    Thread thread3 = new Thread(() => { val3 = Solve(sudoku3, form, buttons, 3); });
                                    thread3.Start();

                                    Thread thread4 = new Thread(() => { val4 = Solve(sudoku4, form, buttons, 4); });
                                    thread4.Start();

                                    thread1.Join();
                                    thread2.Join();
                                    thread3.Join();
                                    thread4.Join();

                                    if (val1 && val2 && val3 && val4)
                                        return true;
                                    else
                                    {
                                        board[i, j] = ' ';
                                    }

                                }

                                else
                                {
                                    board[i, j] = ' ';
                                }

                            }
                        }
                        return false;
                    }
                }
            }

            return true;
        }

        public bool SolveForCenterG(char[,] board, char[,] sudoku1, char[,] sudoku2, char[,] sudoku3, char[,] sudoku4, char[,] sudoku1_n, char[,] sudoku2_n, char[,] sudoku3_n, char[,] sudoku4_n)
        {
            for (int i = 0; i < 9; i++)
            {
                for (int j = 0; j < 9; j++)
                {
                    if (board[i, j] == ' ')
                    {
                        for (char c = '1'; c <= '9'; c++)
                        {
                            if (isValidForCenter(board, i, j, c, sudoku1_n, sudoku2_n, sudoku3_n, sudoku4_n))
                            {

                                board[i, j] = c;

                                if (SolveForCenterG(board, sudoku1, sudoku2, sudoku3, sudoku4, sudoku1_n, sudoku2_n, sudoku3_n, sudoku4_n))
                                {

                                    for (int k = 0; k < 3; k++)
                                    {
                                        for (int l = 0; l < 3; l++)
                                        {
                                            sudoku1[k + 6, l + 6] = board[k, l];
                                            sudoku2[k, l + 6] = board[k + 6, l];
                                            sudoku3[k + 6, l] = board[k, l + 6];
                                            sudoku4[k, l] = board[k + 6, l + 6];
                                        }
                                    }

                                    bool val1 = false, val2 = false, val3 = false, val4 = false;
                                    Thread thread1 = new Thread(() => { val1 = Solve(sudoku1); });
                                    thread1.Start();

                                    Thread thread2 = new Thread(() => { val2 = Solve(sudoku2); });
                                    thread2.Start();

                                    Thread thread3 = new Thread(() => { val3 = Solve(sudoku3); });
                                    thread3.Start();

                                    Thread thread4 = new Thread(() => { val4 = Solve(sudoku4); });
                                    thread4.Start();

                                    thread1.Join();
                                    thread2.Join();
                                    thread3.Join();
                                    thread4.Join();

                                    if (val1 && val2 && val3 && val4)
                                        return true;
                                    else
                                    {
                                        board[i, j] = ' ';
                                    }

                                }

                                else
                                {
                                    board[i, j] = ' ';
                                }

                            }
                        }
                        return false;
                    }
                }
            }

            return true;
        }

        public bool SolveForCenter(char[,] board, char[,] sudoku1, char[,] sudoku2, char[,] sudoku3, char[,] sudoku4, char[,] sudoku1_n, char[,] sudoku2_n, char[,] sudoku3_n, char[,] sudoku4_n,char[,] sudoku1_t, char[,] sudoku2_t, char[,] sudoku3_t, char[,] sudoku4_t, Button[][] buttons, Form form)
        {
            for (int i = 0; i < 9; i++)
            {
                for (int j = 0; j < 9; j++)
                {
                    if (board[i, j] == ' ')
                    {
                        for (char c = '1'; c <= '9'; c++)
                        {
                            if (isValidForCenter(board, i, j, c, sudoku1_n, sudoku2_n, sudoku3_n, sudoku4_n))
                            {

                                board[i, j] = c;
                                if (SolveForCenter(board, sudoku1, sudoku2, sudoku3, sudoku4, sudoku1_n, sudoku2_n, sudoku3_n, sudoku4_n,sudoku1_t, sudoku2_t, sudoku3_t, sudoku4_t, buttons, form))
                                {
                                    for (int k = 0; k < 9; k++)
                                    {
                                        for (int l = 0; l < 9; l++)
                                        {
                                            if (k < 3)
                                            {
                                                buttons[k + 6][l + 6].Text = board[k, l].ToString();
                                            }

                                            else if (3 <= k && k < 6)
                                            {
                                                buttons[k + 6][l].Text = board[k, l].ToString();
                                            }

                                            else
                                            {
                                                buttons[k + 6][l + 6].Text = board[k, l].ToString();
                                            }

                                        }
                                    }
                                    //form.Refresh();

                                    for (int k = 0; k < 3; k++)
                                    {
                                        for (int l = 0; l < 3; l++)
                                        {
                                            sudoku1[k + 6, l + 6] = board[k, l];
                                            sudoku2[k, l + 6] = board[k + 6, l];
                                            sudoku3[k + 6, l] = board[k, l + 6];
                                            sudoku4[k, l] = board[k + 6, l + 6];
                                        }
                                    }

                                    bool val1 = false, val2 = false, val3 = false, val4 = false,val5 = false, val6 = false,val7 = false,val8 = false;
                                    Thread thread1 = new Thread(() => { val1 = Solve(sudoku1, form, buttons, 1); });
                                    Thread thread2 = new Thread(() => { val2 = Solve(sudoku1, form, buttons, 1,8,0); });
                                    thread1.Start();
                                    thread2.Start();

                                    Thread thread3 = new Thread(() => { val3 = Solve(sudoku2, form, buttons, 2); });
                                    Thread thread4 = new Thread(() => { val4 = Solve(sudoku2, form, buttons, 2,8,0); });
                                    thread3.Start();
                                    thread4.Start();

                                    Thread thread5 = new Thread(() => { val5 = Solve(sudoku3, form, buttons, 3); });
                                    Thread thread6 = new Thread(() => { val6 = Solve(sudoku3, form, buttons, 3,8,0); });
                                    thread5.Start();
                                    thread6.Start();

                                    Thread thread7 = new Thread(() => { val7 = Solve(sudoku4, form, buttons, 4); });
                                    Thread thread8 = new Thread(() => { val8 = Solve(sudoku4, form, buttons, 4,8,0); });
                                    thread7.Start();
                                    thread8.Start();

                                    thread1.Join();
                                    thread2.Join();
                                    thread3.Join();
                                    thread4.Join();
                                    thread5.Join();
                                    thread6.Join();
                                    thread7.Join();
                                    thread8.Join();

                                    if ((val1 || val2) && (val3 || val4) && (val5 || val6) && (val7 || val8))
                                        return true;
                                    else
                                    {
                                        board[i, j] = ' ';
                                    }

                                }

                                else
                                {
                                    board[i, j] = ' ';
                                }

                            }
                        }
                        return false;
                    }
                }
            }

            return true;
        }

        public bool SolveForCenterG(char[,] board, char[,] sudoku1, char[,] sudoku2, char[,] sudoku3, char[,] sudoku4, char[,] sudoku1_n, char[,] sudoku2_n, char[,] sudoku3_n, char[,] sudoku4_n, char[,] sudoku1_t, char[,] sudoku2_t, char[,] sudoku3_t, char[,] sudoku4_t)
        {
            for (int i = 0; i < 9; i++)
            {
                for (int j = 0; j < 9; j++)
                {
                    if (board[i, j] == ' ')
                    {
                        for (char c = '1'; c <= '9'; c++)
                        {
                            if (isValidForCenter(board, i, j, c, sudoku1_n, sudoku2_n, sudoku3_n, sudoku4_n))
                            {

                                board[i, j] = c;
                                if (SolveForCenterG(board, sudoku1, sudoku2, sudoku3, sudoku4, sudoku1_n, sudoku2_n, sudoku3_n, sudoku4_n, sudoku1_t, sudoku2_t, sudoku3_t, sudoku4_t))
                                {
                                    for (int k = 0; k < 3; k++)
                                    {
                                        for (int l = 0; l < 3; l++)
                                        {
                                            sudoku1[k + 6, l + 6] = board[k, l];
                                            sudoku2[k, l + 6] = board[k + 6, l];
                                            sudoku3[k + 6, l] = board[k, l + 6];
                                            sudoku4[k, l] = board[k + 6, l + 6];
                                        }
                                    }

                                    bool val1 = false, val2 = false, val3 = false, val4 = false, val5 = false, val6 = false, val7 = false, val8 = false;
                                    Thread thread1 = new Thread(() => { val1 = Solve(sudoku1); });
                                    Thread thread2 = new Thread(() => { val2 = Solve(sudoku1, 8, 0); });
                                    thread1.Start();
                                    thread2.Start();

                                    Thread thread3 = new Thread(() => { val3 = Solve(sudoku2); });
                                    Thread thread4 = new Thread(() => { val4 = Solve(sudoku2, 8, 0); });
                                    thread3.Start();
                                    thread4.Start();

                                    Thread thread5 = new Thread(() => { val5 = Solve(sudoku3); });
                                    Thread thread6 = new Thread(() => { val6 = Solve(sudoku3, 8, 0); });
                                    thread5.Start();
                                    thread6.Start();

                                    Thread thread7 = new Thread(() => { val7 = Solve(sudoku4); });
                                    Thread thread8 = new Thread(() => { val8 = Solve(sudoku4, 8, 0); });
                                    thread7.Start();
                                    thread8.Start();

                                    thread1.Join();
                                    thread2.Join();
                                    thread3.Join();
                                    thread4.Join();
                                    thread5.Join();
                                    thread6.Join();
                                    thread7.Join();
                                    thread8.Join();

                                    if ((val1 || val2) && (val3 || val4) && (val5 || val6) && (val7 || val8))
                                        return true;
                                    else
                                    {
                                        board[i, j] = ' ';
                                    }

                                }

                                else
                                {
                                    board[i, j] = ' ';
                                }

                            }
                        }
                        return false;
                    }
                }
            }

            return true;
        }
        public bool SolveForCenter(char[,] board, char[,] sudoku1, char[,] sudoku2, char[,] sudoku3, char[,] sudoku4, char[,] sudoku1_n, char[,] sudoku2_n, char[,] sudoku3_n, char[,] sudoku4_n)
        {
            for (int i = 0; i < 9; i++)
            {
                for (int j = 0; j < 9; j++)
                {
                    if (board[i, j] == ' ')
                    {
                        for (char c = '1'; c <= '9'; c++)
                        {
                            if (isValidForCenter(board, i, j, c, sudoku1_n, sudoku2_n, sudoku3_n, sudoku4_n))
                            {
                                board[i, j] = c;
                                
                                if (SolveForCenter(board, sudoku1, sudoku2, sudoku3, sudoku4, sudoku1_n, sudoku2_n, sudoku3_n, sudoku4_n))
                                {

                                    for (int k = 0; k < 3; k++)
                                    {
                                        for (int l = 0; l < 3; l++)
                                        {
                                            sudoku1[k + 6, l + 6] = board[k, l];
                                            sudoku2[k, l + 6] = board[k + 6, l];
                                            sudoku3[k + 6, l] = board[k, l + 6];
                                            sudoku4[k, l] = board[k + 6, l + 6];
                                        }
                                    }

                                    if (Solve(sudoku1) && Solve(sudoku2) && Solve(sudoku3) && Solve(sudoku4))
                                        return true;
                                    else
                                    {
                                        board[i, j] = ' ';
                                    }

                                }

                                else
                                {
                                    board[i, j] = ' ';
                                }
                            }
                        }
                        return false;
                    }
                }
            }

            return true;
        }
    }
}

