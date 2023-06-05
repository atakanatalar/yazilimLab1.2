using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SudokuProject
{
    public static class DataAccess
    {
        public static StreamWriter streamWriter;
        public static char[][] Read()
        {
            FileStream fileStream = new FileStream(@"C:\Users\yasin\source\repos\SudokuProject\SudokuProject\sudoku.txt", FileMode.OpenOrCreate, FileAccess.ReadWrite);
            char[][] sudoku = new char[21][];
            int count = 0;
            using (StreamReader reader = new StreamReader(fileStream))
            {
                while (true)
                {
                    string satir = reader.ReadLine();
                    if (satir == null) break;
                    sudoku[count] = new char[satir.Length];
                    for (int i = 0; i < satir.Length; i++)
                    {
                        if (satir[i] == '*')
                        {
                            sudoku[count][i] = ' ';
                            continue;
                        }
                        sudoku[count][i] = satir[i];
                    }
                    count++;
                }
                reader.Close();
            }
            fileStream.Close();

            return sudoku;
        }

        public static void Write()
        {
            string dateTime = DateTime.Now.ToString();
            dateTime = dateTime.Replace(':', '-');
            string dosyaYolu = @"C:\Users\yasin\source\repos\SudokuProject\SudokuProject\ProgramCiktilari\" + dateTime + ".txt";
            FileStream fs = new FileStream(dosyaYolu, FileMode.OpenOrCreate, FileAccess.Write);
            streamWriter = new StreamWriter(fs);
        }
    }
}
