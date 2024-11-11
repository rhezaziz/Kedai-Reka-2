using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class WorldPos
{
    int max = -2;
    int min = 28;
    
    public int valueLayer(float posZ)
    {
        float distance = min - max;
        float value = Mathf.Abs((posZ - max) - distance) / 3 + 2;

        int temp = Mathf.RoundToInt(value);
        return temp;
    }
}
