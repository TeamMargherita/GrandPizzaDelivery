using UnityEngine;

/// <summary>
/// SelectScene���� �޴� ������ ���� Ŭ����
/// </summary>
public class RhythmSetMenu : MonoBehaviour
{
    public GameObject Menu;
    public AudioSource BgSound;
    void Update()
    {
        BgSound.volume = RhythmManager.Instance.MusicSound;
        if (Input.GetKeyDown(KeyCode.F10))
        {
            Menu.SetActive(!Menu.activeSelf);
        }
    }
}
