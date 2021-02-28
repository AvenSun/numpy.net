﻿/*
 * BSD 3-Clause License
 *
 * Copyright (c) 2018-2021
 * All rights reserved.
 *
 * Redistribution and use in source and binary forms, with or without
 * modification, are permitted provided that the following conditions are met:
 *
 * 1. Redistributions of source code must retain the above copyright notice,
 *    this list of conditions and the following disclaimer.
 *
 * 2. Redistributions in binary form must reproduce the above copyright notice,
 *    this list of conditions and the following disclaimer in the documentation
 *    and/or other materials provided with the distribution.
 *
 * 3. Neither the name of the copyright holder nor the names of its
 *    contributors may be used to endorse or promote products derived from
 *    this software without specific prior written permission.
 *
 * THIS SOFTWARE IS PROVIDED BY THE COPYRIGHT HOLDERS AND CONTRIBUTORS "AS IS"
 * AND ANY EXPRESS OR IMPLIED WARRANTIES, INCLUDING, BUT NOT LIMITED TO, THE
 * IMPLIED WARRANTIES OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR PURPOSE ARE
 * DISCLAIMED. IN NO EVENT SHALL THE COPYRIGHT HOLDER OR CONTRIBUTORS BE LIABLE
 * FOR ANY DIRECT, INDIRECT, INCIDENTAL, SPECIAL, EXEMPLARY, OR CONSEQUENTIAL
 * DAMAGES (INCLUDING, BUT NOT LIMITED TO, PROCUREMENT OF SUBSTITUTE GOODS OR
 * SERVICES; LOSS OF USE, DATA, OR PROFITS; OR BUSINESS INTERRUPTION) HOWEVER
 * CAUSED AND ON ANY THEORY OF LIABILITY, WHETHER IN CONTRACT, STRICT LIABILITY,
 * OR TORT (INCLUDING NEGLIGENCE OR OTHERWISE) ARISING IN ANY WAY OUT OF THE USE
 * OF THIS SOFTWARE, EVEN IF ADVISED OF THE POSSIBILITY OF SUCH DAMAGE.
 */

using NumpyDotNet;
using NumpyLib;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
#if NPY_INTP_64
using npy_intp = System.Int64;
#else
using npy_intp = System.Int32;
#endif

namespace NumpyDotNet
{
    public static class NumpyExtensions
    {
        /// <summary>
        /// Returns an array containing the same data with a new shape.
        /// </summary>
        /// <param name="a">array to reshape</param>
        /// <param name="newshape">New shape for the array. The new shape should be compatible with the original shape</param>
        /// <returns></returns>
        public static ndarray reshape(this ndarray a, params int[] newshape)
        {
            npy_intp[] newdims = new npy_intp[newshape.Length];
            for (int i = 0; i < newshape.Length; i++)
                newdims[i] = newshape[i];
            return a.reshape(newdims, NPY_ORDER.NPY_ANYORDER);
        }
        /// <summary>
        /// Returns an array containing the same data with a new shape.
        /// </summary>
        /// <param name="a">array to reshape</param>
        /// <param name="newshape">New shape for the array. The new shape should be compatible with the original shape</param>
        /// <returns></returns>
        public static ndarray reshape(this ndarray a, params long[] newshape)
        {
            npy_intp[] newdims = new npy_intp[newshape.Length];
            for (int i = 0; i < newshape.Length; i++)
                newdims[i] = (npy_intp)newshape[i];
            return a.reshape(newdims, NPY_ORDER.NPY_ANYORDER);
        }
        /// <summary>
        /// Returns an array containing the same data with a new shape.
        /// </summary>
        /// <param name="a">array to reshape</param>
        /// <param name="newshape">New shape for the array. The new shape should be compatible with the original shape</param>
        /// <param name="order">{‘C’, ‘F’, ‘A’}, optional</param>
        /// <returns></returns>
        public static ndarray reshape(this ndarray a, shape newshape, NPY_ORDER order = NPY_ORDER.NPY_CORDER)
        {
            return np.reshape(a, newshape, order);
        }
        /// <summary>
        /// Returns an array containing the same data with a new shape.
        /// </summary>
        /// <param name="a">array to reshape</param>
        /// <param name="newshape">New shape for the array. The new shape should be compatible with the original shape</param>
        /// <param name="order">{‘C’, ‘F’, ‘A’}, optional</param>
        /// <returns></returns>
        public static ndarray reshape(this ndarray a, object oshape, NPY_ORDER order = NPY_ORDER.NPY_CORDER)
        {
            shape newshape = ConvertTupleToShape(oshape);
            if (newshape == null)
            {
                throw new Exception("Unable to convert shape object");
            }
            
            return np.reshape(a, newshape, order);
        }

