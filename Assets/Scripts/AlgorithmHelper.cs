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
            return Mathf.Exp(x * effectiveness);
        }

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