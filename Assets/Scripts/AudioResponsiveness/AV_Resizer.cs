using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AV_Resizer : MonoBehaviour
{
    //add: variables that can influence this
    public float sensitivity;

    public int band;

    [Tooltip("0 = no limit")]
    public float maxSize = 0;
    public Tooling.Algorithm algorithm;


    // Update is called once per frame
    void Update()
    {
        switch (algorithm)
        {
            case Tooling.Algorithm.Linear:
                Vector3 newScale = this.transform.localScale;
                newScale.y =  1 + AudioSampleTooling._freqBand[band];
                this.transform.localScale = newScale;
                break;
            case Tooling.Algorithm.Exp:
                break;
            case Tooling.Algorithm.Log:
                break;
            default:
                Debug.LogWarning("No algorithm has been chosen on " + this.gameObject.name);
                break;
        }
    }
}