        internal static shape ConvertTupleToShape(object oshape)
        {
            var T = oshape.GetType();

            if (oshape is shape)
            {
                return oshape as shape;
            }

            if (oshape is Int32)
            {
                return new shape((Int32)oshape);
            }
            if (oshape is Int64)
            {
                return new shape((Int64)oshape);
            }

            if (oshape is ValueTuple<int>)
            {
                ValueTuple<int> T1 = (ValueTuple<int>)oshape;
                return new shape(T1.Item1);
            }
            if (oshape is ValueTuple<long>)
            {
                ValueTuple<long> T1 = (ValueTuple<long>)oshape;
                return new shape(T1.Item1);
            }

            if (oshape is ValueTuple<int, int>)
            {
                ValueTuple<int, int> T2 = (ValueTuple<int, int>)oshape;
                return new shape(T2.Item1, T2.Item2);
            }
            if (oshape is ValueTuple<long, long>)
            {
                ValueTuple<long, long> T2 = (ValueTuple<long, long>)oshape;
                return new shape(T2.Item1, T2.Item2);
            }

            if (oshape is ValueTuple<int, int, int>)
            {
                ValueTuple<int, int, int> T3 = (ValueTuple<int, int, int>)oshape;
                return new shape(T3.Item1, T3.Item2, T3.Item3);
            }
            if (oshape is ValueTuple<long, long, long>)
            {
                ValueTuple<long, long, long> T3 = (ValueTuple<long, long, long>)oshape;
                return new shape(T3.Item1, T3.Item2, T3.Item3);
            }

            if (oshape is ValueTuple<int, int, int, int>)
            {
                ValueTuple<int, int, int, int> T4 = (ValueTuple<int, int, int, int>)oshape;
                return new shape(T4.Item1, T4.Item2, T4.Item3, T4.Item4);
            }
            if (oshape is ValueTuple<long, long, long, long>)
            {
                ValueTuple<long, long, long, long> T4 = (ValueTuple<long, long, long, long>)oshape;
                return new shape(T4.Item1, T4.Item2, T4.Item3, T4.Item4);
            }

            if (oshape is ValueTuple<int, int, int, int, int>)
            {
                ValueTuple<int, int, int, int, int> T5 = (ValueTuple<int, int, int, int, int>)oshape;
                return new shape(new npy_intp[] { T5.Item1, T5.Item2, T5.Item3, T5.Item4, T5.Item5 });
            }
            if (oshape is ValueTuple<long, long, long, long, long>)
            {
                ValueTuple<long, long, long, long, long> T5 = (ValueTuple<long, long, long, long, long>)oshape;
                return new shape(new long[] { T5.Item1, T5.Item2, T5.Item3, T5.Item4, T5.Item5 });
            }

            if (oshape is ValueTuple<int, int, int, int, int, int>)
            {
                ValueTuple<int, int, int, int, int, int> T6 = (ValueTuple<int, int, int, int, int, int>)oshape;
                return new shape(new npy_intp[] { T6.Item1, T6.Item2, T6.Item3, T6.Item4, T6.Item5, T6.Item6 });
            }
            if (oshape is ValueTuple<long, long, long, long, long, long>)
            {
                ValueTuple<long, long, long, long, long, long> T6 = (ValueTuple<long, long, long, long, long, long>)oshape;
                return new shape(new long[] { T6.Item1, T6.Item2, T6.Item3, T6.Item4, T6.Item5, T6.Item6 });
            }

            if (oshape is ValueTuple<int, int, int, int, int, int, int>)
            {
                ValueTuple<int, int, int, int, int, int, int> T7 = (ValueTuple<int, int, int, int, int, int, int>)oshape;
                return new shape(new npy_intp[] { T7.Item1, T7.Item2, T7.Item3, T7.Item4, T7.Item5, T7.Item6, T7.Item7 });
            }
            if (oshape is ValueTuple<long, long, long, long, long, long, long>)
            {
                ValueTuple<long, long, long, long, long, long, long> T7 = (ValueTuple<long, long, long, long, long, long, long>)oshape;
                return new shape(new long[] { T7.Item1, T7.Item2, T7.Item3, T7.Item4, T7.Item5, T7.Item6, T7.Item7 });
            }

            if (oshape is ValueTuple<int, int, int, int, int, int, int, int>)
            {
                ValueTuple<int, int, int, int, int, int, int, int> T8 = (ValueTuple<int, int, int, int, int, int, int, int>)oshape;
                return new shape(new npy_intp[] { T8.Item1, T8.Item2, T8.Item3, T8.Item4, T8.Item5, T8.Item6, T8.Item7, T8.Rest });
            }
            if (oshape is ValueTuple<long, long, long, long, long, long, long, long>)
            {
                ValueTuple<long, long, long, long, long, long, long, long> T8 = (ValueTuple<long, long, long, long, long, long, long, long>)oshape;
                return new shape(new long[] { T8.Item1, T8.Item2, T8.Item3, T8.Item4, T8.Item5, T8.Item6, T8.Item7, T8.Rest });
            }
            return null;
        }

