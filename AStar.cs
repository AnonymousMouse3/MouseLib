using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;

public class Node
{
    #if GOAL_ORIENTED_ACTION_PLANNING
    public GoapAction action;
    public GoapGoal goal;
    #endif
    
    public float gCost;
    public float hCost;
    public float fCost;
    public Node parent;
    public List<Node> neighbours = new List<Node>();
}

public class AStar
{
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public List<Node> GoapPathfind(List<Node> nodeList, Node start, Node goal)
    {
        // we assume that the node list is a prepared grid with neighbours predetermined
        List<Node> openList = nodeList;
        List<Node> closedList = new List<Node>();
        Node currentNode = start;
        
        // initialise start node
        start.gCost = 0;
        start.hCost = 0;
        start.fCost = 0;

        while (openList.Count > 0)
        {
            // Sort the open list by f cost every iteration
            openList.Sort((node1, node2) => node1.fCost.CompareTo(node2.fCost)); // check if this works
            
            // take the cheapest node and remove it from the open list
            currentNode = openList[0];
            openList.Remove(currentNode);
            closedList.Add(currentNode);

            if (currentNode == goal) return ReconstructPath(currentNode);
            
            foreach (Node neighbour in currentNode.neighbours)
            {
                // Skip closed neighbours
                if (closedList.Contains(neighbour)) continue;
                
                // Calculate tentative g score
                float tentativeG = neighbour.action.Cost + currentNode.action.Cost;
            
                // if the neighbour isn't in the open list, add it
                // if it is, check if the g cost to it from this node is less than its current g cost
                // if not, skip it
                if (openList.Contains(neighbour) && !(neighbour.gCost < tentativeG)) continue;
                
                // calculate the costs of the neighbour (this is usually based on distance and calculated at runtime
                // but for GOAP this is stored in each Action as its Cost)
                neighbour.parent = currentNode;
                neighbour.gCost = tentativeG;
                neighbour.hCost = Heuristic(neighbour, goal);
                neighbour.fCost = neighbour.gCost + neighbour.hCost;
                
            }
        }

        // could not find a path
        return null;
    }

    private float Heuristic(Node start, Node goal)
    {
        return 0;
    }

    private List<Node> ReconstructPath(Node node)
    {
        List<Node> path = new List<Node>();
        Node currentNode = node;
        
        while (currentNode.parent != null)
        {
            path.Add(currentNode);
            currentNode = currentNode.parent;
        }
        
        return path;
    }
}
