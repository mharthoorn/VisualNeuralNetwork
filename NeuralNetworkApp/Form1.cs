using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace NeuralNetworkApp
{
    public partial class Form1 : Form
    {
        public Network network = NetworkBuilder.Build("MyNetwork", NetworkBuilder.Link, 3, 5, 3);
        
        public Form1()
        {
            InitializeComponent();

            Random R = new Random();
            
            foreach (Axon a in network.Axons)
            {
                a.Weight = Utilities.Random() / 30;
            }
        }


        public void SetScenario(int scenario)
        {
            if (scenario == 0)
            {
                network.Input[0].Fire();
                network.Output[0].Desired = 1;
                network.Output[1].Desired = 0;
                network.Output[2].Desired = 0;
            }
            else if (scenario == 1)
            {
                network.Input[1].Fire();
                network.Output[0].Desired = 0;
                network.Output[1].Desired = 1;
                network.Output[2].Desired = 0;
            }
            else
            {
                network.Input[2].Fire();
                network.Output[0].Desired = 0;
                network.Output[1].Desired = 0;
                network.Output[2].Desired = 1;
            }
        }

        int scenario = 0;

        public void Iteration()
        {
            scenario = (scenario + 1) % 3;
            network.Reset();
            SetScenario(scenario);
            network.Measure();
            network.Learn(1);
        }


        private void button1_Click(object sender, EventArgs e)
        {
            Iteration();
            display();
        }

        private void button2_Click(object sender, EventArgs e)
        {

            for (int i = 0; i < 1000; i++) 
            {
                Iteration();
            }
            display();
        }

        private void button3_Click(object sender, EventArgs e)
        {

        }

        private void display()
        {
            picturebox.Image = network.Draw();
        }

        private void readinput()
        {
            network.Reset();
            var values = textBox1.Text.Split(',').Select(s => s.Trim());
            
            foreach (string value in values)
            {
                int i = 0;
                if (int.TryParse(value, out i))
                {
                    if (i >= 1 && network.Input.Count >= i)
                    {
                        network.Input[i - 1].Fire();
                    }
                }
            }
        }

        private void writeoutput()
        {
            List<int> list = new List<int>();
            foreach (Neuron neuron in network.Output)
            {
                if (neuron.Signal > 0.75)
                {
                    list.Add(neuron.Location.Y);
                }
            }
            string s = string.Join(",", list.Select(j => (char)(65 + j)));
            label1.Text = (s.Length > 0) ? s : "(Network gave no output)"; 
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            readinput();
            writeoutput();
            display();
                    
        }
    }
}
