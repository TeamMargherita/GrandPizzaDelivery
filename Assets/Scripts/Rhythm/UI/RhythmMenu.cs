using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// ������� �÷��� ���� �޴��� Ȱ��ȭ Ŭ����
/// </summary>
public class RhythmMenu : MonoBehaviour
{
    public GameObject Menu;         // Ȱ��ȭ �� �޴�
    public AudioSource Sound;       // �����
    public float Delay;
    private BGSound bgSound;

    private void Start()
    {
        bgSound = Sound.GetComponent<BGSound>();
    }

    void Update()
    {
        // ��� �������� �޴�x
        if (Sound.time <= 0f)
            return;

        if (SceneManager.GetActiveScene().name == "SelectScene" && RhythmManager.Instance.IsSelectGuide)
            return;

        // F10 Ű�� Ȱ��ȭ/��Ȱ��ȭ
        if (Input.GetKeyDown(KeyCode.F10))
        {
            if (bgSound != null && bgSound.IsReWind)
                return;
            // ���� �޴����� Ȱ��ȭ ���ο� ���� ����Ī
            Menu.SetActive(!Menu.activeSelf);

            if (bgSound == null)
                return;

            // �޴��� Ȱ��ȭ �� ���� �Ͻ�����/ ��Ȱ��ȭ �� ���� ���
            if (Menu.activeSelf)
                Sound.Pause();
            else
                bgSound.RePlay(Delay);
        }
    }
    public void OnOffButton()
    {
        Menu.SetActive(!Menu.activeSelf);
    }
    public void CloseButton()
    {
        Menu.SetActive(false);
        if (bgSound != null)
            bgSound.RePlay(Delay);
    }
}