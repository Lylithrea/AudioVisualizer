using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AVS_Bool : MonoBehaviour
{

    public string propertyName;
    public bool toggle = false;
    public float length;
    private float currentTime = 0;
    public int beats;
    public int offsetBeat;

    private bool isOn = false;
    private Material mat;
    private int currentBeat = 0;


    // Start is called before the first frame update
    void Start()
    {
        AudioSampleTooling.onBeat += onBeat;
        mat = this.GetComponent<MeshRenderer>().material;
        currentBeat = offsetBeat;
    }

    // Update is called once per frame
    void Update()
    {
        if (!toggle)
        {
            if (isOn)
            {
                currentTime += Time.deltaTime;
                if (currentTime >= length)
                {
                    isOn = false;
                    mat.SetInt("_" + propertyName, 0);
                    currentTime = 0;
                }
            }

        }
    }

    void onBeat()
    {
        currentBeat++;
        if (currentBeat >= beats)
        {
            currentBeat = 0;

            if (toggle)
            {
                isOn = !isOn;
                mat.SetInt("_" + propertyName, System.Convert.ToInt32(isOn));
            }
            else
            {
                isOn = true;
                mat.SetInt("_" + propertyName, 1);
            }
        }

    }


}