        /// <summary>
        /// Write array to a file as text or binary (default).
        /// </summary>
        /// <param name="a">array to write to file</param>
        /// <param name="fileName">string containing a filename</param>
        /// <param name="sep">Separator between array items for text output.</param>
        /// <param name="format">Format string for text file output.</param>
        public static void tofile(this ndarray a, string fileName, string sep = null, string format = null)
        {
            np.tofile(a, fileName, sep, format);
        }
        /// <summary>
        /// Write array to a stream as text or binary (default).
        /// </summary>
        /// <param name="a">array to write to stream</param>
        /// <param name="stream">stream to write to</param>
        /// <param name="sep">Separator between array items for text output.</param>
        /// <param name="format">Format string for text file output.</param>
        public static void tofile(this ndarray a, Stream stream, string sep = null, string format = null)
        {
            np.tofile(a, stream, sep, format);
        }

        /// <summary>
        /// New view of array with the same data.
        /// </summary>
        /// <param name="a"></param>
        /// <param name="dtype">Data-type descriptor of the returned view</param>
        /// <param name="type">Type of the returned view, e.g., ndarray or matrix (not used)</param>
        /// <returns></returns>
        public static ndarray view(this ndarray a, dtype dtype = null, object type = null)
        {
            return np.view(a, dtype, type);
        }
        /// <summary>
        /// Return a copy of the array collapsed into one dimension.
        /// </summary>
        /// <param name="a">array to flatten</param>
        /// <param name="order">{‘C’, ‘F’, ‘A’, ‘K’}</param>
        /// <returns></returns>
        public static ndarray Flatten(this ndarray a, NPY_ORDER order)
        {
            return NpyCoreApi.Flatten(a, order);
        }
        /// <summary>
        /// Return a contiguous flattened array.
        /// </summary>
        /// <param name="a">array to flatten</param>
        /// <param name="order">{‘C’, ‘F’, ‘A’, ‘K’}</param>
        /// <returns></returns>
        public static ndarray Ravel(this ndarray a, NPY_ORDER order)
        {
            return np.ravel(a, order);
        }
        /// <summary>
        /// Change shape and size of array in-place.
        /// </summary>
        /// <param name="a"></param>
        /// <param name="newdims">Shape of resized array.</param>
        /// <param name="order"></param>
        public static void Resize(this ndarray a, npy_intp[] newdims)
        {
            np.resize(a, newdims);
        }
        /// <summary>
        /// Remove axes of length one from a.
        /// </summary>
        /// <param name="a">array to squeeze</param>
        /// <returns></returns>
        public static ndarray Squeeze(this ndarray a)
        {
            return np.squeeze(a);
        }
        /// <summary>
        /// Interchange two axes of an array.
        /// </summary>
        /// <param name="a">Input array.</param>
        /// <param name="axis1">First axis.</param>
        /// <param name="axis2">Second axis.</param>
        /// <returns></returns>
        public static ndarray SwapAxes(this ndarray a, int axis1, int axis2)
        {
            return np.swapaxes(a, axis1, axis2);
        }
        /// <summary>
        /// Returns a view of the array with axes transposed.
        /// </summary>
        /// <param name="a">array to transpose</param>
        /// <param name="axes">array of npy_intp: i in the j-th place in the array means a’s i-th axis becomes a.transpose()’s j-th axis</param>
        /// <returns></returns>
        public static ndarray Transpose(this ndarray a, npy_intp[] axes = null)
        {
            return np.transpose(a, axes);
        }
        /// <summary>
        /// Construct an array from an index array and a set of arrays to choose from.
        /// </summary>
        /// <param name="a">array to perform choose operation on.</param>
        /// <param name="choices">Choice arrays. a and all of the choices must be broadcastable to the same shape</param>
        /// <param name="out">f provided, the result will be inserted into this array</param>
        /// <param name="clipMode">{‘raise’ (default), ‘wrap’, ‘clip’}</param>
        /// <returns></returns>
        public static ndarray Choose(this ndarray a, IEnumerable<ndarray> choices, ndarray @out = null, NPY_CLIPMODE clipMode = NPY_CLIPMODE.NPY_RAISE)
        {
            return np.choose(a, choices, @out, clipMode);
        }

