using ImageToAscii;
using NUnit.Framework;

/* *****************************************************************************
 * ImageConverterTests.cs
 * -----------------------------------------------------------------------------
 * The purpose of this class is to test the class ImageConverter contained in 
 * the file ImageConverter.cs.
 * -----------------------------------------------------------------------------
 * Notes:       We only test object creation here because the class only act as
 *              a wrapper for other classes in the program.
 *              Thus it is tested enough by testing each individual part.
 * -----------------------------------------------------------------------------
 * TODO:        None
 * -----------------------------------------------------------------------------
 * Author:      Jonas Nilsson
 * LastChanged: 2021-04-06
 * Version:     Version 1.0
 * ****************************************************************************/
namespace ImageToAsciiNUnitTests
{
    class ImageConverterTests
    {
        /* Object_Creation_Not_Null
         * ---------------------------------------------------------
         * Check that object creation of ImageConverter is not null.
         * ---------------------------------------------------------
         * Input:       None
         * Output:      None
         */
        [Test]
        public void Object_Creation_Not_Null()
        {
            Assert.NotNull(new ImageConverter(new GSAverage(), new ACPx()));
        }
    }
}