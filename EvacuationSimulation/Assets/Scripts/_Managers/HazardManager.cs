using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HazardManager : MonoBehaviour 
{
    public Transform flamePrefab;
    public CustomGrid CustomGrid;


	void Start()
	{
        
	}

	void Update()
	{
        if(Input.GetKeyDown(KeyCode.Space))
        {
            StartFire(CustomGrid.RandomNode);
        }
	}

	void StartFire(PhysicalNode node)
    {
        int rand = Random.Range(0, 360);
        Instantiate(flamePrefab, node.worldPos, Quaternion.Euler(270, rand, rand));
    }
}
