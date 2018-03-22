using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// This class builds graphical representation of functions. 
/// </summary>

public class Graph : MonoBehaviour 
{
    public GameObject pointPrefab; 
    [Range(10,100)]public int resolution;

    GameObject[] points; 

	void Awake()
	{
        points = new GameObject[resolution];
        float step = 2.0f / resolution;
        Vector3 pos = Vector3.zero;
        Vector3 scale = Vector3.one;
        for (int i = 0; i < resolution; i++)
        {
            GameObject point = Instantiate(pointPrefab) as GameObject;
            pos.x = (i + 0.5f) * step - 1.0f;
            point.transform.localPosition = pos;
            point.transform.localScale = scale * step;
            point.transform.parent = transform;
            points[i] = point;
        }
	}

	void Update()
	{
        float t = Time.time;
	    for (int i = 0; i < points.Length; i++)
        {
            Vector3 position = points[i].transform.localPosition;
            position.y = SineFunction(position.x, t);
            points[i].transform.localPosition = position;
        }
	}

    float SineFunction(float x, float t) {
        return Mathf.Sin(Mathf.PI * (x + t));
    }

    float MultiSineFunction(float x, float t)
    {
        float y = Mathf.Sin(Mathf.PI * (x + t));
        y += Mathf.Sin(2f * Mathf.PI * (x + t)) / 2f;
        return y;
    }

}
