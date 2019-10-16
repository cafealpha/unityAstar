using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
            Ray ray = MainCamera.ScreenPointToRay(Input.mousePosition);
            rayCasting(ray);
        }
    }

    void rayCasting(Ray ray)
    {
        RaycastHit hitObj;
        if(Physics.Raycast(ray, out hitObj, Mathf.Infinity))
        {
            if(hitObj.transform.tag.Equals("block"))
            {
                EBlockController ec = hitObj.transform.GetComponent<EBlockController>();
                if (null != ec)
                    ec.getBlockInfo();
            }
        }
    }
}
