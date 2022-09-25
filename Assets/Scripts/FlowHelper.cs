using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlowHelper : MonoBehaviour
{


    private Vector3 intialScale;

    private Vector3 startScale;
    private Vector3 endScale;

    private float time;
    public float riseSpeed;
    public float fallSpeed;
    private bool isRising = false;


    private void Start()
    {
        intialScale = this.transform.localScale;
    }

    public void SetVariable(Vector3 scale)
    {
        endScale = scale;
        startScale = this.transform.localScale;
        time = 0;
        isRising = true;
        //StartCoroutine(Rising());
    }

    public void Update()
    {
        if (isRising)
        {
            time += Time.deltaTime * riseSpeed;
            if (time >= 1)
            {
                time = 1;
                isRising = false;
            }
            transform.localScale = Vector3.Lerp(startScale, endScale, time);
        }
        else
        {
            if (time > 0)
            {
                time -= Time.deltaTime * fallSpeed;
                if (time <= 0)
                {
                    time = 0;
                }

                transform.localScale = Vector3.Lerp(intialScale, this.transform.localScale, time);
            }
        }
    }


}
