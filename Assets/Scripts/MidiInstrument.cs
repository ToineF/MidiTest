using UnityEngine;
using Minis;

public class MidiInstrument : MonoBehaviour
{
    [SerializeField] private AudioClip[] _noteClips = new AudioClip[128]; 
    [SerializeField] private AudioSource _audioSourcePrefab;
    [SerializeField] private float _masterVolume = 1.0f;

    private AudioSource[] _audioSources = new AudioSource[128];

    void Awake()
    {
        _noteClips = SineWaveClips.GenerateAllMidiClips();
        for (int i = 0; i < 128; i++)
        {
            var source = Instantiate(_audioSourcePrefab, transform);
            source.clip = _noteClips[i];
            _audioSources[i] = source;
        }
    }

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

    void OnNoteOn(MidiNoteControl note, float velocity)
    {
        var index = note.noteNumber;
        if (index < 0 || index >= 128 || _noteClips[index] == null) return;

        var source = _audioSources[index];
        source.volume = velocity * _masterVolume;
        source.time = 0f;
        source.Play();
    }

    void OnNoteOff(MidiNoteControl note)
    {
        var index = note.noteNumber;
        if (index < 0 || index >= 128)
            return;

        var source = _audioSources[index];
        if (source.isPlaying)
        {
            source.Stop();
        }
    }
}
