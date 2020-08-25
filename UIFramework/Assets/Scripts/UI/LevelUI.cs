using UnityEngine;
using System.Collections;
using UICore;
using UnityEngine.UI;

public class LevelUI : BaseUI {

    private Button btn_Return;
    //所有关卡的父物体
     private Transform content;
    //两个关卡的预制体
    private GameObject levelItemUp;
    private GameObject levelItemDown;

    private GameObject scrollView;
    protected override void InitUiOnAwake()
    {
        base.InitUiOnAwake();
        scrollView = GameTool.FindTheChild(this.gameObject,"Scroll View").gameObject;
        btn_Return = GameTool.GetTheChildComponent<Button>(this.gameObject, "Btn_Return");
        btn_Return.onClick.AddListener(ReturnUI);
        content = GameTool.FindTheChild(this.gameObject, "Content");
        levelItemUp = Resources.Load<GameObject>("LevelPrefab/LevelItemUp");
        levelItemDown = Resources.Load<GameObject>("LevelPrefab/LevelItemDown");
        InitLevel();
    }
    protected override void InitDataOnAwake()
    {
        base.InitDataOnAwake();
        this.uiId = E_UiId.LevelUI;
    }
    private void InitLevel()
    {
        
        int levelCount = DataController.Instance.GetCfgCount(DataController.Instance.dicLevel);
        PerfitWidth(levelCount);
        //获取当前可进入的最高级关卡
        int currentLevel = LevelManager.Instance.GetCurrentMaxLevel();
        GameObject levelItem = null;
        for (int i = 1; i < 16; i++)
        {
          
            if (i % 2 == 0)
            {
                //下
                levelItem = Instantiate(levelItemDown);
            }
            else
            {
                //上
                levelItem = Instantiate(levelItemUp);
            }
            //打卡对应的关卡
            if (i <= currentLevel)
            {
                //可进入的关卡
                GameTool.GetTheChildComponent<Image>(levelItem, "Btn_Level").color = Color.white;
            }
            else
            {
                //不可进入的关卡
                GameTool.GetTheChildComponent<Button>(levelItem, "Btn_Level").enabled = false;
            }
            levelItem.AddComponent<LevelEntity>().levelId=i;
            //通过配置表获取关卡名称
            string levelName = DataController.Instance.ReadCfg("LevelName",i,DataController.Instance.dicLevel);
            GameTool.GetTheChildComponent<Text>(levelItem, "Txt_LevelName").text = levelName;
            GameTool.GetTheChildComponent<Text>(levelItem, "Txt_LevelId").text = i.ToString();
            GameTool.AddChildToParent(content, levelItem.transform);
        }
        levelItem = null;
        scrollView.AddComponent<ChangePage>();
    }
    private void PerfitWidth(int itemCount)
    {
        GridLayoutGroup glg = content.GetComponent<GridLayoutGroup>();
        //关卡的宽度
        float itemWidth = glg.cellSize.x;
        //计算总长度
        float allWidth = itemWidth * itemCount + glg.spacing.x * (itemCount - 1);
        content.GetComponent<RectTransform>().sizeDelta = new Vector2(allWidth,0);

    }
    private void ReturnUI()
    {
        // GameObject.Find("UIManager").GetComponent<UIManager>().ReturnBeforeUI(beforeUiId);
        UIManager.Instance.ReturnBeforeUI(beforeUiId);
    }
    protected override void OnEnable()
    {
        if (content.childCount==0)
        {
            return;
        }
        int current = LevelManager.Instance.GetCurrentMaxLevel();
        Debug.Log("当前开启的最高关卡时"+ current);
        for (int i = 1; i < content.childCount+1; i++)
        {
            if (i<= current)
            {
                GameObject levelItem = content.GetChild(i - 1).gameObject;
                GameTool.GetTheChildComponent<Button>(levelItem, "Btn_Level").enabled = true;
                GameTool.GetTheChildComponent<Image>(levelItem, "Btn_Level").color = Color.white;
                
            }
        }
    }
}
