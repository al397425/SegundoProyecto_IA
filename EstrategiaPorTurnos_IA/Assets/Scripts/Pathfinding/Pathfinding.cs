using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Pathfinding
{
    public static Pathfinding Instance { get; private set; }

    private const int MOVE_STRAIGHT_COST = 10;
    private const int MOVE_DIAGONAL_COST = 14;
    
    private Grid<PathNode> _grid;
    private List<PathNode> _openNodes;
    private List<PathNode> _closedNodes;
    private Tilemap.TilemapObject.TilemapSprite[,] _spriteMatrix;
    
    public Pathfinding(int width, int height, Tilemap.TilemapObject.TilemapSprite[,] spriteMatrix)
    {
        Instance = this;
        _grid = new Grid<PathNode>(width, height, 10f, Vector3.zero, (g, x, y) => new PathNode(g, x, y, 0));
        _spriteMatrix = spriteMatrix;
    }
    
    public Grid<PathNode> GetGrid() {
        return _grid;
    }

    public List<Vector3> FindPath(Vector3 startWorldPos, Vector3 endWorldPos)
    {
        
        _grid.GetXY(startWorldPos, out var startX, out var startY);
        _grid.GetXY(endWorldPos, out var endX, out var endY);

        var path = FindPath(startX, startY, endX, endY);
        if (path == null)
            return null;
        
        var vPath = new List<Vector3>();
        foreach (var pathNode in path)
        {
            vPath.Add(new Vector3(pathNode.x, pathNode.y) * _grid.GetCellSize() + Vector3.one * _grid.GetCellSize() * .5f);
        }
        return vPath;
    }
    
    public List<PathNode> FindPath(int startX, int startY, int endX, int endY)
    {
        PathNode startNode = _grid.GetGridObject(startX, startY);
        PathNode endNode = _grid.GetGridObject(endX, endY);
        _openNodes = new List<PathNode> { startNode };
        _closedNodes = new List<PathNode>();

        for (var x = 0; x < _grid.GetWidth(); x++)
        {
            for (var y = 0; y < _grid.GetHeight(); y++) //Para inicializar los nodos
            {
                var pathNode = _grid.GetGridObject(x, y);
                pathNode.gCost = int.MaxValue;
                pathNode.CalculateFCost();
                pathNode.parent = null;

                switch (_spriteMatrix[x, y])
                {
                    case Tilemap.TilemapObject.TilemapSprite.Ground:
                        pathNode.spriteType = "ground";
                        pathNode.movementPenalty = 5;
                        break;
                    case Tilemap.TilemapObject.TilemapSprite.Path:
                        pathNode.spriteType = "path";
                        break;
                    case Tilemap.TilemapObject.TilemapSprite.Water:
                        pathNode.spriteType = "water";
                        break;
                    case Tilemap.TilemapObject.TilemapSprite.Mountain:
                        pathNode.spriteType = "mountain";
                        break;
                    default:
                        pathNode.spriteType = "";
                        break;
                }
                /*pathNode.spriteType = _spriteMatrix[x, y] switch    //Esto sera utilizado para comprobar los pesos segun el terreno
                {
                    Tilemap.TilemapObject.TilemapSprite.Ground => "ground",
                    Tilemap.TilemapObject.TilemapSprite.Path => "path",
                    Tilemap.TilemapObject.TilemapSprite.Dirt => "dirt",
                    Tilemap.TilemapObject.TilemapSprite.Water => "water",
                    Tilemap.TilemapObject.TilemapSprite.Mountain => "mountain",
                    _ => ""
                };*/
            }
        }

        startNode.gCost = 0;    //Es el nodo de inicio por lo tanto será 0
        startNode.hCost = CalculateDistance(startNode, endNode);
        startNode.CalculateFCost();

        while (_openNodes.Count > 0)
        {
            var currentNode = GetLowestFCostNode(_openNodes);
            if (currentNode == endNode)
            { 
                return CalculatePath(endNode);
            }
                
            _openNodes.Remove(currentNode);
            _closedNodes.Add(currentNode);

            foreach (var neighbourNode in GetNeighbourList(currentNode))
            { 
                if (neighbourNode.spriteType is "water" or "mountain")
                    _closedNodes.Add(neighbourNode);
                
                if (!_closedNodes.Contains(neighbourNode))
                { 
                    var possibleGCost = currentNode.gCost + CalculateDistance(currentNode, neighbourNode) + neighbourNode.movementPenalty; 
                    if (possibleGCost < neighbourNode.gCost)
                    {
                        neighbourNode.parent = currentNode;
                        neighbourNode.gCost = possibleGCost;
                        neighbourNode.hCost = CalculateDistance(neighbourNode, endNode);
                        neighbourNode.CalculateFCost();
                            
                        if (!_openNodes.Contains(neighbourNode))
                            _openNodes.Add(neighbourNode);
                    }
                }
            }
        }

        return null;    //Aquí se llegara si no ha encontrado camino. Cuando salga del while
    }
    
    private List<PathNode> GetNeighbourList(PathNode currentNode) {
        var neighbourList = new List<PathNode>();

        if (currentNode.x - 1 >= 0) {
            //Izquierda
            neighbourList.Add(GetNode(currentNode.x - 1, currentNode.y));
            //Izquierda Abajo
            if (currentNode.y - 1 >= 0) neighbourList.Add(GetNode(currentNode.x - 1, currentNode.y - 1));
            //Izquierda Arriba
            if (currentNode.y + 1 < _grid.GetHeight()) neighbourList.Add(GetNode(currentNode.x - 1, currentNode.y + 1));
        }
        if (currentNode.x + 1 < _grid.GetWidth()) {
            //Derecha
            neighbourList.Add(GetNode(currentNode.x + 1, currentNode.y));
            //Derecha Abajo
            if (currentNode.y - 1 >= 0) neighbourList.Add(GetNode(currentNode.x + 1, currentNode.y - 1));
            //Derecha Arriba
            if (currentNode.y + 1 < _grid.GetHeight()) neighbourList.Add(GetNode(currentNode.x + 1, currentNode.y + 1));
        }
        //Abajo
        if (currentNode.y - 1 >= 0) neighbourList.Add(GetNode(currentNode.x, currentNode.y - 1));
        //Arriba
        if (currentNode.y + 1 < _grid.GetHeight()) neighbourList.Add(GetNode(currentNode.x, currentNode.y + 1));

        return neighbourList;
    }

    public PathNode GetNode(int x, int y) {
        return _grid.GetGridObject(x, y);
    }
    
    private List<PathNode> CalculatePath(PathNode endNode)
    {
        var path = new List<PathNode> { endNode };
        var currentNode = endNode;

        while (currentNode.parent != null)
        {
            path.Add(currentNode.parent);
            currentNode = currentNode.parent;
        }
        path.Reverse();
        return path;
    }
    
    private int CalculateDistance(PathNode a, PathNode b)
    {
        var xDistance = Mathf.Abs(a.x - b.x);
        var yDistance = Mathf.Abs(a.y - b.y);
        var remaining = Mathf.Abs(xDistance - yDistance);
        return MOVE_DIAGONAL_COST * Mathf.Min(xDistance, yDistance) + MOVE_STRAIGHT_COST * remaining;
    }

    private PathNode GetLowestFCostNode(List<PathNode> pathNodeList)
    {
        var lowestFCostNode = pathNodeList[0];
        for (var i = 1; i < pathNodeList.Count; i++)
        {
            if (pathNodeList[i].fCost < lowestFCostNode.fCost)
                lowestFCostNode = pathNodeList[i];
        }

        return lowestFCostNode;
    }
}
