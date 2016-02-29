using System;
using System.Linq;

public static class ArrayHelper
{ 
    public static int[] GenerateRandomIndexesArray(int length)
    {
        int[] answer = new int[length];

        for (int i = 0; i < length; i++)
        {
            answer[i] = i;
        }

        Random rnd = new Random();
        answer = answer.OrderBy(x => rnd.Next()).ToArray();

        return answer;
    }
}
