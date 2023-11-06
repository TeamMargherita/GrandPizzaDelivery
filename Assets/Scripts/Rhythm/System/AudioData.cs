using System.Collections.Generic;

public enum NoteType { None = 0, Normal, Hold }

/// <summary>
/// ∞Ó µ•¿Ã≈Õ∏¶ ¥„∞Ì ¿÷¥¬ ≈¨∑°Ω∫
/// </summary>
public class AudioData
{
    public string Name;                                 // ∞Ó ¿Ã∏ß
    public float BPM;                                   // ∞Ó BPM
    public string Level;                                // ∞Ó ≥≠¿Ãµµ
    public float Length;                                // ∞Ó ±Ê¿Ã
    public float Sync;                                  // ∞Ó ΩÃ≈©
    public SortedList<int, NoteType>[] NoteLines;       // ≥Î∆Æ ª˝º∫ µ•¿Ã≈Õ

    public AudioData()
    {
        Name = "no title";
        BPM = 60f;
        Level = "Easy";
        Length = 60f;
        Sync = 0f;
        NoteLines = new SortedList<int, NoteType>[2];
        NoteLines[0] = new SortedList<int, NoteType>();
        NoteLines[1] = new SortedList<int, NoteType>();
    }

    public AudioData(string fileName)
    {
        AudioData data = JsonManager<AudioData>.Load(fileName);
        if (data == null)
        {
            data = new AudioData();
        }
        Name = data.Name;
        BPM = data.BPM;
        Level = data.Level;
        Length = data.Length;
        Sync = data.Sync;
        NoteLines = data.NoteLines;
    }
}