using ImageToAscii.Interfaces;
using System;
using System.Drawing;

/* *****************************************************************************
 * GSWavelength.cs (IMPLEMENTS IGreyscaleConverter)
 * -----------------------------------------------------------------------------
 * Used to convert a bitmap image into greyscale by averaging the RGB values
 * for each pixel but to also consider the different visual light wavelengths
 * for each pixel color.
 * -----------------------------------------------------------------------------
 * Notes:       None
 * -----------------------------------------------------------------------------
 * TODO:        None
 * -----------------------------------------------------------------------------
 * Author:      Jonas Nilsson
 * LastChanged: 2021-04-06
 * Version:     Version 2.0 (Version 1.0 written: 2019-04-16)
 * ****************************************************************************/
namespace ImageToAscii
{
    public class GSWavelength : IGreyscaleConverter
    {
        /* Wave length constants 
         * (better greyscale if wavelength of the colors are considered)
         */
        private const float WAVE_LENGTH_R = 0.3f;
        private const float WAVE_LENGTH_G = 0.59f;
        private const float WAVE_LENGTH_B = 0.11f;

        /* Convert 
         * ---------------------------------------------------------
         * Used to convert the bitmap input to greyscale and then
         * returning the result as a pixel array.
         * ---------------------------------------------------------
         * Input:       (bitmap): The image to convert to 
         *                  greyscale.
         * Output:      Pixels of the bitmap in greyscale, i.e. 
         *              1 px representation of the image
         */
        public int[,] Convert(Bitmap bitmap)
        {
            int[,] pixels = new int[bitmap.Width, bitmap.Height];
            for (int i = 0; i < bitmap.Width; i++)
            {
                for (int j = 0; j < bitmap.Height; j++)
                {
                    Color color = bitmap.GetPixel(i, j);
                    int avg = (int) Math.Round(
                        ((WAVE_LENGTH_R * color.R) + 
                        (WAVE_LENGTH_G * color.G) + 
                        (WAVE_LENGTH_B * color.B)) / 3.0f);
                    pixels[i, j] = avg;
                }
            }
            return pixels;
        }
    }
}