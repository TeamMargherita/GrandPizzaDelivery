using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FineMessage : MonoBehaviour
{
    [SerializeField] private UnityEngine.UI.Text text;

    private static Color minusColor = new Color(0, 0, 0, 0.1f / 255f);

    private UnityEngine.UI.Image img;
    public void Awake()
    {
        img = this.GetComponent<UnityEngine.UI.Image>();
    }
    public void OnEnable()
    {
        img.color = new Color(0, 0, 0, 1f);
        text.color = new Color(1, 1, 1, 1);
    }
    /// <summary>
    /// 텍스트 바꾸기
    /// </summary>
    /// <param name="t"></param>
    public void SetText(string t)
    {
        text.text = t;
    }
    void Update()
    {
        img.color -= minusColor;
        text.color -= minusColor;
        if (img.color.a <= 0) { this.gameObject.SetActive(false); }
    }
}
