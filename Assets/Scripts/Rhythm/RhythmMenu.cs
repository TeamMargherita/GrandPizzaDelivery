using UnityEngine;

public class RhythmMenu : MonoBehaviour
{
    public GameObject Menu;
    public AudioSource BgSound;
    void Update()
    {
        if (RhythmManager.Instance.CurrentTime <= 0m)
            return;

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Menu.SetActive(!Menu.activeSelf);
            if (Menu.activeSelf)
                BgSound.Pause();
            else
                BgSound.Play();
        }
    }
}
