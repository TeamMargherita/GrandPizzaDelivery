using UnityEngine;
using UnityEngine.UI;

public class Selector : MonoBehaviour
{
    public string[] Titles;
    public AudioSource Sound;
    public AudioClip[] Clips;
    public Text Title;
    public Text Bpm;
    public Text Level;
    public RectTransform LPDisk;

    private float startAngle = 0f;
    private float endAngle = 0f;
    private float angle;
    private bool isChange = false;
    private static int index = 0;
    private AudioData audioData;
    private float timer = 0f;
    void Start()
    {

        ChangeSong(index);
    }

    void Update()
    {
        if (isChange)
        {
            if (timer < 1f)
            {
                timer += Time.deltaTime * 2f;
                angle = Mathf.LerpAngle(startAngle, endAngle, timer);
                LPDisk.eulerAngles = new Vector3(0, 0, angle);
            }
            else
            {
                timer = 0f;
                isChange = false;
                LPDisk.eulerAngles = new Vector3(0, 0, 0);
                ChangeSong(index);
            }
        }
        else
        {
            if (Input.GetKeyDown(KeyCode.UpArrow))
            {
                index = (index + 1) >= Titles.Length ? 0 : index + 1;
                endAngle = 90f;
                isChange = true;
            }
            if (Input.GetKeyDown(KeyCode.DownArrow))
            {
                index = (index - 1) < 0 ? Titles.Length - 1 : index - 1;
                endAngle = -90f;
                isChange = true;
            }
            if (Input.GetKeyDown(KeyCode.Return))
            {
                RhythmManager.Instance.Title = Titles[index];
                RhythmManager.Instance.AudioClip = Clips[index];
                LoadScene.Instance.ActiveTrueFade("RhythmScene");
            }
        }
    }

    private void ChangeSong(int index)
    {
        audioData = new AudioData(Titles[index]);
        Title.text = audioData.Name;
        Bpm.text = audioData.BPM.ToString() + "bpm";
        Level.text = audioData.Level;
        Sound.clip = Clips[index];
        Sound.Play();
    }
}
