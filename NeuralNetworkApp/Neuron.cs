using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NeuralNetworkApp
{
    public delegate double SignalFunction(double sum);
    public delegate double ErrorFunction(Neuron neuron);
    public enum NeuronKind { Input, Hidden, Output };

    public class Axon
    {
        public Neuron Source;
        public Neuron Target;
        public double Weight;

        public double Signal;

        public void Fire(double signal)
        {
            this.Signal = signal;
            Target.Feed(this.Signal * this.Weight);
        }

        public void Reset()
        {
            this.Signal = 0;
        }

    }

    public class Neuron
    {
        internal List<Axon> Axons = new List<Axon>();
        internal List<Axon> Dendrites = new List<Axon>();
        public Point Location;
        public Point Coordinate;
        public double Sum;
        public double Bias;
        public SignalFunction SignalFunction = null;
        public ErrorFunction ErrorFunction = null;
        public NeuronKind Kind;
        public double Desired;
        public double Signal
        {
            get
            {
                return SignalFunction(Sum + Bias);
            }
        }

        private double _errorgradient;

        private bool measured = false;

        public double ErrorGradient
        {
            get
            {
                return _errorgradient;
            }
        }

        public void Measure()
        {
            if (!measured)
            {
                if (ErrorFunction != null)
                    _errorgradient = ErrorFunction(this);
                measured = true;
            }
        }

        public void Learn(double rate)
        {
            foreach (Axon axon in this.Axons)
            {
                double delta = rate * axon.Target.ErrorGradient * this.Signal;
                axon.Weight += delta;
            }
        }

        public void ResetAxons()
        {
            foreach (Axon axon in this.Axons)
            {
                axon.Reset();
            }
        }

        public void Reset()
        {
            this.Sum = 0;
            this._errorgradient = 0;
            this.measured = false;
            this.Desired = 0;
            this.ResetAxons();

        }

        private void Propagate()
        {
            foreach (Axon axon in Axons)
            {
                axon.Fire(this.Signal);
            }
        }

        public void Fire()
        {
            Sum = 2;
            Propagate();
        }

        public void Feed(double signal)
        {
            Sum += signal;
            Propagate();

        }

        public override string ToString()
        {
            return string.Format("({0},{1}) {2} ({3})", Location.X, Location.Y, Signal, Kind);
        }
    }

   

    
}
