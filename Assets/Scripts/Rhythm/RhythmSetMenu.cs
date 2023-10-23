using UnityEngine;

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
