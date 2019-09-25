using System;
using System.Collections.Generic;

public enum nodeDirection { UP_LEFT, UP, UP_RIGHT, RIGHT, BOTTOM_RIGHT, BOTTOM, BOTTOM_LEFT, LEFT, CENTER, NULL };

// 노드 클래스
public class Node : INode
{
    //생성할때 타일 속성과 위치 설정
    public Node(nodeProp property, int xPos, int yPos)
    {
        init(property, xPos, yPos);
    }

    public void init(nodeProp property, int xPos, int yPos)
    {
        this.property = property;
        this.stat = nodeStat.NULL;
        this.xPos = xPos;
        this.yPos = yPos;

        G = 0;
        H = 0;

        parent = null;
    }

    public nodeProp property { get; set; }
    public nodeStat stat { get; set; }

    public int F
    {
        get => G + H;
    }

    public int G { get; set; }
    public int H { get; set; }
    public int xPos { get; set; }
    public int yPos { get; set; }

    //노드가 가리키는 노드
    public INode parent { get; set; }
}

//A스타 알고리즘 클래스
public class Astar
{
    public void init(ref INode[,] map)
    {
        this.map = map;

        foreach (INode node in map)
        {
            if (node.property == nodeProp.START)
                start = node;
            if (node.property == nodeProp.GOAL)
                goal = node;
        }

        curTile = null;
        isCalculating = false;

        if(OpenList == null)
        {
            OpenList = new List<INode>();
        }
        else
        {
            OpenList.Clear();
        }

        if (CloseList == null)
        {
            CloseList = new List<INode>();
        }
        else
        {
            CloseList.Clear();
        }

        if (PathList == null)
        {
            PathList = new List<INode>();
        }
        else
        {
            PathList.Clear();
        }
    }

    private INode[,] map;

    //시작점과 끝점
    private INode start;
    private INode goal;

    //오픈 리스트
    private List<INode> OpenList;
    //클로즈 리스트
    private List<INode> CloseList;
    //최종 경로
    private List<INode> PathList;

    //지금 계산중인가?
    public bool isCalculating;
    //현재 탐색중인 타일 기록
    private INode curTile;

    //경로 계산
    public bool calc()
    {
        //스탭 계산중이었으면 할 필요 없다.
        if (!isCalculating)
        {
            //현재 선택된 타일
            curTile = start;
            start.stat = nodeStat.CLOSE;
            CloseList.Add(start);
        }

        isCalculating = true;
        //주변을 돌려서 골에 도착하지 않으면 계속 루프를 돌림
        while (!searchAround(ref curTile))
        {
            //오픈리스트에 타일이 없을때까지 경로를 못찾으면 실패
            if (OpenList.Count == 0) return false;

            //오픈리스트를 검색해 가장 F값이 낮은 노드를 클로즈 리스트로 옮기고 curTile로 지정
            INode tNode = null;
            foreach (INode node in OpenList)
            {
                if (tNode == null) tNode = node;
                else if (tNode.F > node.F) tNode = node;
            }
            curTile = tNode;
            OpenList.Remove(tNode);

            tNode.stat = nodeStat.CLOSE;
            CloseList.Add(tNode);
        }
        isCalculating = false;
        return true;
    }

    public INode stepCalc()
    {
        //스탭 계산중이었으면 할 필요 없다.
        if (!isCalculating)
        {
            //현재 선택된 타일
            curTile = start;
            start.stat = nodeStat.CLOSE;
            CloseList.Add(start);
        }

        if(!searchAround(ref curTile))
        {
            //오픈리스트에 타일이 없을때까지 경로를 못찾으면 실패
            if (OpenList.Count == 0)
            {
                isCalculating = false;
                return null;
            }

            //오픈리스트를 검색해 가장 F값이 낮은 노드를 클로즈 리스트로 옮기고 curTile로 지정
            INode tNode = null;
            foreach (INode node in OpenList)
            {
                if (tNode == null) tNode = node;
                else if (tNode.F > node.F) tNode = node;
            }
            curTile = tNode;
            OpenList.Remove(tNode);

            tNode.stat = nodeStat.CLOSE;
            CloseList.Add(tNode);
        }
        else
        {
            isCalculating = false;
            return null;
        }
        return null;
    }

