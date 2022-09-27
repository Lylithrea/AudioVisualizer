using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;

public class AVV_PlayVFX : MonoBehaviour
{
    public VisualEffect effect;


    void Start()
    {
        AudioSampleTooling.onBeat += spawn;
    }

    void spawn()
    {
        effect.Play();
    }
}
