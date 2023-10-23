using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BuildingAddressNS;
using ClerkNS;

// 한석호 작성

public class MakingPizza : MonoBehaviour
{
    [SerializeField] private GameObject[] makingPizzaPanelArr; // 만들고 있는, 혹은 만든 피자를 보여주기 위한 패널들
    [SerializeField] private GameObject uiControl;

    private List<Request> pizzaRequestList = new List<Request>();   // 만들어야할 피자 리스트

    private List<Pizza> completePizzaList = new List<Pizza>();  // 완성된 피자 리스트

    private RectTransform[] makingPizzaPanelRect;
    private MakingPizzaPanel[] makingPizzaPanelClass;
    private Coroutine makingPizzaCoroutine; // 피자를 만드는 코루틴

    private Vector3 initMakingPizzaPanelVec = new Vector3(-600, 400);

    private IAlarmMessagePanel iAlarmMessagePanel;
    private void Awake()
	{
        Constant.ClerkList.Add(new ClerkC(47, Tier.THREE, Tier.ONE, Tier.FOUR, 0, 20000));  // 임의로 점원 생성
        iAlarmMessagePanel = uiControl.GetComponent<IAlarmMessagePanel>();
        InitArr();
    }
	void Start()
    {
        makingPizzaCoroutine = StartCoroutine(Making());
    }
    /// <summary>
    /// 초기화
    /// </summary>
    private void InitArr()
	{
        makingPizzaPanelRect = new RectTransform[makingPizzaPanelArr.Length];
        makingPizzaPanelClass = new MakingPizzaPanel[makingPizzaPanelArr.Length];

        for (int i = 0; i < makingPizzaPanelRect.Length; i++)
        {
            makingPizzaPanelRect[i] = makingPizzaPanelArr[i].GetComponent<RectTransform>();
            makingPizzaPanelClass[i] = makingPizzaPanelArr[i].GetComponent<MakingPizzaPanel>();
        }
    }
    /// <summary>
    /// 주문을 접수한다.
    /// </summary>
    /// <param name="request"></param>
    public void AddRequestPizza(Request request)
	{
        Request re = new Request(request.Pizza, request.Accept);
        re.AddressS = request.AddressS;
        pizzaRequestList.Add(re);
	}
    /// <summary>
    /// 파인애플 피자 주문을 접수한다.
    /// </summary>
    /// <param name="request"></param>
    public void AddRequestPineapplePizza(Request request)
	{
        if (Constant.PineappleCount <= 0) { return; }
        pizzaRequestList.Add(request);
	}
    /// <summary>
    /// 자동으로 현재 만들 피자를 골라 만듭니다. 
    /// </summary>
    /// <returns></returns>
    private IEnumerator Making()
	{
        int makeTime = 0;
        float makingRate = 0;
        int panelIndex = -1;
        while (true)
        {
            // 만들 피자를 고릅니다.
            if (pizzaRequestList.Count <= 0) 
            {
                for (int i = 0; i < 5; i++)
                {
                    yield return Constant.OneTime;
                }
                continue; 
            }

            // 피자 만드는데 걸리는 시간을 계산한다.
            makeTime = 80;
            for (int i = 0; i < Constant.ClerkList.Count; i++)
            {
                makeTime -= (60 + (int)Constant.ClerkList[i].Agility);
            }
            // 돈이 빠져나간다.

            // 피자를 만들 준비합니다.
            // 비활성화된 패널을 찾습니다.
            for (int i = 0; i < makingPizzaPanelArr.Length; i++)
			{
                if (!makingPizzaPanelArr[i].activeSelf)
				{
                    panelIndex = i;
                    break;
				}
			}
            // 패널을 켜주고 시작 위치를 잡아준다.
            makingPizzaPanelArr[panelIndex].SetActive(true);
            makingPizzaPanelRect[panelIndex].localPosition = initMakingPizzaPanelVec;
            makingPizzaPanelClass[panelIndex].SetPizza(pizzaRequestList[0].Pizza);

            // 패널을 이동시킨다.
            while(makingPizzaPanelRect[panelIndex].localPosition.x < 0)
			{
                makingPizzaPanelRect[panelIndex].localPosition += new Vector3(5, 0);
                yield return Constant.OneTime;
			}

            // 만드는 중.
            for (int i = 0; i < makeTime * 10; i++)
            {
                // 얼만큼 만들어졌는지 퍼센트를 보여줍니다.
                makingRate = (100f / (makeTime * 10f)) * i;
                // 퍼센트 게이지 갱신.
                makingPizzaPanelClass[panelIndex].SetMainPanelRect(makingRate);
                // 일정 시간 대기
                for (int j = 0; j < 5; j++)
                {
                    yield return Constant.OneTime;
                }
            }
            // 피자가 완성되었다. 완성된 피자는 피자집 인벤에 들어간다.
            completePizzaList.Add(pizzaRequestList[0].Pizza);
            // 파인애플 피자였다면 파인애플이 하나 줄어든다.
            if (pizzaRequestList[0].Pizza.Ingreds.FindIndex(a => a.Equals(PizzaNS.Ingredient.PINEAPPLE)) != -1)
			{
                Constant.PineappleCount--;
			}

            // 알림이 뜬다.
            iAlarmMessagePanel.ControlAlarmMessageUI(true, $"{pizzaRequestList[0].Pizza.Name}가 완성되었습니다.");

            // 피자 패널들을 아래로 내린다.
            for (int i = 0; i < 7; i++)
			{
                for (int j = 0; j < makingPizzaPanelArr.Length; j++)
                {
                    if (makingPizzaPanelArr[j].activeSelf)
                    {
                        makingPizzaPanelRect[j].localPosition -= new Vector3(0, 20);
                    }
                }
                yield return Constant.OneTime;
			}
            
            // 완성된 피자는 리스트에서 제거
            pizzaRequestList.RemoveAt(0);


        }
	}
    /// <summary>
    /// 피자집에 있는 피자를 가져오는 함수. 
    /// </summary>
    /// <param name="index">가져올 피자리스트의 인덱스. 맞지 않는다면 빈 피자를 반환한다.</param>
    /// <returns></returns>
    public Pizza GetInvenPizzaList(int index)
	{
        // 가져올 수 없는 범위에 있다면 빈 피자를 반환함
        if (completePizzaList.Count <= index) { return new Pizza(); }

        // 피자를 임시로 저장한다.
        Pizza temPizza = completePizzaList[index];
        // 피자집에 있는 피자들 명단에서 제외한다.
        //Debug.Log(temPizza.Name);
        completePizzaList.RemoveAt(index);
        Debug.Log(index + " " + completePizzaList.Count);
        // 명단에서 제외했으므로, 피자집 피자 패널을 꺼준다.
        for (int i = 0; i < makingPizzaPanelArr.Length; i++)
		{
            if (makingPizzaPanelClass[i].ComparePizza(temPizza) && makingPizzaPanelClass[i].gameObject.activeSelf)
			{
                makingPizzaPanelClass[i].SetMainPanelRect(0f);
                makingPizzaPanelClass[i].gameObject.SetActive(false);
                break;
			}
		}
        // 피자 패널의 위치를 재조정한다.
        int ind = 0;
        for (int i = 0; i < makingPizzaPanelArr.Length; i++)
		{
            if (makingPizzaPanelArr[i].activeSelf && makingPizzaPanelArr[i].transform.GetChild(1).GetComponent<RectTransform>().rect.width == 0f)
			{
                makingPizzaPanelRect[i].localPosition = new Vector3(0, 260 - (140 * ind));
                ind++;
			}
		}

        // 임시로 저장해둔 피자를 전달한다.
        return temPizza;
	}

}
