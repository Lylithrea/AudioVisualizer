using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Tooling
{
   public class AlgorithmHelper : MonoBehaviour
    {
        public static float EaseSimple(float x, float effectiveness)
        {
            return (Mathf.Pow(x, 2 * (3 - 2 * x))) * effectiveness;
        }

        public static float Exp(float x, float effectiveness)
        {
            float value = Mathf.Exp( x *  Mathf.Abs(effectiveness));
            if (effectiveness < 0)
            {
                value *= -1;
            }
            return value;
        }

        public static Vector3 AxisVector(Tooling.Axis ax)
        {
            switch (ax)
            {
                case Tooling.Axis.x:
                    return new Vector3(1, 0, 0);
                case Tooling.Axis.y:
                    return new Vector3(0, 1, 0);
                case Tooling.Axis.z:
                    return new Vector3(0, 0, 1);
                default:
                    Debug.LogWarning("Axis is not supported");
                    return new Vector3(0,0,0);
            }
        }


    }

    
    public enum Pass
    {
        low,
        mid,
        high,
        lowlow,
        lowmid,
        lowhigh,
        midlow,
        midmid,
        midhigh,
        highlow,
        highmid,
        highhigh
    }

    public enum Algorithm
    {
        Linear,
        Log,
        Exp,
        EaseSimple
    }

    public enum Axis
    {
        x,
        y,
        z
    }

}