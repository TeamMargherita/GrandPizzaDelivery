using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Gun;
public class PlayerMove : PlayerStat
{
    private Vector3 angle = new Vector3(0, 0, 300);

    private float time;
    private float reloadTime;
    public bool Stop = false;
    private bool bananaTrigger = false;
    public short CurrentMagagine;
    [SerializeField]
    private MakingPizza MakingPizzaScript;
    [SerializeField]
    private InventoryManager InventoryManagerScript;
    [SerializeField]
    private GameObject PlayerSmoke;
    GunShooting gunMethod;
    private void Awake()
    {
        gunMethod = new PlayerGunShooting(transform, "Player");
    }

    void PlayerFire()
    {
        if(CurrentMagagine > 0)
        {
            if(Constant.nowGun[0] != -1)
            {
                if (gunMethod.Fire(1 - Constant.GunInfo[Constant.nowGun[0]].Speed, Constant.GunInfo[Constant.nowGun[0]].Damage))
                {
                    CurrentMagagine -= 1;
                }
            }
        }
        else
        {
            if (Constant.nowGun[0] != -1)
            {
                reloadTime += Time.deltaTime;
                if (Constant.GunInfo[Constant.nowGun[0]].ReloadSpeed <= reloadTime)
                {
                    reloadTime = 0;
                    CurrentMagagine = Constant.GunInfo[Constant.nowGun[0]].Magazine;
                }
            }
        }
    }
    void Update()
    {
        PlayerFire();
        InventoryManagerScript.UIMagagineTextUpdate(CurrentMagagine);
        if (Input.GetKeyDown(KeyCode.X))
        {
            if(MakingPizzaScript.CompletePizzaList.Count > 0)
            {
                foreach (var i in GameManager.Instance.PizzaInventoryData)
                {
                    if (i == null)
                    {
                        InventoryManagerScript.InventoryAddItem(MakingPizzaScript.GetInvenPizzaList(0));
                        break;
                    }
                }
            }
            InventoryManagerScript.inventoryTextUpdate("PizzaInventory");
        }
        if (!Stop && !bananaTrigger)
        {
            time += Time.deltaTime;
            if (Input.GetKey(KeyCode.W))
            {
                if (Speed < 0)
                {
                    Speed *= Braking;
                }
                CreateSmoke();
                Speed += acceleration * Time.deltaTime;
            }
            else if (Input.GetKey(KeyCode.S))
            {
                if (Speed > 0)
                {
                    Speed *= Braking;
                }
                Speed -= acceleration * Time.deltaTime;
            }
            else
            {
                if (time > 0.01f)
                {
                    Speed *= Braking;
                    time = 0;
                }
            }

            float angleRatio;
            if(Speed > 0)
            {
                angleRatio = Speed / (Speed + (MaxSpeed / 2));
            }
            else
            {
                angleRatio = -(Speed / (Speed + (-MaxSpeed / 2)));
            }

            if (Input.GetKey(KeyCode.A))
            {
                transform.Rotate(angle * angleRatio * Time.deltaTime);
            }
            if (Input.GetKey(KeyCode.D))
            {
                transform.Rotate(-angle * angleRatio * Time.deltaTime);
            }
            GetComponent<Rigidbody2D>().velocity = transform.rotation * new Vector2(0, Speed);
        }
        else if(Stop && !bananaTrigger)
        {
            Speed = 0;
            GetComponent<Rigidbody2D>().velocity = transform.rotation * new Vector2(0, Speed);
        }else if(Stop && bananaTrigger)
        {
            StopCoroutine(bananaCoroutine);
            bananaTrigger = false;
        }
    }
    IEnumerator bananaCoroutine;
    IEnumerator banana(float time, Transform t)
    {
        bananaTrigger = true;
        float count = 0;
        if(Speed >= 0)
            GetComponent<Rigidbody2D>().velocity = t.rotation * new Vector2(0, 10);
        else
            GetComponent<Rigidbody2D>().velocity = t.rotation * new Vector2(0, -10);
        while (true)
        {
            count += Time.deltaTime;
            transform.Rotate(angle * 10 * Time.deltaTime);
            if (count > time)
            {
                bananaTrigger = false;
                break;
            }
            yield return null;
        }
    }
    float smokeTime;
    void CreateSmoke()
    {
        if (Input.GetKeyUp(KeyCode.W))
        {
            smokeTime = 1;
        }
        smokeTime += Time.deltaTime;
        if(smokeTime > 0.2f)
        {
            
            Instantiate(PlayerSmoke, transform.position + (transform.up * -0.5f), transform.rotation);
            smokeTime = 0;
        }
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.transform.CompareTag("Banana"))
        {
            if (bananaCoroutine != null)
                StopCoroutine(bananaCoroutine);
            bananaCoroutine = banana(2, transform);
            StartCoroutine(bananaCoroutine);
            Destroy(other.gameObject);
        }
    }
    Vector2 Power;
    float TestPower;
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.transform.CompareTag("Police"))
        {
            Power = GetComponent<Rigidbody2D>().velocity - (Vector2)(collision.transform.right * collision.transform.GetComponent<PoliceCar>().Speed);
            TestPower = Power.magnitude;
            HP -= (int)(TestPower * 1.5);
        }else if (collision.transform.CompareTag("ChaserPoliceCar"))
        {
            Power = GetComponent<Rigidbody2D>().velocity - (Vector2)(collision.transform.right * collision.transform.GetComponent<ChasePoliceCar>().Speed);
            TestPower = Power.magnitude;
            HP -= (int)(TestPower * 1.5);
        }
    }
}
