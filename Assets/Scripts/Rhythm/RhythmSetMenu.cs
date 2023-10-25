using UnityEngine;

/// <summary>
/// SelectScene에서 메뉴 관리를 위한 클래스
/// </summary>
public class RhythmSetMenu : MonoBehaviour
{
    public GameObject Menu;
    public AudioSource BgSound;
    void Update()
    {
        BgSound.volume = RhythmManager.Instance.MusicSound;
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Menu.SetActive(!Menu.activeSelf);
        }
    }
}
