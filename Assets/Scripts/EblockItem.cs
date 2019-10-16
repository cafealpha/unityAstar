using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EblockItem
{
    //맵이름
    public string m_sMapName;

    //맵 X사이즈
    public int m_iMapX;
    //맵 Y사이즈
    public int m_iMapY;

    EBlock[,] map;

    public EblockItem(int x, int y, string mapName = "Noname")
    {
        init(x, y, mapName);
    }

    void init(int x, int y, string mapName)
    {
        m_sMapName = mapName;

        m_iMapX = x;
        m_iMapY = y;

        map = new EBlock[m_iMapX, m_iMapY];

        for (int i = 0; i < m_iMapX; i++)
        {
            for (int j = 0; j < m_iMapY; j++)
            {
                //GameObject block = Instantiate(testObject, new Vector3(j - m_iMapX / 2, i - m_iMapY / 2, 0), Quaternion.identity, this.gameObject.transform);
                //블럭 정보만 있으면 되기 떄문에 필요없음.
                map[j, i] = new EBlock(nodeProp.EMPTY, j, i);
            }
        }
    }

    public EBlock getItem(int xPos, int yPos) { return map[xPos, yPos]; }
}
