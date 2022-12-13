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
        
        //Obteniendo la info de la posición calculada en los nodos anteriores podemos saber cuál es la unidad que queremos activar
        //Con esta info después lo ideal sería "activarla" y usar los BT de la clase Unidades para realizar la acción que sea necesaría
        int pos = (int)this.parent.GetData("posDanger");
        Debug.Log(IAAarmy[pos].transform.position.x);

        //En este caso simplemente para poder probar hago que se mueva hacía la posición de la primera unidad de la IA
        IAAarmy[pos].GetComponent<CharacterPathfindingMovementHandler>().SetTargetPosition(IAAarmy[0].transform.position);

        state = NodeState.RUNNING;

        return state;
    }
}
