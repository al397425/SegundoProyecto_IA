using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GeneralBehaviorTree;
public class FindPosToSpawn : GeneralNode
{

   
    private Vector3 spawnPos;
    public FindPosToSpawn() : base()
    {
    }
    public override NodeState Evaluate()
    {

        spawnPos = new Vector3(20, 20);
        this.parent.SetData("spawnPos", spawnPos);
        state = NodeState.SUCCESS;

        return state;
    }
}
