using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GeneralAI : MonoBehaviour
{
    // Start is called before the first frame update
    public Transform[] playerArmyPos;
    public Transform[] IAAarmyPos;
    public GameObject[] IAArmy;
    void Start()
    {
        ActiveUnitTree.SetActivePlayerTurn(false);
    }

}
