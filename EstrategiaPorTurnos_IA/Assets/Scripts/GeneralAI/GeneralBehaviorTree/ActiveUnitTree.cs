using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GeneralBehaviorTree;

public class ActiveUnitTree : GeneralTree
{
    
    public static bool isPlayerTurn = true;
    private UnitSelection UnitSelection;

    protected override GeneralNode SetupTree()
    {
        GeneralNode root = new GeneralSelector(new List<GeneralNode>
        {
            new WaitForTurnNode(),
            new GeneralSequence( new List<GeneralNode>{
                new MakeDangerList(),
                new SelectUnitNode(),
                new UnitActivationNode(),
                new EndTurnNode(),
            }),
        });

        return root;
    }

    public static void SetActivePlayerTurn(bool turn)
    {
        isPlayerTurn = turn;
    }


}
