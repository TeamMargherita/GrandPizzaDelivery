using UnityEngine;
using UnityEngine.UI;

public class JudgeCount : MonoBehaviour
{
    public Text Attractive;
    public Text Accuracy;
    public Text Perfect;
    public Text Great;
    public Text Good;
    public Text Miss;

    private RhythmManager manager;

    private void Start()
    {
        manager = RhythmManager.Instance;
    }

    void Update()
    {
        Attractive.text = "Attractive : " + manager.Judges.Attractive.ToString();
        Accuracy.text = "Accuracy : " + manager.Judges.Accuracy.ToString("00.0") + "%";
        Perfect.text = "Perfect : " + manager.Judges.Perfect.ToString();
        Great.text = "Great : " + manager.Judges.Great.ToString();
        Good.text = "Good : " + manager.Judges.Good.ToString();
        Miss.text = "Miss : " + manager.Judges.Miss.ToString();
    }
}
