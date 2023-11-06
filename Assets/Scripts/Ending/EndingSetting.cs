using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// 한석호 작성
public class EndingSetting : MonoBehaviour
{
    [SerializeField] private Sprite[] spriteImg;
    [SerializeField] private Text endingTypeText;
    [SerializeField] private Text endingExplainText;
    [SerializeField] private Image endingImg;
    
    // Start is called before the first frame update
    void Awake()
    {
        if (GameManager.Instance.Money - Constant.Dept >= 10000000)
		{
            endingImg.sprite = spriteImg[2];
            endingTypeText.text = "(굿 엔딩) - 1억 이상 흑자";
            endingExplainText.text = "피자 가게로 큰 매출을 올리고 부자가 되었다 !";
		}
        else if (GameManager.Instance.Money >= Constant.Dept)
		{
            endingImg.sprite = spriteImg[0];
            endingTypeText.text = "(노멀 엔딩) - 아무튼 흑자";
            endingExplainText.text = "가까스로 빚을 청산하고 자유가 되었다 !";
		}
        else
		{
            endingImg.sprite = spriteImg[1];
            endingTypeText.text = "(배드 엔딩) - 적자";
            endingExplainText.text = "결국 빚을 갚지 못하고 나는 어디론가 끌려갔다...";
		}

    }

    public void EndingNext()
	{
        GameManager.Instance.time = 32400;
        LoadScene.Instance.ActiveTrueFade("InGameScene");
	}
}
