using ImageToAscii;
using NUnit.Framework;
using System;
using System.Drawing;
using System.Drawing.Imaging;

/* *****************************************************************************
 * GSWavelengthTests.cs
 * -----------------------------------------------------------------------------
 * The purpose of this class is to test the class GSWavelength contained in the
 * file GSWavelength.cs.
 * -----------------------------------------------------------------------------
 * Notes:       If WAVE_LENGTH_R, WAVE_LENGTH_G and WAVE_LENGTH_B are changed
 *              in GSWavelength.cs, then remember to change them here as well.
 * -----------------------------------------------------------------------------
 * TODO:        None
 * -----------------------------------------------------------------------------
 * Author:      Jonas Nilsson
 * LastChanged: 2021-04-06
 * Version:     Version 1.0
 * ****************************************************************************/
namespace ImageToAsciiNUnitTests
{
    class GSWavelengthTests
    {
        // Used for all tests (except for Object_Creation_Not_Null)
        private GSWavelength gsWl           = null;
        private int[,] expected             = null;
        private int[,] converted            = null;
        private Bitmap bitmap               = null;
        private const float WAVE_LENGTH_R   = 0.3f;
        private const float WAVE_LENGTH_G   = 0.59f;
        private const float WAVE_LENGTH_B   = 0.11f;

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
            gsWl        = new GSWavelength();
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
            gsWl        = null;
            expected    = null;
            converted   = null;
        }


        /* Object_Creation_Not_Null
         * ---------------------------------------------------------
         * Check that object creation of GSWavelength is not null.
         * ---------------------------------------------------------
         * Input:       None
         * Output:      None
         */
        [Test]
        public void Object_Creation_Not_Null()
        {
            Assert.NotNull(new GSWavelength());
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
            converted = gsWl.Convert(bitmap);
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
                    expected[i, j] = (int)Math.Round((
                        (WAVE_LENGTH_R * 255) +
                        (WAVE_LENGTH_G * 255) +
                        (WAVE_LENGTH_B * 255)) / 3.0f);
                    bitmap.SetPixel(
                        i, j, Color.FromArgb(255, 255, 255, 255));
                }
            }
            converted = gsWl.Convert(bitmap);
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
                    expected[i, j] = (int)Math.Round((
                        (WAVE_LENGTH_R * (i + j)) +
                        (WAVE_LENGTH_G * (i + j)) +
                        (WAVE_LENGTH_B * (i + j))) / 3.0f);
                    bitmap.SetPixel(i, j, Color.FromArgb(
                        (i + j), (i + j), (i + j), (i + j)));
                }
            }
            converted = gsWl.Convert(bitmap);
            Assert.AreEqual(expected, converted);
        }
    }
}