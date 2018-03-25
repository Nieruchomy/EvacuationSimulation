using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhysicalNode
{

    public Vector3 onGridPos;
    public  bool walkable;

    public PhysicalNode( Vector3 onGridPos, bool walkable)
    {
        this.onGridPos = onGridPos;
        this.walkable = walkable;

    }
}
