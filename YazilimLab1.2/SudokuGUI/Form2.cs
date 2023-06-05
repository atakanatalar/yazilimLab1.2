using SudokuProject;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SudokuGUI
{
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();
        }

        private void Form2_Load(object sender, EventArgs e)
        {
            chart1.ChartAreas[0].AxisX.Title = "Süre";
            chart1.ChartAreas[0].AxisY.Title = "Kare Sayısı";


            chart1.ChartAreas[0].AxisX.Minimum = 0;
            chart1.Series["5 Thread"].Points.AddXY(Form1.timesThread5[Form1.timesThread5.Count / 10].ToString(), Form1.timesThread5.Count / 10);
            chart1.Series["5 Thread"].Points.AddXY(Form1.timesThread5[Form1.timesThread5.Count / 5].ToString(), Form1.timesThread5.Count / 5);
            chart1.Series["5 Thread"].Points.AddXY(Form1.timesThread5[Form1.timesThread5.Count / 3].ToString(), Form1.timesThread5.Count / 3);
            chart1.Series["5 Thread"].Points.AddXY(Form1.timesThread5[Form1.timesThread5.Count / 2].ToString(), Form1.timesThread5.Count / 2);
            chart1.Series["5 Thread"].Points.AddXY(Form1.timesThread5[Form1.timesThread5.Count / 3 * 2].ToString(), Form1.timesThread5.Count / 3 * 2);
            chart1.Series["5 Thread"].Points.AddXY(Form1.timesThread5[Form1.timesThread5.Count / 5 * 4].ToString(), Form1.timesThread5.Count / 5 * 4);
            chart1.Series["5 Thread"].Points.AddXY(Form1.timesThread5[Form1.timesThread5.Count - 1].ToString(), Form1.timesThread5.Count - 1);

            chart1.ChartAreas[0].AxisX.Minimum = 0;
            chart1.Series["10 Thread"].Points.AddXY(Form1.timesThread10[Form1.timesThread10.Count / 10].ToString(), Form1.timesThread10.Count / 10);
            chart1.Series["10 Thread"].Points.AddXY(Form1.timesThread10[Form1.timesThread10.Count / 5].ToString(), Form1.timesThread10.Count / 5);
            chart1.Series["10 Thread"].Points.AddXY(Form1.timesThread10[Form1.timesThread10.Count / 3].ToString(), Form1.timesThread10.Count / 3);
            chart1.Series["10 Thread"].Points.AddXY(Form1.timesThread10[Form1.timesThread10.Count / 2].ToString(), Form1.timesThread10.Count / 2);
            chart1.Series["10 Thread"].Points.AddXY(Form1.timesThread10[Form1.timesThread10.Count / 3 * 2].ToString(), Form1.timesThread10.Count / 3 *2);
            chart1.Series["10 Thread"].Points.AddXY(Form1.timesThread10[Form1.timesThread10.Count / 5 * 4].ToString(), Form1.timesThread10.Count / 5 *4);
            chart1.Series["10 Thread"].Points.AddXY(Form1.timesThread10[Form1.timesThread10.Count - 1].ToString(), Form1.timesThread10.Count - 1);

        }
    }
}
