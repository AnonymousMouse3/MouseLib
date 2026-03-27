using System.Collections.Generic;
using UnityEngine;

public class GoapNode
{
    public GoapAction action;
    public GoapGoal goal;
    
    public float gCost;
    public float hCost;
    public float fCost;
    public GoapNode parent;
    public List<GoapNode> neighbours = new List<GoapNode>();
}

public class AStar : MonoBehaviour
{
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public GoapNode Pathfind(GoapNode start, GoapNode goal)
    {
        List<GoapNode> openList = new List<GoapNode>();
        List<GoapNode> closedList = new List<GoapNode>();
        
        
        return closedList[0];
    }
}
