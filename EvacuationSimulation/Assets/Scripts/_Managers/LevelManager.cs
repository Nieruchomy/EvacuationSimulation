using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour 
{
    public HazardManager hazardManager;
    public CustomGrid customGrid;

	void Start()
    {
        hazardManager.SetGrid(customGrid.GetGrid());
        hazardManager.SetSize(customGrid.size);
	}

}
