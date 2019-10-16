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
        m_btOK.onClick.AddListener(destroyDialog);
        m_btCancel.onClick.AddListener(destroyDialog);
    }

    void destroyDialog()
    {
        Destroy(this.gameObject);
    }
}
