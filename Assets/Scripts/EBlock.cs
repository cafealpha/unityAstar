using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EBlock
{
    public EBlock(nodeProp property, int xPos, int yPos)
    {
        init(property, xPos, yPos);
    }

    public nodeProp property { get; set; }
    public int xPos { get; set; }
    public int yPos { get; set; }

    public void init(nodeProp property, int xPos, int yPos)
    {
        this.property = property;
        this.xPos = xPos;
        this.yPos = yPos;
    }

}
