using ImageToAscii;
using NUnit.Framework;

/* *****************************************************************************
 * ArgumentValidatorTests.cs
 * -----------------------------------------------------------------------------
 * The purpose of this class is to test the class ArgumentValidator contained 
 * in the file ArgumentValidator.cs.
 * -----------------------------------------------------------------------------
 * Notes:       None
 * -----------------------------------------------------------------------------
 * TODO:        None
 * -----------------------------------------------------------------------------
 * Author:      Jonas Nilsson
 * LastChanged: 2021-04-06
 * Version:     Version 1.0
 * ****************************************************************************/
namespace ImageToAsciiNUnitTests
{
    class ArgumentValidatorTests
    {
        // Used for all tests (except for Object_Creation_Not_Null)
        private ArgumentValidator argumentValidator = null;
        private ImageConverter imageConverter       = null;
        private string image_file_path              = null;
        private string save_file_path               = null;

        /* Setup
         * ---------------------------------------------------------
         * Used for all tests (except for Object_Creation_Not_Null)
         * ---------------------------------------------------------
         * Input:       None
         * Output:      None
         */
        [SetUp]
        public void Setup()
        {
            imageConverter  = null;
            image_file_path = null;
            save_file_path  = null;
        }

        /* Object_Creation_Not_Null
         * ---------------------------------------------------------
         * Check that object creation of ArgumentValidator is not
         * null.
         * ---------------------------------------------------------
         * Input:       None
         * Output:      None
         */
        [Test]
        public void Object_Creation_Not_Null()
        {
            Assert.NotNull(new ArgumentValidator(new string[] {"", ""}));
        }

        /* Too_Many_Arguments
         * ---------------------------------------------------------
         * Test creating an object of the ArgumentValidator with too
         * many arguments.
         * ---------------------------------------------------------
         * Input:       None
         * Output:      None
         */
        [Test]
        public void Too_Many_Arguments()
        {
            argumentValidator = new ArgumentValidator(
                new string[] { "1", "2", "3", "4", "5", "6" });
            Assert.IsFalse(argumentValidator.Validate(
                out imageConverter, out image_file_path, out save_file_path));
        }

        /* Too_Few_Arguments
         * ---------------------------------------------------------
         * Test creating an object of the ArgumentValidator with too
         * few arguments.
         * ---------------------------------------------------------
         * Input:       None
         * Output:      None
         */
        [Test]
        public void Too_Few_Arguments()
        {
            argumentValidator = new ArgumentValidator(
                new string[] { });
            Assert.IsFalse(argumentValidator.Validate(
                out imageConverter, out image_file_path, out save_file_path));
        }

        /* Valid_Image_File_Path
         * ---------------------------------------------------------
         * Test creating an object of the ArgumentValidator with
         * only one argument (the image file path).
         * ---------------------------------------------------------
         * Input:       None
         * Output:      None
         */
        [Test]
        public void Valid_Image_File_Path()
        {
            argumentValidator = new ArgumentValidator(
                new string[] {"./img.png"});
            Assert.IsTrue(argumentValidator.Validate(
                out imageConverter, out image_file_path, out save_file_path));
        }

        /* Create_Argument_Validator_Empty_Image_File_Path
         * ---------------------------------------------------------
         * Test creating an object of the ArgumentValidator with
         * an empty argument given (the image file path).
         * ---------------------------------------------------------
         * Input:       None
         * Output:      None
         */
        [Test]
        public void Create_Argument_Validator_Empty_Image_File_Path()
        {
            argumentValidator = new ArgumentValidator(
                new string[] { "" });
            Assert.IsFalse(argumentValidator.Validate(
                out imageConverter, out image_file_path, out save_file_path));
        }

