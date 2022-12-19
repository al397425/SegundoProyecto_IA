using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GeneralBehaviorTree;
public class SelectUnitNode : GeneralNode
{

    private float[] dangerValuesList;
    private float max = float.MinValue;
    public SelectUnitNode() : base()
    {
    }
    public override NodeState Evaluate()
    {

        //Obtenemos la posición de la lista donde el valor de "Peligrosidad" es el maximo y lo guardamos en la data del padre.
        dangerValuesList = (float[])this.parent.GetData("valueList");
        int i;
        //Le asigno un valor cualquiera para poder rastrear que en caso de que ese valor no haya cambiado es que no quedan unidades por mover.
        int posDanger = -1;
        for(i = 0; i < dangerValuesList.Length; i++)
        {
            if (/*dangerValuesList[i] > max &&*/ dangerValuesList[i] != 0)
            {
                Debug.Log("En el bucle dangerValues " +i+"con un valor de"+dangerValuesList[i]);
                max = dangerValuesList[i];
                posDanger = i;
            }
        }
        this.parent.ClearData("posDanger");
        this.parent.SetData("posDanger", posDanger);
        state = NodeState.SUCCESS;

        return state;
    }
}
