using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoliceInspecting : Conversation
{
    public PoliceInspecting()
    {
        inspectingPoliceTextStart = new string[23]
        {
            "잠깐 이쪽좀 볼까?",   // 0
            "거기 멈춰 주실까?",   // 1
            "이게 무슨 냄새야?",   // 2
            "무슨일인가요?",  // 3
            "무시한다.",    // 4
            "피자배달부 같은데...거기 짐좀 열어봐라.",  // 5
            "잠시 검문을 해야겠어.", // 6
            "피자 냄새가 나는거 같은데? 검문좀 해야겠어.",    // 7
            "한번 살펴보세요. 불법 음식같은 건 없어요.", // 8
            "(설득 주사위 7 이상) 설마 이상한 거라도 있을까봐요? 이봐요, 이 도시에 피자집은 한 곳 뿐이고, \n저희는 언제든지 주문 내역을 공개할 의사가 있어요. 그러니 불시에 이런 짓은 하지 말아주세요.",   // 9
            "(20000원을 준다.)흠...그만 가봐도 되겠습니까?",   // 10
            "이런 파인애플 피자잖아 ! 이런 불법음식을 소지하고 있다니...이건 압수야. \n벌금은 그쪽 가게에 통지할테니 다음부턴 이런 짓 하지마 !",  // 11
            "좋아. 가봐도 좋다.",  // 12
            "확실히 그 말이 맞군. 가봐도 좋다.", // 13
            "혀가 너무 길어. 잔말말고 그 짐이나 열어봐라.",   // 14
            "좋아. 이번엔 눈감아주마. 다음부턴 조심하도록",    // 15
            "음..이 일은 참 고달프단 말이지.",  // 16
            "(20000원을 준다.) 그러지 말고. 어떻게 안되겠습니까?",    // 17
            "더러운 녀석. 너한테 더 줄 돈은 없다.",   // 18
            "도망간다.", // 19
            "시간을 낭비했군. 당장 체포하겠어 !",  // 20
            "(간다.)", // 21
            "(검문을 받는다.)" // 22
        };
    }
}
