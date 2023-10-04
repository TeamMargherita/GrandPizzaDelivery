using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 한석호 작성
public class CheckObstacles : MonoBehaviour, ICheckCol
{
	private IUpdateCheckList iUpdateCheckList;

	private List<Collider2D> col2DList = new List<Collider2D>();	// 현재 콜라이더에 감지되고 있는 오브젝트 수

	private int checkNum;

	private void OnTriggerEnter2D(Collider2D collision)
	{
		if (collision.gameObject.layer != LayerMask.NameToLayer("WallObstacle") &&
			collision.gameObject.layer != LayerMask.NameToLayer("MoveObstacle"))
		{
			return;
		}

		//Debug.Log(collision.gameObject.layer);

		// 처음에 한해서만 콜라이더 감지 리스트에 추가
		if (col2DList.Count == 0)
		{
			iUpdateCheckList.UpdateCheck(checkNum, true);
		}
		col2DList.Add(collision);

	}
	private void OnTriggerExit2D(Collider2D collision)
	{
		if (collision.gameObject.layer != LayerMask.NameToLayer("WallObstacle") &&
			collision.gameObject.layer != LayerMask.NameToLayer("MoveObstacle"))
		{
			return;
		}

		col2DList.Remove(collision);
		// 모든 콜라이더가 빠져나간 경우에는 콜라이더 감지 리스트에서 제거
		if (col2DList.Count == 0)
		{
			iUpdateCheckList.UpdateCheck(checkNum, false);
		}
	}

	public void InitNumber(int num, IUpdateCheckList iUpdateCheckList)
	{
		checkNum = num;
		this.iUpdateCheckList = iUpdateCheckList;
	}
}
