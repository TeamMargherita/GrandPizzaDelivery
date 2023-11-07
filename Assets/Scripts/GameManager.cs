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
    public List<Pizza> PizzaMenu = new List<Pizza>() { new Pizza("CheesePizza", 60, 5000, 10000, 800, new List<Ingredient>() { Ingredient.CHEESE }, 250, 100, 0) };
    public List<Pizza> PineapplePizzaMenu = new List<Pizza>();
    public List<Slot> InventorySlotList = new List<Slot>();

    public bool isDarkDelivery = false;

    public TestMoneyText MoneyText;
    private CameraMove cameraMove;

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
        PineapplePizzaMenu.Add(Constant.PineapplePizza);

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
            if(MoneyText != null )
                MoneyText.CreateMoneyEffect(value - money);
            money = value;
        }
    }

    

    public void PlayerDead()
    {
        StartCoroutine(DeadWait());
    }
    IEnumerator DeadWait()
    {
        cameraMove = GameObject.Find("Main Camera").GetComponent<CameraMove>();
        cameraMove.playerDead = true;
        Time.timeScale = 0.1f;
        Time.fixedDeltaTime = Time.timeScale * 0.02f;
        yield return new WaitForSecondsRealtime(3f);
        Time.timeScale = 1;
        Time.fixedDeltaTime = Time.timeScale * 0.02f;
        LoadScene.Instance.LoadNextDay(true);
        HospitalRespawn();
        isDarkDelivery = false;
        time = 32400;
    }
    public void NextDay()
    {
        LoadScene.Instance.LoadNextDay(false);
        isDarkDelivery = false;
        time = 32400;
    }

    public void HospitalRespawn()
    {
        //석호에게 주는 선물
        Constant.IsHospital = true;
    }
    private void TimeSkip()
    {
        if(Input.GetKeyDown(KeyCode.F3))
            time = 82800;
    }
    private void Update()
    {
        //TimeSkip();
        if (!Constant.StopTime)
        {
            time += Time.deltaTime * timeSpeed; //게임기준1분 = 현실시간1초
        }
        /*if (Input.GetKeyDown(KeyCode.F1))
        {
            time = 14400;
        }
        if (Input.GetKeyDown(KeyCode.F2))
		{
            time = 32400;
		}*/
        /*if (Input.GetKeyDown(KeyCode.CapsLock))
        {
            if(Time.timeScale == 1)
            {
                Time.timeScale = 0.1f;
                Time.fixedDeltaTime = Time.timeScale * 0.02f;
            }else if (Time.timeScale == 0.1f)
            {
                Time.timeScale = 1;
                Time.fixedDeltaTime = Time.timeScale * 0.02f;
            }
        }*/
        //게임1초 * timeSpeed = 현실시간1초
        //TimeText.GetComponent<Text>().text = (int)time/3600 + " : " + (int)(time / 60 % 60);
    }
}
