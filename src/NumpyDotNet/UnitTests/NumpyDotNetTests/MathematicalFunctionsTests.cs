﻿using System;
using System.Numerics;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NumpyDotNet;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using NumpyLib;

namespace NumpyDotNetTests
{
    [TestClass]
    public class MathematicalFunctionsTests : TestBaseClass
    {
        #region Trigonometric Functions
        [TestMethod]
        public void test_sin_1()
        {
            var ExpectedResult = new double[] { 0, 0.909297426825682, -0.756802495307928, -0.279415498198926, 0.989358246623382 };

            var a = np.arange(0, 10, dtype : np.Float64);
            a = a["::2"] as ndarray;
            var b = np.sin(a);
            AssertArray(b, ExpectedResult);
            print(b);

            a = np.arange(0, 10, dtype : np.Float32);
            a = a["::2"] as ndarray;
            b = np.sin(a);
            AssertArray(b, ExpectedResult);
            print(b);

            a = np.arange(0, 10, dtype : np.Int16);
            a = a["::2"] as ndarray;
            b = np.sin(a);
            AssertArray(b, ExpectedResult);
            print(b);

            print("********");

            a = np.arange(0, 10, dtype: np.Float64).reshape((1,2,5));
            a = a["::2"] as ndarray;
            b = np.sin(a);

            var ExpectedDataB = new double[,,]
                {{{ 0,                  0.841470984807897, 0.909297426825682, 0.141120008059867, -0.756802495307928},
                  {-0.958924274663138, -0.279415498198926, 0.656986598718789, 0.989358246623382,  0.412118485241757}}};

            AssertArray(b, ExpectedDataB);
            print(b);

            print("********");

            a = np.array(new int[,] {{0, 1, 2, 3, 4},{5,6,7,8,9}});
            a = a["::2"] as ndarray;
            b = np.sin(a, where: a > 2);
            AssertArray(b, new double[,] { { np.NaN, np.NaN, np.NaN, 0.141120008059867, -0.756802495307928 } });
            print(b);

            a = np.array(new int[,] { { 0, 1, 2, 3, 4 }, { 5, 6, 7, 8, 9 } });
            a = a["::2"] as ndarray;
            b = np.sin(a, where: new bool[,] { { false, false, false, true, true } });
            AssertArray(b, new double[,] { { np.NaN, np.NaN, np.NaN, 0.141120008059867, -0.756802495307928 } });
            print(b);

        }

        [TestMethod]
        public void test_sin_2()
        {

            var a = np.arange(0, 1024 * 1024, dtype: np.Float64).reshape((256, 64, 32, 2));

            var sw1 = new  System.Diagnostics.Stopwatch();
            sw1.Start();
            ndarray b = np.sin(a);
            sw1.Stop();

            var sw2 = new System.Diagnostics.Stopwatch();
            sw2.Start();
            ndarray c = np.sin(a);
            sw2.Stop();


            Console.WriteLine("Entries1: {0} Elapsed1: {1}", b.size, sw1.ElapsedMilliseconds);
            Console.WriteLine("Entries2: {0} Elapsed2: {1}", c.size, sw2.ElapsedMilliseconds);

            //Assert.IsTrue(CompareArrays(b, c));
        }

        [TestMethod]
        public void test_cos_1()
        {
            var ExpectedResult = new double[] { 1.0, -0.416146836547142, -0.653643620863612, 0.960170286650366, -0.145500033808614 };

            var a = np.arange(0, 10, dtype: np.Float64);
            a = a["::2"] as ndarray;
            var b = np.cos(a);
            AssertArray(b, ExpectedResult);
            print(b);

            a = np.arange(0, 10, dtype: np.Float32);
            a = a["::2"] as ndarray;
            b = np.cos(a);
            AssertArray(b, ExpectedResult);
            print(b);

            a = np.arange(0, 10, dtype: np.Int16);
            a = a["::2"] as ndarray;
            b = np.cos(a);
            AssertArray(b, ExpectedResult);
            print(b);

            print("********");

            a = np.arange(0, 10, dtype: np.Float64).reshape((1, 2, 5));
            a = a["::2"] as ndarray;
            b = np.cos(a);

            var ExpectedDataB = new double[,,]
                {{{ 1.0,               0.54030230586814, -0.416146836547142, -0.989992496600445, -0.653643620863612},
                  { 0.283662185463226, 0.960170286650366, 0.753902254343305, -0.145500033808614, -0.911130261884677}}};

            AssertArray(b, ExpectedDataB);
            print(b);

            print("********");

            a = np.array(new int[,] { { 0, 1, 2, 3, 4 }, { 5, 6, 7, 8, 9 } });
            a = a["::2"] as ndarray;
            b = np.cos(a, where: a > 2);
            AssertArray(b, new double[,] { { np.NaN, np.NaN, np.NaN, -0.989992496600445, -0.65364362086361 } });
            print(b);

            a = np.array(new int[,] { { 0, 1, 2, 3, 4 }, { 5, 6, 7, 8, 9 } });
            a = a["::2"] as ndarray;
            b = np.cos(a, where: new bool[,] { { false, false, false, true, true } });
            AssertArray(b, new double[,] { { np.NaN, np.NaN, np.NaN, -0.989992496600445, -0.65364362086361 } });
            print(b);

        }


        #endregion

        private bool CompareArrays(ndarray a1, ndarray a2)
        {
            if (a1.size != a2.size)
                return false;

            if (a1.Dtype.TypeNum != a2.Dtype.TypeNum)
                return false;

            long ElementCount = a1.size;

            for (int i = 0; i < ElementCount; i++)
            {
                var a1d = a1.GetItem(i);
                var a2d = a2.GetItem(i);

                if (!a1d.Equals(a2d))
                    return false;
            }

            return true;

        }

        [Ignore]
        [TestMethod]
        public void xxx_Test_MathematicalFunctions_Placeholder()
        {
        }


    }
}
