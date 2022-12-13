using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GeneralBehaviorTree;
public class UnitActivationNode : GeneralNode
{

    private GameObject[] IAAarmy;
    public UnitActivationNode() : base()
    {
        IAAarmy = GameObject.FindObjectOfType<GeneralAI>().IAArmy;
    }
    public override NodeState Evaluate()
    {
        
        //Obteniendo la info de la posici�n calculada en los nodos anteriores podemos saber cu�l es la unidad que queremos activar
        //Con esta info despu�s lo ideal ser�a "activarla" y usar los BT de la clase Unidades para realizar la acci�n que sea necesar�a
        int pos = (int)this.parent.GetData("posDanger");
        Debug.Log(IAAarmy[pos].transform.position.x);

        //En este caso simplemente para poder probar hago que se mueva hac�a la posici�n de la primera unidad de la IA
        IAAarmy[pos].GetComponent<CharacterPathfindingMovementHandler>().SetTargetPosition(IAAarmy[0].transform.position);

        state = NodeState.RUNNING;

        return state;
    }
}
