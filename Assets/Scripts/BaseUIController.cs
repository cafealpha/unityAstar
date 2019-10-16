using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BaseUIController : MonoBehaviour
{
    // Start is called before the first frame update
    //시작버튼
    public Button m_BtStart;
    //목표점
    public Button m_BtTarget;
    //벽
    public Button m_BtWall;
    //새 맵 생성
    public Button m_BtNewMap;
    //저장
    public Button m_BtSave;
    //나가기
    public Button m_BtExit;

    void Start()
    {
        m_BtNewMap.onClick.AddListener(openSetMapMenu);
    }

    //맵설정 메뉴 호출
    void openSetMapMenu()
    {
        Debug.Log("팝업 열기 호출");
        GameObject g = Resources.Load("Prefabs/Menu/SetMapSizePopup") as GameObject;
        GameObject popupMenu = MonoBehaviour.Instantiate(g) as GameObject;
    }
}
