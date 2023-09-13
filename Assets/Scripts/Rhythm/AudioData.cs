using System.Collections;
using System.Collections.Generic;
using UnityEngine;
class AudioData
{
    public string Name;         // ∞Ó ¿Ã∏ß
    public decimal BPM;         // ∞Ó BPM
    public decimal Length;      // ∞Ó ±Ê¿Ã
    public bool[] IsNote;     // ≥Î∆Æ ª˝º∫ Ω√∞£

    public AudioData()
    {
        IsNote = new bool[100];
        for (int i = 0; i < IsNote.Length; i++)
            IsNote[i] = true;
    }
}
