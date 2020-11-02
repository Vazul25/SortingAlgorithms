# SortingAlgorithms
Measured the performance using benchmarkdotnet and made some improvements on the merge sort implementation found at: https://exceptionnotfound.net/merge-sort-csharp-the-sorting-algorithm-family-reunion/ to try out span, and answer a comment there.

# Benchmark with 300 000 int
Sadly i could not increase the workload any further since the benchmark hangs when the original algorithm is included in the test. 
So we got those nasty warns, but we can still see the difference between the results 

![3](https://user-images.githubusercontent.com/11043176/97875523-cbc56a80-1d1a-11eb-8f5c-84987c9cb667.PNG)

# Benchmark without blog provided algorithm on 30 000 000 int 
The creation of the original Array/List is not in the measured values

![30M](https://user-images.githubusercontent.com/11043176/97878304-c2d69800-1d1e-11eb-9970-17a682c8647b.PNG)
