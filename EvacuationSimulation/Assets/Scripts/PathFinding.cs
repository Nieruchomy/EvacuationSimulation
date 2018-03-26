using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathFinding : MonoBehaviour 
{

    public Transform target;
    public CustomGrid grid;

	void Update()
	{
        FindPath(transform.position, target.position);
	}

	void FindPath(Vector3 startPos, Vector3 endPos) 
    {
        PhysicalNode startNode = grid.GetNode(startPos);
        PhysicalNode endNode = grid.GetNode(endPos);

        List<PhysicalNode> openSet = new List<PhysicalNode>();
        HashSet<PhysicalNode> closedSet = new HashSet<PhysicalNode>();
        openSet.Add(startNode);

        //current = node in OPEN with the lowest f_cost
        while(openSet.Count > 0)
        {
            PhysicalNode currentNode = openSet[0];
            for (int i = 1; i < openSet.Count; i++)
            {
                if(openSet[i].fCost < currentNode.fCost || openSet[i].fCost == currentNode.fCost)
                {
                    if (openSet[i].hCost < currentNode.hCost)
                        currentNode = openSet[i];
                }
            }
            // remove current from OPEN
            openSet.Remove(currentNode);
            // add current to CLOSED
            closedSet.Add(currentNode);

            // if current is the target node 
            //path has been found return
            if(currentNode == endNode)
            {
                RetracePath(startNode, endNode);
                return;
            }

            foreach(PhysicalNode neighbour in grid.GetNodeNeighbours(currentNode))
            {
                // if neighbour is not traversable or neighbour is in CLOSED
                if(!neighbour.walkable || closedSet.Contains(neighbour))
                {
                    continue;
                }

                int newPathToNeighbour = currentNode.gCost + GetDistance(currentNode, neighbour);

                // if new path to neighbour is shorter OR neighbour is not in OPEN
                if (newPathToNeighbour < neighbour.gCost || !openSet.Contains(neighbour))
                {
                    neighbour.gCost = newPathToNeighbour;
                    neighbour.hCost = GetDistance(neighbour, endNode);
                    neighbour.parent = currentNode;

                    if (!openSet.Contains(neighbour)) 
                    {
                        openSet.Add(neighbour);
                    }
                }
            }
        }
    }

    void RetracePath(PhysicalNode startNode, PhysicalNode endNode)
    {
        List<PhysicalNode> path = new List<PhysicalNode>();
        PhysicalNode currentNode = endNode;

        while(currentNode != startNode)
        {
            path.Add(currentNode);
            currentNode = currentNode.parent;
        }
        path.Reverse();

        grid.path = path; 
    }

    int GetDistance(PhysicalNode node, PhysicalNode otherNode)
    {
        int xDistance = Mathf.Abs(node.onGridX - otherNode.onGridX);
        int yDistance = Mathf.Abs(node.onGridY - otherNode.onGridY);

        if(xDistance > yDistance)
        {
            return 14 * yDistance + 10 * (xDistance - yDistance);
        }
        return 14 * xDistance + 10 * (yDistance - xDistance);

    }


}
