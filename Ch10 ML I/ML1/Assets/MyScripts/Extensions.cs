using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Extensions 
{
    // Extension method adds Shuffle() method to existing List class. 
    // Extensions methods are static but called on an instance of an object
    public static void Shuffle<T>(this IList<T> ThisList)
    {
        var Count = ThisList.Count;
        var Last = Count - 1;
        for (var i = 0; i < Last; ++i)
        {
            var RandomIndex = UnityEngine.Random.Range(i, Count);
            var Temp = ThisList[i];
            ThisList[i] = ThisList[RandomIndex];
            ThisList[RandomIndex] = Temp;
        }
    }
}
