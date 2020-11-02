 
using NUnit.Framework;
using SortingAlgorithms;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SortingAlrotihmsTest
{
    public class Tests
    {
        static readonly Random _r = new Random();
        MergeSortingAlgorithms algorithms;
        [SetUp]
        public void Setup()
        {
            algorithms = new MergeSortingAlgorithms();
        }

        [TestCase(new int[] { 1, 5, 2, 30,22,4,11,7,7,44,7,54,754,234,321 })]
        [TestCase(new int[] { -57, -798, -986, 600, 881, 41, 942, 510, 208, 537, 232, 399, 178, 305, 194, 273, -141, -392, 760, -949, -621, 293, -694, 371, 988, -191, -60, 485, 798, -927, -342, -383, -124, 345, 451, 5, -173, -236, 260, -291, 974, 886, -577, 29, -703, -166, 689, 271, 114, 217, 679, -275, 320, -581, 287, 650, 896, -357, 611, 71, -900, -543, 711, 134, -121, -385, -478, 833, 853, -811, -555, -905, 348, 50, -924, -508, -131, 276, 74, 153, 119, 669, -630, -419, -607, 932, 297, -544, -943, 801, 108, -817, 152, -812, -391, 516, -826, 289, -850, 681 })]
        public void MergeSpan_ShouldOrderTheElementsTheSameAsOrderByDescExtension(int[] unorderedList)
        {
            var orderedList = unorderedList.OrderByDescending(s => s);

            var myOrderedList = algorithms.SortWithSpan(unorderedList);

            CollectionAssert.AreEqual(myOrderedList.ToArray(),orderedList.ToArray());
        }

        [TestCase(new int[] { 1, 5, 2, 30, 22, 4, 11, 7, 7, 44, 7, 54, 754, 234, 321 })]
        [TestCase(new int[] { -57, -798, -986, 600, 881, 41, 942, 510, 208, 537, 232, 399, 178, 305, 194, 273, -141, -392, 760, -949, -621, 293, -694, 371, 988, -191, -60, 485, 798, -927, -342, -383, -124, 345, 451, 5, -173, -236, 260, -291, 974, 886, -577, 29, -703, -166, 689, 271, 114, 217, 679, -275, 320, -581, 287, 650, 896, -357, 611, 71, -900, -543, 711, 134, -121, -385, -478, 833, 853, -811, -555, -905, 348, 50, -924, -508, -131, 276, 74, 153, 119, 669, -630, -419, -607, 932, 297, -544, -943, 801, 108, -817, 152, -812, -391, 516, -826, 289, -850, 681 })]
        public void MergeSpanImproved_ShouldOrderTheElementsTheSameAsOrderByDescExtension(int[] unorderedList)
        {
             
            var orderedList = unorderedList.OrderByDescending(s => s);

            var myOrderedList = algorithms.SortWithSpanImproved(unorderedList,new int[unorderedList.Length]);

            CollectionAssert.AreEqual(myOrderedList.ToArray(), orderedList.ToArray());
        }

        [TestCase(new int[] { 1, 5, 2, 30, 22, 4, 11, 7, 7, 44, 7, 54, 754, 234, 321 })]
        [TestCase(new int[] { -57, -798, -986, 600, 881, 41, 942, 510, 208, 537, 232, 399, 178, 305, 194, 273, -141, -392, 760, -949, -621, 293, -694, 371, 988, -191, -60, 485, 798, -927, -342, -383, -124, 345, 451, 5, -173, -236, 260, -291, 974, 886, -577, 29, -703, -166, 689, 271, 114, 217, 679, -275, 320, -581, 287, 650, 896, -357, 611, 71, -900, -543, 711, 134, -121, -385, -478, 833, 853, -811, -555, -905, 348, 50, -924, -508, -131, 276, 74, 153, 119, 669, -630, -419, -607, 932, 297, -544, -943, 801, 108, -817, 152, -812, -391, 516, -826, 289, -850, 681 })]
        public void MyMergeSort_ShouldOrderTheElementsTheSameAsOrderByDescExtension(int[] unorderedList)
        {
            var orderedList =unorderedList.OrderByDescending(s => s);

            var myOrderedList = algorithms.MyMergeSort(unorderedList.ToList());

            CollectionAssert.AreEqual(myOrderedList, orderedList);
        }

        [TestCase(new int[] { 1, 5, 2, 30, 22, 4, 11, 7, 7, 44, 7, 54, 754, 234, 321 })]
        [TestCase(new int[] { -57, -798, -986, 600, 881, 41, 942, 510, 208, 537, 232, 399, 178, 305, 194, 273, -141, -392, 760, -949, -621, 293, -694, 371, 988, -191, -60, 485, 798, -927, -342, -383, -124, 345, 451, 5, -173, -236, 260, -291, 974, 886, -577, 29, -703, -166, 689, 271, 114, 217, 679, -275, 320, -581, 287, 650, 896, -357, 611, 71, -900, -543, 711, 134, -121, -385, -478, 833, 853, -811, -555, -905, 348, 50, -924, -508, -131, 276, 74, 153, 119, 669, -630, -419, -607, 932, 297, -544, -943, 801, 108, -817, 152, -812, -391, 516, -826, 289, -850, 681 })]
        public void OnlineSort_ShouldOrderTheElementsTheSameAsOrderByDescExtension(int[] unorderedList)
        {
            var orderedList = unorderedList.OrderBy(s => s);

            var myOrderedList = algorithms.OnilneMergeSort(unorderedList.ToList());

            CollectionAssert.AreEqual(myOrderedList.ToArray(), orderedList.ToArray());
        }
    }
}