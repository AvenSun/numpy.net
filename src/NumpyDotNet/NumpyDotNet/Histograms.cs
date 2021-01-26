﻿/*
 * BSD 3-Clause License
 *
 * Copyright (c) 2018-2019
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

using NumpyLib;
using System;
using System.Collections.Generic;
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
    public static partial class np
    {
        /*
        *
        * bincount accepts one, two or three arguments. The first is an array of
        * non-negative integers The second, if present, is an array of weights,
        * which must be promotable to double. Call these arguments list and
        * weight. Both must be one-dimensional with len(weight) == len(list). If
        * weight is not present then bincount(list)[i] is the number of occurrences
        * of i in list.  If weight is present then bincount(self,list, weight)[i]
        * is the sum of all weight[j] where list [j] == i.  Self is not used.
        * The third argument, if present, is a minimum length desired for the
        * output array.
        */
        public static ndarray bincount(object x, object weights = null, npy_intp? minlength = null)
        {
            ndarray list = np.asanyarray(x);
            ndarray weight = weights != null ? np.asanyarray(weights) : null;
            ndarray ans;

            #region validation
            if (list.ndim != 1)
            {
                throw new Exception("Histograms only supported on 1d arrays");
            }
            if (weight != null)
            {
                if (weight.ndim != 1)
                {
                    throw new Exception("weights array must be 1d");
                }

                if (list.Size != weight.Size)
                {
                    throw new Exception("the list and weights must of the same length");
                }
            }
            if (minlength != null)
            {
                if (minlength < 0)
                {
                    throw new Exception("minlength must not be a negative value");
                }
            }
      
            switch (list.TypeNum)
            {
                case NPY_TYPES.NPY_BYTE:
                case NPY_TYPES.NPY_INT16:
                case NPY_TYPES.NPY_INT32:
                case NPY_TYPES.NPY_INT64:
                    break;
                case NPY_TYPES.NPY_UBYTE:
                case NPY_TYPES.NPY_UINT16:
                case NPY_TYPES.NPY_UINT32:
                case NPY_TYPES.NPY_UINT64:
                    // not sure why these are not supported in python. I will support them.
                    break;

                default:
                    throw new Exception("Histograms only supported on integer arrays");

            }
            #endregion

            // convert input array to intp if not already
            ndarray lst = np.asarray(list, np.intp);
            npy_intp len = lst.Size;

            /* handle empty list */
            if (len == 0)
            {
                ans = np.zeros(new shape(1), dtype: np.intp);
                return ans;
            }


            // get pointer to raw input data
            npy_intp[] numbers = lst.ToArray<npy_intp>();

            // get min and max values of the input data
            npy_intp mn = (npy_intp)np.amin(lst).GetItem(0);
            npy_intp mx = (npy_intp)np.amax(lst).GetItem(0);
            if (mn < 0)
            {
                throw new Exception("histogram arrays must not contain negative numbers");
            }

            // determine size of return array.
            npy_intp ans_size = mx + 1;
            if (minlength != null)
            {
                if (ans_size < minlength.Value)
                {
                    ans_size = minlength.Value;
                }
            }

            // if weight is null, we return array of npy_intp, else doubles.
            if (weight == null)
            {
                npy_intp[] ians = new npy_intp[ans_size];
                for (npy_intp i = 0; i < len; i++)
                {
                    ians[numbers[i]] += 1;
                }

                ans = np.array(ians, dtype: np.intp);
                return ans;
            }
            else
            {
                ndarray wts = np.asarray(weight, dtype: np.Float64);
                double[] _weights = wts.ToArray<double>();

                double[] dans = new double[ans_size];

                for (npy_intp i = 0; i < len; i++)
                {
                    dans[numbers[i]] += _weights[i];
                }

                ans = np.array(dans, dtype: np.Float64);
                return ans;
            }

        }

    }
}
