using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class rayController : MonoBehaviour
{
    // Start is called before the first frame update

    Camera MainCamera;

    void Start()
    {
        MainCamera = this.GetComponent<Camera>();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
            rayCasting(0);
        }
        else if(Input.GetMouseButtonDown(1))
        {
            rayCasting(1);
        }
    }


    //마우스 버튼
    //0왼쪽, 1오른쪽, 2가운데
    void rayCasting(int Button)
    {
        if (EventSystem.current.IsPointerOverGameObject() == true) return;
        Ray ray = MainCamera.ScreenPointToRay(Input.mousePosition);

        RaycastHit hitObj;
        if(Physics.Raycast(ray, out hitObj, Mathf.Infinity))
        {
            if(hitObj.transform.tag.Equals("block"))
            {
                EBlockController ec = hitObj.transform.GetComponent<EBlockController>();
                if (null == ec) return;

                switch(Button)
                {
                    case 0:
                        ec.getBlockInfo();
                        ec.setProperty(nodeProp.WALL);
                        break;
                    case 1:
                    case 2:
                        ec.setProperty(nodeProp.EMPTY);
                        break;
                }
            }
        }
    }
}