        /* Set_Valid_Save_Path
         * ---------------------------------------------------------
         * Test creating an object of the ArgumentValidator with
         * the image file path as well as the --out- flag.
         * ---------------------------------------------------------
         * Input:       None
         * Output:      None
         */
        [Test]
        public void Set_Valid_Save_Path()
        {
            argumentValidator = new ArgumentValidator(
                new string[] { "./img.png", "--out-./tst.txt" });
            Assert.IsTrue(argumentValidator.Validate(
                out imageConverter, out image_file_path, out save_file_path));
        }

        /* Set_Custom_Save_File_Path_Empty_String
         * ---------------------------------------------------------
         * Test creating an object of the ArgumentValidator with
         * the image file path as well as the --out- flag with 
         * no path specified.
         * ---------------------------------------------------------
         * Input:       None
         * Output:      None
         */
        [Test]
        public void Set_Custom_Save_File_Path_Empty_String()
        {
            argumentValidator = new ArgumentValidator(
                new string[] { "./img.png", "--out-" });
            Assert.IsFalse(argumentValidator.Validate(
                out imageConverter, out image_file_path, out save_file_path));
        }

        /* Set_Valid_Greyscale
         * ---------------------------------------------------------
         * Test creating an object of the ArgumentValidator with
         * the image file path as well as the --gs- flag.
         * ---------------------------------------------------------
         * Input:       None
         * Output:      None
         */
        [Test]
        public void Set_Valid_Greyscale()
        {
            argumentValidator = new ArgumentValidator(
                new string[] { "./img.png", "--gs-avg" });
            Assert.IsTrue(argumentValidator.Validate(
                out imageConverter, out image_file_path, out save_file_path));
        }

        /* Set_Greyscale_Wrong_Argument
         * ---------------------------------------------------------
         * Test creating an object of the ArgumentValidator with
         * the image file path as well as the --gs- flag with wrong 
         * specification of gs type to use.
         * ---------------------------------------------------------
         * Input:       None
         * Output:      None
         */
        [Test]
        public void Set_Greyscale_Wrong_Argument()
        {
            argumentValidator = new ArgumentValidator(
                new string[] { "./img.png", "--gs-doesnotexist" });
            Assert.IsFalse(argumentValidator.Validate(
                out imageConverter, out image_file_path, out save_file_path));
        }

        /* Set_Valid_Ascii_Converter
         * ---------------------------------------------------------
         * Test creating an object of the ArgumentValidator with
         * the image file path as well as the --ac- flag.
         * ---------------------------------------------------------
         * Input:       None
         * Output:      None
         */
        [Test]
        public void Set_Valid_Ascii_Converter()
        {
            argumentValidator = new ArgumentValidator(
                new string[] { "./img.png", "--ac-px" });
            Assert.IsTrue(argumentValidator.Validate(
                out imageConverter, out image_file_path, out save_file_path));
        }

        /* Set_Ascii_Converter_Wrong_Argument
         * ---------------------------------------------------------
         * Test creating an object of the ArgumentValidator with
         * the image file path as well as the --ac- flag with wrong 
         * specification of ac type to use.
         * ---------------------------------------------------------
         * Input:       None
         * Output:      None
         */
        [Test]
        public void Set_Ascii_Converter_Wrong_Argument()
        {
            argumentValidator = new ArgumentValidator(
                new string[] { "./img.png", "--ac-doesnotexist" });
            Assert.IsFalse(argumentValidator.Validate(
                out imageConverter, out image_file_path, out save_file_path));
        }

        /* Set_Valid_Character_Map
         * ---------------------------------------------------------
         * Test creating an object of the ArgumentValidator with
         * the image file path as well as the --cm- flag.
         * ---------------------------------------------------------
         * Input:       None
         * Output:      None
         */
        [Test]
        public void Set_Valid_Character_Map()
        {
            argumentValidator = new ArgumentValidator(
                new string[] { "./img.png", "--cm-123456789" });
            Assert.IsTrue(argumentValidator.Validate(
                out imageConverter, out image_file_path, out save_file_path));
        }

