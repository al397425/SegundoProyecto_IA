using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GeneralBehaviorTree;
public class UnitToSpawnNode : GeneralNode
{

    private float[] dangerValuesList;
    private float max = float.MinValue;
    public UnitToSpawnNode() : base()
    {
    }
    public override NodeState Evaluate()
    {

        this.parent.SetData("type", "aerial");
        state = NodeState.SUCCESS;

        return state;
    }
}
