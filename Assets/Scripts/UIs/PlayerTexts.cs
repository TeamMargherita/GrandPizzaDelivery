using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

// 한석호 작성

public class PlayerTexts : MonoBehaviour, IPointerClickHandler
{

    public int TextNum { get; set; }

    private IInspectingUIText iInspectingUIText;

    private Text playerText;
    

    void Awake()
    {
        playerText = this.gameObject.GetComponent<Text>();
    }

    public void SetIInspectingUIText(IInspectingUIText iInspectingUIText)
	{
        this.iInspectingUIText = iInspectingUIText;
	}

    public void OnPointerClick(PointerEventData eventData)
    {
        if (TextNum / 1000 == 1)
        {
            iInspectingUIText.ChoiceText(TextNum);
        }


    }
}
