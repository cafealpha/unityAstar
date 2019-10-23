using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BaseUIController : MonoBehaviour
{
    public GameObject testData;

    // Start is called before the first frame update
    //시작버튼
    public Button m_BtStart;
    //목표점
    public Button m_BtGoal;
    //벽
    public Button m_BtWall;
    //새 맵 생성
    public Button m_BtNewMap;
    //불러오기
    public Button m_BtLoad;
    //저장
    public Button m_BtSave;
    //나가기
    public Button m_BtExit;
    
    void Awake()
    {
        //버튼 이벤트 연결
        m_BtStart.onClick.AddListener(startButtonClick);
        m_BtGoal.onClick.AddListener(goalButtonClick);
        m_BtWall.onClick.AddListener(wallButtonClick);

        m_BtNewMap.onClick.AddListener(openSetMapMenu);

        m_BtLoad.onClick.AddListener(loadButtonClick);
        m_BtSave.onClick.AddListener(saveButtonClick);
        m_BtExit.onClick.AddListener(exitButtonClick);

    }

    private void Start()
    {
        
    }

    //맵설정 메뉴 호출
    void openSetMapMenu()
    {
        Debug.Log("팝업 열기 호출");
        GameObject g = Resources.Load("Prefabs/Menu/SetMapSizePopup") as GameObject;
        GameObject popupMenu = MonoBehaviour.Instantiate(g) as GameObject;
    }

    void startButtonClick()
    {
        EditorManager.Instance.currentCursor = nodeProp.START;
    }
    void goalButtonClick()
    {
        EditorManager.Instance.currentCursor = nodeProp.GOAL;
    }
    void wallButtonClick()
    {
        EditorManager.Instance.currentCursor = nodeProp.WALL;
    }
    void loadButtonClick()
    {
        print("로드버튼");
        EblockItem ei = ManageData.loadData("");
        MapLoader ML = GameObject.Find("MapCreator").GetComponent<MapLoader>();
        ML.destroyCurrentMap();
        ML.setData(ei);
        ML.loadMap();
    }

    void saveButtonClick()
    {
        ManageData.saveData(testData.GetComponent<MapLoader>().mapData);
        print("세이브버튼");
    }
    void exitButtonClick()
    {
        print("종료버튼");
    }
}
