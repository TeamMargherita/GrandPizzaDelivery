using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// 진행도에 따른 피자판 UI 표시 클래스
/// </summary>
public class RhythmProgress : MonoBehaviour
{
    public Image Front;                 // 피자 전면부
    public Image Back;                  // 피자 후면부
    private RhythmManager manager;      // 리듬 매니저 캐싱

    private void Start()
    {
        // 리듬 매니저 캐싱
        manager = RhythmManager.Instance;
    }

    void Update()
    {
        // 현재 시간이 음수인 경우 전면부 피자 돌리기
        if((float)manager.CurrentTime <= 0f)
        {
            Front.fillAmount = (2 + (float)manager.CurrentTime) / 2;
        }

        // 현재 시간이 양수인 경우 후면부 피자 돌리기
        else
        {
            if(Front.fillAmount < 1f)
                Front.fillAmount = 1f;
            Back.fillAmount = (float)manager.CurrentTime / manager.Data.Length;
        }
    }
}