        /// <summary>
        /// Repeat elements of an array.
        /// </summary>
        /// <param name="a"></param>
        /// <param name="repeats">The number of repetitions for each element</param>
        /// <param name="axis">The axis along which to repeat values</param>
        /// <returns></returns>
        public static ndarray Repeat(this ndarray a, object repeats, int? axis)
        {
            return np.repeat(a, repeats, axis);
        }
        /// <summary>
        /// Replaces specified elements of an array with given values.
        /// </summary>
        /// <param name="a">target ndarray</param>
        /// <param name="values">Values to place in a at target indices</param>
        /// <param name="ind">Target indices, interpreted as integers.</param>
        /// <param name="mode">{‘raise’, ‘wrap’, ‘clip’}</param>
        public static void PutTo(this ndarray a, ndarray values, ndarray ind, NPY_CLIPMODE mode)
        {
            int ret = np.put(a, ind, values, mode);
        }
        /// <summary>
        /// Sort an array in-place.
        /// </summary>
        /// <param name="a">array to sort</param>
        /// <param name="axis">Axis along which to sort. Default is -1, which means sort along the last axis.</param>
        /// <param name="sortkind">Sorting algorithm. The default is ‘quicksort’.</param>
        /// <returns></returns>
        public static ndarray Sort(this ndarray a, int? axis = -1, NPY_SORTKIND sortkind = NPY_SORTKIND.NPY_QUICKSORT)
        {
            return np.sort(a, axis, sortkind, null);
        }

        /// <summary>
        /// Returns the indices that would sort an array.
        /// </summary>
        /// <param name="a">array to sort</param>
        /// <param name="axis">Axis along which to sort.</param>
        /// <param name="kind">Sorting algorithm. The default is ‘quicksort’.</param>
        /// <returns></returns>
        public static ndarray ArgSort(this ndarray a, int? axis =-1, NPY_SORTKIND kind = NPY_SORTKIND.NPY_QUICKSORT)
        {
            return np.argsort(a, axis, kind);
        }

        public static ndarray ArgMax(this ndarray a, int? axis = -1, ndarray ret = null)
        {
            return np.argmax(a, axis, ret);
        }

        public static ndarray ArgMin(this ndarray a, int? axis = -1, ndarray ret = null)
        {
            return np.argmin(a, axis, ret);
        }

