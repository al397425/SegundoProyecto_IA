using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GeneralBehaviorTree;
public class SelectUnitNode : GeneralNode
{

    private bool myTurn = true;
    private float[] dangerValuesList;
    private float min = float.MaxValue;
    public SelectUnitNode() : base()
    {
    }
    public override NodeState Evaluate()
    {

        //Obtenemos la posición de la lista donde el valor de "Peligrosidad" es el minimo y lo guardamos en la data del padre.
        dangerValuesList = (float[])this.parent.GetData("valueList");
        int i;
        int posDanger;
        for(i = 0; i < dangerValuesList.Length; i++)
        {
            if (dangerValuesList[i] < min)
            {
                min = dangerValuesList[i];
                posDanger = i;
            }
        }
        this.parent.SetData("posDanger", (i - 1));
        state = NodeState.SUCCESS;

        return state;
    }
}
