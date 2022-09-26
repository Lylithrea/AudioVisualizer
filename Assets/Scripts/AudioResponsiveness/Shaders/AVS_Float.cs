using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AVS_Float : MonoBehaviour
{
    public string propertyName;
    public float sensitivity;
    public float min, max;
    public Tooling.Algorithm algorithm;
    public Tooling.Pass pass;

    private Material mat;

    // Start is called before the first frame update
    void Start()
    {
        mat = this.GetComponent<MeshRenderer>().material;
    }

    // Update is called once per frame
    void Update()
    {

        switch (algorithm)
        {
            case Tooling.Algorithm.Linear:
                updateValue(min + AudioSampleTooling.getPitch(pass) * sensitivity);
                break;
            case Tooling.Algorithm.Exp:
                updateValue(Tooling.AlgorithmHelper.Exp(min + AudioSampleTooling.getPitch(pass), sensitivity));
                break;
            default:
                Debug.LogWarning("No implementation of algorithm");
                break;
        }

 


    }


    void updateValue(float value)
    {
        if (max != 0 && max < value)
        {
            value = max;
        }
        mat.SetFloat("_" + propertyName, value);
    }

}
