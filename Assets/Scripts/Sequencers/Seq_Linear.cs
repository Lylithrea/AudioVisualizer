using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Seq_Linear : SequencerFactory
{

    [Tooltip("Enable this for the top sequencer, this is the god of all sequences")]
    public bool isAwaken = false;

    public bool isLoop = false;
    public float timer;
    private float currentTimer = 0;


    public void Start()
    {
        if (isAwaken)
        {
            AudioSampleTooling.onBeat += blep;
        }
    }


    public void Update()
    {

        if (isPlaying)
        {
            if (currentTimer <= 0)
            {
                currentTimer = timer;

                if (effects[currentEffect].Test())
                {
                    currentEffect++;
                }

                if (currentEffect >= effects.Count)
                {
                    if (isLoop)
                    {
                        currentEffect = 0;
                    }
                    else
                    {
                        isPlaying = false;
                        currentEffect = 0;
                    }
                }
            }
            else
            {
                currentTimer -= Time.deltaTime;
            }
        }
    }

    public void blep()
    {
        Test();
    }

    //true means its done
    public override bool Test()
    {

        if (playFullSequence)
        {
            isPlaying = true;
            return true;
        }


        //check if the last effect was always playing, if so, then disable it
        if (currentEffect == 0)
        {
            if (effects[effects.Count - 1].playFullSequence)
            {
                resetLastEffect(effects.Count - 1);
            }
        }
        else if (effects[currentEffect - 1].playFullSequence)
        {
            resetLastEffect(currentEffect - 1);
        }

        //check if the next one is done, if so increase the effect count
        if (effects[currentEffect].Test())
        {
            currentEffect++;
        }

        //check if we reached the end
        if (currentEffect == effects.Count)
        {
            currentEffect = 0;
            return true;
        }

        return false;

    }

    void resetLastEffect(int effectCount)
    {
        effects[effectCount].isPlaying = false;
        if (!effects[effectCount].doNotReset || effects[effectCount].playFullSequence)
        {
            effects[effectCount].ResetEffect();
        }

    }



















    //loops one effect


    /*        if (effects[currentEffect].Test())
            {
                currentEffect++;
                if (currentEffect >= effects.Count)
                {
                    currentEffect = 0;
                }
    return false;
            }*/



    /* public override void Play()
     {
         base.Play();

         //the issue is: we set this to being done after the last effect of this sequence is done in the same call
         //so we cant set the last effect to false because it should still be playing, only when the next call it should be disabled.

         if (playFullSequence)
         {
             isPlaying = true;
             isDone = true;
             return;
         }

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
             effects[currentEffect].isPlaying = false;
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

             //if the current effect is the last one in the row, then we reset the values and set this script to done
             if (currentEffect == effects.Count - 1)
             {
                 //after wards we check if its done (else it takes another beat to check if this should be done
                 if (effects[currentEffect].isDone)
                 {
                     isPlaying = false;
                     isDone = true;
                     currentEffect = 0;
                     return;
                 }
             }


         }



     }*/


}
