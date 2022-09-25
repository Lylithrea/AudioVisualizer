using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioSampleTooling : MonoBehaviour
{
    public delegate void OnBeat();
    public static OnBeat onBeat;

    public delegate void OnSnare();
    public static OnSnare onSnare;


    public static float spectrumValue;
    public static float avgSpectrumValue;
    public static float[] _freqBand;
    public static float[] _gainBand;
    public static float bpm;

    public static int bands = 64;

    public int samples = 2048;
    public float maxVolume = 50;
    public float bias = 50;

    protected float lowPitch;
    protected float midPitch;
    protected float highPitch;

    protected float m_timer;
    protected float[] audioSpectrum;
    protected float bpmTimer = 0;

    protected float biasTimer = 0;


    public delegate void toggleObjects();
    public static toggleObjects onToggle;

    public delegate void resetObjects();
    public static resetObjects onResetObjects;


    public void Awake()
    {
        onBeat = null;
        onToggle = null;
        onResetObjects = null;
    }


    public virtual void Start()
    {
        onBeat();
    }

    public virtual void Update()
    {
        bpmTimer += Time.deltaTime;
        snareBeatTimer += Time.deltaTime;

        FrequencyBands();
        CheckBeat();
        CheckSnareBeat();
        getDynamicVolume();
    }

    public float volTimer = 0;
    private float avgVol = 0;
    private List<float> volumes = new List<float>();








    public void getDynamicVolume()
    {
        if (volTimer > 1)
        {
            volumes.Insert(0,getLowPitch());
            volumes.Insert(0,getMidPitch());
            volumes.Insert(0,getHighPitch());

            if (volumes.Count > 500)
            {
                for (int i = 0; i < 500; i++)
                {
                    avgVol += volumes[i];
                }
                avgVol /= 500;
            }
            else
            {
                for (int i = 0; i < volumes.Count; i++)
                {
                    avgVol += volumes[i];
                }
                avgVol /= volumes.Count;
            }
            avgSpectrumValue = avgVol;

            volTimer = 0;
        }

        volTimer += Time.deltaTime;
    }










    #region Beat


    private float averageVol = 5;
    private List<float> lowBeatsVol = new List<float>();

    //TODO: Create dynamic bias, based on total volume of low frequency
    //based on this total volume, adjust bias accordingly
    //implement minimum check timer (instead of every beat)
    //so that if its silent the bpm will be updated.

    public void Beat()
    {
        m_timer = 0;
        BPMCounter();
        if (onBeat != null)
        {
            onBeat();
        }

    }


    public void CheckBias()
    {
        lowBeatsVol.Insert(0, getLowPitch());
        if (lowBeatsVol.Count > 150)
        {
            for (int i = 0; i < 150; i++)
            {
                averageVol += lowBeatsVol[i];
            }
            averageVol /= 150;
        }
        else
        {
            for (int i = 0; i < lowBeatsVol.Count; i++)
            {
                averageVol += lowBeatsVol[i];
            }
            averageVol /= lowBeatsVol.Count;
        }
        bias = averageVol;
    }

    public void CheckBeat()
    {
        if (biasTimer > 0.25f)
        {
            biasTimer = 0;
            CheckBias();
        }

        if (getLowPitch() > averageVol)
        {
            lowBeatsVol.Insert(0, getLowPitch());
            if (m_timer > 0.1f)
                Beat();
        }

        m_timer += Time.deltaTime;
        biasTimer += Time.deltaTime;
    }

    public void BPMCounter()
    {
        bpm = 60 / bpmTimer;
        bpmTimer = 0;
    }


    #endregion

    #region Snare

    private float snareTimer = 0;
    private float snareBpmTimer = 0;
    protected float snareBeatTimer = 0;
    private float snareVol = 0;
    public float snareBpm = 0;
    private List<float> midBeatsVol = new List<float>();
    public float snareBias = 0;

    public void Snare()
    {
        snareTimer = 0;
        SnareBPMCounter();
        //onSnare();
    }


    public void CheckSnareBias()
    {
        midBeatsVol.Insert(0, getHighPitch());
        if (midBeatsVol.Count > 150)
        {
            for (int i = 0; i < 150; i++)
            {
                snareVol += midBeatsVol[i];
            }
            snareVol /= 150;
        }
        else
        {
            for (int i = 0; i < midBeatsVol.Count; i++)
            {
                snareVol += midBeatsVol[i];
            }
            snareVol /= midBeatsVol.Count;
        }
        snareBias = snareVol;
    }

    public void CheckSnareBeat()
    {
        if (snareBpmTimer > 0.25f)
        {
            snareBpmTimer = 0;
            CheckSnareBias();
        }

        if (getHighPitch() > snareVol)
        {
            //midBeatsVol.Insert(0, getHighPitch());
            if (snareTimer > 0.2f)
                Snare();
        }

        snareTimer += Time.deltaTime;
        snareBpmTimer += Time.deltaTime;
    }

    public void SnareBPMCounter()
    {
        snareBpm = 60 / snareBeatTimer;
        snareBeatTimer = 0;
    }


    #endregion





    public static float getPitch(Tooling.Pass pass)
    {
        switch (pass)
        {
            case Tooling.Pass.low:
                return getLowPitch();
            case Tooling.Pass.mid:
                return getMidPitch();
            case Tooling.Pass.high:
                return getHighPitch();

            case Tooling.Pass.lowlow:
                return getLowLowPitch();
            case Tooling.Pass.lowmid:
                return getLowMidPitch();
            case Tooling.Pass.lowhigh:
                return getLowHighPitch();

            case Tooling.Pass.midlow:
                return getMidLowPitch();
            case Tooling.Pass.midmid:
                return getMidMidPitch();
            case Tooling.Pass.midhigh:
                return getMidHighPitch();

            case Tooling.Pass.highlow:
                return getHighLowPitch();
            case Tooling.Pass.highmid:
                return getHighMidPitch();
            case Tooling.Pass.highhigh:
                return getHighHighPitch();

            default:
                return getLowPitch();
        }
    }

    public static float getVolume()
    {
        float value = 0;
        for (int i = 0; i < _freqBand.Length; i++)
        {
            value += _freqBand[i];
        }
        value /= _freqBand.Length;
        return value;
    }

    #region Pitches


    private static float getPitchRange(int start, int end, int max)
    {
        float edge = Mathf.Floor(_freqBand.Length / max);
        float counter = 0;
        for (int i = (int)(edge * start); i < edge * end; i++)
        {
            if (i < 4)
            {
                continue;
            }
            if (i > _freqBand.Length)
            {
                break;
            }
            counter += _freqBand[i];
        }
        return counter / edge;
    }

    public static float getLowLowPitch()
    {
        return getPitchRange(0, 1, 9);
    }

    public static float getLowMidPitch()
    {
        return getPitchRange(1, 2, 9);
    }

    public static float getLowHighPitch()
    {
        return getPitchRange(2, 3, 9);
    }

    public static float getMidLowPitch()
    {
        return getPitchRange(3, 4, 9);
    }

    public static float getMidMidPitch()
    {
        return getPitchRange(4, 5, 9);
    }

    public static float getMidHighPitch()
    {
        return getPitchRange(5, 6, 9);
    }

    public static float getHighLowPitch()
    {
        return getPitchRange(6, 7, 9);
    }

    public static float getHighMidPitch()
    {
        return getPitchRange(7, 8, 9);
    }

    public static float getHighHighPitch()
    {
        return getPitchRange(8, 9, 9);
    }


    public static float getLowPitch()
    {
        return getPitchRange(0, 1, 4);
    }

    public static float getMidPitch()
    {
        return getPitchRange(1, 3, 4);
    }

    public static float getHighPitch()
    {
        return getPitchRange(3, 4, 4);
    }


#endregion



    #region FrequencyBands

    private float previousHz = 0;
    public void FrequencyBands()
    {
        int count = 0;

        float normalizer = Mathf.Pow(2, bands - 1);

        for (int i = 0; i < bands - 1; i++)
        {
            //Debug.Log(i);
            float average = 0;

            float sampleCount = Mathf.Pow(2, i);
            float normalizedCount = sampleCount / normalizer;
            float hz = normalizedCount * samples;

            //go through all the samples, and add them together
            for (float j = previousHz; j < hz; j++)
            {
                //Debug.Log("count: " + count);
                //Debug.Log(" spectrum: " + audioSpectrum.Length);
                if (count > samples - 1)
                {
                    break;
                }
                average += audioSpectrum[count] * ((count + 1) / 2);
                count++;
            }
            previousHz = hz;

            if (count != 0)
            {
                average /= count;
            }

            

            if (average <= 0.001f)
            {
                average = 0;
            }
            float final = average * 15;

            //Debug.Log("Max bands : "  + bands + " Band " + i + " with value: " + final + " with average of : " + average + " count: " + count + " Spectrum length: " + audioSpectrum.Length);
            _freqBand[i] = final;
        }

    }

    #endregion

}
