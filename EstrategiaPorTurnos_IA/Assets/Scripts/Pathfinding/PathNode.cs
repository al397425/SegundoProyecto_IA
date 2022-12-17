using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathNode
{
    private Grid<PathNode> _grid;
    public int x, y;

    public int gCost, hCost, fCost;
    public int movementPenalty;
    public PathNode parent;
    public string spriteType;
    
    public PathNode(Grid<PathNode> grid, int x, int y, int penalty)
    {
        _grid = grid;
        this.x = x;
        this.y = y;
        movementPenalty = penalty;
        spriteType = "";
    }

    public void CalculateFCost()
    {
        fCost = gCost + hCost;
    }
}
