using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HazardManager : MonoBehaviour 
{
    public Transform flamePrefab;

    List<Transform> flames; 
    PhysicalNode[,] grid;
   Vector2Int size;
    Vector3 initPos;


	void Start()
	{
        flames = new List<Transform>();
//        initPos = GetRandomNode(grid).worldPos;
    //    CreateFlame(initPos);
	}

	void Update()
	{
       
	}

	public void SetGrid(PhysicalNode[,] grid)
    {
        this.grid = grid;
    }

    public void SetSize(Vector2Int size)
    {
        this.size = size;
    }

    void CreateFlame(Vector3 pos)
    {
        Vector3 offsetY = new Vector3(0, 0.3f, 0);
        Transform flame = Instantiate(flamePrefab, pos + offsetY, Quaternion.Euler(-90, 0, 0));
        flame.parent = transform;
        flames.Add(flame);
    }

    void UpgradeFlame()
    {
        
    }

    PhysicalNode GetRandomNode(PhysicalNode[,] grid)
    {
        int randomX = Random.Range(0, size.x - 1);
        int randomZ = Random.Range(0, size.y - 1);
        PhysicalNode node = null;
        for (int z = 0; z < size.y; z++)
        {
            for (int x = 0; x < size.x; x++)
            {
                if (x == randomX && z == randomZ)
                {
                    node = grid[x, z];
                    print(node);
                }
            }
        }
        return node;
    }
}
