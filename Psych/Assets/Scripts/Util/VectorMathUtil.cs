using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VectorMathUtil : MonoBehaviour
{
 
    public static Vector3 CalculateDirectionBetweenTwoPositions(Vector3 fromPosition, Vector3 toPosition)
    {
        return fromPosition - toPosition;
    }
}
