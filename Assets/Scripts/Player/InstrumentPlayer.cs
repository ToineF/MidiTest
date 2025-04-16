using Minis;

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
}