    //현재 지점에서 체크하는 지점의 G값을 계산
    private int calcG(nodeDirection dir, int curG)
    {
        int diagonal = 14;
        int straight = 10;

        switch (dir)
        {
            case nodeDirection.UP_LEFT:
            case nodeDirection.UP_RIGHT:
            case nodeDirection.BOTTOM_LEFT:
            case nodeDirection.BOTTOM_RIGHT:
                return curG + diagonal;
            case nodeDirection.LEFT:
            case nodeDirection.RIGHT:
            case nodeDirection.UP:
            case nodeDirection.BOTTOM:
                return curG + straight;
        }

        //에러시 -1
        return -1;
    }

    //현재위치부터 목적지까지 맨하탄거리를 계산
    private int calcH(int x, int y)
    {
        return (System.Math.Abs((goal.xPos - x)) + System.Math.Abs((goal.yPos - x))) * 10;
    }

    //선택된 블럭 주변을 탐색하는 함수
    private bool searchAround(ref INode curTile)
    {
        for (int i = -1; i < 2; i++)
        {
            for (int j = -1; j < 2; j++)
            {
                //방향 판정
                nodeDirection dir = getDirection(j, i);
                //중앙부는 항상 자신이므로 패스
                if (dir == nodeDirection.CENTER) continue;

                int txPos = curTile.xPos + j;
                int tyPos = curTile.yPos + i;

                //지정된 범위를 벗어나면 리턴
                if (txPos < 0 || txPos > map.GetLength(0) - 1) continue;
                if (tyPos < 0 || tyPos > map.GetLength(1) - 1) continue;


                //벽이나 곡면 예외 처리 클로즈 리스트 예외 처리

                //판정할 타일 캐싱
                INode tile = map[txPos, tyPos];

                //클로즈 리스트에 들어가있는 블럭이면 패스
                if (CloseList.Contains(tile)) continue;

                //벽이면 패스
                if (tile.property == nodeProp.BLOCK) continue;

                //피해야 하는 블럭 판정
                if (isCorner(curTile.xPos, curTile.yPos, dir)) continue;

                //골이면 완료
                if (isGoal(tile.xPos, tile.yPos))
                {
                    tile.parent = curTile;
                    return true;
                }

                //0이면 아직 거리계산이 안됐다는 의미
                if (tile.F == 0)
                {
                    tile.H = calcH(tile.xPos, tile.yPos);
                    tile.G = calcG(dir, curTile.G);
                    tile.parent = curTile;

                    //거리계산이 안된 블럭은 무조건 들어간다.
                    tile.stat = nodeStat.OPEN;
                    OpenList.Add(tile);
                }
                else
                {
                    //타일이 작성이 됐다면 값을 비교해서 작은쪽으로 수정
                    int tG = calcG(dir, curTile.G);

                    if (tile.G > tG)
                    {
                        tile.G = tG;
                        tile.parent = curTile;
                    }
                }
            }
        }
        return false;
    }

    //갈 수 있는 길인지 판정
    private bool isEmpty(int x, int y)
    {
        //맵 위치에 있는 타일 불러옴
        INode node = map[x, y];

        //클로즈 리스트에 들어가있는 블럭이면 패스
        if (CloseList.Contains(node)) return false;

        //지정된 범위를 벗어나면 리턴
        if (x < 0 || x > map.GetLength(0) - 1) return false;
        if (y < 0 || y > map.GetLength(1) - 1) return false;

        //벽이면 패스
        if (node.property == nodeProp.BLOCK) return false;

        return true;
    }

