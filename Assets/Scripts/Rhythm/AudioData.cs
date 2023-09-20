public class AudioData
{
    public string Name;         // ∞Ó ¿Ã∏ß
    public float BPM;         // ∞Ó BPM
    public float Length;      // ∞Ó ±Ê¿Ã
    public bool[] IsNote;     // ≥Î∆Æ ª˝º∫ Ω√∞£

    public AudioData()
    {
        Name = "no title";
        BPM = 60f;
        Length = 0f;
        IsNote = new bool[5000];
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
        IsNote = data.IsNote;
    }
    public AudioData(AudioData data)
    {
        Name = data.Name;
        BPM = data.BPM;
        Length = data.Length;
        IsNote = data.IsNote;
    }
}
