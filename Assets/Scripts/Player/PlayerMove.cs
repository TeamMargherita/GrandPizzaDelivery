using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Gun;
public class PlayerMove : PlayerStat
{
    private Vector3 angle = new Vector3(0, 0, 300);

    private float time;
    public bool Stop = false;
    private bool bananaTrigger = false;
    [SerializeField]
    private MakingPizza MakingPizzaScript;
    [SerializeField]
    private InventoryManager InventoryManagerScript;

    GunShooting gunMethod;
    private void Awake()
    {
        gunMethod = new PlayerGunShooting(transform);
    }
    void Update()
    {
        gunMethod.Fire("Player");
        if (Input.GetKeyDown(KeyCode.X))
        {
            if(MakingPizzaScript.CompletePizzaList.Count > 0)
                InventoryManagerScript.InventoryAddItem(MakingPizzaScript.GetInvenPizzaList(0));
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
                this.transform.Rotate(angle * angleRatio * Time.deltaTime);
            }
            if (Input.GetKey(KeyCode.D))
            {
                this.transform.Rotate(-angle * angleRatio * Time.deltaTime);
            }
            this.GetComponent<Rigidbody2D>().velocity = transform.rotation * new Vector2(0, Speed);
        }
        else if(Stop && !bananaTrigger)
        {
            Speed = 0;
            this.GetComponent<Rigidbody2D>().velocity = transform.rotation * new Vector2(0, Speed);
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
            this.GetComponent<Rigidbody2D>().velocity = t.rotation * new Vector2(0, 10);
        else
            this.GetComponent<Rigidbody2D>().velocity = t.rotation * new Vector2(0, -10);
        while (true)
        {
            count += Time.deltaTime;
            this.transform.Rotate(angle * 10 * Time.deltaTime);
            if (count > time)
            {
                bananaTrigger = false;
                break;
            }
            yield return null;
        }
    }

    IEnumerator HPBarUpdate()
    {

        yield return null;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.transform.CompareTag("Banana"))
        {
            if (bananaCoroutine != null)
                StopCoroutine(bananaCoroutine);
            bananaCoroutine = banana(2, this.transform);
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
