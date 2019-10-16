using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//인스턴스된 게임오브젝트에 블록정보 반영만 해줌.
public class EBlockController : MonoBehaviour {

    public EBlock block;

    public void getBlockInfo()
    {
        Debug.Log("(" + block.xPos + "," + block.yPos + ") " + block.property);
    }

    public void init(nodeProp property, int xPos, int yPos)
    {
        
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

}
