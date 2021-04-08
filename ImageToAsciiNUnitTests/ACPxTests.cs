using ImageToAscii;
using NUnit.Framework;
using System.Collections.Generic;

/* *****************************************************************************
 * ACPxTests.cs
 * -----------------------------------------------------------------------------
 * The purpose of this class is to test the class ACPx contained in the file 
 * ACPx.cs.
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
    class ACPxTests
    {
        // Used for all tests (except for Object_Creation_Not_Null)
        private ACPx aCPx = null;
        private readonly char[] characterMap = {
                '0', '1', '2', '3', '4', '5', '6', '7', '8', '9'
            };

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
            aCPx = new ACPx();
            aCPx.SetCharacterMap(characterMap);
        }

        /* Object_Creation_Not_Null
         * ---------------------------------------------------------
         * Check that object creation of ACPx is not null.
         * ---------------------------------------------------------
         * Input:       None
         * Output:      None
         */
        [Test]
        public void Object_Creation_Not_Null()
        {
            Assert.NotNull(new ACPx());
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
            List<char> expected = new List<char>();
            List<char> ascii = aCPx.Convert(new int[100, 100]);
            for (int i = 0; i < 100; i++)
            {
                for (int j = 0; j < 100; j++)
                {
                    expected.Add('9');
                }
                expected.Add('\n');
            }
            Assert.AreEqual(expected, ascii);
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
            int[,] px255 = new int[100, 100];
            List<char> expected = new List<char>();
            for (int i = 0; i < px255.GetLength(0); i++)
            {
                for (int j = 0; j < px255.GetLength(1); j++)
                {
                    px255[i, j] = 255;
                    expected.Add('0');
                }
                expected.Add('\n');
            }
            List<char> ascii = aCPx.Convert(px255);
            Assert.AreEqual(expected, ascii);
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
            /*
             * We call convert on the 2D pixel array (A) and we expect
             * the the convertion result (R) with the character map
             * ['0', '1', '2', '3','4','5','6','7','8','9'] to be:
             * 
             *          (A)                             (R)
             *      50  100 150                 '8', '6', '4', '\n',
             *      100 150 200 --(convert)-->  '8', '6', '4', '\n',
             *      150 200 250                 '4', '2', '0', '\n'
             *      
             * (Note: See ACLinearPx.cs for more info)
             */
            List<char> expected = new List<char>(new char[] {
                '8', '6', '4', '\n',
                '6', '4', '2', '\n',
                '4', '2', '0', '\n'
            });
            List<char> ascii = aCPx.Convert(new int[,] {
                { 50, 100, 150 }, { 100, 150, 200 }, { 150, 200, 250 }
            });
            Assert.AreEqual(expected, ascii);
        }
    }
}