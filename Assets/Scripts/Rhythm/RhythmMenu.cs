using UnityEngine;

/// <summary>
/// 리듬게임 플레이 도중 메뉴판 활성화 클래스
/// </summary>
public class RhythmMenu : MonoBehaviour
{
    public GameObject Menu;         // 활성화 할 메뉴
    public AudioSource Sound;       // 배경음
    public float Delay;
    private BGSound bgSound;

    private void Start()
    {
        bgSound = Sound.GetComponent<BGSound>();
    }

    void Update()
    {
        // 재생 이전에는 메뉴x
        if (Sound.time <= 0f)
            return;

        // Esc 키로 활성화/비활성화
        if (Input.GetKeyDown(KeyCode.Escape) && !bgSound.IsReWind)
        {
            // 현재 메뉴판의 활성화 여부에 따른 스위칭
            Menu.SetActive(!Menu.activeSelf);

            // 메뉴판 활성화 시 음악 일시정지/ 비활성화 시 음악 재생
            if (Menu.activeSelf)
                Sound.Pause();
            else
                bgSound.RePlay(Delay);
        }
    }
}