        public static ndarray SearchSorted(this ndarray a, ndarray keys, NPY_SEARCHSIDE side = NPY_SEARCHSIDE.NPY_SEARCHLEFT)
        {
            return np.searchsorted(a, keys, side);
        }

        public static ndarray diagonal(this ndarray a, int offset = 0, int axis1 = 0, int axis2 = 1)
        {
            return np.diagonal(a, offset, axis1, axis2);
        }

        public static ndarray trace(this ndarray a, int offset = 0, int axis1 = 0, int axis2 = 1, dtype dtype = null, ndarray @out = null)
        {
            return np.trace(a, offset, axis1, axis2, dtype, @out);
        }


        public static ndarray trace(this ndarray a, int offset = 0, int axis1 = 0, int axis2 = 1)
        {
            return np.diagonal(a, offset, axis1, axis2);
        }


        public static ndarray[] NonZero(this ndarray a)
        {
            return np.nonzero(a);
        }

        public static ndarray compress(this ndarray a, object condition, object axis = null, ndarray @out = null)
        {
            return np.compress(condition, a, axis, @out);
        }

        public static ndarray compress(this ndarray a, ndarray condition, int? axis = null, ndarray @out = null)
        {
            return np.compress(condition, a, axis, @out);
        }

        public static ndarray clip(this ndarray a, object a_min, object a_max, ndarray ret = null)
        {
            return np.clip(a, a_min, a_max, ret);
        }

        public static ndarray Sum(this ndarray a, int? axis = null, dtype dtype = null, ndarray ret = null)
        {
            return np.sum(a, axis, dtype, ret);
        }

        public static ndarray Any(this ndarray a, object axis = null, ndarray @out = null, bool keepdims = false)
        {
            return np.any(a, axis, @out, keepdims);
        }
        public static bool Anyb(this ndarray a, object axis = null, ndarray @out = null, bool keepdims = false)
        {
            return np.anyb(a, axis, @out, keepdims);
        }

        public static ndarray All(this ndarray a, object axis = null, ndarray @out = null, bool keepdims = false)
        {
            return np.all(a, axis, @out, keepdims);
        }

        public static ndarray cumsum(this ndarray a, int? axis = null, dtype dtype = null, ndarray ret = null)
        {
            return np.cumsum(a, axis, dtype, ret);
        }

        public static ndarray ptp(this ndarray a, object axis = null, ndarray @out = null, bool keepdims = false)
        {
            return np.ptp(a, axis, @out, keepdims);
        }

        public static ndarray AMax(this ndarray a, int? axis = null, ndarray @out = null, bool keepdims = false)
        {
            return np.amax(a, axis, @out, keepdims);
        }

        public static ndarray AMin(this ndarray a, int? axis = null, ndarray @out = null, bool keepdims = false)
        {
            return np.amin(a, axis, @out, keepdims);
        }


        public static ndarray Prod(this ndarray a, int? axis = null, dtype dtype = null, ndarray ret = null)
        {
            return np.prod(a, axis, dtype, ret);
        }

        public static ndarray CumProd(this ndarray a, int axis, dtype dtype, ndarray ret = null)
        {
            return np.cumprod(a, axis, dtype, ret);
        }
        
        public static ndarray Mean(this ndarray a, int? axis = null, dtype dtype = null)
        {
            return np.mean(a, axis, dtype);
        }

        public static ndarray Std(this ndarray a, int? axis = null, dtype dtype = null)
        {
            return np.std(a, axis, dtype);
        }

        public static ndarray partition(this ndarray a, npy_intp[] kth, int? axis = null, string kind = "introselect", IEnumerable<string> order = null)
        {
            return np.partition(a, kth, axis, kind, order);
        }

        public static List<T> ToList<T>(this ndarray a)
        {
            if (a.IsASlice)
            {
                List<T> Data = new List<T>();
                foreach (T d in a)
                {
                    Data.Add(d);
                }
                return Data;
            }
            else
            {
                T[] data = (T[])a.rawdata(0).datap;
                return data.ToList();
            }

        }

        public static T[] ToArray<T>(this ndarray a)
        {
            if (a.IsASlice)
            {
                List<T> Data = a.ToList<T>();
                return Data.ToArray();
            }
            else
            {
                T[] data = (T[])a.rawdata(0).datap;
                return data;
            }
 
        }

