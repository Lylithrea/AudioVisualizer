using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;

public class AVV_PlayVFX : PlayFactory
{
    public VisualEffect effect;


    public override void Start()
    {
        //AudioSampleTooling.onBeat += spawn;
    }

    public override void Play()
    {
        base.Play();
        effect.Play();
        isDone = true;
    }

    void spawn()
    {
        effect.Play();
    }
}
