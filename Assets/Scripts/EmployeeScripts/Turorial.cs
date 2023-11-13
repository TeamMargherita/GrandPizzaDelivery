using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Turorial : MonoBehaviour
{
    string[] tutorialText = new string[19]
           { "안녕하세요! 점장님께 일이 생기신 후 정말 걱정했는데 이렇게 대신 와주셔서 다행이에요!",
            "피자 가게를 운영해 보신 적이 있으신가요?",
            "그러시군요... 그래도 문제 없어요! 제가 옆에서 도와드릴 수 있어요!",
        "믿음직스럽네요! 그럼 오늘부터 당분간 잘 부탁드려요~",
        "여기 오른쪽을 보시면 새로 오실 점장님의 편의를 고려하여 제가 여러가지 편의 기능을 마련했어요.", // 4
        "이건 점장님을 도와드릴 알바 분을 모집 할 수 있는 곳이에요.", // 5
        "알바는 다같이 피자를 만드는데, 알바의 능력이 좋을수록 피자의 퀄리티와 만드는 속도도 달라질거에요~",
        "손재주는 피자의 퀄리티에 직접적인 영향을 주고, 창의력은 피자의 퀄리티가 높아질 확률을 높여주며, 경력은 피자의 퀄리티가 낮아질 확률을 방어해줍니다.",
        "당연히 일급과 근무요일은 점장님 마음이에요! 저야 일급이나 근무 요일이 어떻든 괜찮지만, 새로 고용한 알바들은 스트레스를 받을 수 있답니다.",
        "이쪽은 메뉴를 개발할 수 있는 곳이에요.", //9
        "재료를 고르고, 박자에 맞춰 키를 누르면 레시피는 금방이랍니다!",
            "이쪽은 개발해둔 피자 메뉴를 등록하는 곳이에요.", //11
            "레시피의 이름도 직접 고르시고, 등록하고 싶은 메뉴를 추가하실 수 있죠. 물론 삭제도 가능하답니다.",
            "이쪽은 채무 목록이에요. 빛을... 남기고 가셨거든요. 잘 확인하고 갚아주세요.",//13
            "갚지 않으신다면... 끝이 좋지 않으실거에요! 그러니 주의해주세요.",
            "자아 이제 조금 넘어가서 이쪽에서는 피자를 굽는것을 확인 하실수 있으며, 또한 좌클릭으로 받아서 배달하실 수도 있답니다.", // 15
            "빨간색이 꽉 차면 전부 구워졌다는 신호입니다.",
        "제가 알려드릴 것은 여기까지에요.", // 17
        "그럼 무운을 빌어요, 점장님~"
           };

    string[] employeeTutoText = new string[7]
    {
    "고용 가능한 인원은 하루에 3명으로 한정되어있으며, 아침마다 새로운 인원이 들어옵니다.",//0
    "고용한 인원은 정보창을 눌러서 상세 정보를 볼 수 있습니다.",//1
    "일급은 숫자 옆의 화살표를 눌러 줄이거나, 늘릴 수 있습니다. 숫자가 빨간 색이면 적정 급여보다 적다는 뜻이고, 초록 색이면 적정 급여보다 많다는 뜻입니다.",
    "적정 급여보다 많으면 스트레스가 줄어들고 적정 급여보다 적으면 스트레스가 증가합니다.",
    "근무표를 통해 해당 알바의 근무 요일을 설정할 수 있습니다.",//4
    "요일이 적힌 버튼이 빨간색이면 버튼을 눌렀을 시 밑의 근무 인원 목록에서 삭제되며, 하얀색일 시 추가됩니다.",
    "만약 선호요일과 다른 날에 근무를 서게 된다면 알바의 스트레스가 쌓입니다.",
    };

    [SerializeField] GameObject Tuto;
    [SerializeField] GameObject EmployeeTutoPanel;
    [SerializeField] GameObject EmloyeePanel;

    bool isSkip = true;
    public static bool IsTuto = false;
    public static bool IsEmployeeTuto = false;

    int tutoProgressCount = 0;

    [SerializeField] Vector2[] TutoPos;

    private void Awake()
    {
        tutoProgressCount = 0;

        EmloyeePanel.SetActive(false);

        if (IsTuto == false)
        {
            Tuto.SetActive(true);

            TextBox();

            Tuto.transform.GetChild(1).gameObject.SetActive(true);

            Tuto.transform.GetChild(1).GetChild(2).gameObject.SetActive(false);

            Tuto.transform.GetChild(0).gameObject.SetActive(false);
        }
        else
        {
            Tuto.SetActive(false);
        }
    }

    public void TextBox()
    {
        // 4 5 9 11 13 15 17
        switch (tutoProgressCount)
        {
            case 1:
                Tuto.transform.GetChild(1).GetChild(1).gameObject.SetActive(true);

                Tuto.transform.GetChild(1).GetChild(2).gameObject.SetActive(true);

                Tuto.transform.GetChild(1).GetChild(1).gameObject.SetActive(false);
                break;
            case 4:
                Tuto.transform.GetChild(1).GetChild(1).gameObject.SetActive(true);  

                Tuto.transform.GetChild(0).gameObject.SetActive(true);
                break;
            case 5:
                Tuto.transform.GetChild(1).GetChild(1).gameObject.SetActive(true);

                Tuto.transform.GetChild(0).GetChild(0).gameObject.SetActive(false);

                Tuto.transform.GetChild(0).GetChild(1).gameObject.SetActive(false);
                Tuto.transform.GetChild(0).GetChild(2).gameObject.SetActive(false);
                Tuto.transform.GetChild(0).GetChild(3).gameObject.SetActive(false);
                Tuto.transform.GetChild(0).GetChild(4).gameObject.SetActive(false);

                Tuto.transform.GetChild(0).GetChild(0).gameObject.SetActive(true);
                break;
            case 9:
                Tuto.transform.GetChild(1).GetChild(1).gameObject.SetActive(true);
                Tuto.transform.GetChild(0).GetChild(1).gameObject.SetActive(true);

                Tuto.transform.GetChild(0).GetChild(0).gameObject.SetActive(false);
                break;
            case 11:
                Tuto.transform.GetChild(1).GetChild(1).gameObject.SetActive(true);
                Tuto.transform.GetChild(0).GetChild(2).gameObject.SetActive(true);

                Tuto.transform.GetChild(0).GetChild(1).gameObject.SetActive(false);
                break;
            case 13:
                Tuto.transform.GetChild(1).GetChild(1).gameObject.SetActive(true);

                Tuto.transform.GetChild(0).GetChild(2).gameObject.SetActive(false);
                Tuto.transform.GetChild(0).GetChild(3).gameObject.SetActive(true);
                break;
            case 15:
                Tuto.transform.GetChild(1).gameObject.SetActive(false);
                Tuto.transform.GetChild(0).GetChild(3).gameObject.SetActive(false);
                Tuto.transform.GetChild(0).GetChild(4).gameObject.SetActive(true);

                Tuto.transform.GetChild(2).gameObject.SetActive(true);
                break;
            case 17:
                Tuto.transform.GetChild(0).gameObject.SetActive(false);

                for (int i = 0; i < Tuto.transform.GetChild(0).childCount; i++)
                {
                    Tuto.transform.GetChild(0).GetChild(i).gameObject.SetActive(true);
                }
                break;
        }

        if(tutoProgressCount < 15)
        {
            Tuto.transform.GetChild(1).GetChild(0).GetComponent<Text>().text = tutorialText[tutoProgressCount];

            tutoProgressCount++;
        }
        else if(tutoProgressCount == tutorialText.Length)
        {
            tutoProgressCount = 0;

            IsTuto = true;
        }
        else if(tutoProgressCount >= 15 && tutoProgressCount != tutorialText.Length)
        {
            Tuto.transform.GetChild(2).GetChild(0).GetComponent<Text>().text = tutorialText[tutoProgressCount];

            tutoProgressCount++;
        }

        if (IsTuto == true)
        {
            Tuto.SetActive(false);
        }
    }

    public void YesOrNO(bool value)
    {
        isSkip = value;

        SkipText();
    }

    void SkipText()
    {
        if (isSkip)
        {
            tutoProgressCount = 2;

            Tuto.transform.GetChild(1).GetChild(2).gameObject.SetActive(false);
            Tuto.transform.GetChild(1).GetChild(1).gameObject.SetActive(true);

            Tuto.transform.GetChild(1).GetChild(0).GetComponent<Text>().text = tutorialText[tutoProgressCount];

            TextBox();

            IsTuto = false;
        }
        else
        {
            tutoProgressCount = 3;

            Tuto.transform.GetChild(1).GetChild(2).gameObject.SetActive(false);
            Tuto.transform.GetChild(1).GetChild(1).gameObject.SetActive(true);

            Tuto.transform.GetChild(1).GetChild(0).GetComponent<Text>().text = tutorialText[tutoProgressCount];

            TextBox();

            IsTuto = true;
        }

        tutoProgressCount = 4;
    }

    int EmployeeTutoCount = 0;
    int TextPosCount = 0;

    public void EmployeeTuto()
    {
        if (IsEmployeeTuto == false && IsTuto == true)
        {
            switch (EmployeeTutoCount)
            {
                case 0:
                    EmployeeTutoPanel.SetActive(true);

                    EmployeeTutoPanel.transform.GetChild(1).gameObject.SetActive(true);
                    EmployeeTutoPanel.transform.GetChild(1).GetChild(0).gameObject.SetActive(true);
                    EmployeeTutoPanel.transform.GetChild(1).GetChild(1).gameObject.SetActive(false);
                    EmployeeTutoPanel.transform.GetChild(1).GetChild(2).gameObject.SetActive(false);
                    break;
                case 1:
                    EmployeeTutoPanel.transform.GetChild(1).GetChild(0).gameObject.SetActive(false);
                    EmployeeTutoPanel.transform.GetChild(1).GetChild(1).gameObject.SetActive(true);
                    EmployeeTutoPanel.transform.GetChild(1).GetChild(2).gameObject.SetActive(false);
                    break;
                case 5:
                    EmployeeTutoPanel.transform.GetChild(1).GetChild(2).gameObject.SetActive(true);
                    break;
                case 6:
                    IsEmployeeTuto = true;
                    break;
            }

            if (EmployeeTutoCount == 0 || EmployeeTutoCount == 1 || EmployeeTutoCount == 4)
            {
                EmployeeTutoPanel.transform.GetChild(2).GetComponent<RectTransform>().anchoredPosition = TutoPos[TextPosCount];

                TextPosCount++;
            }

            EmployeeTutoPanel.transform.GetChild(2).GetChild(0).GetChild(0).GetComponent<Text>().text = employeeTutoText[EmployeeTutoCount];

            if (EmployeeTutoCount < employeeTutoText.Length - 1)
            {
                EmployeeTutoCount++;
            }
        }
        else if(IsEmployeeTuto == true)
        {
            EmployeeTutoPanel.SetActive(false);

            TextPosCount = 0;
            EmployeeTutoCount = 0;
        }
    }
}