        public static double Mean<T>(this ndarray a)
        {
            return a.ToList<T>().Mean();
        }
        public static double Mean<T>(this IList<T> values)
        {
            return values.Count == 0 ? 0 : values.Mean(0, values.Count);
        }
        public static double Mean<T>(this IList<T> values, int start, int end)
        {
            double s = 0;

            for (int i = start; i < end; i++)
            {
                s += Convert.ToDouble(values[i]);
            }

            return s / (end - start);
        }

        public static double Variance<T>(this ndarray a)
        {
            return a.ToList<T>().Variance();
        }
        public static double Variance<T>(this IList<T> values)
        {
            return values.Variance(values.Mean(), 0, values.Count);
        }
        public static double Variance<T>(this IList<T> values, double mean)
        {
            return values.Variance(mean, 0, values.Count);
        }
        public static double Variance<T>(this IList<T> values, double mean, int start, int end)
        {
            double variance = 0;

            for (int i = start; i < end; i++)
            {
                variance += Math.Pow((Convert.ToDouble(values[i]) - mean), 2);
            }

            int n = end - start;
            if (start > 0) n -= 1;

            return variance / (n);
        }

        public static double StandardDeviation<T>(this ndarray a)
        {
            return a.ToList<T>().StandardDeviation();
        }
        public static double StandardDeviation<T>(this IList<T> values)
        {
            return values.Count == 0 ? 0 : values.StandardDeviation(0, values.Count);
        }
        public static double StandardDeviation<T>(this IList<T> values, int start, int end)
        {
            double mean = values.Mean(start, end);
            double variance = values.Variance(mean, start, end);

            return Math.Sqrt(variance);
        }

        public static bool[] AsBoolArray(this ndarray a)
        {
            return np.AsBoolArray(a);
        }
        public static sbyte[] AsSByteArray(this ndarray a)
        {
            return np.AsSByteArray(a);
        }
        public static byte[] AsByteArray(this ndarray a)
        {
            return np.AsByteArray(a);
        }
        public static Int16[] AsInt16Array(this ndarray a)
        {
            return np.AsInt16Array(a);
        }
        public static UInt16[] AsUInt16Array(this ndarray a)
        {
            return np.AsUInt16Array(a);
        }
        public static Int32[] AsInt32Array(this ndarray a)
        {
            return np.AsInt32Array(a);
        }
        public static UInt32[] AsUInt32Array(this ndarray a)
        {
            return np.AsUInt32Array(a);
        }
        public static Int64[] AsInt64Array(this ndarray a)
        {
            return np.AsInt64Array(a);
        }
        public static UInt64[] AsUInt64Array(this ndarray a)
        {
            return np.AsUInt64Array(a);
        }
        public static float[] AsFloatArray(this ndarray a)
        {
            return np.AsFloatArray(a);
        }
        public static double[] AsDoubleArray(this ndarray a)
        {
            return np.AsDoubleArray(a);
        }
        public static decimal[] AsDecimalArray(this ndarray a)
        {
            return np.AsDecimalArray(a);
        }
        public static System.Numerics.Complex[] AsComplexArray(this ndarray a)
        {
            return np.AsComplexArray(a);
        }
        public static System.Numerics.BigInteger[] AsBigIntArray(this ndarray a)
        {
            return np.AsBigIntArray(a);
        }
        public static object[] AsObjectArray(this ndarray a)
        {
            return np.AsObjectArray(a);
        }
        public static string[] AsStringArray(this ndarray a)
        {
            return np.AsStringArray(a);
        }
    }

    public partial class np
    {
        internal class ufuncbase
        {

            internal static ndarray reduce(UFuncOperation ops, object a, int axis = 0, dtype dtype = null, ndarray @out = null, bool keepdims = false)
            {
                ndarray arr = asanyarray(a);
                if (arr == null)
                {
                    throw new ValueError("unable to convert a to ndarray");
                }

                NPY_TYPES rtype = dtype != null ? dtype.TypeNum : arr.TypeNum;
                return NpyCoreApi.PerformReduceOp(arr, axis, ops, rtype, @out, keepdims);
            }

