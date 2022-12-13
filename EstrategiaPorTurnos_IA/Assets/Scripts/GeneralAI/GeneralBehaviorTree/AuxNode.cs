using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GeneralBehaviorTree;
public class AuxNode : GeneralNode
{
    //valor para ver si es turno, solo para testeo
    public bool myTurn = true;
    public AuxNode() : base() { }
    public override NodeState Evaluate()
    {
        //Comprueba si es mi turno
        if (myTurn)
        {
            state = NodeState.RUNNING;
            Debug.Log("Aux running");
        }
        else
        {
            state = NodeState.FAILURE;
        }
        return state;
    }
}
