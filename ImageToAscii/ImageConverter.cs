using ImageToAscii.Interfaces;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;

/* *****************************************************************************
 * ImageConverter.cs
 * -----------------------------------------------------------------------------
 * Used to convert an image to an ASCII representation.
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
    public class ImageConverter
    {
        // Injections
        private readonly IGreyscaleConverter         greyscale       = null;
        private readonly AsciiConverter     asciiConverter  = null;

        /* ImageReader (Constructor)
         * ---------------------------------------------------------
         * Used for dependency injection / initialization.
         * ---------------------------------------------------------
         * Input:       (greyscale): Used for converting an image
         *                  to greyscale
         *              (asciiConverter): Used to convert the image to 
         *                  an ASCII representation.
         * Output:      None
         */
        public ImageConverter(IGreyscaleConverter greyscale, AsciiConverter asciiConverter)
        {
            this.greyscale = greyscale;
            this.asciiConverter = asciiConverter;
        }

        /* Convert 
         * ---------------------------------------------------------
         * Used in order to convert an image to ASCII and then 
         * save the resulting ASCII to a given file path.
         * ---------------------------------------------------------
         * Input:       (image_file_path): The file path of the 
         *                  image to convert.
         *              (save_file_path): The file path used for
         *                  saving the resulting ASCII to.
         * Output:      Bool depending on result
         */
        public bool Convert(string image_file_path, string save_file_path)
        {
            try
            {
                // Load the image, Convert to greyscale
                Bitmap bitmap = LoadImage(image_file_path);
                int[,] pixels = greyscale.Convert(bitmap);
                bitmap.Dispose();
                // Convert the image to ASCII and save result
                List<char> ascii = asciiConverter.Convert(pixels);
                Save(save_file_path, ascii);
            }
            catch(Exception)
            {
                return false;
            }
            return true;
        }

        /* LoadImage 
         * ---------------------------------------------------------
         * Used in order to load an image into memory.
         * ---------------------------------------------------------
         * Input:       (image_file_path): The file path to the
         *                  image that is to be loaded.
         * Output:      The loaded image as bitmap
         */
        private Bitmap LoadImage(string image_file_path)
        {
            Bitmap bitmap = null;
            try
            {
                using Image image = Image.FromFile(image_file_path);
                bitmap = new Bitmap(image);
            }
            catch (Exception e)
            {
                Console.Error.WriteLine("ERROR: Could not read /"
                    + " process the file at path: " + image_file_path);
                Console.Error.WriteLine(e);
                throw;
            }
            return bitmap;
        }

        /* Save 
         * ---------------------------------------------------------
         * Used for saving the resulting ASCII to a file path.
         * ---------------------------------------------------------
         * Input:       (save_file_path): The location to save the
         *                  result to.
         *              (ascii): The ASCII representation of the
         *                  image to save.
         * Output:      None
         */
        private void Save(string save_file_path, List<char> ascii)
        {
            string data = new string(ascii.ToArray());
            try
            {
                using StreamWriter writeText 
                    = new StreamWriter(save_file_path);
                writeText.Write(data);
            }
            catch (Exception e)
            {
                Console.Error.WriteLine("ERROR: Could not save "
                    + " the file to path: " + save_file_path);
                Console.Error.WriteLine(e);
                throw;
            }
        }
    }
}