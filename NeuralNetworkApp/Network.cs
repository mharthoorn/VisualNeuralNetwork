using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NeuralNetworkApp
{

    public class Layer
    {
        public List<Neuron> Neurons = new List<Neuron>();
        public Layer(int size)
        {
            for (int i = 0; i < size; i++)
            {
                Neuron neuron = new Neuron();
                Neurons.Add(neuron);
            }
        }

        public IEnumerable<Axon> Axons
        {
            get
            {
                return Neurons.SelectMany(n => n.Axons);
            }
        }

        public int Size
        {
            get
            {
                return Neurons.Count;
            }
        }
    }

    public class Network
    {
        public string Name;
        public List<Layer> Layers = new List<Layer>();

        public IEnumerable<Neuron> Neurons
        {
            get
            {
                return Layers.SelectMany(L => L.Neurons);

            }
        }
        public Neuron this[int x, int y]
        {
            get
            {
                Layer l = Layers[x];
                return l.Neurons[y];
            }
        }

        public IEnumerable<Axon> Axons
        {
            get
            {
                return Layers.SelectMany(L => L.Axons);
            }
        }

        public Layer LayerOf(Neuron neuron)
        {
            foreach(Layer layer in Layers)
            {
                layer.Neurons.Contains(neuron);
                return layer;
            }
            return null;
        }

        public List<Neuron> Input
        {
            get
            {
                return Layers.First().Neurons;
            }
        }

        public List<Neuron> Output
        {
            get
            {
                return Layers.Last().Neurons;
            }
        }

        public void Reset()
        {
            foreach (Neuron n in Neurons)
            {
                n.Reset();
            }
        }
    }
}
