using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NeuralNetworkApp
{
    public static class NetworkBuilder
    {
        public static void Link(this Neuron a, Neuron b)
        {
            Axon axon = new Axon();
            axon.Source = a;
            axon.Target = b;
            a.Axons.Add(axon);
            b.Dendrites.Add(axon);
        }

        public static void Link(Layer first, Layer second)
        {
            foreach (Neuron n in first.Neurons)
            {
                foreach (Neuron m in second.Neurons)
                {
                    Link(n, m);
                }
            }
        }

        public static void Link(Network network)
        {
            Layer previous = null;
            foreach (Layer layer in network.Layers)
            {
                if (previous != null)
                {
                    Link(previous, layer);
                }
                previous = layer;
            }
        }

        public static void DeepLink(Network network)
        {
            Layer preprevious = null;
            Layer previous = null;
            foreach (Layer layer in network.Layers)
            {
                if (previous != null)
                {
                    Link(previous, layer);
                }
                if (preprevious != null)
                {
                    Link(preprevious, layer);
                }
                preprevious = previous;
                previous = layer;
            }
        }

        public static void Classify(this Network network)
        {
            foreach (Neuron n in network.Neurons)
            {
                n.Kind = NeuronKind.Hidden;
            }

            foreach (Neuron n in network.Layers.First().Neurons)
            {
                n.Kind = NeuronKind.Input;
            }

            foreach (Neuron n in network.Layers.Last().Neurons)
            {
                n.Kind = NeuronKind.Output;
            }
        }

        public static void InjectFunctions(this Network network)
        {
            foreach (Neuron neuron in network.Neurons)
            {
                neuron.SignalFunction = NetworkFunctions.SigmoidFunction;
                switch (neuron.Kind)
                {
                    case NeuronKind.Output:
                        neuron.ErrorFunction = NetworkFunctions.CalcOutputErrorGradient;
                        break;
                    case NeuronKind.Hidden:
                        neuron.ErrorFunction = NetworkFunctions.CalcErrorGradient;
                        break;
                    case NeuronKind.Input:
                        neuron.ErrorFunction = NetworkFunctions.CalcErrorGradient;
                        break;
                }
            }
        }

        public static void CreateLayers(this Network network, int[] layersizes)
        {
            foreach (int size in layersizes)
            {
                network.Layers.Add(new Layer(size));
            }
        }

        public static Network Build(string name, Action<Network> link, params int[] layersizes)
        {
            Network network = new Network();
            network.Name = name;
            network.CreateLayers(layersizes);
            network.Position();
            network.Coordinate();
            network.Classify();
            network.InjectFunctions();
            link(network);
            return network;
        }

        public static void Position(this Network network)
        {
            int x = 0;
            foreach (Layer layer in network.Layers)
            {
                int y = 0;
                foreach (Neuron neuron in layer.Neurons)
                {
                    neuron.Location = new Point(x, y);
                    y++;
                }
                x++;
            }
        }

        public static void Coordinate(this Network network)
        {
            int max = network.Layers.Max(l => l.Size);

            foreach (Layer layer in network.Layers)
            {
                int len = layer.Size;
                int offset = (max - len);
                foreach (Neuron neuron in layer.Neurons)
                {

                    neuron.Coordinate.X = neuron.Location.X;
                    neuron.Coordinate.Y = 2 * neuron.Location.Y + offset;
                }
            }
        }
    }
}
