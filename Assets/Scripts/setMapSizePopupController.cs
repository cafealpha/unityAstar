using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class setMapSizePopupController : MonoBehaviour
{
    //입력상자
    public InputField m_IFxPos;
    public InputField m_IFyPos;

    //확인버튼
    public Button m_btOK;
    //취소버튼
    public Button m_btCancel;


    // Start is called before the first frame update
    void Start()
    {
        m_btOK.onClick.AddListener(makeMap);
        m_btCancel.onClick.AddListener(destroyDialog);
    }

    void makeMap()
    {
        EblockItem EI = new EblockItem(int.Parse(m_IFxPos.text), int.Parse(m_IFyPos.text));
        MapLoader ML = GameObject.Find("MapCreator").GetComponent<MapLoader>();
        ML.setData(EI);
        ML.loadMap();
        destroyDialog();
    }

    void destroyDialog()
    {
        Destroy(this.gameObject);
    }
}
