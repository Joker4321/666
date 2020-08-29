using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UICore;

public class ItemEntity : MonoBehaviour {

    public int itemId = 0;
    public Sprite itemSprite;
    public E_GoodsType goodsType;
    void Start()
    {
        this.GetComponent<Button>().onClick.AddListener(ShowItemInfor);
    }
    private void ShowItemInfor()
    {
        //GameObject packPanel=   UIManager.Instance.GetUIPanel(E_UiId.PackUI);
        // //根据ID读取物品名称
        // string name = DataController.Instance.ReadCfg("Name",itemId,DataController.Instance.dicItem);
        // GameTool.GetTheChildComponent<Text>(packPanel, "Txt_Name").text = name;
        // //根据ID读取物品价格
        // string price = DataController.Instance.ReadCfg("Price", itemId, DataController.Instance.dicItem);
        // GameTool.GetTheChildComponent<Text>(packPanel, "Txt_Price").text = price;
        // //读取物品的介绍
        // string introduce= DataController.Instance.ReadCfg("Introduce", itemId, DataController.Instance.dicItem);
        // GameTool.GetTheChildComponent<Text>(packPanel, "Txt_Introduce").text = introduce;
        // //读取物品的图片名称
        // //string iconName = DataController.Instance.ReadCfg("IconName", itemId, DataController.Instance.dicItem);
        // //GameTool.GetTheChildComponent<Image>(packPanel, "Img_Item").sprite = Resources.Load<Sprite>("PackSprite/"+ iconName);
        // GameTool.GetTheChildComponent<Image>(packPanel, "Img_Item").sprite = itemSprite;

        MessageCenter.SendMessage(E_MessageType.ItemBeChoose,this);
    }
}
