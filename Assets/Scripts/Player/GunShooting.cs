using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunShooting : MonoBehaviour
{
    public bool ShootingStance;
    public Vector3 dir;
    void Start()
    {
        ShootingStance = false;
    }
    private Vector3 MousePos()
    {
       Vector3 point = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x,
                Input.mousePosition.y, -Camera.main.transform.position.z));
        return point;
    }
    void Update()
    {
        
        if (Input.GetKey(KeyCode.Mouse1))
        {
            ShootingStance = true;
            Debug.Log(ShootingStance);
            dir = MousePos() - transform.position;
        }
        else if (Input.GetKeyUp(KeyCode.Mouse1))
        {
            ShootingStance = false;
        }

        if (ShootingStance)
        {
            if (Input.GetKey(KeyCode.Mouse0))
            {
                
                int layerMask = ((1 << LayerMask.NameToLayer("Player")) | (1 << LayerMask.NameToLayer("WallObstacle"))); ;  // Everything에서 Player, WallObstacle 레이어만 제외하고 충돌 체크함
                layerMask = ~layerMask;
                RaycastHit2D hit = Physics2D.Raycast(transform.position, dir.normalized, 1000, layerMask);
                //Debug.Log(hit.transform.name);
            } 
        }
    }
}
