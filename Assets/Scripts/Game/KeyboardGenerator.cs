using System;
using System.Collections.Generic;
using Minis;
using UnityEngine;

/// <summary>
/// Create keys in the world
/// </summary>
public class KeyboardGenerator : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private KeyboardKey _whiteKeyPrefab;
    [SerializeField] private KeyboardKey _blackKeyPrefab;
    [SerializeField] private float _spacing;
    [SerializeField] private Transform _parent;
    [SerializeField] private int _startOctave = 2;
    [SerializeField] private int _endOctave = 7;
    [SerializeField] private int _additionalKeysAtTheEnd;

    private Dictionary<int, KeyboardKey> _keys;
    private int _startOffset;
    private HashSet<int> _blackKeysIndexes = new HashSet<int>() { 1, 3, 6, 8, 10 };


    private void Start()
    {
        SpawnKeys();
    }

    private void SpawnKeys()
    {
        _startOffset = Mathf.Clamp(_startOctave * 12, 0, 128);
        var end = Mathf.Clamp((_endOctave + 1) * 12 + _additionalKeysAtTheEnd, 0, 128);
        
        _keys = new Dictionary<int, KeyboardKey>();
        float totalSpacing = 0f;
        for (int i = _startOffset; i < end; i++)
        {
            _keys.Add(i, SpawnKey(i, ref totalSpacing));
        }

        // Offsets parent
        _parent.localScale *= 0.4f;
        _parent.transform.localPosition += Vector3.left * 9f;
    }

    private KeyboardKey SpawnKey(int note, ref float totalSpacing)
    {
        var isBlackKey = _blackKeysIndexes.Contains(note % 12);
        var isLastBlackKey = _blackKeysIndexes.Contains((note - 1 + 12) % 12);
        var newKey = Instantiate(isBlackKey ? _blackKeyPrefab : _whiteKeyPrefab, _parent);
        totalSpacing += isBlackKey || isLastBlackKey ? _spacing/2 : _spacing;
        newKey.transform.localPosition = new Vector3(totalSpacing, 0, 0);
        return newKey;
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

    private void OnNoteOn(MidiNoteControl midiNote, float velocity)
    {
        if (_keys.ContainsKey(midiNote.noteNumber) == false) return;
        Debug.Log(midiNote.shortDisplayName);
        _keys[midiNote.noteNumber].On(velocity);
    }
    private void OnNoteOff(MidiNoteControl midiNote)
    {
        if (_keys.ContainsKey(midiNote.noteNumber) == false) return;
        _keys[midiNote.noteNumber].Off();
    }
    
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.H)) _keys[60].On(100);
        if (Input.GetKeyUp(KeyCode.H)) _keys[60].Off();
        if (Input.GetKeyDown(KeyCode.J)) _keys[61].On(100);
        if (Input.GetKeyUp(KeyCode.J)) _keys[61].Off();
        if (Input.GetKeyDown(KeyCode.K)) _keys[62].On(100);
        if (Input.GetKeyUp(KeyCode.K)) _keys[62].Off();
    }
}