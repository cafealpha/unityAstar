using System.Collections;
using System.Collections.Generic;
using System.Linq;

using UnityEngine;

public class NodeManager : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject Node;

    //맵 X사이즈
    public int MapX;
    //맵 Y사이즈
    public int MapY;

    void Start()
    {
        //노드 객체가 지정되어있지 않으면 만들지 않음.
        if (Node == null) return;
        //맵크기는 항상 1이상이어야 함.
        if (MapX == 0 || MapY == 0) return;

        Astar star = new Astar();

        INode[,] map = new INode[MapX, MapY];

        for (int i = 0; i < MapX; i++)
        {
            for (int j = 0; j < MapY; j++)
            {
                GameObject node = Instantiate(Node, new Vector3(j * 10 - MapX/2*10, 0, i * 10 - MapX/2*10), Quaternion.identity);
                map[j,i] = node.GetComponent<INode>();
                map[j, i].init(nodeProp.EMPTY, j, i);
            }
        }

        map[2, 4].property = nodeProp.START;
        map[6, 4].property = nodeProp.GOAL;

        map[4, 3].property = nodeProp.BLOCK;
        map[4, 4].property = nodeProp.BLOCK;
        map[4, 5].property = nodeProp.BLOCK;

        star.init(ref map);

        star.calc();
        star.printPath();

        List<INode> PathList = star.getPath();

        if (PathList.Count != 0)
        {
            INode ln = PathList.Last<INode>();
            foreach (var item in PathList)
            {
                Debug.Log("(" + item.xPos + " , " + item.yPos + ")");
                if (ln != item)
                    Debug.Log(" -> ");
                else Debug.Log("");
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
