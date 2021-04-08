using System;
using System.Collections.Generic;

/* *****************************************************************************
 * ACArea.cs (EXTENDS AsciiConverter)
 * -----------------------------------------------------------------------------
 * Converts an image represented as a 2D pixel array into a list of characters
 * containing the ASCII representation of the image.
 * 
 * This specific implementation does this by inspecting each individual pixel
 * of the image, as well as all neighbors of that pixel, summing them up and
 * replacing them by an ASCII character.
 * The replacement is done by checking the pixel brightness(intensity) and then
 * adding a character from the characters array.
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
    public class ACArea : AsciiConverter
    {
        /* Convert 
         * ---------------------------------------------------------
         * Used to convert an image represented as a 2D pixel array
         * into an ASCII representation of the image.
         * ---------------------------------------------------------
         * Input:       (pixels): A 2D pixel array containing the
         *                  pixels in the image (0 - 255).
         * Output:      List of characters representing the ASCII 
         *                  representation.
         */
        public override List<char> Convert(int[,] pixels)
        {
            List<char> ascii = new List<char>();
            float[,] pxIntensities = CalculateIntensity(pixels);
            for (int i = 0; i < pixels.GetLength(1); i++)
            {
                for (int j = 0; j < pixels.GetLength(0); j++)
                {
                    ascii.Add(characters[ 
                        (255 - (int)Math.Round(pxIntensities[j, i], 0)) * 
                        characters.Length / 256
                        ]);
                }
                ascii.Add('\n');
            }
            return ascii;
        }

        /* CalculateIntensity 
         * ---------------------------------------------------------
         * Used to calculate the intensity of each pixel in the
         * 2D array. 
         * This is done by summing up the pixel, as well
         * as all neighbors of the pixel and taking their average.
         * ---------------------------------------------------------
         * Input:       (pixels): A 2D pixel array containing the
         *                  pixels in the image (0 - 255).
         * Output:      2D float array containing pixel intensities.
         */
        private float[,] CalculateIntensity(int[,] pixels)
        {
            /* actual value, NW, N, NE, E, SE, S, SW, W
             * Match the arrays with loop indices to get
             * the pixel all neighbors for a pixel.
             */
            int[] br = { 0, -1, -1, -1, 0, 1, 1, 1, 0 };
            int[] bc = { 0, -1, 0, 1, 1, 1, 0, -1, -1 };
            // Ref variables
            int width = pixels.GetLength(0);
            int height = pixels.GetLength(1);
            float [,] result = new float[width, height];
            for (int i = 0; i < width; i++)
            {
                for (int j = 0; j < height; j++)
                {
                    // Calculate each pixels intensity by averaging neighbours
                    float pxIntensity = 0.0f;
                    int calculations = 0;
                    for (int k = 0; k < br.Length; k++)
                    {
                        if (br[k] + i < width && br[k] + i >= 0
                            && bc[k] + j < height && bc[k] + j >= 0)
                        { // Pixel neighbor is not out of bounds
                            int px = pixels[br[k] + i, bc[k] + j];
                            pxIntensity += px;
                            calculations++;
                        }
                    }
                    result[i, j] = (pxIntensity / calculations);
                }
            }
            return result;
        }
    }
}