using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class nodeBlock : MonoBehaviour, INode
{
    public GameObject NodePanel;

    private NodePanalController NPC;

    //노드 인터페이스 구현 부분
    public nodeProp property { get; set; }

    private nodeStat currStat;

    private nodeStat _stat;
    public nodeStat stat {
        get
        {
            return _stat;
        }
        set
        {
            _stat = value;
        }
    }
    public int F
    {
        get => G + H;
    }
    private int _G;
    public int G {
        get { return _G; }
        set
        {
            _G = value;
            if (NPC == null) { Debug.Log("Npc is null"); return; }
            NPC.iG = value;
        }
    }
    private int _H;
    public int H
    {
        get { return _H; }
        set {
            _H = value;
            if (NPC == null) { Debug.Log("Npc is null"); return; }
            NPC.iH = value;
        }
    }

    public int xPos { get; set; }
    public int yPos { get; set; }

    private INode _parent;
    public INode parent {
        get { return _parent; }
        set {
            if (value == null)
            {
                _parent = value;
                return;
            }
            _parent = value;
            float angle = Mathf.Atan2(_parent.xPos- xPos, _parent.yPos - yPos) * 180 / Mathf.PI; ;
            if (angle < 0f) angle += 360f;
            NPC.AppendAngle(angle);
        }
    }

    public void init(nodeProp property, int xPos, int yPos)
    {
        this.property = property;
        this.xPos = xPos;
        this.yPos = yPos;

        G = 0;
        H = 0;

        parent = null;
    }

    void Awake()
    {
        if (NodePanel == null) return;
        NPC = NodePanel.GetComponent<NodePanalController>();
    }

    void Start()
    {
        
    }

    void Update()
    {
        if (currStat != stat)
        {
            currStat = stat;
            if (stat == nodeStat.ROAD) this.transform.GetChild(2).gameObject.SetActive(true);
            else this.transform.GetChild(2).gameObject.SetActive(false);
        }
        
    }
}
