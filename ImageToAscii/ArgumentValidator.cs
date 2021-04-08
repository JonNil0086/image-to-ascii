using ImageToAscii.Interfaces;
using System;
using System.Linq;
using System.Collections.Generic;
using System.IO;

/* *****************************************************************************
 * ArgumentValidator.cs
 * -----------------------------------------------------------------------------
 * This class validates the command line input / arguments and creates an 
 * ImageConverter object as well as arguments to call the created image 
 * converter with.
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
namespace ImageToAscii
{
    public class ArgumentValidator
    {
        // The command line arguments
        private readonly string[] arguments;

        /* Dictionary containing parsed command line arguments
         * Key / Value:     
         *  (img_fp):   Path to image to convert
         *  (--cm-):    User specified character map
         *  (--gs-):    User specified greyscale type
         *  (--ac-):    User specified ascii convertion type
         *  (--out-):   User specified path to save result to    
         */
        private readonly Dictionary<string, string> parsedArgumentsDict;

        /* ArgumentValidator (Constructor)
         * ---------------------------------------------------------
         * Used for initialization.
         * ---------------------------------------------------------
         * Input:       (arguments): The command line arguments
         *                  given when the program is run.
         * Output:      None
         */
        public ArgumentValidator(string[] arguments)
        {
            this.arguments = arguments;
            parsedArgumentsDict = new Dictionary<string, string>();
        }

        /* Validate 
         * ---------------------------------------------------------
         * Validates the command line arguments, creates and returns
         * the image converter to use together with the 
         * image file path and the file path to save the results to.
         * ---------------------------------------------------------
         * Input:       None (out values that will be set)
         * Output:      Bool depending on result
         *              (out > imageConverter): Created image 
         *                  converter with the arguments given in
         *                  constructor.
         *              (out > image_file_path): The file path to
         *                  the image that is to be converted.
         *              (out > save_file_path): The path to the
         *                  resulting ASCII.
         */
        public bool Validate(out ImageConverter imageConverter
            , out string image_file_path, out string save_file_path)
        {   
            // Default out values
            imageConverter  = null;
            image_file_path = null;
            save_file_path  = null;
            // Basic Argument validation
            if (arguments.Length < 1 || arguments.Length > 5)
            {
                Console.Error.WriteLine("ERROR: Wrong amount of " 
                    + "command line arguments were given. ");
                PrintHowToRun();
                return false;
            }
            // Parse the arguments and validate / create needed inputs
            if (arguments.Length == 1 && string.Equals(arguments[0], "--help") 
                || !ParseArguments()
                || !GetFilePaths(out image_file_path, out save_file_path)
                || !CreateGreyscaleConverter(out IGreyscaleConverter greyscaleConverter)
                || !CreateAsciiConverter(out AsciiConverter asciiConverter))
            {
                PrintHowToRun();
                return false;
            }
            // Create the image converter
            imageConverter = new ImageConverter(greyscaleConverter, asciiConverter);
            return true;
        }

        /* ParseArguments
         * ---------------------------------------------------------
         * Parses raw command line arguments and performs minor
         * argument validations in order to fill a dictionary
         * with processed command line arguments.
         * NOTE: The command line arguments are further validated in
         *       their respective method in this class.
         * ---------------------------------------------------------
         * Input:       None (uses parsedArgumentsDict)
         * Output:      Bool depending on result.
         *              Modifies parsedArgumentsDict by adding 
         *              values.
         */
        private bool ParseArguments()
        {
            parsedArgumentsDict.Add("img_fp", arguments[0]);
            for (int i = 1; i < arguments.Length; i++)
            {
                string key = 
                    arguments[i].StartsWith("--out-")   ? "--out-" :
                    arguments[i].StartsWith("--gs-")    ? "--gs-" :
                    arguments[i].StartsWith("--ac-")    ? "--ac-" :
                    arguments[i].StartsWith("--cm-")    ? "--cm-" : null;
                if (key == null)
                {
                    Console.Error.WriteLine("ERROR: Argument: " + arguments[i] 
                        + " is not supported.");
                    return false;
                }
                if (parsedArgumentsDict.ContainsKey(key))
                {
                    Console.Error.WriteLine("ERROR: Cannot set " + key + " twice.");
                    return false;
                }
                parsedArgumentsDict.Add(key, arguments[i].Replace(key, ""));
            }
            return true;
        }

        /* GetFilePaths
         * ---------------------------------------------------------
         * Generates / creates the file paths to use as the image
         * location and the save destination.
         * ---------------------------------------------------------
         * Input:       None (out values that will be set)
         * Output:      Bool depending on result:
         *              True:   (out > image_file_path): Path to image
         *                      (out > save_file_path): destination 
         *                              to save result to
         *              False:  (out > image_file_path): null
         *                      (out > save_file_path): null
         */
        private bool GetFilePaths(out string image_file_path, out string save_file_path)
        {
            if (!parsedArgumentsDict.TryGetValue("img_fp", out image_file_path))
            { // No origin file path of the img has been set / given
                Console.Error.WriteLine("ERROR: No origin file path was set.");
                save_file_path = null;
                return false;
            }
            if (!parsedArgumentsDict.TryGetValue("--out-", out save_file_path))
            { // No out file specified. Create default destination path
                save_file_path = Path.GetDirectoryName(arguments[0])
                    + "/" + Path.GetFileNameWithoutExtension(arguments[0])
                    + "ASCII.txt";
            }
            if (string.IsNullOrEmpty(image_file_path) 
                || string.IsNullOrEmpty(save_file_path)) 
            { // Small check to see that there at least is something specified in the paths
                Console.Error.Write("ERROR: File paths cannot be null or empty.");
                return false;
            }
            return true;
        }

        /* CreateGreyscaleConverter
         * ---------------------------------------------------------
         * Creates a greyscale converter.
         * Checks for keys in parsedArgumentsDict if the user has
         * specified a specific greyscale converter to use.
         * This method also performs minor validation on the user 
         * input that specifies the greyscale converter to use if
         * any has been given.
         * ---------------------------------------------------------
         * Input:       None (out value that will be set)
         * Output:      Bool depending on result.
         *              True: (out) The greyscale converter to use
         *              False: (out) null
         */
        private bool CreateGreyscaleConverter(out IGreyscaleConverter greyscaleConverter)
        {
            if (!parsedArgumentsDict.TryGetValue("--gs-", out string gsType))
            { // User did not specify a greyscale type, set default
                gsType = "avg";
            }
            switch (gsType)
            {
                case "avg":
                    greyscaleConverter = new GSAverage();
                    break;
                case "wl":
                    greyscaleConverter = new GSWavelength();
                    break;
                default:
                    Console.Error.WriteLine("ERROR: Wrong argument given for" 
                        + " greyscale type.");
                    greyscaleConverter = null;
                    return false;
            }
            return true;
        }

        /* CreateAsciiConverter
         * ---------------------------------------------------------
         * Creates an ASCII converter and sets a custom character
         * map if there is a new one to set.
         * Checks for keys in parsedArgumentsDict if the user has
         * specified a what ascii converter to use and/or a custom
         * character map.
         * This method also performs minor validation on the user 
         * input that specifies the ascii converter to use as well
         * as the custom character map if any has been given.        
         * ---------------------------------------------------------
         * Input:       None (out value that will be set)
         * Output:      Bool depending on result.
         *              True: (out) converter to use
         *              False: (out) null
         */
        private bool CreateAsciiConverter(out AsciiConverter asciiConverter)
        {
            if (!parsedArgumentsDict.TryGetValue("--ac-", out string acType))
            { // User did not specify ascii convertion type, set default
                acType = "px";
            }
            switch (acType)
            {
                case "px":
                    asciiConverter = new ACPx();
                    break;
                case "area":
                    asciiConverter = new ACArea();
                    break;
                default:
                    Console.Error.WriteLine("ERROR: Wrong argument given for "
                        + "ASCII converison type.");
                    asciiConverter = null;
                    return false;
            }
            // Check if the user entered a character map
            if (parsedArgumentsDict.TryGetValue("--cm-", out string characters))
            { // Make sure the string is ASCII, then convert it to char[]
                if (characters.Any(c => c >= 128))
                {
                    Console.Error.WriteLine("ERROR: The character map needs " 
                        + "to only contain ASCII characters.");
                    asciiConverter = null;
                    return false;
                }
                asciiConverter.SetCharacterMap(
                    characters.ToCharArray(0, characters.Length));
            }
            return true;
        }

        /* PrintHowToRun 
         * ---------------------------------------------------------
         * Prints how to run the program.
         * ---------------------------------------------------------
         * Input:       None
         * Output:      None
         */
        private void PrintHowToRun()
        {
            Console.WriteLine("".PadRight(34, '*') + "\n" +
                " IMAGE TO ASCII CONVERTER\n" +
                " Made by:\t Jonas Nilsson\n" +
                " Version:\t 1.0 (2021-03-31)\n" +
                "".PadRight(34, '*'));
            Console.WriteLine("\n The program is run in the following way:\n > " +
                System.Diagnostics.Process.GetCurrentProcess().ProcessName + 
                " image_file_path option1 option2 ...'");
            Console.WriteLine(" Example: " + 
                System.Diagnostics.Process.GetCurrentProcess().ProcessName +
                 " ./img.png --out-./img.txt --gs-wl --ac-px --cm-12345\n");
            Console.WriteLine("".PadRight(44, '-') + "\n" +
                " ARGUMENT EXPLENATION AND AVAILABLE OPTIONS\n" +
                "".PadRight(44, '-') + "\n" +
                "\n (REQUIRED) | IMAGE FILE PATH | KEYWORD: NONE (FIRST PARAMETER)\n" +
                "  - The file path to the image to convert to ASCII.\n" +
                "\n (OPTIONAL) | SAVE FILE PATH | KEYWORD: --out-\n" +
                "  - The command --out- followed by a file path can be used to specify a custom save location.\n" +
                "  - Note: If this option is left out, the result will be saved at: ./img_name + ASCII.txt\n" +
                "  - Example: Adding --out-./abc.txt\n" +
                "\n (OPTIONAL) | SPECIFYING GREYSCALE TYPE | KEYWORD: --gs-\n" +
                "  - The method to use to convert the image to greyscale before ASCII conversion.\n" +
                "  - Options (default --gs-avg):\n" +
                "\t > --gs-avg: Average RGB values and divide by 3.\n" +
                "\t > --gs-wl: Same as gsAverage but also consider that each RGB value is visualized\n" +
                "\t   differently, thus multiplying each RGB value with wavelength constants before division.\n" +
                "\n (OPTIONAL) | SPECIFYING ASCII CONVERSION TYPE | KEYWORD: --ac-\n" +
                "  - The method to use to convert the image to ASCII.\n" +
                "  - Options (default: --ac-acPx):\n" +
                "\t > --ac-px: Replace each pixel with a character depending on 'brightness' of the pixel.\n" +
                "\t > --ac-area: Same as acPx but also considering pixel neighbors.\n" +
                "\t   Thus, add the pixel and its neighbors values and take their average\n" +
                "\t   before replacing the characters.\n" +
                "\n (OPTIONAL) | SPECIFYING CHARACTER MAP | KEYWORD: --cm-\n" +
                "  - The command --cm- followed by characters can be used to specify what characters to replace pixels with.\n" +
                "  - The character map is linear, meaning that pixels are replaced from left to right depending on a pixels\n" +
                "    'brightness'.\n" +
                "  - Example: Adding --cm-12345 will use 12345 to replace pixels.\n" +
                "\n NOTE: Run the program with the command --help to display this text again.\n"
                );
        }
    }
}