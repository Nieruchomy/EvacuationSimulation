using UnityEngine;

[RequireComponent(typeof(Renderer))]
public class CustomGrid : MonoBehaviour
{


    [Header("Parameters")]
    public Transform player;
    public Vector3 worldPos;
    public Vector2 worldSize;
    public float nodeRadius = 0.5f;
    public LayerMask unwalkableMask;

    [Header("Gizmos Settings")]
    public bool displayGrid = false;

    //Private fields
    PhysicalNode[,] grid;
    Vector3 pivot;
    Vector2Int localSize; // number of nodes
    Vector3 initPointPos; // -x-y corner
    float nodeDiameter; // 



    void Awake()
    {
        worldPos = transform.localPosition; // set up world position
        nodeDiameter = nodeRadius * 2; // sey up diameter

        float worldSizeX = worldSize.x;
        float worldSizeZ = worldSize.y;

        initPointPos = worldPos + (Vector3.back * (worldSizeZ / 2)) + (Vector3.left * (worldSizeX / 2));

        localSize.x =  Mathf.RoundToInt(worldSizeX / nodeDiameter);
        localSize.y = Mathf.RoundToInt(worldSizeZ / nodeDiameter);

        GenerateGrid();
    }

	void GenerateGrid()
    {
        grid = new PhysicalNode[localSize.x, localSize.y];

        for (int z = 0; z < localSize.y; z++)
        {
            for (int x = 0; x < localSize.x; x++)
            {
                Vector3 onGridPos = initPointPos + (Vector3.right * (x * nodeDiameter + nodeRadius)) +
                (Vector3.forward * (z * nodeDiameter + nodeRadius));

                bool walkable = !(Physics.CheckSphere(onGridPos, nodeRadius, unwalkableMask));

                grid[x, z] = new PhysicalNode(onGridPos, walkable);
            }
        }
    }

    public PhysicalNode GetNode(Vector3 point)
    {
        float percentX = (point.x + worldSize.x / 2) / worldSize.x;
        float percentY = (point.z + worldSize.y / 2) / worldSize.y;

        percentX = Mathf.Clamp01(percentX);
        percentY = Mathf.Clamp01(percentY);

        int x = Mathf.RoundToInt((worldSize.x - 1) * percentX);
        int y = Mathf.RoundToInt((worldSize.y - 1) * percentY);
        return grid[x,y];
    }

    void OnDrawGizmos()
    {
        //Grid
        Gizmos.color = Color.white;
        Gizmos.DrawWireCube(transform.position, new Vector3(worldSize.x, 0.5f, worldSize.y));

        //InitCorner
        Gizmos.color = Color.green;
            Gizmos.DrawWireSphere(initPointPos, 0.2f);

        //Nodes
        if(displayGrid == true)
        {
            if (grid != null)
            {
                PhysicalNode playerNode = GetNode(player.position);
                foreach (PhysicalNode n in grid)
                {
                    if(playerNode == n) {
                        Gizmos.color = Color.cyan;
                        Gizmos.DrawCube(n.onGridPos, new Vector3(nodeDiameter - 0.1f, 0.1f, nodeDiameter - 0.1f));
                    }else {
                        Gizmos.color = (n.walkable) ? Color.white : Color.red;
                        Gizmos.DrawCube(n.onGridPos, new Vector3(nodeDiameter - 0.1f, 0.1f, nodeDiameter - 0.1f));   
                    }

                }
            }
   
        }


    }

}