            internal static ndarray reduceat(UFuncOperation ops, object a, object indices, int axis = 0, dtype dtype = null, ndarray @out = null)
            {
                ndarray arr = asanyarray(a);
                if (arr == null)
                {
                    throw new ValueError("unable to convert a to ndarray");
                }

                ndarray indicesarr = asanyarray(indices);
                if (indicesarr == null)
                {
                    throw new ValueError("unable to convert indices to ndarray");
                }

                NPY_TYPES rtype = dtype != null ? dtype.TypeNum : arr.TypeNum;
                return NpyCoreApi.PerformReduceAtOp(arr, indicesarr, axis, ops, rtype, @out);
            }

            internal static ndarray accumulate(UFuncOperation ops, object a, int axis = 0, dtype dtype = null, ndarray @out = null)
            {
                ndarray arr = asanyarray(a);
                if (arr == null)
                {
                    throw new ValueError("unable to convert a to ndarray");
                }


                NPY_TYPES rtype = dtype != null ? dtype.TypeNum : arr.TypeNum;
                return NpyCoreApi.PerformAccumulateOp(arr, axis, ops, rtype, @out);
            }

            public static ndarray outer(UFuncOperation ops, dtype dtype, object a, object b, ndarray @out = null, int? axis = null)
            {

                var a1 = np.asanyarray(a);
                var b1 = np.asanyarray(b);


                List<npy_intp> destdims = new List<npy_intp>();
                foreach (var dim in a1.shape.iDims)
                    destdims.Add(dim);
                foreach (var dim in b1.shape.iDims)
                    destdims.Add(dim);



                ndarray dest = @out;
                if (dest == null)
                    dest = np.empty(new shape(destdims), dtype: dtype != null ? dtype : a1.Dtype);
                
                return NpyCoreApi.PerformOuterOp(a1, b1, dest, ops);
            }

        }

        public class ufunc 
        {
            public static ndarray accumulate(UFuncOperation operation, object a, int axis = 0, ndarray @out = null)
            {
                return ufuncbase.accumulate(operation, a, axis, null, @out);
            }

            public static ndarray reduce(UFuncOperation operation, object a, int axis = 0, ndarray @out = null, bool keepdims = false)
            {
                return ufuncbase.reduce(operation, a, axis, null, @out, keepdims);
            }

            public static ndarray reduceat(UFuncOperation operation, object a, object indices, int axis = 0, ndarray @out = null)
            {
                return ufuncbase.reduceat(operation, a, indices, axis, null, @out);
            }

            public static ndarray outer(UFuncOperation operation, dtype dtype, object a, object b, ndarray @out = null, int? axis = null)
            {
                return ufuncbase.outer(operation, dtype, a, b, @out, axis);
            }



        }

    }


    public static partial class np
    {
        #region as(.NET System.Array)

