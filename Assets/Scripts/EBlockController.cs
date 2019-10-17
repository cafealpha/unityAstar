using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

//인스턴스된 게임오브젝트에 블록정보 반영만 해줌.
public class EBlockController : MonoBehaviour {

    public EBlock block;
    public nodeProp property { get; set; }
    private MeshRenderer mr;
    private Material mat;

    public Texture m_tNormal;
    public Texture m_tStart;
    public Texture m_tGoal;
    public Texture m_tWall;

    // Start is called before the first frame update
    void Start()
    {
        mr = this.gameObject.GetComponent<MeshRenderer>();
    }


    public void getBlockInfo()
    {
        Debug.Log("(" + block.xPos + "," + block.yPos + ") " + block.property);
    }

    public void destroy()
    {
        Destroy(gameObject);
    }

    public void setProperty(nodeProp prop)
    {
        if (property == prop) return;
        property = prop;
        invoke();
    }

    //상태 갱신
    public void invoke()
    {
        switch (property)
        {
            case nodeProp.EMPTY:
                mr.material.mainTexture = m_tNormal;
                break;
            case nodeProp.START:
                mr.material.mainTexture = m_tStart;
                break;
            case nodeProp.GOAL:
                mr.material.mainTexture = m_tGoal;
                break;
            case nodeProp.WALL:
                mr.material.mainTexture = m_tWall;
                break;
        }
    }

    public void init(nodeProp property, int xPos, int yPos)
    {
        
    }



}
