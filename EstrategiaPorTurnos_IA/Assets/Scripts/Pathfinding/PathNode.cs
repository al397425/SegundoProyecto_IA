using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathNode
{
    private Grid<PathNode> _grid;
    public int x, y;
    
    public int gCost, hCost, fCost;
    public PathNode parent;

    public bool isRiver;
    public bool isWoods;
    
    public PathNode(Grid<PathNode> grid, int x, int y)
    {
        _grid = grid;
        this.x = x;
        this.y = y;
        isRiver = false;
    }

    public void CalculateFCost()
    {
        fCost = gCost + hCost;
    }
}
