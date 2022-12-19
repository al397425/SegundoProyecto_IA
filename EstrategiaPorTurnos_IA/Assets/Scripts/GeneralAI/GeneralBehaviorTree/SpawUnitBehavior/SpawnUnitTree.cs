using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GeneralBehaviorTree;

public class SpawnUnitTree : GeneralTree
{

    public static bool canSpawnUnit = false;
    public static CreateCharacter creator;
    protected override GeneralNode SetupTree()
    {
        GeneralNode root = new GeneralSelector(new List<GeneralNode>
        {
            new WaitForTurnNode(),
            new GeneralSequence( new List<GeneralNode>{
                new UnitToSpawnNode(),
                new FindPosToSpawn(),
                new SpawnNode()

            }),
        }); ;
        return root;
    }

    public static void SetSpawnUnit(bool canSpawn)
    {
        canSpawnUnit = canSpawn;
    }


}
