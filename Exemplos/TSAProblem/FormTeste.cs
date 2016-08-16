using System;
using System.Threading;
using System.Windows.Forms;
using TSAProblem.GeneticAlgorithm;
using System.Drawing;

namespace TSAProblem
{
    public partial class FormTeste : Form
    {
        GAParams Param;
        Thread Thead;
        Bitmap MapImage;
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
                    mutationRate = Convert.ToDouble(txtMutation.Text),
                    crossoverRate = Convert.ToDouble(txtCrossover.Text),
                    populationSize = Convert.ToInt16(txtPopulation.Text),
                    numberOfCities = Convert.ToInt16(txtCities.Text),
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
            var objGA = new GaTSP(Param);
            objGA.DrawMap += objGA_DrawMap;

            while (true)
            {
                objGA.Epoch();
                BestSolution = objGA.BestSolution;
                generation   = objGA.generation;
                Thread.Sleep(100);
            }
        }

        void objGA_DrawMap(object sender, EventArgs e)
        {
            var temp = e as GAEventArgs;
            MapImage = temp.MapImage;
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
