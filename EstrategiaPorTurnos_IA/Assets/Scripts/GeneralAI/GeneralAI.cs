using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GeneralAI : MonoBehaviour
{

    public GameObject[] IAUnits;
    public GameObject[] PlayerUnits;
    public List<Vector3> test;


    void Start()
    {
        ActiveUnitTree.SetActivePlayerTurn(false);
        IAUnits = GameObject.FindGameObjectsWithTag("unitIA");
        PlayerUnits = GameObject.FindGameObjectsWithTag("unitPlayer");

        //Path();


    }

    private void Path()
    {
        test = Pathfinding.Instance.FindPath(IAUnits[0].transform.position, PlayerUnits[0].transform.position, 100, "infantry");
    }

    public static void RestartMovementIA()
    {
        GameObject[] IAUnits = GameObject.FindGameObjectsWithTag("unitIA");
        for (int i = 0; i < IAUnits.Length; i++)
        {
            IAUnits[i].GetComponent<CharacterClass>().wasMoved = false;
        }
    }




}
