﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapLoader : MonoBehaviour
{
    public GameObject obj;

    public EblockItem mapData;

    public int blockSizeX;
    public int blockSizeY;
    public void setData(EblockItem map)
    {
        mapData = map;
    }

    //인스턴트된 블록을 담아놓을 리스트
    List<GameObject> blockList;

    public void loadMap()
    {
        if (mapData == null) return;

        for (int i = 0; i < mapData.m_iMapY; i++)
        {
            for (int j = 0; j < mapData.m_iMapX; j++)
            {
                GameObject tObj = Instantiate(obj, new Vector3(j - mapData.m_iMapX/2 - blockSizeX / 2, i - mapData.m_iMapY/2 - blockSizeY / 2, 0), Quaternion.identity, this.gameObject.transform);
                tObj.GetComponent<EBlockController>().block = mapData.getItem(j, i);
                blockList.Add(tObj);
            }
        }
    }

    public void destroyCurrentMap()
    {
        while(blockList.Count != 0)
        {
            GameObject temp = blockList[0];
            blockList.RemoveAt(0);
            Destroy(temp);
        }

        mapData = null;
    }

    // Start is called before the first frame update
    void Start()
    {
        blockList = new List<GameObject>();
    }

}
