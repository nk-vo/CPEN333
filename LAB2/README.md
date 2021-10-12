## Name: Nick Vo
## Prof told me not to upload my student ID here, sorry for the troubles.

## Part 1

I'm unsure whether I need to merge sort again after multithreading. Prior to multithread I split the array into multiple sub arrays, which 
I then merge sort each of them and join the threads together. As the instruction indicated, I can only merge the sub arrays together after 
threads join. But merging sorted sub array together will create a big unsorted array. It's unclear whether I need to sort the merged array 
again. If so, wouldn't that defeat the purpose of spliting the array into smaller parts. I could just merge sort them as a single array.
I, however, did include the code that sorts the merged array after on line 81. And by performing merge sort task twice, the multithread 
approach takes significantly longer time than single thread.

I might be missing something.... If so, please do let me know. Thanks

## Question
1. In a table summarize the duration of time for your single thread and multi-thread merge sort algorithms for the following unsorted array sizes of {10, 10^2, 10^3, 10^4, 10^5, 10^6, 10^7}

- With 8 threads

| Array size | Single thread | Multithread  |
| :--------: | :-----------: | :---------:  |
|     10     |    0.0003s    |   0.0100s    |
|    10^2    |    0.0006s    |   0.0110s    |
|    10^3    |    0.0007s    |   0.0155s    |
|    10^4    |    0.0016s    |   0.0173s    |
|    10^5    |    0.0082s    |   0.0309s    |
|    10^6    |    0.0728s    |   0.0610s    |
|    10^7    |    7.0014s    |   3.0773s    |

2. How much speed-up were you expecting based on the number of processors/cores on your machine?
- Speed-up = number of cores = 2

3. Did you achieve the speed-up you expected? If not, what do you think might be interfering with this?
- Speed-up gets better with larger array size and approaching optimal speed up factor but will never equal
- Posible interferences: branching, threads take time to start and join, other background tasks that run parallel with my program

4. In your parallel implementation, try different number of threads for an array size of million elements. Observe the speed up factor as a function of thread size, i.e. speed-up factor (Y-axis) for #threads increasing (X-axis). Summarize your results in a table in your README file in the repo.

| #threads | speed-up factor |
| :------: | :-------------: |
|     1    |       1.06      |
|     2    |       1.42      |
|     3    |       1.71      |
|     4    |       1.74      |
|     5    |       1.29      |
|     6    |       1.39      |
|     7    |       1.39      |
|     8    |       1.45      |
|     9    |       1.26      |
|    10    |       1.16      |
|    20    |       0.74      |
|    40    |       0.07      |
|    80    |       0.03      |
|   100    |       0.04      |


## Part 2

## Question
1. What have you learned in terms of splitting up work between threads?

- I've learned that if I need to work with large sample size and many iterations, multithreading will help speed up the running speed of my program. However, there's a sweet spot of how many threads I need to initilize for better performance. From what I've found, using twice the number of cores my computer has would yeild the best result.

2. What implications does this have when designing concurrent code?

- It seems like multithreading only works for sample size of 10^6 or higher. So when I face such problems in the future, I will surely use multithread instead of the regular serial work on a single thread.

3. How many samples do you think you will need for an accuracy of 7 decimal places? Is the Monte Carlo simulation an efficient method to estimate Pi with high accuracy (feel free to research in the internet)?

- 10^9 samples accurate to the 3 decimal places 
- https://stats.stackexchange.com/questions/295007/how-many-samples-to-get-the-accuracy-to-the-kth-digits-of-pi
- The link above provides some insight into how many sample I need for a 7-decimal-accurary estimation of PI with 5% error
- The answer is 4.9x10^13 samples
- Since Monte Carlo approach requires so many samples, it is very ineffcient and takes really long for only a few decimal accuracy
- http://www.cecm.sfu.ca/organics/papers/borwein/paper/html/paper.html 
- This website provides another approach using Ramanujan formulae gives hundreds of digits accuracy in a fraction of a second.