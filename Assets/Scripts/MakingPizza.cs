using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// �Ѽ�ȣ �ۼ�

public class MakingPizza : MonoBehaviour, IResetPizzaMaking
{
    [SerializeField] private GameObject[] makingPizzaPanelArr; // ����� �ִ�, Ȥ�� ���� ���ڸ� �����ֱ� ���� �гε�
    [SerializeField] private GameObject uiControl;

    public static List<Request> pizzaRequestList = new List<Request>();   // �������� ���� ����Ʈ

    public static List<Pizza> CompletePizzaList = new List<Pizza>();  // �ϼ��� ���� ����Ʈ
    public static int MakeTimeIndex = 0;
    public static bool IsSaveIndex = false;
    public static int nowDate = 0;

    private RectTransform[] makingPizzaPanelRect;
    private MakingPizzaPanel[] makingPizzaPanelClass;
    private Coroutine makingPizzaCoroutine; // ���ڸ� ����� �ڷ�ƾ

    private Vector3 initMakingPizzaPanelVec = new Vector3(-600, 400);

    private IAlarmMessagePanel iAlarmMessagePanel;
    private void Awake()
    {
        //Constant.ClerkList.Add(new ClerkC(47, Tier.THREE, Tier.ONE, Tier.FOUR, 0, 20000));  // ���Ƿ� ���� ����
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
    /// �ʱ�ȭ
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
    /// �ֹ��� �����Ѵ�.
    /// </summary>
    /// <param name="request"></param>
    public void AddRequestPizza(Request request)
    {
        Request re = new Request(request.Pizza, request.Accept);
        re.AddressS = request.AddressS;
        pizzaRequestList.Add(re);
    }
    /// <summary>
    /// ���ξ��� ���� �ֹ��� �����Ѵ�.
    /// </summary>
    /// <param name="request"></param>
    public void AddRequestPineapplePizza(Request request)
    {
        if (Constant.PineAppleCount <= 0) { return; }
        pizzaRequestList.Add(request);
    }
    /// <summary>
    /// �ڵ����� ���� ���� ���ڸ� ��� ����ϴ�. 
    /// </summary>
    /// <returns></returns>
    private IEnumerator Making()
    {
        int makeTime = 0;
        float makingRate = 0;
        int panelIndex = -1;
        bool isIng = true;
        while (true)
        {
            isIng = true;
            // ���� ���ڸ� ���ϴ�.
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
            // ���� ����µ� �ɸ��� �ð��� ����Ѵ�.
            makeTime = 15;
            int han = 0;
            for (int i = 0; i < EmployeeStressCon.WorkingDay[(int)Constant.NowDay].Count; i++)
            {
                //makeTime -= (60 + (int)Constant.ClerkList[i].Agility);
                makeTime -= (3 + (int)EmployeeStressCon.WorkingDay[(int)Constant.NowDay][i].Agility);
                han += Random.Range((int)EmployeeStressCon.WorkingDay[(int)Constant.NowDay][i].Min, (int)EmployeeStressCon.WorkingDay[(int)Constant.NowDay][i].Max);
                //Debug.Log(makeTime);
            }
            makeTime = makeTime < 3 ? 3 : makeTime;
            han /= EmployeeStressCon.WorkingDay[(int)Constant.NowDay].Count;
            // ���� ����������.

            // ���ڸ� ���� �غ��մϴ�.
            // ��Ȱ��ȭ�� �г��� ã���ϴ�.
            for (int i = 0; i < makingPizzaPanelArr.Length; i++)
            {
                if (!makingPizzaPanelArr[i].activeSelf)
                {
                    panelIndex = i;
                    break;
                }
            }
            // �г��� ���ְ� ���� ��ġ�� ����ش�.
            makingPizzaPanelArr[panelIndex].SetActive(true);
            makingPizzaPanelRect[panelIndex].localPosition = initMakingPizzaPanelVec;
            makingPizzaPanelClass[panelIndex].SetPizza(pizzaRequestList[0].Pizza);

            // �г��� �̵���Ų��.
            while (makingPizzaPanelRect[panelIndex].localPosition.x < 0)
            {
                makingPizzaPanelRect[panelIndex].localPosition += new Vector3(5, 0);
                yield return Constant.OneTime;
            }

            int k = (MakeTimeIndex < makeTime * 10 ? MakeTimeIndex : makeTime * 10 - 5);
            // ����� ��.
            for (int i = k; i <= makeTime * 10; i++)
            {
                // ��ŭ ����������� �ۼ�Ʈ�� �����ݴϴ�.
                makingRate = (100f / (makeTime * 10f)) * i;
                // �ۼ�Ʈ ������ ����.
                makingPizzaPanelClass[panelIndex].SetMainPanelRect(makingRate);
                // ���� �ð� ���
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
            // ���ڰ� �ϼ��Ǿ���. �ϼ��� ���ڴ� ������ �κ��� ����.
            pizzaRequestList[0].Pizza.ProductTime = GameManager.Instance.time;


            CompletePizzaList.Add(
                new Pizza
                (pizzaRequestList[0].Pizza.Name,
                (int)(pizzaRequestList[0].Pizza.Perfection * han * 0.01),
                pizzaRequestList[0].Pizza.ProductionCost,
                pizzaRequestList[0].Pizza.SellCost,
                pizzaRequestList[0].Pizza.Charisma,
                pizzaRequestList[0].Pizza.Ingreds,
                pizzaRequestList[0].Pizza.TotalDeclineAt,
                pizzaRequestList[0].Pizza.Freshness,
                pizzaRequestList[0].Pizza.ProductTime)
                );
            // ���ξ��� ���ڿ��ٸ� ���ξ����� �ϳ� �پ���.
            if (pizzaRequestList[0].Pizza.Ingreds.FindIndex(a => a.Equals(PizzaNS.Ingredient.PINEAPPLE)) != -1)
            {
                Constant.PineAppleCount--;
            }

            // �˸��� ���.
            iAlarmMessagePanel.ControlAlarmMessageUI(true, $"{pizzaRequestList[0].Pizza.Name}�� �ϼ��Ǿ����ϴ�.");

            // ���� �гε��� �Ʒ��� ������.
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
            // �ϼ��� ���ڴ� ����Ʈ���� ����
            pizzaRequestList.RemoveAt(0);


        }
    }
    /// <summary>
    /// �������� �ִ� ���ڸ� �������� �Լ�. 
    /// </summary>
    /// <param name="index">������ ���ڸ���Ʈ�� �ε���. ���� �ʴ´ٸ� �� ���ڸ� ��ȯ�Ѵ�.</param>
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

        //Debug.Log( $"{k} CompletePizzaList.Count : {CompletePizzaList.Count}");

        if (k < 0) { return new Pizza(); }

        // ���ڸ� �ӽ÷� �����Ѵ�.
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
        }// �������� �ִ� ���ڵ� ��ܿ��� �����Ѵ�.
        //Debug.Log(temPizza.Name);
        CompletePizzaList.RemoveAt(nn);
        //Debug.Log(index + " " + CompletePizzaList.Count);
        // ��ܿ��� ���������Ƿ�, ������ ���� �г��� ���ش�.
        for (int i = 0; i < makingPizzaPanelArr.Length; i++)
        {
            if (makingPizzaPanelClass[i].ComparePizza(temPizza) && makingPizzaPanelClass[i].gameObject.activeSelf && makingPizzaPanelClass[i].GetMainPanelRect() == 0f)
            {
                makingPizzaPanelClass[i].SetMainPanelRect(0f);
                makingPizzaPanelClass[i].isComplete = false;
                makingPizzaPanelClass[i].PizzaIndex = i;
                makingPizzaPanelClass[i].gameObject.SetActive(false);
                break;
            }
        }
        // ���� �г��� ��ġ�� �������Ѵ�.
        int ind = 0;
        for (int i = 0; i < makingPizzaPanelArr.Length; i++)
        {
            if (makingPizzaPanelArr[i].activeSelf && makingPizzaPanelArr[i].transform.GetChild(1).GetComponent<RectTransform>().rect.width == 0f)
            {
                makingPizzaPanelRect[i].localPosition = new Vector3(0, 260 - (140 * ind));
                ind++;
            }
        }

        // �ӽ÷� �����ص� ���ڸ� �����Ѵ�.
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
