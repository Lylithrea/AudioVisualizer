using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Seq_Linear : SequencerFactory
{
    public bool isLoop = false;
    private int currentEffect = 0;
    public bool isAwaken = false;
    private bool isFirst = false;

    public void Start()
    {
        if (isAwaken)
        {
            AudioSampleTooling.onBeat += Play;
        }
    } 


    public override void Play()
    {
        base.Play();

        //if current effect is not done yet keep on playing that one.
        if (!effects[currentEffect].isDone)
        {
            effects[currentEffect].Play();
        }

        //if that one is done, we go to the next effect
        else
        {
            //set value back to false
            effects[currentEffect].isDone = false;
            currentEffect++;
            //if the current effect goes over the max we check if it should be looping
            if (currentEffect >= effects.Count)
            {
                //if it does loop we reset the value back to 0
                //if not then it never even is suppose to get here
                if (isLoop)
                {
                    currentEffect = 0;
                }

            }

            //we play the effect
            effects[currentEffect].Play();
            //after wards we check if its done (else it takes another beat to check if this should be done
            if (effects[currentEffect].isDone)
            {
                //if the current effect is the last one in the row, then we reset the values and set this script to done
                if (currentEffect == effects.Count - 1)
                {
                    isDone = true;
                    currentEffect = 0;
                    return;
                }
            }

        }

    }


}
