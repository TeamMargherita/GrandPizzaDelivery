using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// 타이머 UI 표시 클래스
/// </summary>
public class RhythmProgress : MonoBehaviour
{
    public Image pizza;      // 피자 이미지
    private RhythmManager manager;

    private void Start()
    {
        manager = RhythmManager.Instance;
    }

    void Update()
    {
        pizza.fillAmount = (float)manager.CurrentTime / manager.Data.Length;
    }
}
