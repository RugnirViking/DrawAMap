
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace DrawAMap
{
    /// <summary>
    /// Interaction logic for MapPort.xaml
    /// </summary>
    public partial class MapPort : UserControl
    {
        public MapTile[,] Tiles;
        public Random randomSeed;
        public Rect SizeRect;
        public double offsetX;
        public double offsetY;
        private MainWindow mw;
        private bool anythingChanged;

        public MapPort()
        {
            InitializeComponent();
        }
        public void LoadEverything(MainWindow mw)
        {
            Tiles = new MapTile[100, 100];
            randomSeed = new Random(3);
            SizeChanged += OnResize;
            var map = MapGenerationHelper.returnSmoothedMap(100,100, randomSeed);
            for (int x = 0; x < 100; x++)
            {
                for (int y = 0; y < 100; y++)
                {
                    var a = Noise2d.Noise(x, y);
                    Tiles[x, y] = new MapTile(mw, x, y, 0,0, map[x, y]);
                }
            }
            anythingChanged = true;
        }
        public void OnResize(object sender, SizeChangedEventArgs e)
        {
            SizeRect = new Rect(0, 0, this.ActualWidth, this.ActualHeight);
            anythingChanged = true;
        }
        protected override void OnRender(System.Windows.Media.DrawingContext drawingContext)
        {
            if (SizeRect.Width == 0)
            {
                SizeRect = new Rect(0, 0, this.ActualWidth, this.ActualHeight);
            }
            if (anythingChanged)
            {
                for (int x = 0; x < 100; x++)
                {
                    for (int y = 0; y < 100; y++)
                    {
                        MapTile currentTile = Tiles[x, y];
                        Rect mapTileRect = currentTile.GetRect();
                        if (SizeRect.Contains(mapTileRect))
                        {
                            currentTile.Draw(drawingContext);
                        }
                    }
                }
                anythingChanged = false;
            }
        }
    }
}
