﻿/*
 * The MIT License
 *
 * Copyright 2016 feature[23]
 *
 * Permission is hereby granted, free of charge, to any person obtaining a copy
 * of this software and associated documentation files (the "Software"), to deal
 * in the Software without restriction, including without limitation the rights
 * to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
 * copies of the Software, and to permit persons to whom the Software is
 * furnished to do so, subject to the following conditions:
 *
 * The above copyright notice and this permission notice shall be included in
 * all copies or substantial portions of the Software.
 *
 * THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
 * IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
 * FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
 * AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
 * LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
 * OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
 * THE SOFTWARE.
 */

using System;
using F23.StringSimilarity.Interfaces;
// ReSharper disable SuggestVarOrType_Elsewhere

namespace F23.StringSimilarity
{
    /// The longest common subsequence (LCS) problem consists in finding the longest
    /// subsequence common to two (or more) sequences. It differs from problems of
    /// finding common substrings: unlike substrings, subsequences are not required
    /// to occupy consecutive positions within the original sequences.
    ///
    /// It is used by the diff utility, by Git for reconciling multiple changes, etc.
    ///
    /// The LCS distance between Strings X (length n) and Y (length m) is n + m - 2
    /// |LCS(X, Y)| min = 0 max = n + m
    ///
    /// LCS distance is equivalent to Levenshtein distance, when only insertion and
    /// deletion is allowed (no substitution), or when the cost of the substitution
    /// is the double of the cost of an insertion or deletion.
    ///
    /// ! This class currently implements the dynamic programming approach, which has
    /// a space requirement O(m * n)!
    public class LongestCommonSubsequence : IStringDistance
    {
        /// <summary>
        /// Return the LCS distance between strings s1 and s2, computed as |s1| +
        /// |s2| - 2 * |LCS(s1, s2)|.
        /// </summary>
        /// <param name="s1">The first string to compare.</param>
        /// <param name="s2">The second string to compare.</param>
        /// <returns>
        /// The LCS distance between strings s1 and s2, computed as |s1| +
        /// |s2| - 2 * |LCS(s1, s2)|
        /// </returns>
        /// <exception cref="ArgumentNullException">If s1 or s2 is null.</exception>
        public double Distance(string s1, string s2)
        {
            if (s1 == null)
            {
                throw new ArgumentNullException(nameof(s1));
            }

            if (s2 == null)
            {
                throw new ArgumentNullException(nameof(s2));
            }

            if (s1.Equals(s2))
            {
                return 0;
            }

            return s1.Length + s2.Length - 2 * Length(s1, s2);
        }

        /// <summary>
        ///  Return the length of Longest Common Subsequence (LCS) between strings s1
        ///  and s2.
        /// </summary>
        /// <param name="s1">The first string to compare.</param>
        /// <param name="s2">The second string to compare.</param>
        /// <returns>The length of LCS(s2, s2)</returns>
        /// <exception cref="ArgumentNullException">If s1 or s2 is null.</exception>
        public int Length(string s1, string s2)
        {
            /* function LCSLength(X[1..m], Y[1..n])
             C = array(0..m, 0..n)
             for i := 0..m
             C[i,0] = 0
             for j := 0..n
             C[0,j] = 0
             for i := 1..m
             for j := 1..n
             if X[i] = Y[j]
             C[i,j] := C[i-1,j-1] + 1
             else
             C[i,j] := max(C[i,j-1], C[i-1,j])
             return C[m,n]
             */
            int m = s1.Length;
            int n = s2.Length;
            char[] x = s1.ToCharArray();
            char[] y = s2.ToCharArray();

            int[,] c = new int[m + 1, n + 1];

            for (int i = 0; i <= m; i++)
            {
                c[i,0] = 0;
            }

            for (int j = 0; j <= n; j++)
            {
                c[0,j] = 0;
            }

            for (int i = 1; i <= m; i++)
            {
                for (int j = 1; j <= n; j++)
                {
                    if (x[i - 1] == y[j - 1])
                    {
                        c[i,j] = c[i - 1,j - 1] + 1;

                    }
                    else
                    {
                        c[i,j] = Math.Max(c[i,j - 1], c[i - 1,j]);
                    }
                }
            }

            return c[m,n];
        }
    }
}
