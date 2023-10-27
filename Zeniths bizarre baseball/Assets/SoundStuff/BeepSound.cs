using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeepSound : MonoBehaviour
{
    float[] frequency = { 261.63f, 293.66f, 329.63f, 349.23f, 392.00f, 440.00f, 493.88f, 523.25f }; // Example notes (C4 to C5)
    float[] duration = { 0.5f, 0.3f, 0.4f, 0.2f, 0.5f, 0.3f, 0.4f, 0.2f };
    void CreateBeep(float frequence, float duration)
    {
        int sampleFreq = (int)MathF.Round(44000);
        float frequency = frequence;
        float[] samples = new float[(int)Mathf.Round(44000 * duration)];
        for(int i = 0; i < samples.Length; i++)
        {
            samples[i] = Mathf.Sin(Mathf.PI*2*i*frequency/sampleFreq);
        }

        AudioClip ac = AudioClip.Create("Test", samples.Length, 1, sampleFreq, false);
        ac.SetData(samples, 0);
        AudioSource.PlayClipAtPoint(ac, Vector2.zero);
    }

    private void Start() {
        StartCoroutine(PlayMusic());
    }

    IEnumerator PlayMusic()
    {
        while(true)
        {
            for(int i = 0; i < frequency.Length; i++)
            {
                CreateBeep(frequency[i], duration[i]);
                yield return new WaitForSecondsRealtime(duration[i]);
            }
        }
    }
}
