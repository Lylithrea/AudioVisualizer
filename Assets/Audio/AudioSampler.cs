using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace AudioHelper
{

    public class AudioSampler : AudioSampleTooling
    {

        public LoopbackAudio loopbackAudio;
        public Assets.Scripts.Audio.AudioVisualizationStrategy strategy;
        //public bool isUsingMicrophoneInput = false;
        public bool isUsingLasp = false;
        private Lasp.SpectrumAnalyzer analyzer;

/*        public void Awake()
        {
            if (isUsingMicrophoneInput)
            {

                var audio = GetComponent<AudioSource>();
                audio.clip = Microphone.Start(Microphone.devices[0], true, 10, 44100);

                //audio.loop = true;
                while (!(Microphone.GetPosition(null) > 0)) { }
                audio.Play();

            }
        }*/

        // Start is called before the first frame update
        public override void Start()
        {
            audioSpectrum = new float[samples];
            _freqBand = new float[bands];
            _gainBand = new float[samples];
            analyzer = GetComponent<Lasp.SpectrumAnalyzer>();

        }

        private void OnValidate()
        {
            _freqBand = new float[bands];
            _gainBand = new float[samples];
        }

        // Update is called once per frame
        public override void Update()
        {
            //AudioListener.GetSpectrumData(audioSpectrum, 0, FFTWindow.Hamming);
            if (isUsingLasp)
            {
                audioSpectrum = analyzer.spectrumArray.ToArray();
            }
            else
            {
                audioSpectrum = loopbackAudio.GetAllSpectrumData(strategy);
            }





            if (audioSpectrum != null && audioSpectrum.Length > 0)
            {
                spectrumValue = audioSpectrum[0] * 100;
            }


            base.Update();
        }
    }
}

