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

    public void ShootRaycast(string exception)
    {
        if (ShootingStance)
        {
            if (Input.GetKey(KeyCode.Mouse0))
            {
                int layerMask = ((1 << LayerMask.NameToLayer(exception)) | (1 << LayerMask.NameToLayer("WallObstacle"))); ;  // Everything에서 Player, WallObstacle 레이어만 제외하고 충돌 체크함
                layerMask = ~layerMask;
                RaycastHit2D hit = Physics2D.Raycast(MyTransform.position, dir.normalized, 1000, layerMask);
                Debug.Log(hit.transform.name);
            }
        }
    }

    public void Fire(string exception)
    {
        aiming();
        ShootRaycast(exception);
    }
}
