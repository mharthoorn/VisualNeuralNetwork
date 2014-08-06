using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NeuralNetworkApp
{
    public static class Utilities
    {
        private static Random random = new Random();
        public static double Random()
        {
            return random.NextDouble();
        }

        
        public static void BackPropagate(this Network network, Action<Neuron> action)
        {
            IEnumerable<Layer> reversed = (network.Layers as IEnumerable<Layer>).Reverse();
            foreach (Layer layer in reversed)
            {
                foreach (Neuron neuron in layer.Neurons)
                {
                    action(neuron);
                }
            }
        }

        public static void Measure(this Network network)
        {
            network.BackPropagate(neuron => neuron.Measure());
        }

        public static void Learn(this Network network, double rate)
        {
            foreach (Neuron n in network.Neurons)
            {
                n.Learn(rate);
            }
        }

       
    }

    
}
