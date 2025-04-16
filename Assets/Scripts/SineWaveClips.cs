using UnityEngine;

public class SineWaveClips
{
    public static AudioClip[] GenerateAllMidiClips()
    {
        var midiClips = new AudioClip[128];
        for (int i = 0; i < 128; i++)
        {
            float frequency = MidiNoteToFrequency(i);
            midiClips[i] = CreateSineWaveAudioClip(frequency, 1.0f); // 1 second duration
        }

        return midiClips;
    }

    private static float MidiNoteToFrequency(int midiNote)
    {
        return 440f * Mathf.Pow(2, (midiNote - 69) / 12f);
    }

    private static AudioClip CreateSineWaveAudioClip(float frequency, float durationSeconds)
    {
        int sampleRate = 44100;
        int sampleCount = (int)(sampleRate * durationSeconds);
        float[] samples = new float[sampleCount];

        for (int i = 0; i < sampleCount; i++)
        {
            samples[i] = Mathf.Sin(2 * Mathf.PI * frequency * i / sampleRate);
        }

        AudioClip clip = AudioClip.Create("Note_" + frequency, sampleCount, 1, sampleRate, false);
        clip.SetData(samples, 0);
        return clip;
    }
}