using System;
using Minis;
using UnityEngine;
using UnityEngine.InputSystem;

public class MidiInputs : MonoBehaviour
{
    public static Action<MidiNoteControl, float> NoteOn;
    public static Action<MidiNoteControl> NoteOff;
    
    private void Start()
    {
        InputSystem.onDeviceChange += (device, change) =>
        {
            if (change != InputDeviceChange.Added) return;

            var midiDevice = device as Minis.MidiDevice;
            if (midiDevice == null) return;

            midiDevice.onWillNoteOn += (note, velocity) =>  NoteOn?.Invoke(note, velocity);

            midiDevice.onWillNoteOff += note => NoteOff?.Invoke(note);
        };
    }
}