    //골인지 판정
    private bool isGoal(int x, int y)
    {
        //골이면 완료
        if (map[x, y].property == nodeProp.GOAL) return true;

        return false;
    }

    //피해야 하는 길인지 판정
    private bool isCorner(int curX, int curY, nodeDirection dir)
    {
        switch (dir)
        {
            case nodeDirection.UP_LEFT:
                {
                    //왼쪽 상단으로 넘어갈때
                    //바로 왼쪽이 벽
                    if (map[curX - 1, curY].property == nodeProp.BLOCK) return true;
                    //바로 위쪽이 벽
                    if (map[curX, curY - 1].property == nodeProp.BLOCK) return true;
                }
                break;
            case nodeDirection.UP_RIGHT:
                {
                    //오른쪽 상단으로 넘어갈때
                    //바로 오른쪽이 벽
                    if (map[curX + 1, curY].property == nodeProp.BLOCK) return true;
                    //바로 위쪽이 벽
                    if (map[curX, curY - 1].property == nodeProp.BLOCK) return true;
                }
                break;
            case nodeDirection.BOTTOM_LEFT:
                {
                    //좌측하단으로 넘어갈때
                    //바로 왼쪽이 벽
                    if (map[curX - 1, curY].property == nodeProp.BLOCK) return true;
                    //바로 아래쪽이 벽
                    if (map[curX, curY + 1].property == nodeProp.BLOCK) return true;
                }
                break;
            case nodeDirection.BOTTOM_RIGHT:
                {
                    //우측하단으로 넘어갈때
                    //바로 오른쪽이 벽
                    if (map[curX + 1, curY].property == nodeProp.BLOCK) return true;
                    //바로 아래쪽이 벽
                    if (map[curX, curY + 1].property == nodeProp.BLOCK) return true;
                }
                break;
        }

        return false;
    }

    //디렉션 판정
    private nodeDirection getDirection(int x, int y)
    {
        if (x == -1 && y == -1) return nodeDirection.UP_LEFT;
        if (x == 0 && y == -1) return nodeDirection.UP;
        if (x == 1 && y == -1) return nodeDirection.UP_RIGHT;

        if (x == -1 && y == 0) return nodeDirection.LEFT;
        if (x == 0 && y == 0) return nodeDirection.CENTER;
        if (x == 1 && y == 0) return nodeDirection.RIGHT;

        if (x == -1 && y == 1) return nodeDirection.BOTTOM_LEFT;
        if (x == 0 && y == 1) return nodeDirection.BOTTOM;
        if (x == 1 && y == 1) return nodeDirection.BOTTOM_RIGHT;

        return nodeDirection.NULL;
    }


    public INode getNode(int x, int y)
    {
        foreach (INode node in map)
        {
            if ((node.xPos == x) && (node.yPos == y))
                return node;
        }

        return null;
    }

    //최종 패스 반환
    public List<INode> getPath()
    {
        //goal에 parent가 없으면 아직 경로가 생성이 안된거.
        if (goal.parent == null) return null;

        PathList.Clear();
        INode tNode = goal;

        PathList.Insert(0, tNode);
        while (tNode.property != nodeProp.START)
        {
            tNode.stat = nodeStat.ROAD;
            tNode = tNode.parent;
            PathList.Insert(0, tNode);
        }
        if (tNode.property == nodeProp.START)
        {
            tNode.stat = nodeStat.ROAD;
            PathList.Insert(0, tNode);
        }
        return PathList;
    }

    virtual public void printPath()
    {
        getPath();
        if (PathList.Count != 0)
        {
            INode ln = PathList[PathList.Count - 1];
            foreach (var item in PathList)
            {
                Console.Write("({0} , {1} ) ", item.xPos, item.yPos);
                if (ln != item)
                    Console.Write(" -> ");
                else Console.WriteLine();
            }
        }
    }

}



