using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AV_Move : MonoBehaviour
{

    public float sensitivity;

    public Tooling.Pass pass;

    private Vector3 startPosition = new Vector3(0,0,0);

    //transform.localPosition = startValue + (this.transform.forward* AudioSampler.getPitch(pitch)* (sensitivity* Settings.circleSensitivity) * AudioSampler.avgSpectrumValue);

    // Start is called before the first frame update
    void Start()
    {
        startPosition = this.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = startPosition + this.transform.forward * AudioHelper.AudioSampler.getPitch(pass) * sensitivity;
    }
}
