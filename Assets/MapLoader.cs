using System.Collections;
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

        for (int i = 0; i < mapData.m_iMapX; i++)
        {
            for (int j = 0; j < mapData.m_iMapY; j++)
            {
                GameObject tObj = Instantiate(obj, new Vector3(j - blockSizeX / 2, i - blockSizeY / 2, 0), Quaternion.identity, this.gameObject.transform);
                tObj.GetComponent<EBlockController>().block = mapData.getItem(j, i);
                blockList.Add(tObj);
            }
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        blockList = new List<GameObject>();
        EblockItem test = new EblockItem(10, 10);
        setData(test);

        loadMap();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
