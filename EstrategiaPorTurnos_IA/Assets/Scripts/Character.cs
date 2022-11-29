using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;



public abstract class Character : MonoBehaviour
{
    public int movement; //n�mero casillas que se podr� desplazar
    public int attackRange;
    public int health;
    public int attack;
    public string type;

    public virtual void moveUnit() { Debug.Log("Esta unidad se mueve x distancia"); }
    public virtual void attackUnit() { }
  
}

public class Archer : Character
{
    public override void moveUnit()
    {
        movement = 3;
        Debug.Log("Esta unidad se mueve 3 de distancia");
        //aqu� c�digo 
    }
    public override void attackUnit()
    {
        attackRange = 3;
        //comprueba si est� en el rango de ataque


        //si lo est� se inicia el enfrentamiento entre las unidades
    }

}

public class Infantry : Character
{
    public override void moveUnit()
    {
        movement = 3;
        Debug.Log("Esta unidad se mueve 3 de distancia");
        //aqu� c�digo 
    }
    public override void attackUnit()
    {
        attackRange = 1;
        //comprueba si est� en el rango de ataque


        //si lo est� se inicia el enfrentamiento entre las unidades
    }
}

public class Tank : Character
{
    public override void moveUnit()
    {
        movement = 2;
        Debug.Log("Esta unidad se mueve 2 de distancia");
        //aqu� c�digo 
    }
    public override void attackUnit()
    {
        attackRange = 2;
        //comprueba si est� en el rango de ataque


        //si lo est� se inicia el enfrentamiento entre las unidades
    }
}

public class Aerial : Character
{
    public override void moveUnit()
    {
        movement = 4;
        Debug.Log("Esta unidad se mueve 2 de distancia");
        //aqu� c�digo 
    }
    public override void attackUnit()
    {
        attackRange = 2;
        //comprueba si est� en el rango de ataque


        //si lo est� se inicia el enfrentamiento entre las unidades
    }
}
