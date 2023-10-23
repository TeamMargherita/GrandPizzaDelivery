using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// 타이머 UI 표시 클래스
/// </summary>
public class RhythmProgress : MonoBehaviour
{
    public Image Front;
    public Image Back;      // 피자 이미지
    private RhythmManager manager;

    private void Start()
    {
        manager = RhythmManager.Instance;
    }

    void Update()
    {
        if((float)manager.CurrentTime <= 0f)
        {
            Front.fillAmount = (2 + (float)manager.CurrentTime) / 2;
        }
        else
        {
            Back.fillAmount = (float)manager.CurrentTime / manager.Data.Length;
        }
    }
}
