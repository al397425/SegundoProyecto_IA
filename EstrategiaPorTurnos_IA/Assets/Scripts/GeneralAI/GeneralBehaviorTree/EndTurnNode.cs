using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GeneralBehaviorTree;
public class EndTurnNode : GeneralNode
{

    private GameObject[] AIArmyList;
    public EndTurnNode() : base()
    {
      
    }
    public override NodeState Evaluate()
    {
        int pos = (int)this.parent.GetData("posDanger");
        //Pasamos el turno al jugador
        if (pos == -1)
        {
            //Se acaba turno, se debe pasar las el turno al jugador y reiniciar todos los parametros de wasMoved a false en las unidades de la IA
            ActiveUnitTree.SetActivePlayerTurn(true);
            GeneralAI.RestartMovementIA();
        }
        state = NodeState.SUCCESS;

        return state;
    }
}
