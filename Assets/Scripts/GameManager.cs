using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private static GameManager _instance;

    public List<Pizza> PizzaMenu = new List<Pizza>();
    public List<Pizza> RequestList = new List<Pizza>();
    public void RandomCall()//랜덤피자주문 메서드
    {
        int i = Random.Range(0, PizzaMenu.Count);
        RequestList.Add(PizzaMenu[i]);
    }
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
        CheesePizza = new Pizza("cheesePizza", 60, 5000, 10000);
        PizzaMenu.Add(CheesePizza);

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

    public struct Pizza
    {
        public string Name;//피자이름
        public int Perfection;//완성도
        public int Production_Cost;//생산비용
        public int Sell_Cost;//판매비용

        public Pizza(string name, int perfection, int production_Cost, int Sell_Cost)
        {
            this.Name = name;
            this.Perfection = perfection;
            this.Production_Cost = production_Cost;
            this.Sell_Cost = Sell_Cost;
        }
    }
    public Pizza CheesePizza;
    private void Start()
    {
        RandomCall();//테스트용 피자주문
    }
    private void Update()
    {
        time += Time.deltaTime * timeSpeed; //게임기준1분 = 현실시간2초
        //게임1초 * timeSpeed = 현실시간1초
    }
}
