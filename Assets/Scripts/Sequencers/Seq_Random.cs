using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Seq_Random : SequencerFactory
{
    public bool isLoop = false;
    [Tooltip("Just choses 1 from the list, will always loop")]
    public bool isTrulyRandom = false;
    private int currentEffect = 0;
    private List<SequenceManager> availableEffects = new List<SequenceManager>();
    public bool isAwaken = false;

    public void Start()
    {
        availableEffects.AddRange(effects);
        if (isAwaken)
        {
            AudioSampleTooling.onBeat += Play;
        }
    }

/*
    public override void Play()
    {
        base.Play();
        if (!isDone)
        {
            if (isTrulyRandom)
            {
                currentEffect = Random.Range(0, effects.Count);
                effects[currentEffect].Play();
            }
            else if (isLoop)
            {
                if (availableEffects.Count <= 0)
                {
                    availableEffects.AddRange(effects);
                }
                currentEffect = Random.Range(0, availableEffects.Count);
                availableEffects[currentEffect].Play();
                availableEffects.RemoveAt(currentEffect);
            }
            else
            {
                if (availableEffects.Count <= 0)
                {
                    //isDone = true;
                    return;
                }
                currentEffect = Random.Range(0, availableEffects.Count);
                availableEffects[currentEffect].Play();
                availableEffects.RemoveAt(currentEffect);
            }

        }
    }*/

}
