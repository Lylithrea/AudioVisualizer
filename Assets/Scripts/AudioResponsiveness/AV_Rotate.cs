using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AV_Rotate : MonoBehaviour
{

    public float sensitivity;
    public float radius;
    public Tooling.Pass pass;
    public Tooling.Axis axis;
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

        newRotation = Tooling.AlgorithmHelper.AxisVector(axis) * Tooling.AlgorithmHelper.Exp(AudioSampleTooling.getPitch(pass), sensitivity);

        switch (axis)
        {
            case Tooling.Axis.x:
                newRotation.y = this.transform.localRotation.y;
                newRotation.z = this.transform.localRotation.z;
                break;
            case Tooling.Axis.y:
                newRotation.x = this.transform.localRotation.x;
                newRotation.z = this.transform.localRotation.z;
                break;
            case Tooling.Axis.z:
                newRotation.x = this.transform.localRotation.x;
                newRotation.y = this.transform.localRotation.y;
                break;
            default:
                Debug.LogWarning("Incorrect axis");
                break;
        }

        transform.localRotation = startRotation * Quaternion.Euler(newRotation);
    }
}
