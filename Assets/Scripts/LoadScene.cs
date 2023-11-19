using DayNS;
using UnityEngine;
using UnityEngine.SceneManagement;

// �Ѽ�ȣ �ۼ�
public class LoadScene : MonoBehaviour
{
    /* // �̱��� //
* instance��� ������ static���� ������ �Ͽ� �ٸ� ������Ʈ ���� ��ũ��Ʈ������ instance�� �ҷ��� �� �ְ� �մϴ� 
*/
    public static LoadScene Instance = null;

    private bool oneTimeMethod = false;

    private void Awake()
    {
        if (Instance == null) //instance�� null. ��, �ý��ۻ� �����ϰ� ���� ������
        {
            Instance = this; //���ڽ��� instance�� �־��ݴϴ�.
            DontDestroyOnLoad(gameObject); //OnLoad(���� �ε� �Ǿ�����) �ڽ��� �ı����� �ʰ� ����
        }
        else
        {
            if (Instance != this) //instance�� ���� �ƴ϶�� �̹� instance�� �ϳ� �����ϰ� �ִٴ� �ǹ�
            {
                Destroy(this.gameObject); //�� �̻� �����ϸ� �ȵǴ� ��ü�̴� ��� AWake�� �ڽ��� ����
            }
        }
    }
    /// <summary>
    /// ���̵� ������ �����ְ� ���� �ҷ��ɴϴ�.
    /// </summary>
    /// <param name="str">�ҷ��� ���� �̸��Դϴ�.</param>
    public void ActiveTrueFade(string str)
    {
        UIControl.isIn = false;
        Fade.Instance.gameObject.SetActive(true);
        Fade.Instance.SetLoadSceneName(str);
    }
    public void LoadNextDay(bool isDead)
    {
        if (oneTimeMethod) { return; }
        oneTimeMethod = true;
        if (Constant.NowDay != DayEnum.SUNDAY)
        {
            Constant.NowDay++;
        }
        else
        {
            Constant.NowDay = DayEnum.MONDAY;
        }
        Constant.NowDate++;
        if (isDead) { Constant.IsDead = true; }

        ActiveTrueFade("CalculateScene");
    }
    public void LoadS(string str)
    {
        oneTimeMethod = false;
        SceneManager.LoadScene(str);
    }
    public void LoadRhythm()
    {
        if (Constant.ChoiceIngredientList.Count > 0)
        {
            ActiveTrueFade("SelectScene");
        }
    }
    public void LoadPrologueToInGameScene()
    {
        Constant.isStartGame = true;
        DataManager.LoadData();
        ActiveTrueFade("InGameScene");
    }

    public void LoadPizzaMenu()
    {
        Constant.IsMakePizza = true;
        Constant.DevelopPizza.Add(new Pizza("�� ����" + System.DateTime.Now.ToString("MM-dd-HH-mm-ss"), Constant.Perfection, Constant.ProductionCost
            , Random.Range(2000 * Constant.ingreds.Count, 4000 * Constant.ingreds.Count + 1) + 10000, Constant.PizzaAttractiveness, Constant.ingreds, Constant.TotalDeclineAt, 100, 0));
        ActiveTrueFade("InGameScene");
    }
}
