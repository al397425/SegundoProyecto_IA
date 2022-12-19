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
    private GameObject[] playerArmyPos;
    private GameObject[] IAAarmyPos;
    private GeneralAI generalData;
    private float[] dangerValuesList;
    public MakeDangerList() : base() {
        generalData = GameObject.FindObjectOfType<GeneralAI>();
        playerArmyPos = generalData.PlayerUnits;
        IAAarmyPos = generalData.IAUnits;
        dangerValuesList = new float[IAAarmyPos.Length];
    }
    public override NodeState Evaluate()
    {
        //Comprueba si es mi turno
        if (myTurn)
        {
            int i;
            int j;
            //Compara la peligrosidad que tiene cada unidad respecto a sus alrededores.
            for(i=0; i < IAAarmyPos.Length; i++)
            {
                CharacterClass IAunit = IAAarmyPos[i].GetComponent<CharacterClass>();
                if (!IAunit.wasMoved)
                {
                    for (j = 0; j < playerArmyPos.Length; j++)
                    {
                        float dist = Vector3.Distance(IAAarmyPos[i].transform.position, playerArmyPos[j].transform.position);
                        dangerValuesList[i] += dist;
                    }
                }
                else //Asignamos el valor a 0 para tomar este valor como referencia en el próximo paso para que nunca tome en cuenta esta unidad
                {
                    dangerValuesList[i] = 0;
                }

            }

            //Guardamos la lista de los valores en el padre para así que sea accesible para sus nodos hermanos
            this.parent.ClearData("valueList");
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
