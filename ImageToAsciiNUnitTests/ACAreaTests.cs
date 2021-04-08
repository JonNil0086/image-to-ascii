using ImageToAscii;
using NUnit.Framework;
using System.Collections.Generic;

/* *****************************************************************************
 * ACAreaTests.cs
 * -----------------------------------------------------------------------------
 * The purpose of this class is to test the class ACArea contained in the file 
 * ACArea.cs.
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
    class ACAreaTests
    {
        // Used for all tests (except for Object_Creation_Not_Null)
        private ACArea aCArea = null;
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
            aCArea = new ACArea();
            aCArea.SetCharacterMap(characterMap);
        }

        /* Object_Creation_Not_Null
         * ---------------------------------------------------------
         * Check that object creation of ACArea is not null.
         * ---------------------------------------------------------
         * Input:       None
         * Output:      None
         */
        [Test]
        public void Object_Creation_Not_Null()
        {
            Assert.NotNull(new ACArea());
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
            List<char> ascii = aCArea.Convert(new int[100, 100]);
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
            List<char> ascii = aCArea.Convert(px255);
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
             * the algorithm to first average neighbors into (B):
             * 
             *          (A)                         (B)
             *      50  100 150                 100 125 150
             *      100 150 200 ------------->  125 150 175
             *      150 200 250                 150 175 200
             *      
             * Then we expect the algorithm to convert (B) into the final
             * result (R) if the following character map is used:
             * ['0', '1', '2', '3','4','5','6','7','8','9']. 
             * 
             * Thus, the finally output should be:
             * 
             *      '6', '5', '4', '\n',
             *      '5', '4', '3', '\n',
             *      '4', '3', '2', '\n'
             *      
             * (Note: See ACLinearArea.cs for more info)
             */
            List<char> expected = new List<char>(new char[] {
                '6', '5', '4', '\n',
                '5', '4', '3', '\n',
                '4', '3', '2', '\n'
            });
            List<char> ascii = aCArea.Convert(new int[,] {
                { 50, 100, 150 }, { 100, 150, 200 }, { 150, 200, 250 }
            });
            Assert.AreEqual(expected, ascii);
        }
    }
}