using System.Collections.Generic;

/* *****************************************************************************
 * ACPx.cs (EXTENDS AsciiConverter)
 * -----------------------------------------------------------------------------
 * Converts an image represented as a 2D pixel array into a list of characters
 * containing the ASCII representation of the image.
 * 
 * This specific implementation does this by inspecting each individual pixel
 * of the image and replacing them by an ASCII character.
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
    public class ACPx : AsciiConverter
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
            for(int i = 0; i < pixels.GetLength(1); i++)
            {
                for (int j = 0; j < pixels.GetLength(0); j++)
                {
                    ascii.Add(characters[
                        (255 - pixels[j, i]) * 
                        characters.Length / 256
                        ]);
                }
                ascii.Add('\n');
            }
            return ascii;
        }
    }
}
