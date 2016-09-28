

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SimplexNoise;

namespace DrawAMap
{
    public static class Randomizer
    {
        public static void Randomize<T>(T[] items, Random rand)
        {
            // For each spot in the array, pick
            // a random item to swap into that spot.
            for (int i = 0; i < items.Length - 1; i++)
            {
                int j = rand.Next(i, items.Length);
                T temp = items[i];
                items[i] = items[j];
                items[j] = temp;
            }
        }
    }
    static class MapGenerationHelper
    {

        public static double[,] returnSmoothedMap(int width, int height, Random random)
        {
            double[,] mapToReturn = getNoise(width, height, random);



            return mapToReturn;
        }




        private static double[,] getNoise(int width, int height, Random random)
        {
            double[,] mapToReturn = new double[width, height];
            byte[] noiseSeed;
            noiseSeed = new byte[512];
            random.NextBytes(noiseSeed);


            Noise.perm = noiseSeed;

            // Here comes the fun stuff
            for (int x = 0; x < width; x++)
            {
                for (int y = 0; y < height; y++)
                {
                    byte cval = (byte)(Noise.Generate(x / 40f, y / 40f)*64+64);
                    cval += (byte)(Noise.Generate(x / 10f, y / 10f) * 32 + 32);
                    cval += (byte)(Noise.Generate(x / 5f, y / 5f) * 16 + 16);
                    cval += (byte)(Noise.Generate(x / 2f, y / 2f) * 8 + 8);
                    mapToReturn[x, y] = cval;
                }
            }

            return mapToReturn;
        }
        private static byte getValAt(int x, int y, float zoom)
        {
            return (byte)(Noise.Generate(x / zoom, y / zoom));
        }
    }
}
