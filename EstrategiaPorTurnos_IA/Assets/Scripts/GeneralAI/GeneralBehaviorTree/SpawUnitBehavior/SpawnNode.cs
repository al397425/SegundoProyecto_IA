using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GeneralBehaviorTree;
using UnityEngine.UI;
using System;

public class SpawnNode : GeneralNode
{


    private Vector3 spawnPos;
    private CreateCharacter creator;
    private int numSpawned = 0;
    public SpawnNode() : base()
    {
        creator = GameObject.FindGameObjectWithTag("creator").GetComponent<CreateCharacter>();
    }
    public override NodeState Evaluate()
    {
        

   
        if(numSpawned == 0)
        {
            creator.GenerateCharactersWithPos("aerial", new Vector3(20, 20));
            Debug.Log("creando unidades");
            //numSpawned++;
        }

        state = NodeState.SUCCESS;

        return state;
    }

}
