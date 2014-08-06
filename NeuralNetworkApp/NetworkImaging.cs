using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NeuralNetworkApp
{
    public static class NetworkImaging
    {

        public static Color Blend(this Color colorA, Color colorB, double amount)
        {
            byte r = (byte)((colorB.R * amount) + colorA.R * (1 - amount));
            byte g = (byte)((colorB.G * amount) + colorA.G * (1 - amount));
            byte b = (byte)((colorB.B * amount) + colorA.B * (1 - amount));
            return Color.FromArgb(r, g, b);
        }

        public static Point Translate(Network network, Neuron neuron)
        {
            // translate a network coordinate to a screen coordinate

            Layer layer = network.LayerOf(neuron);

            Point p = neuron.Coordinate.Multiply(100, 25).Add(50);
            return p;            
        }

        private static void DrawAxons(this Network network, Graphics graphic)
        {
            foreach (Axon axon in network.Axons)
            {
                Point source = Translate(network, axon.Source).Add(5);
                Point target = Translate(network, axon.Target).Add(5);

                Point s2 = source.Shift(50, 0);
                Point t2 = target.Shift(-50, 0);

                Color color = Blend(Color.Gray, Color.Red, axon.Signal);
                float width = (float)axon.Weight*2;
                Pen axonpen = new Pen(color, width);
                
                graphic.DrawBezier(axonpen, source, s2, t2, target);
            }

        }

        private static void DrawNeuron(this Network network, Neuron neuron, Graphics graphic)
        {
            Pen pen = new Pen(Color.Black);
            Point point = Translate(network, neuron);
            Size size = new Size(10, 10);
            Rectangle r = new Rectangle(point, size);

            Color color = Blend(Color.Gray, Color.Red, (neuron.Signal - 0.5));

            Brush brush = new SolidBrush(color);
            graphic.FillEllipse(brush, r);
            graphic.DrawEllipse(pen, r);

            // Draw signal text
            Font font = new Font(FontFamily.GenericSerif, 8);
            Brush fontbrush = new SolidBrush(Color.DarkBlue);
            point = point.Shift(0, 10);

            graphic.TextRenderingHint = System.Drawing.Text.TextRenderingHint.SingleBitPerPixelGridFit;
            graphic.DrawString(neuron.Signal.ToString("f2"), font, fontbrush, point);

            // Draw Error:
            point = point.Shift(0, 10);
            fontbrush = new SolidBrush(Color.Red);
            graphic.DrawString(neuron.ErrorGradient.ToString("f2"), font, fontbrush, point);
        }

        private static void DrawNeurons(this Network network, Graphics graphic)
        {
            

            foreach (Neuron neuron in network.Neurons)
            {
                network.DrawNeuron(neuron, graphic);
            }
        }

        public static Bitmap Draw(this Network network)
        {
            Bitmap bitmap = new Bitmap(600, 600);
            Graphics graphic = Graphics.FromImage(bitmap);
            Brush brush = new SolidBrush(Color.White);
            graphic.FillRectangle(brush, graphic.ClipBounds);

            network.DrawAxons(graphic);
            network.DrawNeurons(graphic);

            return bitmap;
        }

        public static Point Multiply(this Point point, int size)
        {
            point.X *= size;
            point.Y *= size;
            return point;
        }

        public static Point Multiply(this Point point, int sizeX, int sizeY)
        {
            point.X *= sizeX;
            point.Y *= sizeY;
            return point;
        }



        public static Point Add(this Point point, int size)
        {
            point.X += size;
            point.Y += size;
            return point;
        }

        public static Point Shift(this Point point, int x, int y)
        {
            point.X += x;
            point.Y += y;
            return point;
        }

    }

}
