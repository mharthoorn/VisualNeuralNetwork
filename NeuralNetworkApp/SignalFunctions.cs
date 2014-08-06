using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NeuralNetworkApp
{
    
        

    public static class NetworkFunctions
    {
        public static double Step(double x)
        {
            if (x > 0.0) return 1.0;
            else return 0.0;
        }

        public static double SigmoidFunction(double x)
        {
            if (x < -45.0) return 0.0;
            else if (x > 45.0) return 1.0;
            else return 1.0 / (1.0 + Math.Exp(-x));
        }

        public static double HyperTanFunction(double x)
        {
            if (x < -10.0) return -1.0;
            else if (x > 10.0) return 1.0;
            else return Math.Tanh(x);
        }

        public static double CalcErrorGradient(Neuron neuron) // for a non-output neuron
        {
            // this function needs to be called through the network from output towards input!!! (backpropagated)

            double signal = neuron.Signal;
            double deltasum = 0;
            foreach (Axon axon in neuron.Axons)
            {
                deltasum += axon.Weight * axon.Target.ErrorGradient;
            }
            double gradient = signal * (1 - signal);
            double result = gradient * deltasum;
            return result;
        }

        public static double CalcOutputErrorGradient(Neuron neuron) // for an output neuron
        {
            double signal = neuron.Signal;
            double desired = neuron.Desired;
            double delta = (desired - signal);
            double gradient = signal * (1 - signal);
            double result = gradient * delta;
            return result;
        }

    }
}
