using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Running;
using Microsoft.VisualBasic.CompilerServices;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SortingAlgorithms
{
    [MemoryDiagnoser]
    public class MyBenchmark
    {
        List<int> Data;
        int[] DataArray;
        MergeSortingAlgorithms algorithms = new MergeSortingAlgorithms();
        [GlobalSetup]
        public void Setup()
        {
            Random r = new Random(1);
            Data = Enumerable.Range(1, 300000).Select(x => r.Next(-10000, 100000)).ToList();
        }
        [IterationSetup]
        public void SetupI()
        {
            DataArray = Data.ToArray();
        }

        [Benchmark]
        public void ListExtensionsOrderByDesc() { Data.OrderByDescending(l => l).ToList(); }
        [Benchmark]
        public void OnlineMergeSort() => algorithms.OnilneMergeSort(Data);
        [Benchmark]
        public void MyMergeSort() => algorithms.MyMergeSort(Data);
        [Benchmark]
        public void MyMergeSortWithSpanUsingTempLists() => algorithms.SortWithSpan(DataArray);
        [Benchmark]
        public void MyMergeSortWithSpanImprovedUsingTwoSpans() => algorithms.SortWithSpanImproved(DataArray, new int[DataArray.Length]);

    }
    //The driection of sorting is different in the online one and the others (OnlineMergeSort ascending while the others are descending sorts)
    public class MergeSortingAlgorithms {
        //Some implementation copied from https://exceptionnotfound.net/merge-sort-csharp-the-sorting-algorithm-family-reunion/, the question in the comments
        //made me do this little test with spans
        public List<int> OnilneMergeSort(List<int> unsorted)
        {
            if (unsorted.Count <= 1)
            {
                return unsorted;
            }

            List<int> left = new List<int>();
            List<int> right = new List<int>();

            int median = unsorted.Count / 2;
            for (int i = 0; i < median; i++)  //Dividing the unsorted list
            {
                left.Add(unsorted[i]);
            }
            for (int i = median; i < unsorted.Count; i++)
            {
                right.Add(unsorted[i]);
            }
            left = OnilneMergeSort(left);
            right = OnilneMergeSort(right);
            return OnlineMerge(left, right);
        }
        private List<int> OnlineMerge(List<int> left, List<int> right)
        {
            List<int> result = new List<int>(); //The new collection

            while (left.Any() || right.Any())
            {
                if (left.Any() && right.Any())
                {
                    if (left.First() <= right.First())  //Comparing the first element of each sublist to see which is smaller
                    {
                        result.Add(left.First());
                        left.Remove(left.First());
                    }
                    else
                    {
                        result.Add(right.First());
                        right.Remove(right.First());
                    }
                }
                else if (left.Any())
                {
                    result.Add(left.First());
                    left.Remove(left.First());
                }
                else if (right.Any())
                {
                    result.Add(right.First());
                    right.Remove(right.First());
                }
            }
            return result;
        }

        public Span<int> SortWithSpan(Span<int> A)
        {
            if (A.Length <= 1)
                return A;
            var middle = A.Length / 2;
            Span<int> left = A.Slice(0, middle);
            Span<int> right = A.Slice(middle, A.Length - middle);
            left = SortWithSpan(left);
            right = SortWithSpan(right);
            return MergeSpan(left, right, A);
        }
        public Span<int> MergeSpan(Span<int> A, Span<int> B, Span<int> ParentSpan)
        {
            int i = 0, j = 0;
            List<int> merged = new List<int>(A.Length + B.Length);
            while (i != A.Length || j != B.Length)
            {
                if (i == A.Length)
                {
                    merged.AddRange(B.Slice(j, B.Length - j).ToArray());
                    new Span<int>(merged.ToArray()).CopyTo(ParentSpan);
                    return ParentSpan;
                }
                if (j == B.Length)
                {
                    merged.AddRange(A.Slice(i, A.Length - i).ToArray());
                    new Span<int>(merged.ToArray()).CopyTo(ParentSpan);

                    return ParentSpan;

                }
                if (A[i] >= B[j])
                {
                    merged.Add(A[i]);
                    i++;
                }
                else
                {
                    merged.Add(B[j]);
                    j++;
                }
            }
            return ParentSpan;
        }
        public Span<int> SortWithSpanImproved(Span<int> A, Span<int> temporary)
        {
            if (A.Length <= 1)
                return A;
            var middle = A.Length / 2;
            Span<int> left = A.Slice(0, middle);
            Span<int> right = A.Slice(middle, A.Length - middle);
            left = SortWithSpanImproved(left, temporary);
            right = SortWithSpanImproved(right, temporary);
            return MergeSpanImproved(left, right, A, temporary);
        }
        //Theoretically it should be possible to get the parentSpan from the left span by extending it, by i could not figure out how to do it. If you did then tell me please
        public Span<int> MergeSpanImproved(Span<int> A, Span<int> B, Span<int> ParentSpan, Span<int> temporary)
        {
            int i = 0, j = 0, k = 0;
            while (i != A.Length || j != B.Length)
            {
                if (i == A.Length)
                {
                    B.Slice(j, B.Length - j).CopyTo(temporary.Slice(A.Length + j, B.Length - j));
                    break;
                }
                if (j == B.Length)
                {
                    A.Slice(i, A.Length - i).CopyTo(temporary.Slice(B.Length + i, A.Length - i));
                    break;
                }
                if (A[i] >= B[j])
                {
                    temporary[k] = A[i];
                    i++;
                }
                else
                {
                    temporary[k] = B[j];
                    j++;
                }
                k++;
            }
            temporary.Slice(0, ParentSpan.Length).CopyTo(ParentSpan);
            return ParentSpan;
        }
        //Method takes two sorted "sublists" (left and right) of original list and merges them into a new colletion
        public List<int> MyMergeSort(List<int> A)
        {
            if (A.Count <= 1)
                return A;
            var middle = A.Count / 2;
            List<int> left = new List<int>(A.GetRange(0, middle));
            List<int> right = new List<int>(A.GetRange(middle, A.Count - middle));
            left = MyMergeSort(left);
            right = MyMergeSort(right);
            return MyMerge(left, right);
        }
        public List<int> MyMerge(List<int> A, List<int> B)
        {
            int i = 0, j = 0;
            List<int> merged = new List<int>(A.Count + B.Count);
            while (i != A.Count || j != B.Count)
            {
                if (i == A.Count)
                {
                    merged.AddRange(B.GetRange(j, B.Count - j));
                    return merged;
                }
                if (j == B.Count)
                {
                    merged.AddRange(A.GetRange(i, A.Count - i));
                    return merged;

                }
                if (A[i] >= B[j])
                {
                    merged.Add(A[i]);
                    i++;
                }
                else
                {
                    merged.Add(B[j]);
                    j++;
                }
            }
            return merged;
        }
    }
    class Program
    {
       
        static void Main(string[] args)
        {
            //var testList = new List<int> { 5, 2, 7, 3, 3, 4, 5, 7, 10, -5, -6, 25 }; 
            //var testArray = testList.ToArray();
            //var algorithms = new MergeSortingAlgorithms();
            //var orderedList = algorithms.MyMergeSort(testList);
            //Console.WriteLine("testList");
            //testList.ForEach(i => Console.Write(i + " "));
            //Console.WriteLine();
            //Console.WriteLine("orderedList");
            //orderedList.ForEach(i => Console.Write(i + " "));
            //var orderedArray = algorithms.SortWithSpanImproved(testArray.ToArray(), new int[testArray.Length]);
            //Console.WriteLine();
            //Console.WriteLine("orderedArray");
            //foreach (var item in orderedArray)
            //{
            //    Console.Write(item + ", ");
            //};
            //Console.WriteLine();
            //Console.WriteLine("testArray:");
            //foreach (var item in testArray)
            //{
            //    Console.Write(item + ", ");
            //};
            var summary = BenchmarkRunner.Run(typeof(Program).Assembly);
        }

    }
}
