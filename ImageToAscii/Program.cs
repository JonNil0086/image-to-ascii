/* *****************************************************************************
 * Program.cs
 * -----------------------------------------------------------------------------
 * This program converts an image to an ASCII representation.
 * 
 * The program supports the following arguments:
 * 
 *      REQUIRED, ALWAYS FIRST ARGUMENT: image_file_path
 *      ------------------------------------------------
 *      The image file path to read and create an ASCII representation of.
 *          Example run:
 *          > program_name ./img.png
 *          
 *      OPTIONAL: --help
 *      ----------------
 *      Used to get help about how to run the program, as well as information
 *      about supported arguments.
 *          Example run:
 *          > program_name --help
 *      
 *      OPTIONAL: --out-
 *      ----------------
 *      Used to specify a file path to save the result to.
 *          Example run:
 *          > program_name ./img.png --out-./ascii.txt
 *          
 *      OPTIONAL: --gs-
 *      ---------------
 *      Used to specify one of the available greyscale types used to
 *      convert the image to greyscale before converting it to ASCÍI.
 *      
 *          Supported --gs- options
 *          -----------------------
 *              1. --gs-avg     (see GSAverage.cs)
 *              2. --gs-wl      (see GSWavelength.cs)
 *          Example run:
 *          > program_name ./img.png --gs-avg
 *          
 *      OPTIONAL: --ac-
 *      ---------------
 *      Used to specify one of the available algorithms used to convert the
 *      image to ascii.
 *      
 *          Supported --ac- options
 *          -----------------------
 *              1. --ac-px      (see ACPx.cs)
 *              2. --ac-area    (see ACArea.cs)
 *          Example run:
 *          > program_name ./img.png --ac-px
 *          
 *      OPTIONAL: --cm-
 *      ---------------
 *      Used to specify an own character map that is used to replace the
 *      pixels with (only ASCII characters are supported).
 *          Example run:
 *          > program name ./img.png --cm-123456789
 *          
 * Ways to run the program:
 * 
 *      Not specifying --out-
 *      ---------------------
 *      Reads the image at the given file path( which is required) and creates 
 *      an ASCII representation of the image and saves it to the same folder 
 *      (location) with the name image_name + ASCII.txt.
 *          Example: 
 *          Result of convertion of an image located at './img.png' is saved as:
 *          './imgASCII.txt'
 *      
 *      Image file path + more arguments
 *      --------------------------------
 *      Any combination of supported arguments can be added together with the
 *      required image file path.
 *          Eample:
 *          > prog_name ./img.png --gs-avg --ac-px --cm-123 --out-./ascii.txt
 * -----------------------------------------------------------------------------
 * Notes:       None
 * -----------------------------------------------------------------------------
 * TODO:        None
 * -----------------------------------------------------------------------------
 * Author:      Jonas Nilsson
 * LastChanged: 2021-04-06
 * Version:     Version 2.0 (Version 1.0 written: 2019-04-16)
 * ****************************************************************************/

using System;

namespace ImageToAscii
{
    public class Program
    {
        static void Main(string[] args)
        {
            if (!new ArgumentValidator(args).Validate(
                out ImageConverter imageConverter,
                out string image_file_path,
                out string save_file_path))
            {
                return;
            }
            if(imageConverter.Convert(image_file_path, save_file_path))
            {
                Console.WriteLine(
                    "Convertion of image to ascii was successful.\n" +
                    "Image saved at: " + save_file_path
                    );
            }
        }
    }
}