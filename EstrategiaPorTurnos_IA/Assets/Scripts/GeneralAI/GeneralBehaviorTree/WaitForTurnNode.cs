using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GeneralBehaviorTree;
public class WaitForTurnNode : GeneralNode
{
    //valor para ver si es turno, solo para testeo
    public WaitForTurnNode() : base() { }
    public override NodeState Evaluate()
    {
        //Comprueba si es mi turno
        if (ActiveUnitTree.isPlayerTurn == false)
        {
            //Es el turno de la IA enviamos Failure para ejecutar el siguiente nodo del selector
            state = NodeState.FAILURE;
        }
        else
        {
            //No hace nada hasta que no es su turno;
            //Debug.Log("Esperando a mi turno");
            state = NodeState.RUNNING;
        }
        return state;
    }
}
