using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GeneralBehaviorTree;

public class ActiveUnitTree : GeneralTree
{
    
    public static bool isPlayerTurn = true;
    protected override GeneralNode SetupTree()
    {
        GeneralNode root = new GeneralSelector(new List<GeneralNode>
        {
            new WaitForTurnNode(),
            new GeneralSequence( new List<GeneralNode>{
                new MakeDangerList(),
                new SelectUnitNode(),
                new UnitActivationNode(),
            }),
        });
        return root;
    }

    public static void SetActivePlayerTurn(bool turn)
    {
        isPlayerTurn = turn;
    }


}
