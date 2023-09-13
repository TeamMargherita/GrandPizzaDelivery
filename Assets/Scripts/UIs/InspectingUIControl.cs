using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// 한석호 작성

public class InspectingUIControl : MonoBehaviour
{
    [SerializeField] private Text policeText;

    private string[] inspectingPoliceTextStart = new string[3] {
        "잠깐 이쪽좀 볼까?",
        "잠시 불심검문이 있겠습니다. 그러니 두손 들어라.",
        "피자 냄새가 나는 거 같은데 너.",
    };

    private void OnEnable()
    {
        policeText.text = "";
    }
}
