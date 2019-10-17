using UnityEngine;

public enum nodeProp { EMPTY, WALL, START, GOAL, NULL };
public enum nodeStat { OPEN, CLOSE, ROAD, NULL};

public interface INode
{
    //블록 속성
    nodeProp property { get; set; }
    //현재 블록 상태
    nodeStat stat { get; set; }
    int F { get; }

    int G { get; set; }
    int H { get; set; }
    int xPos { get; set; }
    int yPos { get; set; }

    //노드가 가리키는 노드
    INode parent { get; set; }

    void init(nodeProp property, int xPos, int yPos);
}