using Minis;
using UnityEngine;

public class InstrumentPlayer : FluidSynthPlayer
{
    void OnEnable()
    {
        MidiInputs.NoteOn += OnNoteOn;
        MidiInputs.NoteOff += OnNoteOff;
    }

    void OnDisable()
    {
        MidiInputs.NoteOn -= OnNoteOn;
        MidiInputs.NoteOff -= OnNoteOff;
    }

    private void OnNoteOn(MidiNoteControl midiNote, float velocity)
    {
    	NoteOn(midiNote.noteNumber, (int)(100*velocity));
    }

    private void OnNoteOff(MidiNoteControl midiNote)
    {
        NoteOff(midiNote.noteNumber);
    }
    
    
    //#if UNITY_EDITOR
    /// <summary>
    /// Debug to use piano without the midi keyboard
    /// </summary>
    private KeyCode[] _debugInputs = new KeyCode[] { KeyCode.Tab, KeyCode.Q, KeyCode.W, KeyCode.E, KeyCode.R, KeyCode.T, KeyCode.Y, KeyCode.U, KeyCode.I, KeyCode.O, KeyCode.P };
    private void Update()
    {
        for (int i = 0; i < _debugInputs.Length; i++)
        {
            var input = _debugInputs[i];
            var key = 36 + i * 6;
            if (Input.GetKeyDown(input)) NoteOn(key, 100);
            if (Input.GetKeyUp(input)) NoteOff(key);
        }
    }
    //#endif
}