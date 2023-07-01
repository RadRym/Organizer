using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrganizerV2.TeklaModels.Views
{
    public class GenerateColors
    {
        public static int[][] GenerateRainbowColorsWithoutRed(int n)
        {
            int[][] colors = new int[n][];

            double hueStep = 300.0 / n;
            double startHue = 60.0;

            for (int i = 0; i < n; i++)
            {
                double hue = startHue + (i * hueStep);
                int[] color = HsvToRgb(hue, 1.0, 1.0);
                colors[i] = color;
            }

            return colors;
        }

        public static int[][] GenerateRainbowColors(int n)
        {
            int[][] colors = new int[n][];

            double hueStep = 360.0 / n;

            for (int i = 0; i < n; i++)
            {
                double hue = i * hueStep;
                int[] color = HsvToRgb(hue, 1.0, 1.0);
                colors[i] = color;
            }

            return colors;
        }

        public static int[] HsvToRgb(double hue, double saturation, double value)
        {
            double c = value * saturation;
            double x = c * (1 - Math.Abs((hue / 60) % 2 - 1));
            double m = value - c;

            double red, green, blue;

            if (hue >= 60 && hue < 120)
            {
                red = x;
                green = c;
                blue = 0;
            }
            else if (hue >= 120 && hue < 180)
            {
                red = 0;
                green = c;
                blue = x;
            }
            else if (hue >= 180 && hue < 240)
            {
                red = 0;
                green = x;
                blue = c;
            }
            else if (hue >= 240 && hue < 300)
            {
                red = x;
                green = 0;
                blue = c;
            }
            else
            {
                red = c;
                green = 0;
                blue = x;
            }

            int[] rgb = new int[]
            {
            (int)((red + m) * 255),
            (int)((green + m) * 255),
            (int)((blue + m) * 255)
            };

            return rgb;
        }
    }
}
