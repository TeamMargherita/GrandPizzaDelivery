using UnityEngine;
using UnityEngine.UI;

public class ShowKey : MonoBehaviour
{
    [SerializeField] private int MyLine;
    private Text text;
    // Start is called before the first frame update
    void Start()
    {
        text = GetComponent<Text>();
        char k1 = ' ';
        char k2 = ' ';
        if (MyLine == 0)
        {
            k1 = (char)RhythmManager.Instance.ClearKeys[0];
            k2 = (char)RhythmManager.Instance.ClearKeys[1];
        }
        else if (MyLine == 1)
        {
            k1 = (char)RhythmManager.Instance.ClearKeys[2];
            k2 = (char)RhythmManager.Instance.ClearKeys[3];
        }
        text.text = "배정된 키 : [ " + k1.ToString().ToUpper() + " ] " + " [ " + k2.ToString().ToUpper() + " ]";

    }

    // Update is called once per frame
    void Update()
    {

    }
}
