using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AV_LightActivator : MonoBehaviour
{

    private Vector3 intialScale;

    private Vector3 startScale;
    private Vector3 endScale;

    private float time;
    public float riseSpeed;
    public float fallSpeed;
    private bool isRising = false;

    public float sensitivity = 0.1f;
    public float maxIntensity = 5;

    public int band;

    private Light light;
    public Tooling.Pass pass;

    // Start is called before the first frame update
    void Start()
    {
        light = GetComponent<Light>();
        startScale = this.transform.localScale;
    }


    public void Update()
    {
        //light.intensity = Tooling.AlgorithmHelper.Exp(AudioSampleTooling._freqBand[band], sensitivity);
        light.intensity = Tooling.AlgorithmHelper.Exp(AudioHelper.AudioSampler.getPitch(pass), sensitivity);
        if (light.intensity > maxIntensity * 10000)
        {
            light.intensity = maxIntensity * 10000;
        }

        /*        if (isRising)
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
                }*/
    }
}
