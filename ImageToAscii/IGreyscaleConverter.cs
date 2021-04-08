using System.Drawing;

/* *****************************************************************************
 * IGreyscaleConverter.cs (INTERFACE)
 * -----------------------------------------------------------------------------
 * Interface used for different implementations of converting a bitmap image
 * into greyscale as a 2D pixel array.
 * -----------------------------------------------------------------------------
 * Notes:       For a new implementation of this class, the developer also
 *              needs to update one switch statement in the 
 *              "CreateGreyscaleConverter" method contained in the
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
namespace ImageToAscii.Interfaces
{
    public interface IGreyscaleConverter
    {
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
        int[,] Convert(Bitmap bitmap);
    }
}