using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AV_Rotate : MonoBehaviour
{

    public float sensitivity;
    public float radius;
    public Tooling.Pass pass;
    public bool useSingularPass = false;
    public Tooling.Pass singularPass;
    private Quaternion startRotation;
    private Vector3 newRotation = new Vector3(0, 0, 0);

    //transform.localPosition = startValue + (this.transform.forward* AudioSampler.getPitch(pitch)* (sensitivity* Settings.circleSensitivity) * AudioSampler.avgSpectrumValue);

    // Start is called before the first frame update
    void Start()
    {
        startRotation = this.transform.localRotation;
    }

    // Update is called once per frame
    void Update()
    {
        if (!useSingularPass)
        {
            newRotation = new Vector3(AudioSampleTooling.getPitch(pass) * sensitivity, 0, 0);
            transform.localRotation = startRotation * Quaternion.Euler(newRotation);
        }
        else
        {
            newRotation = new Vector3(AudioSampleTooling.getPitch(singularPass) * sensitivity, 0, 0);
            transform.localRotation = startRotation * Quaternion.Euler(newRotation);
        }
    }
}
