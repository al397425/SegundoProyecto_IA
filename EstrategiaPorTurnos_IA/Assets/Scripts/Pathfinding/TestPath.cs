using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestPath : MonoBehaviour
{
    public CharacterPathfindingMovementHandler characterPathfinding;
    private Pathfinding _pathfinding;
    private float _timer;
    void Start()
    {
        _pathfinding = new Pathfinding(10, 10);
        _pathfinding.GetNode(2, 0).isRiver = true;
        _pathfinding.GetNode(5, 0).isRiver = true;
        _timer = 0f;
    }

    private void CalculatePath()
    {
        var mouseWorldPosition = GetMouseWorldPosition();
        _pathfinding.GetGrid().GetXY(mouseWorldPosition, out int x, out int y);

        List<PathNode> path = _pathfinding.FindPath(0, 0, x, y);
        if (path != null) {
            for (int i=0; i<path.Count - 1; i++) {
                Debug.DrawLine(new Vector3(path[i].x, path[i].y) * 10f + Vector3.one * 5f, new Vector3(path[i+1].x, path[i+1].y) * 10f + Vector3.one * 5f, Color.green, 5f);
                //Debug.Log("ayo bitches");
            }
        }
        characterPathfinding.SetTargetPosition(mouseWorldPosition);
        if (path == null)
            Debug.Log("No path found");
    }
    
    private void Update()
    {
        if (Input.GetMouseButton(1) && _timer <= 0f)
        {
            _timer = .1f;
            CalculatePath();
        }
        _timer -= Time.deltaTime;
    }

    private static Vector3 GetMouseWorldPosition()
    {
        var vec = GetMouseWorldPositionWithZ(Input.mousePosition, Camera.main);
        vec.z = 0f;
        return vec;
    }
    private static Vector3 GetMouseWorldPositionWithZ(Vector3 screenPosition, Camera worldCamera)
    {
        var worldPosition = worldCamera.ScreenToWorldPoint(screenPosition);
        return worldPosition;
    }
}
