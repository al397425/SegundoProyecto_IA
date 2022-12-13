using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GeneralBehaviorTree;

//Esta clase crea una lista para cada uno de las unidades que tiene la IA y les asigna un valor de peligrosidad de moverlas( en cuanto más baja mejor).
//En este caso simplemente se miden las distancias a otras unidades y se suman entre ellas, tendría que usar el pathinding para caluclarlo de forma correcta.
public class MakeDangerList : GeneralNode
{
    //valor para ver si es turno, solo para testeo
    private bool myTurn = true;
    private Transform[] playerArmyPos;
    private Transform[] IAAarmyPos;
    private GeneralAI generalData;
    private float[] dangerValuesList;
    public MakeDangerList() : base() {
        generalData = GameObject.FindObjectOfType<GeneralAI>();
        playerArmyPos = generalData.playerArmyPos;
        IAAarmyPos = generalData.IAAarmyPos;
        dangerValuesList = new float[IAAarmyPos.Length];
    }
    public override NodeState Evaluate()
    {
        //Comprueba si es mi turno
        if (myTurn)
        {
            int i;
            int j;
            for(i=0; i < IAAarmyPos.Length; i++)
            {
                //Debug.Log("IA "+i+": "+IAAarmyPos[i].position);
                for (j = 0; j < playerArmyPos.Length; j++)
                {
                    //Debug.Log("Enemy "+j+": "+playerArmyPos[j].position);
                    float dist = Vector3.Distance(IAAarmyPos[i].position, playerArmyPos[j].position);
                    //Debug.Log("Distance: " + dist);
                    dangerValuesList[i] += dist;
                }
                //Debug.Log("Danger val: " + dangerValuesList[i]);
            }

            //Guardamos la lista de los valores en el padre para así que sea accesible para sus nodos hermanos
            this.parent.SetData("valueList", dangerValuesList);
            state = NodeState.SUCCESS;

        }
        else
        {
            state = NodeState.FAILURE;
        }
        return state;
    }
}
