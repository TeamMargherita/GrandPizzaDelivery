using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Inventory;
using PizzaNS;
public class GameManager : MonoBehaviour
{
    private static GameManager _instance;
    public Pizza?[] PizzaInventoryData = new Pizza?[5];
    public List<Pizza> PizzaMenu = new List<Pizza>();
    public List<Slot> InventorySlotList = new List<Slot>();

    [SerializeField] private SendDeliveryRequest SDR;
    [SerializeField] private GameObject DarkDeliveryOKPanel;

    public static GameManager Instance
    {
        get
        {
            if (!_instance)
            {
                _instance = FindObjectOfType(typeof(GameManager)) as GameManager;

                if (_instance == null)
                    Debug.Log("no Singleton obj");
            }
            return _instance;
        }
    }
    private void Awake()
    {
        
        for (int i = 0; i < 5; i++)
        {
            List<Ingredient> ing = new List<Ingredient>();
            ing.Add(Ingredient.CHEESE);
            GameManager.Instance.PizzaMenu.Add(new Pizza("CheesePizza5", 60, 5000, 10000, Random.Range(0, 500) + 500, ing, Random.Range(0, 100) + 200));
        }
        if (_instance == null)
        {
            _instance = this;
        }else if(_instance != this)
        {
            Destroy(gameObject);
        }

        DontDestroyOnLoad(gameObject);
        
    }

    public float time;
    private float timeSpeed = 60; //하루기준시간

    private int money = 0;
    public int Money
    {
        get {
            return money;
        }
        set {
            //나중에 일정금액도달하면 앤딩 화면가는 함수 짜기
            money = value;
        }
    }

    private void EndDelivery()
    {
        if(SDR != null && DarkDeliveryOKPanel != null)
        {
            if ((SDR.RequestList.Count <= 0 && time >= 75600) || time >= 82800)
            {
                DarkDeliveryOKPanel.SetActive(true);
                Time.timeScale = 0;
            }
        }
    }

    private void Update()
    {
        if (!Constant.StopTime)
        {
            time += Time.deltaTime * timeSpeed; //게임기준1분 = 현실시간2초
        }
        //게임1초 * timeSpeed = 현실시간1초
        //TimeText.GetComponent<Text>().text = (int)time/3600 + " : " + (int)(time / 60 % 60);
        EndDelivery();
    }
}
