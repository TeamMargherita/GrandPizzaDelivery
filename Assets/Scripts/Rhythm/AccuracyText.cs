using UnityEngine;
using UnityEngine.UI;

public class AccuracyText : MonoBehaviour
{
    private Text accuracy;
    private RhythmManager manager;

    private void Start()
    {
        manager = RhythmManager.Instance;
        accuracy = GetComponent<Text>();
    }

    void Update()
    {
        accuracy.text = manager.Judges.Accuracy.ToString("00.0") + "%";
    }
}
