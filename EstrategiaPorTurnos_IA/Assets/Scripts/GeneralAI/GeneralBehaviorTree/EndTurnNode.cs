using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GeneralBehaviorTree;
public class EndTurnNode : GeneralNode
{

    private GameObject[] AIArmyList;
    private UnitSelection UnitSelection;

    public EndTurnNode() : base()
    {
        UnitSelection = GameObject.FindObjectOfType<UnitSelection>();
    }
    public override NodeState Evaluate()
    {
        int pos = (int)this.parent.GetData("posDanger");
        //Pasamos el turno al jugador
        if (pos == -1)
        {
            //Se acaba turno, se debe pasar las el turno al jugador y reiniciar todos los parametros de wasMoved a false en las unidades de la IA
            GeneralAI.RestartMovementIA();
            UnitSelection.changeTurn();
        }

        state = NodeState.SUCCESS;

        return state;
    }
}
