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
    
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.H)) NoteOn(60,100);
        if (Input.GetKeyUp(KeyCode.H)) NoteOff(60);
        if (Input.GetKeyDown(KeyCode.J)) NoteOn(61,100);
        if (Input.GetKeyUp(KeyCode.J)) NoteOff(61);
        if (Input.GetKeyDown(KeyCode.K)) NoteOn(62,100);
        if (Input.GetKeyUp(KeyCode.K)) NoteOff(62);
    }
}