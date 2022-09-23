using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AV_Resizer : MonoBehaviour
{
    //add: variables that can influence this
    public float sensitivity;

    public int band;
    public Tooling.Axis axis;
    [Tooltip("0 = no limit")]
    public float maxSize = 0;
    public Tooling.Algorithm algorithm;


    Vector3 newScale = new Vector3(0,0,0);

    public void Start()
    {
         newScale = this.transform.localScale;
    }

    // Update is called once per frame
    void Update()
    {
        switch (axis)
        {
            case Tooling.Axis.x:
                Algorithm(new Vector3(1,0,0));
                break;
            case Tooling.Axis.y:
                Algorithm(new Vector3(0, 1, 0));
                break;
            case Tooling.Axis.z:
                Algorithm(new Vector3(0, 0, 1));
                break;
            default:
                Debug.LogWarning("No axis has been assigned!");
                break;
        }

    }

    void Algorithm(Vector3 axis)
    {
        switch (algorithm)
        {

            case Tooling.Algorithm.Linear:

                axis *= AudioSampleTooling._freqBand[band];
                this.transform.localScale = newScale + axis;
                break;
            case Tooling.Algorithm.Exp:
                axis *= Tooling.AlgorithmHelper.Exp(AudioSampleTooling._freqBand[band], sensitivity);
                this.transform.localScale = newScale + axis;
                break;
            case Tooling.Algorithm.Log:
                break;
            default:
                Debug.LogWarning("No algorithm has been chosen on " + this.gameObject.name);
                break;
        }
    }

}
