using System;
using System.Collections.Generic;
using System.Linq;

namespace CodeToTest
{
    public class Algorithms
    {
        public int FindMin(int[] arr)
        {
            var min = arr[0];
            foreach (var number in arr)
            {
                if (number < min)
                {
                    min = number;
                }
            }

            return min;
        }

        public int FindMax(int[] arr)
        {
            return arr.Max();
        }

        public bool IsPrime(int number)
        {
            if (number < 2)
                return false;

            var m = number / 2;
            for (var i = 2; i <= m; i++)
            {
                if (number % i == 0)
                {
                    return false;
                }
            }

            return true;
        }

        // source: https://www.tutorialspoint.com/Bubble-Sort-program-in-Chash
        public void BubbleSort(int[] arr)
        {
            for (int j = 0; j <= arr.Length - 2; j++)
            {
                for (int i = 0; i <= arr.Length - 2; i++)
                {
                    if (arr[i] > arr[i + 1])
                    {
                        var temp = arr[i + 1];
                        arr[i + 1] = arr[i];
                        arr[i] = temp;
                    }
                }
            }
        }

        // source: https://www.geeksforgeeks.org/sieve-of-eratosthenes/
        public List<int> SieveOfEratosthenes(int n)
        {
            // Create a boolean array "prime[0..n]" and initialize 
            // all entries it as true. A value in prime[i] will 
            // finally be false if i is Not a prime, else true. 
            bool[] prime = new bool[n + 1];

            for (int i = 0; i < n; i++)
                prime[i] = true;

            for (int p = 2; p * p <= n; p++)
            {
                // If prime[p] is not changed, 
                // then it is a prime 
                if (prime[p] == true)
                {
                    // Update all multiples of p 
                    for (int i = p * p; i <= n; i += p)
                        prime[i] = false;
                }
            }

            var result = new List<int>();
            for (int i = 2; i <= n; i++)
            {
                if (prime[i] == true)
                    result.Add(i);
            }

            return result;
        }
    }
}
