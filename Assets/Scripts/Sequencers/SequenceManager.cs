using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SequenceManager : MonoBehaviour
{
    public bool playFullSequence = false;
    public bool isPlaying = false;
    public bool doNotReset = false;
    public int currentEffect = 0;
    public virtual void Play()
    {

    }

    public virtual bool Test()
    {
        return false;
    }

}
