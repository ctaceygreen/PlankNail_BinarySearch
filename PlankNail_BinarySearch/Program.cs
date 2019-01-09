using System;
using System.Collections.Generic;
using System.Linq;
// you can also use other imports, for example:
// using System.Collections.Generic;

// you can write to stdout for debugging purposes, e.g.
// Console.WriteLine("this is a debug message");

class Solution
{
    public static void Main(string[] args)
    {
        solution(new int[] { 1 },new int[] {1 },new int[] {2 } );
    }

    public static bool checkPlanksNailed(int[] plankStarts, int[] plankEnds, int[] nails, int maxNails, int plankEndsMax)
    {
        
        //Build a prefix sum array of total nails at each index
        int[] prefixSumNails = new int[plankEndsMax + 1];
        for(int i =0;i < maxNails; i++)
        {
            if (nails[i] < prefixSumNails.Length)
            {
                prefixSumNails[nails[i]] += 1;
            }
        }
        for(int i = 1; i <= plankEndsMax; i++)
        {
            prefixSumNails[i] = prefixSumNails[i] + prefixSumNails[i - 1];
        }

        //For each plank, check that there is at least 1 nail in the prefix sum array between start and end of the plank. If not then plank cannot be nailed so return false
        for(int i = 0; i < plankStarts.Length; i++)
        {
            int totalAtPlankStart = prefixSumNails[plankStarts[i] - 1];
            int totalAtPlankEnd = prefixSumNails[plankEnds[i]];
            if(totalAtPlankStart == totalAtPlankEnd)
            {
                //Then no nails within this plank length so return false
                return false;
            }
        }
        return true;
    }
    public static int solution(int[] A, int[] B, int[] C)
    {
        // write your code in C# 6.0 with .NET 4.5 (Mono)

        //Binary search for number of nails sequentially needed. Each time we check if all planks are nailed, if not then increase, if they are then try reducing nails

        int plankEndsMax = B.Max();

        int lowerBound = 1;
        int upperBound = C.Length;

        if(A.Length == 0)
        {
            return 0;
        }

        int lowestNailsNeeded = -1;
        while(lowerBound <= upperBound)
        {
            int mid = (lowerBound + upperBound) / 2;
            if(checkPlanksNailed(A, B, C, mid, plankEndsMax))
            {
                //Planks nailed with this number of nails. Try lower to see 
                upperBound = mid - 1;
                lowestNailsNeeded = mid;
            }
            else
            {
                lowerBound = mid + 1;
            }
        }
        return lowestNailsNeeded;
    }
}