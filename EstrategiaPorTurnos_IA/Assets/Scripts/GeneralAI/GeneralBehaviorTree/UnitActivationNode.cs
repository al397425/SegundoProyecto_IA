using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GeneralBehaviorTree;
public class UnitActivationNode : GeneralNode
{

    private GameObject[] IAAarmy;
    private UnitSelection UnitSelection;
    public UnitActivationNode() : base()
    {
        IAAarmy = GameObject.FindObjectOfType<GeneralAI>().IAUnits;
        UnitSelection = GameObject.FindObjectOfType<UnitSelection>();
    }
    public override NodeState Evaluate()
    {

        //Obteniendo la info de la posición calculada en los nodos anteriores podemos saber cuál es la unidad que queremos activar
        //Con esta info después lo ideal sería "activarla" y usar los BT de la clase Unidades para realizar la acción que sea necesaría
        IAAarmy = GameObject.FindObjectOfType<GeneralAI>().IAUnits;
        int pos = (int)this.parent.GetData("posDanger");

        //Debug.Log("Nombre : " + IAAarmy[pos].name);
        //Debug.Log("SE MUEVE LA UNIDAD EN LA POSICIÓN : " + IAAarmy[pos].transform.position);
        //Debug.Log("Tipo : " + IAAarmy[pos].GetComponent<CharacterClass>().type);

        //En este caso simplemente para poder probar hago que se mueva hacía la posición de la primera unidad de la IA
        Debug.Log("Tamaño ejercito : " + IAAarmy.Length);
        if(pos != -1)
        {
            //IAAarmy[pos].GetComponent<CharacterPathfindingMovementHandler>().SetTargetPosition(new Vector3(10, 10));
            Debug.Log("Unit a activar: " + IAAarmy[pos].name);

            //UnitSelection.activateUnit(IAAarmy[pos]);
            UnitSelection.moveUnitIA(IAAarmy[pos]);

            IAAarmy[pos].GetComponent<CharacterClass>().wasMoved = true;
            state = NodeState.RUNNING;
        }
        else
        {
            state = NodeState.SUCCESS;
        }


       

        return state;
    }
}
