using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinionSpawner : MonoBehaviour
{

    public GameObject minionPrefab;
	// Use this for initialization
	void Start () {
        Spawn();
	}

    void Spawn()
    {
        Instantiate(minionPrefab, transform.position, Quaternion.identity);
    }

	void OnDrawGizmos()
	{
        Gizmos.color = Color.cyan;
        Gizmos.DrawWireCube(transform.position, Vector3.one);
	}
}
