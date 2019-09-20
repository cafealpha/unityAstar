using System.Collections;
using System.Collections.Generic;
//using System.Linq;
using UnityEngine;

public class astarTest : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Astar star = new Astar();

        INode[,] map = new INode[9, 9];

        //데이터 초기화
        for (int i = 0; i < map.GetLength(0); i++)
        {
            for (int j = 0; j < map.GetLength(1); j++)
            {
                map[j, i] = new Node(nodeProp.EMPTY, j, i);
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
            INode ln = PathList[PathList.Count-1];
            string outputStr = "";
            foreach (var item in PathList)
            {
                outputStr += ("(" + item.xPos + " , " + item.yPos + ")");
                if (ln != item)
                    outputStr += " -> ";
            }

            Debug.Log(outputStr);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
