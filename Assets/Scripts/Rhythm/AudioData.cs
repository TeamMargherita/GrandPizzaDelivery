using System.Collections.Generic;

public enum NoteType { None = 0, Normal, Hold }

/// <summary>
/// ∞Ó µ•¿Ã≈Õ∏¶ ¥„∞Ì ¿÷¥¬ ≈¨∑°Ω∫
/// </summary>
public class AudioData
{
    public string Name;                         // ∞Ó ¿Ã∏ß
    public float BPM;                           // ∞Ó BPM
    public float Length;                        // ∞Ó ±Ê¿Ã
    public float Sync;                          // ∞Ó ΩÃ≈©
    public SortedList<int, NoteType> Notes;       // ≥Î∆Æ ª˝º∫ Ω√∞£

    public AudioData()
    {
        Name = "no title";
        BPM = 60f;
        Length = 0f;
        Sync = 0f;
        Notes = new SortedList<int, NoteType>();
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
        Length = data.Length;
        Sync = data.Sync;
        Notes = data.Notes;
    }
}