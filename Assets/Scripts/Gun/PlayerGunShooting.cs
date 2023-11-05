using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Gun;
public class PlayerGunShooting : MonoBehaviour, GunShooting
{
    private bool ShootingStance = false;
    private Vector3 dir;
    private Transform MyTransform;
    int layerMask;
    public GameObject BloodEffect;
    public GameObject WallHitEffect;
    public GameObject Hand;

    public PlayerGunShooting(Transform myTransform, string exception)
    {
        MyTransform = myTransform;
        layerMask = ((1 << LayerMask.NameToLayer(exception)) | (1 << LayerMask.NameToLayer("WallObstacle"))
            | (1 << LayerMask.NameToLayer("CrossWalk")) | (1 << LayerMask.NameToLayer("Default")));// Everything에서 Player, WallObstacle, CrossWalk 레이어만 제외하고 충돌 체크함
        layerMask = ~layerMask;
    }

    public Vector3 GetTargetPos()
    {
       Vector3 point = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x,
                Input.mousePosition.y, -Camera.main.transform.position.z));
        return point;
    }
    
    public void aiming()
    {
        if (Input.GetKey(KeyCode.Mouse1))
        {
            ShootingStance = true;
            Hand.SetActive(true);
            dir = GetTargetPos() - MyTransform.position;
            Hand.transform.up = dir.normalized;
        }
        else if (Input.GetKeyUp(KeyCode.Mouse1))
        {
            ShootingStance = false;
            Hand.SetActive(false);
        }
    }
    public float time = 100;
    
    public bool ShootRaycast(float fireRate, short damage)
    {
        time += Time.deltaTime;
        if(fireRate < time)
        {
            if (ShootingStance)
            {
                if (Input.GetKey(KeyCode.Mouse0))
                {
                    RaycastHit2D hit = Physics2D.Raycast(MyTransform.position, dir.normalized, 1000, layerMask);
                    if (hit.transform.CompareTag("Police") || hit.transform.CompareTag("ChaserPoliceCar"))
                    {
                        GameObject blood = Instantiate(BloodEffect, hit.point, MyTransform.rotation);
                        Destroy(blood, 0.3f);
                        hit.transform.GetComponent<Police>().PoliceHp -= damage;
                    }
                    else if(hit.transform.CompareTag("House"))
                    {
                        GameObject wallhit = Instantiate(WallHitEffect, hit.point, MyTransform.rotation);
                        Destroy(wallhit, 0.3f);
                    }
                    time = 0;
                    return true;
                }
            }
        }
        return false;
    }

    public bool Fire(float fireRate, short damage)
    {
        aiming();
        return ShootRaycast(fireRate, damage);
    }
}
