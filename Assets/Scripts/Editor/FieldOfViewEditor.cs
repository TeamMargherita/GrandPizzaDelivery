using System.Collections;
using System.Collections.Generic;
#if UNITY_EDITOR
using UnityEditor;
#endif
using UnityEngine;
// 한석호 작성

/// <summary>
/// FieldOfViewPoliceCar클래스에 관한 커스텀 에디터
/// </summary>
[CustomEditor (typeof (FieldOfViewPoliceCar))]
public class FieldOfViewEditor : Editor
{
	private void OnSceneGUI()
	{
		FieldOfViewPoliceCar fow = (FieldOfViewPoliceCar)target;
		// 흰색으로 그림
		Handles.color = Color.white;
		// 원을 그림. 기준은 첫번째 인자값, 중심 축은 forward. right 방향쪽으로 반지름이 커짐. 각도는 360도. 반지름 길이는 fow.ViewRadius
		Handles.DrawWireArc(fow.transform.localPosition, Vector3.forward, Vector3.right, 360, fow.ViewRadius);
		Vector3 viewAngleA = fow.DirFromAngle(-fow.ViewAngle / 2, false);
		Vector3 viewAngleB = fow.DirFromAngle(fow.ViewAngle / 2, false);

		Handles.DrawLine(fow.transform.localPosition, fow.transform.localPosition + viewAngleA * fow.ViewRadius);
		Handles.DrawLine(fow.transform.localPosition, fow.transform.localPosition + viewAngleB * fow.ViewRadius);

		Handles.color = Color.red;
		foreach (Transform visibleTarget in fow.visibleTargets)
		{
			Handles.DrawLine(fow.transform.position, visibleTarget.position);
		}
	}
}
