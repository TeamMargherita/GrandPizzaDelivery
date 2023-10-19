using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using StoreNS;
// 한석호 작성
public class StoreManager : MonoBehaviour, IInitStore
{
    [SerializeField] private GameObject[] itemPanelObjArr;
    [SerializeField] private Text[] itemSelectCountArr;
    [SerializeField] private GameObject storePanel;
    private List<int> selectItemCnt = new List<int>();  // 선택한 아이템 개수
    private Dictionary<ItemS,int> selectItemDic = new Dictionary<ItemS,int>(); // 선택한 아이템 종류와 개수
    private ItemPanelArr[] itemPanelArr;
   
    private Store nowStore; // 현재 들어간 가게 

    private int nowPage = 0;
    private bool isSelectItemList;   // 장바구니 클릭 여부

    // Start is called before the first frame update
    void Awake()
    {
        itemPanelArr = new ItemPanelArr[itemPanelObjArr.Length];
        
        for (int i = 0; i < itemPanelObjArr.Length; i++)
		{
            itemPanelArr[i] = itemPanelObjArr[i].GetComponent<ItemPanelArr>();
        }
        
    }
    /// <summary>
    /// 상품 둘러보기를 끝낸다.
    /// </summary>
    public void CancelStore()
	{
        InitSelectItemCnt();

        nowStore.CloseStore.CloseStore(-999, null);
        // 가게 끄기
        storePanel.SetActive(false);
    }
    /// <summary>
    /// 구입할 상품 가격 합을 구하고 다시 대화로 돌아간다.
    /// </summary>
    public void ResultStore()
	{
        int sum = 0;
        for (int i = 0; i <nowStore.StoreItemList.Count; i++)
		{
            sum += nowStore.StoreItemList[i].Cost * selectItemCnt[i];
		}
        // 총 가격을 상점주인에게 다시 보내기
        nowStore.CloseStore.CloseStore(sum, selectItemDic);
        //InitSelectItemCnt();
        // 가게 끄기
        storePanel.SetActive(false);

    }
    private void InitPage()
	{
        nowPage = 0;
	}
    /// <summary>
    /// 선택한 아이템을 전부 다 취소하고, 페이지를 0페이지로 되돌린다.
    /// </summary>
    public void InitSelectItemCnt()
    {
        InitPage();

        selectItemCnt.Clear();
        selectItemDic.Clear();
        if (nowStore != null)
        {
            for (int i = 0; i < nowStore.StoreItemList.Count; i++)
            {
                selectItemCnt.Add(0);
            }
        }
        for (int i = 0; i < itemPanelObjArr.Length; i++)
        {
            itemSelectCountArr[i].text = 0.ToString();
        }
        InitItemPanel();
    }
    /// <summary>
    /// 각 아이템 패널 초기화
    /// </summary>
    private void InitItemPanel()
	{
        for (int i = 0; i < itemPanelArr.Length; i++)
        {
            if (!isSelectItemList)
            {
                if (nowPage * itemPanelArr.Length + i < nowStore.StoreItemList.Count)
                {
                    itemPanelArr[i].Item = nowStore.StoreItemList[nowPage * itemPanelArr.Length + i].Item;
                    itemPanelArr[i].ItemCost = nowStore.StoreItemList[nowPage * itemPanelArr.Length + i].Cost;
                    itemPanelArr[i].SetItemName(nowStore.StoreItemList[nowPage * itemPanelArr.Length + i].Item.Name);
                    itemSelectCountArr[i].text = selectItemCnt[nowPage * itemPanelArr.Length + i].ToString();
                }
                else
                {
                    itemPanelArr[i].Item = null;
                    itemPanelArr[i].ItemCost = -999;
                    itemPanelArr[i].SetItemName("");
                    itemSelectCountArr[i].text = "0";
                }
            }
            else
			{
                if (nowPage * itemPanelArr.Length + i < selectItemDic.Count && selectItemDic.CheckIndexDic(nowPage * itemPanelArr.Length + i))
				{
                    itemPanelArr[i].Item = selectItemDic.FindKeyForIndex(nowPage * itemPanelArr.Length + i).Value;
                    int n = nowStore.StoreItemList.FindIndex(a => a.Item.Equals(selectItemDic.FindKeyForIndex(nowPage * itemPanelArr.Length + i).Value));
                    if (n != -1)
                    {
                        itemPanelArr[i].ItemCost = nowStore.StoreItemList[n].Cost;
                        itemSelectCountArr[i].text = selectItemCnt[n].ToString();
                    }
                    itemPanelArr[i].SetItemName(selectItemDic.FindKeyForIndex(nowPage * itemPanelArr.Length + i).Value.Name);
                }
                else
				{
                    itemPanelArr[i].Item = null;
                    itemPanelArr[i].ItemCost = -999;
                    itemPanelArr[i].SetItemName("");
                    itemSelectCountArr[i].text = "0";
                }
            }
        }
	}
    /// <summary>
    /// 구매할 아이템의 수량 하나 추가
    /// </summary>
    /// <param name="index"></param>
    public void PlusCount(int index)
    {

        if ((nowPage * itemPanelArr.Length + index < nowStore.StoreItemList.Count && !isSelectItemList))
        {
            int cnt = Constant.PlayerItemDIc.ContainsKey(itemPanelArr[index].Item.Value) ? Constant.PlayerItemDIc[itemPanelArr[index].Item.Value] : 0; 

            if (itemPanelArr[index].Item.Value.MaxCnt - cnt > selectItemCnt[nowPage * itemPanelArr.Length + index])
            {
                if (selectItemCnt[nowPage * itemPanelArr.Length + index] == 0)
                {
                    selectItemDic.Add(itemPanelArr[index].Item.Value, 1);
                }
                else
                {
                    selectItemDic[itemPanelArr[index].Item.Value]++;
                }

                selectItemCnt[nowPage * itemPanelArr.Length + index]++;
                itemSelectCountArr[index].text = selectItemCnt[nowPage * itemPanelArr.Length + index].ToString();
            }
        }
        else if (nowPage * itemPanelArr.Length + index < selectItemDic.Count && isSelectItemList)
		{
            int n = nowStore.StoreItemList.FindIndex(a => a.Item.Equals(selectItemDic.FindKeyForIndex(nowPage * itemPanelArr.Length + index).Value));

            int cnt = Constant.PlayerItemDIc.ContainsKey(nowStore.StoreItemList[n].Item) ? Constant.PlayerItemDIc[nowStore.StoreItemList[n].Item] : 0;
            if (selectItemCnt[n] - cnt < itemPanelArr[index].Item.Value.MaxCnt)
			{
                if (selectItemCnt[n] == 0)
				{
                    selectItemDic.Add(itemPanelArr[index].Item.Value, 1);
				}
                else
				{
                    selectItemDic[itemPanelArr[index].Item.Value]++;
				}

                selectItemCnt[n]++;
                itemSelectCountArr[index].text = selectItemCnt[n].ToString();
			}
        }

    }
    /// <summary>
    /// 구매하지 않을 아이템의 수량 하나 감소
    /// </summary>
    /// <param name="index"></param>
    public void MinusCount(int index)
	{
        if ((nowPage * itemPanelArr.Length + index < nowStore.StoreItemList.Count && !isSelectItemList))
		{
            if (selectItemCnt[nowPage * itemPanelArr.Length + index] > 0)
			{
                selectItemCnt[nowPage * itemPanelArr.Length + index]--;
                itemSelectCountArr[index].text = selectItemCnt[nowPage * itemPanelArr.Length + index].ToString();

                if (selectItemCnt[nowPage * itemPanelArr.Length + index] == 0)
				{
                    selectItemDic.Remove(itemPanelArr[index].Item.Value);
                    InitItemPanel();
				}
                else
				{
                    selectItemDic[itemPanelArr[index].Item.Value]--;
				}
			}
		}
        else if (nowPage * itemPanelArr.Length + index < selectItemDic.Count && isSelectItemList)
		{
            int n = nowStore.StoreItemList.FindIndex(a => a.Item.Equals(selectItemDic.FindKeyForIndex(nowPage * itemPanelArr.Length + index).Value));
            if (selectItemCnt[n] > 0)
			{
                selectItemCnt[n]--;
                itemSelectCountArr[index].text = selectItemCnt[n].ToString();

                if (selectItemCnt[n] == 0)
				{
                    selectItemDic.Remove(itemPanelArr[index].Item.Value);
                    InitItemPanel();
				}
				else 
                {
                    selectItemDic[itemPanelArr[index].Item.Value]--;
                }
			}
        }

    }
    public void NextPage()
    {
        if (!isSelectItemList)
        {
            if (nowPage < nowStore.StoreItemList.Count / itemPanelArr.Length)
            {
                nowPage++;
                InitItemPanel();
            }
        }
        else
		{
            if (nowPage < selectItemDic.Count / itemPanelArr.Length)
			{
                nowPage++;
                InitItemPanel();
			}
		}
    }
    public void BackPage()
	{
        if (nowPage > 0)
		{
            nowPage--;
            InitItemPanel();
		}
	}
    public void TrueIsSelectItemList()
	{
        isSelectItemList = true;
        InitPage();
        InitItemPanel();
	}
    public void FalseIsSelectItemList()
	{
        isSelectItemList = false;
        InitPage();
        InitItemPanel();
	}
    public void InitStore(Store store)
    {
        nowStore = store;

        InitSelectItemCnt();
    }
    public void OpenStore()
	{
        storePanel.SetActive(true);
	}
}
