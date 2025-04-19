using System;
using System.IO;
using Minis;
using UnityEngine;
using UnityEngine.InputSystem;

public class MidiInputs : MonoBehaviour
{
    public static Action<MidiNoteControl, float> NoteOn;
    public static Action<MidiNoteControl> NoteOff;

    private void Awake()
    {
        File.AppendAllText(Path.Combine(Application.persistentDataPath, "log.txt"), "MidiInputs.Awake() was called\n");
        // Handle devices already connected
        foreach (var device in InputSystem.devices)
        {
            if (device is MidiDevice midiDevice)
            {
                HookMidiDevice(midiDevice);
            }
        }

        // Handle new devices that get added after launch
        InputSystem.onDeviceChange += (device, change) =>
        {
            if (change == InputDeviceChange.Added && device is MidiDevice midiDevice)
            {
                HookMidiDevice(midiDevice);
            }
        };
    }

    private void HookMidiDevice(MidiDevice midiDevice)
    {
        File.AppendAllText(Path.Combine(Application.persistentDataPath, "log.txt"), $"Hooked MIDI device: {midiDevice.name}\n");

        midiDevice.onWillNoteOn += (note, velocity) =>
        {
            NoteOn?.Invoke(note, velocity);
        };

        midiDevice.onWillNoteOff += note =>
        {
            NoteOff?.Invoke(note);
        };
    }
}
