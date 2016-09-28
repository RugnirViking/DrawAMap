using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;

namespace DrawAMap
{
    public class MapTile
    {
        private Color color;
        private double x, y;
        private Rect rect;
        private Brush currentBrush;
        private Pen pen;
        private double temprature;
        private double humidity;
        private double altitude;

        private MainWindow mw;

        public MapTile(MainWindow mw,double x, double y,double temprature, double humidity, double altitude)
        {
            this.mw = mw;
            this.temprature = temprature;
            this.humidity = humidity;
            this.altitude = altitude;
            this.x = x;
            this.y = y;
            if (altitude < 128) //  THE SEA
            {
                this.color = Color.FromArgb(255, 0, 0, (byte)altitude);
            }
            else if (altitude < 148) // THE BEACH
            {
                this.color = Color.FromArgb(255, 200, 200, (byte)(altitude));
            } else // THE LAND
            {
                this.color = Color.FromArgb(255, (byte)((altitude-128)*2), 200, (byte)((altitude-128)*2));
            }
            rect = new Rect(x * 5, y * 5, 5, 5);
            currentBrush = new SolidColorBrush(color);
            pen = new Pen(currentBrush, 1);
        }
        public double GetScreenX()
        {
            return x * 5;
        }
        public double GetScreenY()
        {
            return y * 5;
        }
        public Rect GetRect(){
            return rect;
        }
        public void Draw(DrawingContext drawingContext)
        {
            drawingContext.DrawRectangle(currentBrush, pen, rect);
        }
    }
}
