using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhysicalNode
{

    public Vector3 worldPos;
    public  bool walkable;

    public PhysicalNode( Vector3 worldPos, bool walkable)
    {
        this.worldPos = worldPos;
        this.walkable = walkable;

    }
}