        public static bool[] AsBoolArray(object oa)
        {
            var a = ConvertToFlattenedArray(oa);

            if (a.TypeNum != NPY_TYPES.NPY_BOOL)
            {
                a = a.astype(np.Bool);
            }

            return a.rawdata(0).datap as bool[];
        }
        public static sbyte[] AsSByteArray(object oa)
        {
            var a = ConvertToFlattenedArray(oa);

            if (a.TypeNum != NPY_TYPES.NPY_BYTE)
            {
                a = a.astype(np.Int8);
            }

            return a.rawdata(0).datap as sbyte[];
        }
        public static byte[] AsByteArray(object oa)
        {
            var a = ConvertToFlattenedArray(oa);

            if (a.TypeNum != NPY_TYPES.NPY_UBYTE)
            {
                a = a.astype(np.UInt8);
            }

            return a.rawdata(0).datap as byte[];
        }
        public static Int16[] AsInt16Array(object oa)
        {
            var a = ConvertToFlattenedArray(oa);

            if (a.TypeNum != NPY_TYPES.NPY_INT16)
            {
                a = a.astype(np.Int16);
            }

            return a.rawdata(0).datap as Int16[];
        }
        public static UInt16[] AsUInt16Array(object oa)
        {
            var a = ConvertToFlattenedArray(oa);

            if (a.TypeNum != NPY_TYPES.NPY_UINT16)
            {
                a = a.astype(np.UInt16);
            }

            return a.rawdata(0).datap as UInt16[];
        }
        public static Int32[] AsInt32Array(object oa)
        {
            var a = ConvertToFlattenedArray(oa);

            if (a.TypeNum != NPY_TYPES.NPY_INT32)
            {
                a = a.astype(np.Int32);
            }

            return a.rawdata(0).datap as Int32[];
        }
        public static UInt32[] AsUInt32Array(object oa)
        {
            var a = ConvertToFlattenedArray(oa);

            if (a.TypeNum != NPY_TYPES.NPY_UINT32)
            {
                a = a.astype(np.UInt32);
            }

            return a.rawdata(0).datap as UInt32[];
        }
        public static Int64[] AsInt64Array(object oa)
        {
            var a = ConvertToFlattenedArray(oa);

            if (a.TypeNum != NPY_TYPES.NPY_INT64)
            {
                a = a.astype(np.Int64);
            }

            return a.rawdata(0).datap as Int64[];
        }
        public static UInt64[] AsUInt64Array(object oa)
        {
            var a = ConvertToFlattenedArray(oa);

            if (a.TypeNum != NPY_TYPES.NPY_UINT64)
            {
                a = a.astype(np.UInt64);
            }

            return a.rawdata(0).datap as UInt64[];
        }
        public static float[] AsFloatArray(object oa)
        {
            var a = ConvertToFlattenedArray(oa);

            if (a.TypeNum != NPY_TYPES.NPY_FLOAT)
            {
                a = a.astype(np.Float32);
            }

            return a.rawdata(0).datap as float[];
        }
        public static double[] AsDoubleArray(object oa)
        {
            var a = ConvertToFlattenedArray(oa);

            if (a.TypeNum != NPY_TYPES.NPY_DOUBLE)
            {
                a = a.astype(np.Float64);
            }

            return a.rawdata(0).datap as double[];
        }
        public static decimal[] AsDecimalArray(object oa)
        {
            var a = ConvertToFlattenedArray(oa);

            if (a.TypeNum != NPY_TYPES.NPY_DECIMAL)
            {
                a = a.astype(np.Decimal);
            }

            return a.rawdata(0).datap as decimal[];
        }
        public static System.Numerics.Complex[] AsComplexArray(object oa)
        {
            var a = ConvertToFlattenedArray(oa);

            if (a.TypeNum != NPY_TYPES.NPY_COMPLEX)
            {
                a = a.astype(np.Complex);
            }

            return a.rawdata(0).datap as System.Numerics.Complex[];
        }
        public static System.Numerics.BigInteger[] AsBigIntArray(object oa)
        {
            var a = ConvertToFlattenedArray(oa);

            if (a.TypeNum != NPY_TYPES.NPY_BIGINT)
            {
                a = a.astype(np.BigInt);
            }

            return a.rawdata(0).datap as System.Numerics.BigInteger[];
        }
        public static object[] AsObjectArray(object oa)
        {
            var a = ConvertToFlattenedArray(oa);

            if (a.TypeNum != NPY_TYPES.NPY_OBJECT)
            {
                a = a.astype(np.Object);
            }

            return a.rawdata(0).datap as Object[];
        }
        public static string[] AsStringArray(object oa)
        {
            var a = ConvertToFlattenedArray(oa);

            if (a.TypeNum != NPY_TYPES.NPY_STRING)
            {
                a = a.astype(np.Strings);
            }

            return a.rawdata(0).datap as string[];
        }

        private static ndarray ConvertToFlattenedArray(object input)
        {
            ndarray arr = null;

            try
            {
                arr = asanyarray(input);
            }
            catch (Exception ex)
            {
                throw new ValueError("Unable to convert input into an ndarray.");
            }


            if (arr.IsASlice)
            {
                arr = arr.flatten();
            }

            return arr;
        }
        #endregion
    }

}
