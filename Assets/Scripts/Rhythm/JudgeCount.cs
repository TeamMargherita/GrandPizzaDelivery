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

    void Update()
    {
        Attractive.text = "Attractive : " + RhythmManager.Instance.Attractive.ToString();
        Accuracy.text = "Accuracy : " + RhythmManager.Instance.Accuracy.ToString("00.0") + "%";
        Perfect.text = "Perfect : " + RhythmManager.Instance.Perfect.ToString();
        Great.text = "Great : " + RhythmManager.Instance.Great.ToString();
        Good.text = "Good : " + RhythmManager.Instance.Good.ToString();
        Miss.text = "Miss : " + RhythmManager.Instance.Miss.ToString();
    }
}
