using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 한석호 작성
public static class Constant
{
	/// <summary>
	/// 피자 재료값. [,0]은 재료번호, [,1]은 매력도, [,2]는 매력하락도, [,3]은 재료값. [0,]은 재료없음임.
	/// </summary>
	public static string[,] IngredientsArray = new string[10, 4]
	{
		{"0","-1","-1","-1" },
		{"1","25","3","150" },
		{"2","30","2","160" },
		{"3","15","2","80" },
		{"4","20","1","120" },
		{"5","45","7","500" },
		{"6","27","3","140" },
		{"7","40","5","320" },
		{"8","65","12","960" },
		{"9","78","20","1350" }
	};
}
