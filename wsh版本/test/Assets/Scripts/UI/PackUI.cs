using UnityEngine;
using System.Collections;
using UICore;
using UnityEngine.UI;

public class PackUI : BaseUI
{
    private Button btn_Level;
    private Button btn_Return;
    //物品的预制体
    private GameObject itemPrefab;
    //物品的父节点
    private Transform content;
    //被选中的物品的相关UI显示
    private Text txt_Name;
    private Text txt_Price;
    private Text txt_Introduce;
    private Image img_Item;
    private Text txt_BeChooseCount;
    //五个分类按钮
    private Button btn_All;
    private Button btn_Equipment;
    private Button btn_Potions;
    private Button btn_Rune;
    private Button btn_Material;
    //用于显示被选中的物品的信息面板
    private GameObject inforBg;

    //以下是出售相关
    private InputField inf_Count;
    private Button btn_Add;
    private Button btn_Minus;
    private Button btn_Sure;
    private Button btn_Cancel;
    private Button btn_Sell;
    private GameObject sellPanel;

    //记录当前被选中的物品的ID
    private int beChooseId = 0;
    //滚动条
    private Scrollbar verticalSb;
    protected override void InitUiOnAwake()
    {
        base.InitUiOnAwake();
        txt_Name = GameTool.GetTheChildComponent<Text>(this.gameObject, "Txt_Name");
        txt_Price = GameTool.GetTheChildComponent<Text>(this.gameObject, "Txt_Price");
        txt_Introduce = GameTool.GetTheChildComponent<Text>(this.gameObject, "Txt_Introduce");
        img_Item = GameTool.GetTheChildComponent<Image>(this.gameObject, "Img_Item");
        txt_BeChooseCount = GameTool.GetTheChildComponent<Text>(this.gameObject, "Txt_BeChooseCount");
        //查找五个分类按钮
        btn_All = GameTool.GetTheChildComponent<Button>(this.gameObject, "Btn_All");
        btn_Equipment = GameTool.GetTheChildComponent<Button>(this.gameObject, "Btn_Equipment");
        btn_Potions = GameTool.GetTheChildComponent<Button>(this.gameObject, "Btn_Potions");
        btn_Rune = GameTool.GetTheChildComponent<Button>(this.gameObject, "Btn_Rune");
        btn_Material = GameTool.GetTheChildComponent<Button>(this.gameObject, "Btn_Material");
        //五个分类按钮添加监听
        btn_All.onClick.AddListener(ShowAllItem);
        btn_Equipment.onClick.AddListener(ShowEquipmentItem);
        btn_Potions.onClick.AddListener(ShowPotionsItem);
        btn_Rune.onClick.AddListener(ShowRuneItem);
        btn_Material.onClick.AddListener(ShowMaterialItem);

        inforBg = GameTool.FindTheChild(this.gameObject, "InforBg").gameObject;

        btn_Level = GameTool.GetTheChildComponent<Button>(this.gameObject, "Btn_Level");
        btn_Return = GameTool.GetTheChildComponent<Button>(this.gameObject, "Btn_Return");
        btn_Level.onClick.AddListener(GotoLevelPanel);
        btn_Return.onClick.AddListener(ReturnUI);
        itemPrefab = Resources.Load<GameObject>("ItemPrefab/Item");
        content = GameTool.FindTheChild(this.gameObject, "Content");
        MessageCenter.AddMessageListener(E_MessageType.ItemBeChoose, ShowItemInfor);

        inf_Count = GameTool.GetTheChildComponent<InputField>(this.gameObject, "Inf_Count");
        // inf_ItemId = GameTool.GetTheChildComponent<InputField>(this.gameObject, "Inf_ItemId");
        btn_Add = GameTool.GetTheChildComponent<Button>(this.gameObject, "Btn_Add");
        btn_Minus = GameTool.GetTheChildComponent<Button>(this.gameObject, "Btn_Minus");
        btn_Sure = GameTool.GetTheChildComponent<Button>(this.gameObject, "Btn_Sure");
        btn_Cancel = GameTool.GetTheChildComponent<Button>(this.gameObject, "Btn_Cancel");
        btn_Sell = GameTool.GetTheChildComponent<Button>(this.gameObject, "Btn_Sell");
        sellPanel = GameTool.FindTheChild(this.gameObject, "SellBg").gameObject;
        btn_Add.onClick.AddListener(Add);
        btn_Minus.onClick.AddListener(Minus);
        btn_Sell.onClick.AddListener(ShowSellPanel);
        btn_Cancel.onClick.AddListener(HideSellPanel);
        btn_Sure.onClick.AddListener(SureSell);

        verticalSb = GameTool.GetTheChildComponent<Scrollbar>(this.gameObject, "Scrollbar Vertical");
    }
    private void ShowSellPanel()
    {
        //根据被选中物体的id,得到数量
        int count = PackData.Instance.ReadCountById(beChooseId);
        if (count > 0)
        {
            sellPanel.SetActive(true);
            inf_Count.text = "1";
        }
  
    }
    private void HideSellPanel()
    {
        sellPanel.SetActive(false);
    }
    private void SureSell()
    {
        //出售的数量
        int sellCount = int.Parse(inf_Count.text);
        //计算出售后所得到的金币
        int coinCount = sellCount * int.Parse(DataController.Instance.ReadCfg("Price", beChooseId, DataController.Instance.dicItem));
        MessageCenter.SendMessage(E_MessageType.ItemBeSell, coinCount);

        int itemCount = PackData.Instance.ReadCountById(beChooseId);
        int newCount = itemCount - sellCount;
        //更新内存
        PackData.Instance.EditorItemCount(beChooseId, newCount);
        txt_BeChooseCount.text = newCount.ToString();
        for (int i = 0; i < content.childCount; i++)
        {
            if (content.GetChild(i).GetComponent<ItemEntity>().itemId==beChooseId)
            {
                content.GetChild(i).transform.Find("Txt_Count").GetComponent<Text>().text= newCount.ToString();
                HideSellPanel();
                break;
            }
        }
    }
    private void Add()
    {
        int infCurrnetCount =int.Parse(inf_Count.text);
        int sellCount = infCurrnetCount + 1;
        if (PackData.Instance.ReadCountById(beChooseId) >= sellCount)
        {
            inf_Count.text = sellCount.ToString();
          
        }
        else
        {  
            //限定出售物品的数量为该物品的拥有总个数
            inf_Count.text = PackData.Instance.ReadCountById(beChooseId).ToString();
            sellCount = PackData.Instance.ReadCountById(beChooseId);
        }
       
    }
    private void Minus()
    {
        int infCurrnetCount = int.Parse(inf_Count.text);
        int sellCount = infCurrnetCount -1;
        if (sellCount >= 1)
        {
            inf_Count.text = sellCount.ToString();
        }
        else
        {
            inf_Count.text = "1";
            sellCount = 1;
        }
    }
    protected override void InitDataOnAwake()
    {
        base.InitDataOnAwake();
        this.uiId = E_UiId.PackUI;

    }
    protected override void InitOnStart()
    {
        base.InitOnStart();
        ShowItem();
    }
    protected override void OnEnable()
    {
        base.OnEnable();
        inforBg.SetActive(false);
        ShowAllItem();
        verticalSb.value = 1;
    }
    protected override void OnDisable()
    {
        base.OnDisable();
        sellPanel.SetActive(false);
    }
    //显示所有的物品
    private void ShowAllItem()
    {
        for (int i = 0; i < content.childCount; i++)
        {
            if (!content.GetChild(i).gameObject.activeInHierarchy)
            {
                content.GetChild(i).gameObject.SetActive(true);
            }
        }
    }
    //显示装备
    private void ShowEquipmentItem()
    {
        ShowItemByType(E_GoodsType.Equipment);
    }
    //显示药水
    private void ShowPotionsItem()
    {
        ShowItemByType(E_GoodsType.Potions);
    }
    //显示符文
    private void ShowRuneItem()
    {
        ShowItemByType(E_GoodsType.Rune);
    }
    //显示材料
    private void ShowMaterialItem()
    {
        ShowItemByType(E_GoodsType.Material);
    }
    //根据物品类型来显示物品
    private void ShowItemByType(E_GoodsType type)
    {
        for (int i = 0; i < content.childCount; i++)
        {
            ItemEntity itemEntity = content.GetChild(i).GetComponent<ItemEntity>();
            if (itemEntity.goodsType != type)
            {
                content.GetChild(i).gameObject.SetActive(false);
            }
            else
            {
                content.GetChild(i).gameObject.SetActive(true);
            }
        }
    }
    private void ShowItemInfor(object obj)
    {
        if (sellPanel.activeInHierarchy)
        {
            sellPanel.SetActive(false);
        }
        inforBg.SetActive(true);

        ItemEntity itemEntity = (ItemEntity)obj;
        // int itemId = itemEntity.itemId;
        beChooseId = itemEntity.itemId;
        txt_BeChooseCount.text = PackData.Instance.ReadCountById(beChooseId).ToString();
        //Debug.Log("被选中的物品的ID为"+obj);
        //根据ID读取物品名称
        string name = DataController.Instance.ReadCfg("Name", beChooseId, DataController.Instance.dicItem);
        txt_Name.text = name;
        //根据ID读取物品价格
        string price = DataController.Instance.ReadCfg("Price", beChooseId, DataController.Instance.dicItem);
        txt_Price.text = price;
        //读取物品的介绍
        string introduce = DataController.Instance.ReadCfg("Introduce", beChooseId, DataController.Instance.dicItem);
        txt_Introduce.text = introduce;
        //string iconName = DataController.Instance.ReadCfg("IconName", itemId, DataController.Instance.dicItem);
        //GameTool.GetTheChildComponent<Image>(this.gameObject, "Img_Item").sprite = Resources.Load<Sprite>("PackSprite/"+ iconName);
        img_Item.sprite = itemEntity.itemSprite;
    }
    private void ShowItem()
    {
        //获取物品的数量
        int itemCount = DataController.Instance.GetCfgCount(DataController.Instance.dicItem);
        //Debug.Log(itemCount);
        for (int i = 0; i < itemCount; i++)
        {
            GameObject item = Instantiate(itemPrefab);
            ItemEntity itemEntity = item.AddComponent<ItemEntity>();
            itemEntity.itemId = i + 1;
            //先获取物品对应的图片名称
            string iconName = DataController.Instance.ReadCfg("IconName", i + 1, DataController.Instance.dicItem);
            item.GetComponent<Image>().sprite = Resources.Load<Sprite>("PackSprite/" + iconName);
            //把物品的图片缓存在物品实体类里面
            itemEntity.itemSprite = item.GetComponent<Image>().sprite;
            //存储物品的类型
            int typeIndex = int.Parse(DataController.Instance.ReadCfg("Type", i + 1, DataController.Instance.dicItem));
            itemEntity.goodsType = (E_GoodsType)typeIndex;
            //给物品的数量赋值
            GameTool.GetTheChildComponent<Text>(item, "Txt_Count").text = PackData.Instance.ReadCountById(i + 1).ToString();
            GameTool.AddChildToParent(content, item.transform);
        }
    }

    private void GotoLevelPanel()
    {
        // GameObject.Find("UIManager").GetComponent<UIManager>().ShowUI(E_UiId.LevelUI);
        UIManager.Instance.ShowUI(E_UiId.LevelUI);
    }
    private void ReturnUI()
    {
        // GameObject.Find("UIManager").GetComponent<UIManager>().ReturnBeforeUI(beforeUiId);
        UIManager.Instance.ReturnBeforeUI(beforeUiId);
    }
}