        /* Set_Character_Map_Non_Ascii
         * ---------------------------------------------------------
         * Test creating an object of the ArgumentValidator with
         * the image file path as well as the --cm- flag, where the 
         * characters contains non-ascii characters.
         * ---------------------------------------------------------
         * Input:       None
         * Output:      None
         */
        [Test]
        public void Set_Character_Map_Non_Ascii()
        {
            argumentValidator = new ArgumentValidator(
                new string[] { "./img.png", "--cm-123456789Å" });
            Assert.IsFalse(argumentValidator.Validate(
                out imageConverter, out image_file_path, out save_file_path));
        }

        /* Argument_Duplicates
         * ---------------------------------------------------------
         * Test creating an object of the ArgumentValidator with
         * the image file path as well as two valid --gs- flags.
         * ---------------------------------------------------------
         * Input:       None
         * Output:      None
         */
        [Test]
        public void Argument_Duplicates()
        {
            argumentValidator = new ArgumentValidator(
                new string[] { "./img.png", "--gs-avg", "--gs-avg" });
            Assert.IsFalse(argumentValidator.Validate(
                out imageConverter, out image_file_path, out save_file_path));
        }

        /* All_Arguments_Valid
         * ---------------------------------------------------------
         * Test creating an object of the ArgumentValidator with
         * all possible arguments. All valid.
         * ---------------------------------------------------------
         * Input:       None
         * Output:      None
         */
        [Test]
        public void All_Arguments_Valid()
        {
            argumentValidator = new ArgumentValidator(new string[] {
                "./img.png", "--gs-avg", "--ac-px", "--out-./tst.txt", "--cm-123456789" 
            });
            Assert.IsTrue(argumentValidator.Validate(
                out imageConverter, out image_file_path, out save_file_path));
        }

        /* All_Arguments_Valid_Converter_Is_Not_Null
         * ---------------------------------------------------------
         * Test creating an object of the ArgumentValidator with
         * all possible arguments. All valid. 
         * Check that the image converter returned is not null.
         * ---------------------------------------------------------
         * Input:       None
         * Output:      None
         */
        [Test]
        public void All_Arguments_Valid_Converter_Is_Not_Null()
        {
            argumentValidator = new ArgumentValidator(new string[] {
                "./img.png", "--gs-avg", "--ac-px", "--out-./tst.txt", "--cm-123456789" 
            });
            argumentValidator.Validate(
                out imageConverter, out image_file_path, out save_file_path);
            Assert.NotNull(imageConverter);
        }

        /* Valid_Returned_Image_File_Path
         * ---------------------------------------------------------
         * Test creating an object of the ArgumentValidator with all
         * possible, all valid arguments. 
         * Check that the path to the image file is correct.
         * ---------------------------------------------------------
         * Input:       None
         * Output:      None
         */
        [Test]
        public void Valid_Returned_Image_File_Path()
        {
            argumentValidator = new ArgumentValidator(new string[] {
                "./img.png", "--gs-avg", "--ac-px", "--out-./tst.txt", "--cm-123456789" 
            });
            argumentValidator.Validate(
                out imageConverter, out image_file_path, out save_file_path);
            Assert.AreEqual("./img.png", image_file_path);
        }

        /* Valid_Returned_Save_File_Path
         * ---------------------------------------------------------
         * Test creating an object of the ArgumentValidator with all
         * possible, all valid arguments. 
         * Check that the path to save the file is correct.
         * ---------------------------------------------------------
         * Input:       None
         * Output:      None
         */
        [Test]
        public void Valid_Returned_Save_File_Path()
        {
            argumentValidator = new ArgumentValidator(new string[] {
                "./img.png", "--gs-avg", "--ac-px", "--out-./tst.txt", "--cm-123456789" 
            });
            argumentValidator.Validate(
                out imageConverter, out image_file_path, out save_file_path);
            Assert.AreEqual("./tst.txt", save_file_path);
        }
    }
}