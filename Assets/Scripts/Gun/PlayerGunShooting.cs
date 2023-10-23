using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Gun;
public class PlayerGunShooting : GunShooting
{
    private bool ShootingStance = false;
    private Vector3 dir;
    private Transform MyTransform;

    public PlayerGunShooting(Transform myTransform)
    {
        MyTransform = myTransform;
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
            Debug.Log(ShootingStance);
            dir = GetTargetPos() - MyTransform.position;
        }
        else if (Input.GetKeyUp(KeyCode.Mouse1))
        {
            ShootingStance = false;
        }
    }
    float time;
    public bool ShootRaycast(string exception, float fireRate, short damage)
    {
        time += Time.deltaTime;
        if(fireRate < time)
        {
            if (ShootingStance)
            {
                if (Input.GetKey(KeyCode.Mouse0))
                {
                    int layerMask = ((1 << LayerMask.NameToLayer(exception)) | (1 << LayerMask.NameToLayer("WallObstacle"))); ;  // Everything에서 Player, WallObstacle 레이어만 제외하고 충돌 체크함
                    layerMask = ~layerMask;
                    RaycastHit2D hit = Physics2D.Raycast(MyTransform.position, dir.normalized, 1000, layerMask);
                    if (hit.transform.CompareTag("Police"))
                    {
                        hit.transform.GetComponent<Police>().PoliceHp -= damage;
                    }
                    time = 0;
                    Debug.Log("발사");
                    return true;
                    //Debug.Log(hit.transform.name);
                }
            }
        }
        return false;
    }

    public bool Fire(string exception, float fireRate, short damage)
    {
        aiming();
        return ShootRaycast(exception, fireRate, damage);
    }
}
