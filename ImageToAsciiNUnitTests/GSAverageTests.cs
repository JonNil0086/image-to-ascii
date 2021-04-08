using ImageToAscii;
using NUnit.Framework;
using System.Drawing;
using System.Drawing.Imaging;

/* *****************************************************************************
 * GSAverageTests.cs
 * -----------------------------------------------------------------------------
 * The purpose of this class is to test the class GSAverage contained in the
 * file GSAverage.cs.
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
    class GSAverageTests
    {
        // Used for all tests (except for Object_Creation_Not_Null)
        private GSAverage gsAvg     = null;
        private int[,] expected     = null;
        private int[,] converted    = null;
        private Bitmap bitmap       = null;

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
            gsAvg       = new GSAverage();
            expected    = new int[100, 100];
            converted   = null;
            bitmap      = new Bitmap(
                100, 100, PixelFormat.Format32bppArgb);
        }

        /* TearDown
         * ---------------------------------------------------------
         * Used for cleaning up after tests have been run.
         * ---------------------------------------------------------
         * Input:       None
         * Output:      None
         */
        [TearDown]
        public void TearDown()
        {
            bitmap.Dispose();
            bitmap      = null;
            gsAvg       = null;
            expected    = null;
            converted   = null;
        }

        /* Object_Creation_Not_Null
         * ---------------------------------------------------------
         * Check that object creation of GSAverage is not null.
         * ---------------------------------------------------------
         * Input:       None
         * Output:      None
         */
        [Test]
        public void Object_Creation_Not_Null()
        {
            Assert.NotNull(new GSAverage());
        }

        /* Convert_Only_Pixels_0
         * ---------------------------------------------------------
         * Test converting a pixel array containing only pixels of
         * 0 (min value of a pixel).
         * ---------------------------------------------------------
         * Input:       None
         * Output:      None
         */
        [Test]
        public void Convert_Only_Pixels_0()
        {
            for (int i = 0; i < bitmap.Width; i++)
            {
                for (int j = 0; j < bitmap.Height; j++)
                {
                    bitmap.SetPixel(
                        i, j, Color.FromArgb(0, 0, 0, 0));
                }
            }
            converted = gsAvg.Convert(bitmap);
            Assert.AreEqual(expected, converted);
        }

        /* Convert_Only_Pixels_255
         * ---------------------------------------------------------
         * Test converting a pixel array containing only pixels of
         * 255 (max value of a pixel).
         * ---------------------------------------------------------
         * Input:       None
         * Output:      None
         */
        [Test]
        public void Convert_Only_Pixels_255()
        {
            for (int i = 0; i < bitmap.Width; i++)
            {
                for (int j = 0; j < bitmap.Height; j++)
                {
                    expected[i, j] = 255;
                    bitmap.SetPixel(
                        i, j, Color.FromArgb(255, 255, 255, 255));
                }
            }
            converted = gsAvg.Convert(bitmap);
            Assert.AreEqual(expected, converted);
        }

        /* Convert_Mixed_Pixels
         * ---------------------------------------------------------
         * Test converting pixels with mixed values.
         * ---------------------------------------------------------
         * Input:       None
         * Output:      None
         */
        [Test]
        public void Convert_Mixed_Pixels()
        {
            for (int i = 0; i < bitmap.Width; i++)
            {
                for (int j = 0; j < bitmap.Height; j++)
                {
                    expected[i, j] = (i + j);
                    bitmap.SetPixel(i, j, Color.FromArgb(
                        (i + j), (i + j), (i + j), (i + j)));
                }
            }
            converted = gsAvg.Convert(bitmap);
            Assert.AreEqual(expected, converted);
        }
    }
}