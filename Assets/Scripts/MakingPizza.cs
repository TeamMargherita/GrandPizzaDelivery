using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BuildingAddressNS;
using ClerkNS;

// 한석호 작성

public class MakingPizza : MonoBehaviour, IResetPizzaMaking
{
    [SerializeField] private GameObject[] makingPizzaPanelArr; // 만들고 있는, 혹은 만든 피자를 보여주기 위한 패널들
    [SerializeField] private GameObject uiControl;

    public static List<Request> pizzaRequestList = new List<Request>();   // 만들어야할 피자 리스트

    public static List<Pizza> CompletePizzaList = new List<Pizza>();  // 완성된 피자 리스트
    public static int MakeTimeIndex = 0;
    public static bool IsSaveIndex = false;
    public static int nowDate = 0;

    private RectTransform[] makingPizzaPanelRect;
    private MakingPizzaPanel[] makingPizzaPanelClass;
    private Coroutine makingPizzaCoroutine; // 피자를 만드는 코루틴

    private Vector3 initMakingPizzaPanelVec = new Vector3(-600, 400);

    private IAlarmMessagePanel iAlarmMessagePanel;
    private void Awake()
	{
        //Constant.ClerkList.Add(new ClerkC(47, Tier.THREE, Tier.ONE, Tier.FOUR, 0, 20000));  // 임의로 점원 생성
        iAlarmMessagePanel = uiControl.GetComponent<IAlarmMessagePanel>();
        IsSaveIndex = false;
        InitArr();
    }
	void Start()
    {
        if (nowDate != Constant.NowDate)
		{
            pizzaRequestList.Clear();
            CompletePizzaList.Clear();
            nowDate = Constant.NowDate;
            MakeTimeIndex = 0;
            IsSaveIndex = false;
		}

        for (int j = 0; j < CompletePizzaList.Count; j++)
        {
            makingPizzaPanelArr[j].SetActive(true);

            makingPizzaPanelRect[j].localPosition -= new Vector3(0, 140 * (j + 1));
            makingPizzaPanelClass[j].SetMainPanelRect(100f);
            makingPizzaPanelClass[j].SetPizza(CompletePizzaList[j]);

        }


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
            makingPizzaPanelClass[i].MakingPizza = this;
            makingPizzaPanelClass[i].PizzaIndex = i;
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
        if (Constant.PineAppleCount <= 0) { return; }
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
        bool isIng = true ;
        while (true)
        {
            isIng = true;
            // 만들 피자를 고릅니다.
            if ((pizzaRequestList.Count <= 0 || CompletePizzaList.Count >= 5) || (GameManager.Instance.isDarkDelivery) && Constant.PineAppleCount == 0) 
            {
                //Debug.Log(CompletePizzaList.Count);
                for (int i = 0; i < 5; i++)
                {
                    yield return Constant.OneTime;
                }
                continue; 
            }

            Constant.PizzaIngMoney += pizzaRequestList[0].Pizza.ProductionCost;
            // 피자 만드는데 걸리는 시간을 계산한다.
            makeTime = 82;
            for (int i = 0; i < EmployeeStressCon.WorkingDay[(int)Constant.NowDay].Count; i++)
            {
                //makeTime -= (60 + (int)Constant.ClerkList[i].Agility);
                makeTime -= (10 + (int)EmployeeStressCon.WorkingDay[(int)Constant.NowDay][i].Agility);
                Debug.Log(makeTime);
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

            int k = (MakeTimeIndex < makeTime * 10 ? MakeTimeIndex : makeTime * 10 - 5);
            // 만드는 중.
            for (int i = k; i < makeTime * 10; i++)
            {
                // 얼만큼 만들어졌는지 퍼센트를 보여줍니다.
                makingRate = (100f / (makeTime * 10f)) * i;
                // 퍼센트 게이지 갱신.
                makingPizzaPanelClass[panelIndex].SetMainPanelRect(makingRate);
                // 일정 시간 대기
                for (int j = 0; j < 5; j++)
                {
                    if (IsSaveIndex)
					{
                        MakeTimeIndex = i + 600;
					}
                    yield return Constant.OneTime;
                }
                while (Constant.StopTime)
                {
                    yield return Constant.OneTime;
                    yield return Constant.OneTime;
                }
            }
            MakeTimeIndex = MakeTimeIndex >= makeTime * 10 ? MakeTimeIndex - makeTime * 10 : MakeTimeIndex;

            if (pizzaRequestList.Count <= 1)
			{
                MakeTimeIndex = 0;
			}
            // 피자가 완성되었다. 완성된 피자는 피자집 인벤에 들어간다.
            pizzaRequestList[0].Pizza.ProductTime = GameManager.Instance.time;
            CompletePizzaList.Add(pizzaRequestList[0].Pizza);
            // 파인애플 피자였다면 파인애플이 하나 줄어든다.
            if (pizzaRequestList[0].Pizza.Ingreds.FindIndex(a => a.Equals(PizzaNS.Ingredient.PINEAPPLE)) != -1)
			{
                Constant.PineAppleCount--;
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
            makingPizzaPanelClass[panelIndex].isComplete = true;
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
        int k = 0;
        for (int i = 0; i < GameManager.Instance.PizzaInventoryData.Length; i++)
        {
            if (GameManager.Instance.PizzaInventoryData[i] != null)
            {
                k++;
            }
        }

        Debug.Log( $"{k} CompletePizzaList.Count : {CompletePizzaList.Count}");
        
        if (k > CompletePizzaList.Count) { return new Pizza(); }

        // 피자를 임시로 저장한다.
        int nn = -1;
        Pizza temPizza = new Pizza();
        for (int i = 0; i < CompletePizzaList.Count; i++)
        {
            if (makingPizzaPanelClass[index].ComparePizza(CompletePizzaList[i]))
            {
                temPizza = CompletePizzaList[i];
                nn = i;
                break;
            }
        }// 피자집에 있는 피자들 명단에서 제외한다.
        //Debug.Log(temPizza.Name);
        CompletePizzaList.RemoveAt(nn);
        //Debug.Log(index + " " + CompletePizzaList.Count);
        // 명단에서 제외했으므로, 피자집 피자 패널을 꺼준다.
        for (int i = 0; i < makingPizzaPanelArr.Length; i++)
		{
            if (makingPizzaPanelClass[i].ComparePizza(temPizza) && makingPizzaPanelClass[i].gameObject.activeSelf && makingPizzaPanelClass[i].GetMainPanelRect() == 0f)
			{
                makingPizzaPanelClass[i].SetMainPanelRect(0f);
                makingPizzaPanelClass[i].gameObject.SetActive(false);
                makingPizzaPanelClass[i].isComplete = false;
                makingPizzaPanelClass[i].PizzaIndex = i;
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

    public void ResetPizzaMaking()
    {
        StopCoroutine(makingPizzaCoroutine);
        for (int i = 0; i < makingPizzaPanelArr.Length; i++)
        {
            makingPizzaPanelRect[i].localPosition = initMakingPizzaPanelVec;
            makingPizzaPanelClass[i].SetMainPanelRect(0f);
            makingPizzaPanelArr[i].SetActive(false);
        }
        CompletePizzaList.Clear();
        pizzaRequestList.Clear();
        makingPizzaCoroutine = StartCoroutine(Making());
    }
}
