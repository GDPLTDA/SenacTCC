using System;
using System.Threading;
using System.Windows.Forms;
using TCC.GeneticAlgorithm;
using System.Drawing;
using System.Collections.Generic;
using TCC.Core;

namespace TSAProblem
{
    public partial class FormTeste : Form
    {
        GAParams Param;
        Thread Thead;
        Bitmap MapImage;
        GaTSP objGA;
        private readonly object thisLock = new object();

        double BestSolution;
        int generation;
        public FormTeste()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (cmdStart.Text == "Start")
            {
                Param = new GAParams
                {
                    MutationRate = Convert.ToDouble(txtMutation.Text),
                    CrossoverRate = Convert.ToDouble(txtCrossover.Text),
                    PopulationSize = Convert.ToInt16(txtPopulation.Text),
                    NumberOfRoutes = Convert.ToInt16(txtCities.Text),
                    MapaSize = pictureBox1.Width
                };
                UpdateTime.Enabled = true;
                cmdStart.Text = "Stop";
                Thead = new Thread(new ThreadStart(Run));
                Thead.Start();
            }
            else
            {
                UpdateTime.Enabled = false;
                Thead.Abort();
                cmdStart.Text = "Start";
            }

        }

        public void Run()
        {
            objGA = new GaTSP(Param);
            while (true)
            {
                MapImage = Draw(objGA.GetBestCitie());
                objGA.Epoch();
                BestSolution = objGA.BestSolution;
                generation   = objGA.generation;
                Thread.Sleep(100);
            }
        }

        public Bitmap CreateBitMap()
        {
            Brush RedBrush = new SolidBrush(Color.Red);
            var bmpImage = new Bitmap((int)(2 * 110), (int)(2 * 110));

            var g = Graphics.FromImage(bmpImage);

            var Ran = new Random();
            var lstCityCoordinates = objGA.GetCityCoordinates();

            foreach (var item in lstCityCoordinates)
            {
                g.FillEllipse(RedBrush, (float)item.X, (float)item.Y, 10, 10);
            }
            return bmpImage;
        }

        public Bitmap Draw(List<Coordinate> pointList)
        {
            Brush blackBrush = new SolidBrush(Color.Black);
            var myImage = CreateBitMap();
            var lstCityCoordinates = objGA.GetCityCoordinates();


            using (Graphics g = Graphics.FromImage(myImage))
            {
                for (int i = 0; i < pointList.Count - 1; i++)
                {
                    var p = Convert.ToInt32(pointList[i].X);
                    var j = Convert.ToInt32(pointList[i + 1].X);

                    var x1 = (int)lstCityCoordinates[p].X + 5;
                    var y1 = (int)lstCityCoordinates[p].Y + 5;
                    var x2 = (int)lstCityCoordinates[j].X + 5;
                    var y2 = (int)lstCityCoordinates[j].Y + 5;

                    using (var pen = new Pen(blackBrush, 2))
                    {
                        g.DrawLine(pen, new Point(x1, y1), new Point(x2, y2));
                    }
                }

            }
            return myImage;
        }

        private void UpdateTime_Tick(object sender, EventArgs e)
        {
            lock(thisLock)
            {
                pictureBox1.Image = MapImage;
                pictureBox1.Update();

                txtFitness.Text = BestSolution.ToString();
                txtFitness.Update();
                txtGeneration.Text = generation.ToString();
                txtGeneration.Update();
            }
        }
    }
}
