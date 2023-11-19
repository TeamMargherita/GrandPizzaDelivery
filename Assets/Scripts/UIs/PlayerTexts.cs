using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

// 한석호 작성

public class PlayerTexts : MonoBehaviour, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] private Image panelImg;

    private static Color oneC = new Color(231 / 255f, 102 / 255f, 102 / 255f, 100 / 255f);
    private static Color twoC = new Color(255 / 255f, 255 / 255f, 255 / 255f, 100 / 255f);
    public int TextNum { get; set; }

    private IInspectingUIText iInspectingUIText;

    public void OnEnable()
    {
        panelImg.color = twoC;
    }
    public void SetIInspectingUIText(IInspectingUIText iInspectingUIText)
    {
        this.iInspectingUIText = iInspectingUIText;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        Debug.Log(TextNum);
        panelImg.color = twoC;
        iInspectingUIText.ChoiceText(TextNum);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        panelImg.color = oneC;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        panelImg.color = twoC;
    }
}
