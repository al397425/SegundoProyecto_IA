using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GeneralBehaviorTree;

//Esta clase crea una lista para cada uno de las unidades que tiene la IA y les asigna un valor de peligrosidad de moverlas( en cuanto más baja mejor).
//En este caso simplemente se miden las distancias a otras unidades y se suman entre ellas, tendría que usar el pathinding para caluclarlo de forma correcta.
public class MakeDangerList : GeneralNode
{
    //valor para ver si es turno, solo para testeo

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

        int i;
        int j;
        playerArmyPos = generalData.PlayerUnits;
        IAAarmyPos = generalData.IAUnits;
        dangerValuesList = new float[IAAarmyPos.Length];
        //Compara la peligrosidad que tiene cada unidad respecto a sus alrededores.
        for (i=0; i < IAAarmyPos.Length; i++)
        {
            CharacterClass IAunit = IAAarmyPos[i].GetComponent<CharacterClass>();
            if (!IAunit.wasMoved)
            {
                for (j = 0; j < playerArmyPos.Length; j++)
                {
                    float dist = Vector3.Distance(IAAarmyPos[i].transform.position, playerArmyPos[j].transform.position);
                  
                    if (IAunit.GetTypeUnit() == "infantry")
                    {
                        Debug.Log("a");
                        if (playerArmyPos[j].GetComponent<CharacterClass>().GetTypeUnit() == "aerial")
                        {
                            dangerValuesList[i] = 0;
                        }
                        else if (playerArmyPos[j].GetComponent<CharacterClass>().GetTypeUnit() == "tank") { dangerValuesList[i] += dist / 2; }
                        else { dangerValuesList[i] = dist; }
                    }
                    else if (IAunit.GetTypeUnit() == "archer")
                    {
                        Debug.Log("b");
                        if (playerArmyPos[j].GetComponent<CharacterClass>().GetTypeUnit() == "aerial") { dangerValuesList[i] = float.MaxValue; }
                        if (playerArmyPos[j].GetComponent<CharacterClass>().GetTypeUnit() == "tank") { dangerValuesList[i] = dist / 2; }
                        else { dangerValuesList[i] = dist; }
                    }
                    else if (IAunit.GetTypeUnit() == "tank")
                    {
                        Debug.Log("c");
                        if (playerArmyPos[j].GetComponent<CharacterClass>().GetTypeUnit() == "aerial") { dangerValuesList[i] = 0; }
                        else { dangerValuesList[i] = dist; }
                    }
                    else if (IAunit.GetTypeUnit() == "aerial")
                    {
                        Debug.Log("d");
                        if (playerArmyPos[j].GetComponent<CharacterClass>().GetTypeUnit() == "archer") { dangerValuesList[i] = float.MaxValue; }
                        if (playerArmyPos[j].GetComponent<CharacterClass>().GetTypeUnit() == "aerial") { dangerValuesList[i] = dist * 2; }
                        else { dangerValuesList[i] = dist; }

                    }
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

        

        return state;
    }
}
