using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Seq_Random : SequencerFactory
{
    public bool isLoop = false;
    [Tooltip("Just choses 1 from the list, will always loop")]
    public bool isTrulyRandom = false;
    private List<SequenceManager> availableEffects = new List<SequenceManager>();
    [Tooltip("Enable this for the top sequencer, this is the god of all sequences")]
    public bool isAwaken = false;

    public float timer;
    private float currentTimer = 0;

    public void Start()
    {
        availableEffects.AddRange(effects);
        currentEffect = Random.Range(0, effects.Count);
        if (isAwaken)
        {
            AudioSampleTooling.onBeat += Play;
        }
    }


    public void Update()
    {
        if (isPlaying)
        {
            if (currentTimer <= 0)
            {
                currentTimer = timer;

                if (isTrulyRandom)
                {
                    if (effects[currentEffect].Test())
                    {
                        currentEffect = Random.Range(0, effects.Count);
                    }
                }

                else if (availableEffects[currentEffect].Test())
                {
                    availableEffects.RemoveAt(currentEffect);
                    currentEffect = Random.Range(0, availableEffects.Count);

                    if (availableEffects.Count == 0)
                    {
                        availableEffects.AddRange(effects);
                        currentEffect = Random.Range(0, availableEffects.Count);

                        if (!isLoop)
                        {
                            isPlaying = false;
                            return;
                        }
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
        if (isTrulyRandom)
        {
            if (effects[currentEffect].Test())
            {
                currentEffect = Random.Range(0, effects.Count);
                return true;
            }

        }
        else
        {
            if (availableEffects[currentEffect].Test())
            {
                availableEffects.RemoveAt(currentEffect);
                if (availableEffects.Count == 0)
                {
                    availableEffects.AddRange(effects);
                    currentEffect = Random.Range(0, availableEffects.Count);
                    return true;
                }
                currentEffect = Random.Range(0, availableEffects.Count);
            }
        }

        return false;

    }

    void resetLastEffect(int effectCount)
    {
        effects[effectCount].isPlaying = false;
        if (!effects[effectCount].doNotReset)
        {
            effects[effectCount].ResetEffect();
        }
    }




    public override void ResetEffect()
    {
        availableEffects.Clear();
        availableEffects.AddRange(effects);
        currentEffect = Random.Range(0, effects.Count);
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
