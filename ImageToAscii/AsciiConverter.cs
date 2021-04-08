using System.Collections.Generic;

/* *****************************************************************************
 * AsciiConverter.cs (ABSTRACT)
 * -----------------------------------------------------------------------------
 * Abstract class used for different implementations of converting an image in 
 * greyscale (represented as a 2D pixel array) into ASCII.
 * -----------------------------------------------------------------------------
 * Notes:       For a new implementation of this class, the developer also
 *              needs to update one switch statement in the 
 *              "ValidateCreateGreyscaleType" method contained in the
 *              ArgumentValidator.cs class.
 *              However, this is the ONLY switch statement that needs to be
 *              updated in the whole program for a new implementation.
 * -----------------------------------------------------------------------------
 * TODO:        None
 * -----------------------------------------------------------------------------
 * Author:      Jonas Nilsson
 * LastChanged: 2021-04-06
 * Version:     Version 2.0 (Version 1.0 written: 2019-04-16)
 * ****************************************************************************/
namespace ImageToAscii
{
    public abstract class AsciiConverter
    {
        /* Contains all characters used in the ASCII transformation.
         * The intensity increases from left to right.
         * Pixels are mapped to characters depending on their brightness.
         */
        protected char[] characters = {
            ' ', '.', 'm', ':',';','o','x','%','#','@','"'
        };

        /* Convert 
         * ---------------------------------------------------------
         * Used to convert an image represented as a 2D pixel array
         * into ASCII.
         * ---------------------------------------------------------
         * Input:       (pixels): A 2D pixel array containing the
         *                  pixels in the image (0 - 255).
         * Output:      List of characters representing the ASCII 
         *                  representation.
         */
        public abstract List<char> Convert(int[,] pixels);

        /* SetCharacterMap 
         * ---------------------------------------------------------
         * Used to set an ASCII character map.
         * ---------------------------------------------------------
         * Input:       (characters): The characters to use when
         *                  converting to ascii.
         * Output:      None
         */
        public void SetCharacterMap(char[] characters)
        {
            this.characters = characters;
        }
    }
}