using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class ManageData
{
    public static string result = "";

    public static void saveData(EblockItem ei)
    {
        result = "";

        string mapName = ei.m_sMapName;
        int x = ei.m_iMapX;
        int y = ei.m_iMapY;

        result += mapName + ",\n";
        result += x + ",\n";
        result += y + ",\n";

        for (int i =0; i< y; i++)
        {
            for(int j=0; j < x; j++)
            {
                result += (int)(ei.getItem(j, i).property);
            }
            result += "\n";
        }
        Debug.Log(result);
    }

    public static EblockItem loadData(string fileName)
    {
        string[] data = result.Split(',');
        string mapName = data[0].Replace("\n", "");

        int x = int.Parse(data[1]);
        int y = int.Parse(data[2]);

        string map = data[3].Replace("\n", "");

        EblockItem ei = new EblockItem();

        ei.init(x, y, mapName);

        for(int i = 0;i< y; i++)
        {
            for(int j = 0; j < x; j++)
            {
                switch (int.Parse(map[i * x + j].ToString()))
                {
                    case (int)nodeProp.START:
                    {
                            ei.setItem(j, i, nodeProp.START);
                            break;
                    }
                    case (int)nodeProp.GOAL:
                    {
                            ei.setItem(j, i, nodeProp.GOAL);
                            break;
                    }
                    case (int)nodeProp.WALL:
                    {
                            ei.setItem(j, i, nodeProp.WALL);
                            break;
                    }
                    case (int)nodeProp.EMPTY:
                    {
                            ei.setItem(j, i, nodeProp.EMPTY);
                            break;
                    }
                }
            }
        }

        return ei;
    }
}
