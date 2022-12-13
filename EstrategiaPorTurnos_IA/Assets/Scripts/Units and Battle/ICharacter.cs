using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;


interface ICharacter 
{

    void MoveUnit(int x, int y);
    void AttackUnit(GameObject go);



}

//Esto es cómo lo tenía antes por si luego todo explota (era una clase base para hacer herencia)
/*
    public abstract class Character : MonoBehaviour
{
    public int movement; //número casillas que se podrá desplazar
    public int attackRange;
    public int health;
    public int attack;
    public string type; //tipo de unidad
    public bool wasMoved; //para saber si la unidad ya fue usada
    public int id;
    public int[] position = { 0, 0 };

    public Character(string t)
    {
        this.type = t;

    }

    public void Start()
    {
        SetStats();
    }
    public virtual void SetStats()
    {

    }
    public void StatsTell() {
        Debug.Log("movement" + movement +", atk range "+attackRange+" type "+type );
    }
    public virtual void MoveUnit(int x, int y) { Debug.Log("Esta unidad se mueve x distancia"); } //(en otro script vendría la IA en sí que una vez decidiese el movimiento llamaría a esta función)
    public virtual void AttackUnit() { }
   public virtual void Update() { }
}

*/

