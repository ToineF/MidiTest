using System;
using System.Runtime.InteropServices;
using UnityEngine;

/// <summary>
/// Uses FluidSynth to play sounds to an AudioSource (low-leve)
/// </summary>
public abstract class FluidSynthPlayer : MonoBehaviour
{
    [DllImport("fluidsynth-3", CallingConvention = CallingConvention.Cdecl)]
    private static extern IntPtr new_fluid_settings();

    [DllImport("fluidsynth-3", CallingConvention = CallingConvention.Cdecl)]
    private static extern IntPtr new_fluid_synth(IntPtr settings);

    [DllImport("fluidsynth-3", CallingConvention = CallingConvention.Cdecl)]
    private static extern int fluid_settings_setstr(IntPtr settings, string name, string val);

    [DllImport("fluidsynth-3", CallingConvention = CallingConvention.Cdecl)]
    private static extern int fluid_synth_sfload(IntPtr synth, string filename, int resetPresets);

    [DllImport("fluidsynth-3", CallingConvention = CallingConvention.Cdecl)]
    private static extern int fluid_synth_noteon(IntPtr synth, int channel, int key, int velocity);

    [DllImport("fluidsynth-3", CallingConvention = CallingConvention.Cdecl)]
    private static extern int fluid_synth_noteoff(IntPtr synth, int channel, int key);

    [DllImport("fluidsynth-3", CallingConvention = CallingConvention.Cdecl)]
    private static extern int fluid_synth_write_s16(
        IntPtr synth,
        int len,
        IntPtr leftBuf, int leftOffset, int leftInc,
        IntPtr rightBuf, int rightOffset, int rightInc
    );

    [DllImport("fluidsynth-3", CallingConvention = CallingConvention.Cdecl)]
    private static extern void delete_fluid_synth(IntPtr synth);

    [DllImport("fluidsynth-3", CallingConvention = CallingConvention.Cdecl)]
    private static extern void delete_fluid_settings(IntPtr settings);
    
    [DllImport("fluidsynth-3", CallingConvention = CallingConvention.Cdecl)]
    private static extern int fluid_settings_setnum(IntPtr settings, string name, double val);

    [SerializeField] private string _soundFontName;

    private IntPtr _synth;
    private IntPtr _settings;

    private GCHandle _leftHandle;
    private GCHandle _rightHandle;

    private short[] _leftBuffer;
    private short[] _rightBuffer;

    private int _sampleRate = 44100;

    private void Start()
    {
        _settings = new_fluid_settings();
        fluid_settings_setnum(_settings, "synth.sample-rate", _sampleRate);

        fluid_settings_setstr(_settings, "audio.driver", "file"); // disable internal playback
        _synth = new_fluid_synth(_settings);

        int loadResult = fluid_synth_sfload(_synth, $"Assets/Resources/Soundfonts/{_soundFontName}.sf2", 1);
        if (loadResult < 0)
        {
            Debug.LogError("Failed to load soundfont.");
        }
    }
    
    private void OnAudioFilterRead(float[] data, int channels)
    {
        int frames = data.Length / channels;

        // Prepare native buffers
        if (_leftBuffer == null || _leftBuffer.Length != frames)
        {
            _leftBuffer = new short[frames];
            _rightBuffer = new short[frames];
            _leftHandle = GCHandle.Alloc(_leftBuffer, GCHandleType.Pinned);
            _rightHandle = GCHandle.Alloc(_rightBuffer, GCHandleType.Pinned);
        }

        int result = fluid_synth_write_s16(
            _synth, frames,
            _leftHandle.AddrOfPinnedObject(), 0, 1,
            _rightHandle.AddrOfPinnedObject(), 0, 1
        );

        if (result != 0)
        {
            Debug.LogError("fluid_synth_write_s16 failed!");
            return;
        }

        // Convert short samples to float
        for (int i = 0, s = 0; i < frames; i++)
        {
            short l = _leftBuffer[i];
            short r = _rightBuffer[i];

            if (channels == 1)
            {
                data[s++] = l / 32768.0f;
            }
            else
            {
                data[s++] = l / 32768.0f;
                data[s++] = r / 32768.0f;
            }
        }
    }

    private void OnDestroy()
    {
        if (_synth != IntPtr.Zero) delete_fluid_synth(_synth);
        if (_settings != IntPtr.Zero) delete_fluid_settings(_settings);

        if (_leftHandle.IsAllocated) _leftHandle.Free();
        if (_rightHandle.IsAllocated) _rightHandle.Free();
    }

    protected void NoteOn(int key, int velocity)
    {
        fluid_synth_noteon(_synth, 0, key, velocity);
    }

    protected void NoteOff(int key)
    {
        fluid_synth_noteoff(_synth, 0, key);
    }
}
