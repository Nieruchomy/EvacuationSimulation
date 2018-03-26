using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhysicalNode
{
    
    public Vector3 worldPos;
    public  bool walkable;
    public int onGridX;
    public int onGridY;
    //A* costs
    public int gCost;
    public int hCost;
    public PhysicalNode parent;

    public PhysicalNode( Vector3 onGridPos, bool walkable, int onGridX, int onGridY)
    {
        this.worldPos = onGridPos;
        this.walkable = walkable;
        this.onGridX = onGridX;
        this.onGridY = onGridY;
    }

    public int fCost
    {
        get
        {
            return gCost + hCost;
        }
    }

}
