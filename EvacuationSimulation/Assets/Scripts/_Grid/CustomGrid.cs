using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Renderer))]
public class CustomGrid : MonoBehaviour
{

    public LayerMask unwalkableMask;
    public IntVector2 size;//number of nodes
    public float nodeRadius = 0.5f;
    public bool displayGrid = false;

    PhysicalNode[,] grid;
    Vector2 worldSize;
    float nodeDiameter;
    Vector3 InitCornerPos;


    void Awake()
    {
        nodeDiameter = nodeRadius * 2;

        Renderer ren = GetComponent<Renderer>();

        worldSize = new Vector2(ren.bounds.size.x, ren.bounds.size.z);

        InitCornerPos = transform.position + (Vector3.back * (worldSize.y / 2)) +
                                 transform.position + (Vector3.left * (worldSize.x / 2));

        size.x = Mathf.RoundToInt(worldSize.x / nodeDiameter);
        size.z = Mathf.RoundToInt(worldSize.y / nodeDiameter);

        GenerateGrid();
    }


    void GenerateGrid()
    {
        grid = new PhysicalNode[size.x, size.z];

        for (int z = 0; z < size.z; z++)
        {
            for (int x = 0; x < size.x; x++)
            {
                Vector3 worldPos = InitCornerPos + (Vector3.right * (x * nodeDiameter + nodeRadius)) +
                    (Vector3.forward * (z * nodeDiameter + nodeRadius));

                bool walkable = !(Physics.CheckSphere(worldPos, nodeRadius, unwalkableMask));

                grid[x, z] = new PhysicalNode(worldPos, walkable);
            }
        }

    }

    public PhysicalNode[,] GetGrid()
    {
        return grid; 
    }



    void OnDrawGizmos()
    {
        //Grid
        Gizmos.color = Color.white;
        Gizmos.DrawWireCube(transform.position, new Vector3(worldSize.x, 0.5f, worldSize.y));

        //InitCorner
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(InitCornerPos, 0.2f);

        //Nodes
        if(displayGrid == true)
        {
            if (grid != null)
            {
                foreach (PhysicalNode n in grid)
                {
                    Gizmos.color = (n.walkable) ? Color.white : Color.red;
                    Gizmos.DrawCube(n.worldPos, Vector3.one * (nodeRadius * 2 - 0.2f));
                }
            }
   
        }
    }

}

[System.Serializable]
public struct IntVector2
{
    public int x;
    public int z;

    public IntVector2(int x, int z)
    {
        this.x = x;
        this.z = z;
    }
}