using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class CharacterClass:MonoBehaviour, ICharacter 
{
    public int movement; //n�mero casillas que se podr� desplazar
    public int attackRange;
    public int health;
    public int attack;
    public string type; //tipo de unidad
    public bool wasMoved; //para saber si la unidad ya fue usada
    public int id;
    public int[] position = { 0, 0 };
    public int team;

    public void Awake()
    {

    }
    // Start is called before the first frame update

    //en verdad este constructor no se utiliza, luego lo borro
    public CharacterClass(string t)
    {
        type = t;

    }

    // Update is called once per frame
    public void Update()
    {
        //Esto ahora mismo no funciona

        Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition); //coge la posicion que has clicado


        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit2D hit = Physics2D.GetRayIntersection(ray, Mathf.Infinity);

        if (Input.GetMouseButtonDown(0) && hit.collider != null)
        {

            GameObject collisionItem = hit.collider.gameObject;
            CharacterClass collisionCharacter = hit.collider.gameObject.GetComponent<CharacterClass>();
            //Debug.Log("collision item " + collisionItem);

            if (hit.collider != null && collisionItem == GameObject.Find("CharacterPrefab(Clone)"))
            {
                collisionCharacter.StatsTell();
            }

           
        }
    }

    public void SetStats()
    {
        if (this.type == "archer")
        {
            movement = 3;
            attackRange = 3;
            health = 1;
            attack = 2;
            type = "archer";
        }
        else if (this.type == "infantry")
        {
            movement = 3;
            attackRange = 1;
            health = 3;
            attack = 2;
            type = "infantry";
        }
        else if (this.type == "tank")
        {
            movement = 2;
            attackRange = 2;
            health = 4;
            attack = 3;
            type = "tank";

        }
        else if (this.type == "aerial")
        {
            movement = 5;
            attackRange = 2;
            health = 2;
            attack = 2;
            type = "aerial";
        }
        else
        {
            Debug.Log("No hay tipo, es "+ this.type);
        }
        StatsTell();


    }

    //La idea aqu� ha sido que haya un MoveUnit gen�rico que compruebe qu� clase es y ya llame
    //al especifico (en verdad esto es m�s donde ir�a lo de la IA y luego el moverlo en s�
    //ser�a maybe al final de los if o algo, y que la IA llame a todo esto pero el jugador sea
    //directamente solo comprobar si es posible con los movimientos y si lo es se mueve sin m�s

    public void MoveUnit(int x, int y)
    {
        Debug.Log("Esta unidad se mueve " + this.movement + " de distancia");

        if (team != 1) //la IA (?) Podr�a haber sido un booleano, pues la verdad es que s�, luego
                           //si eso lo cambio
        {
            if (this.type == "archer")
            {
                MoveArcher(x, y);
            }
            else if (this.type == "infantry")
            {
                MoveInfantry(x, y);
            }
            else if (this.type == "tank")
            {
                MoveTank(x, y);

            }
            else if (this.type == "aerial")
            {
                MoveAerial(x, y);
            }
        }
        
        //if que compruebe si los movimientos de la unidad lo permiten


        //si lo permiten se mueve a la posicion (si eres el jugador la pos es donde has clicado,
        //si es la IA es la posici�n que calcule con la funci�n especifica)
    }
    public void AttackUnit()
    {
        Debug.Log("Esta unidad se mueve " + this.attackRange + " de distancia");
        //comprueba si est� en el rango de ataque 


        //si lo est� se inicia el enfrentamiento entre las unidades
    }

    public void MoveArcher(int x, int y)
    {

    }

    public void MoveInfantry(int x, int y)
    {

    }

    public void MoveTank(int x, int y)
    {

    }

    public void MoveAerial(int x, int y)
    {

    }


    //Puede que esto lo use, puede que se junte con lo del Move al final, no lo s� a�n
    /*
    public void InfantryIA() { }
    public void ArcherIA() { }
    public void TankIA() { }
    public void AerialIA() { }
    */

    public void StatsTell()
    {
        Debug.Log("movement" + movement + ", atk range " + attackRange + " type " + type);
    }
}



//Esto es c�mo lo ten�a antes por si luego todo explota 
/*
public class CharacterClass : Character
{
    public BoxCollider2D collision;
    public int movement; //n�mero casillas que se podr� desplazar
    public int attackRange;
    public int health;
    public int attack;
    public string type; //tipo de unidad
    public bool wasMoved; //para saber si la unidad ya fue usada
    public int id;
    public int[] position = { 0, 0 };

    // Start is called before the first frame update


    // Update is called once per frame
    public override void Update()
    {

        Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition); //coge la posicion que has clicado

        BoxCollider2D collision = this.GetComponent<BoxCollider2D>();

        if (collision.OverlapPoint(mousePos) && Input.GetMouseButtonDown(0))
        {
            this.StatsTell();
        }
    }

    public override void SetStats()
    {
        if (this.type == "archer")
        {
            movement = 3;
            attackRange = 3;
            health = 1;
            attack = 2;
            type = "archer";
        }
        else if (this.type == "infantry")
        {
            movement = 3;
            attackRange = 1;
            health = 3;
            attack = 2;
            type = "infantry";
        }
        else if (this.type == "tank")
        {
            movement = 2;
            attackRange = 2;
            health = 4;
            attack = 3;
            type = "tank";

        }
        else if (this.type == "aerial") 
        {
            movement = 5;
            attackRange = 2;
            health = 2;
            attack = 2;
            type = "aerial";
        }
        else
        {
            Debug.Log("No hay tipo");
            this.type = "none";
        }


    }

    public override void MoveUnit(int x, int y)
    {
        Debug.Log("Esta unidad se mueve " + this.movement + " de distancia");
        if (this.type == "archer")
        {
            MoveArcher(x, y);
        }
        else if (this.type == "infantry")
        {
            MoveInfantry(x, y);
        }
        else if (this.type == "tank")
        {
            MoveTank(x,y);

        }
        else if (this.type == "aerial")
        {
            MoveAerial(x,y);
        }
        //aqu� c�digo 
    }
    public override void AttackUnit()
    {
        Debug.Log("Esta unidad se mueve " + this.attackRange + " de distancia");
        //comprueba si est� en el rango de ataque 


        //si lo est� se inicia el enfrentamiento entre las unidades
    }

    public void MoveArcher(int x, int y)
    {

    }

    public void MoveInfantry(int x, int y)
    {

    }

    public void MoveTank(int x, int y)
    {

    }

    public void MoveAerial(int x, int y)
    {

    }



    public void InfantryIA() { }
    public void ArcherIA() { }
    public void TankIA() { }
    public void AerialIA() { }

}
*/

/*
public class Archer : Character
    {
        
        public override void setStats() {
            movement = 3;
            attackRange = 3;
            health = 1;
            attack = 2;
            type = "archer";
        }

        public override void Update()
        {

            Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition); //coge la posicion que has clicado

            BoxCollider2D collision = this.GetComponent<BoxCollider2D>();

            if (collision.OverlapPoint(mousePos) && Input.GetMouseButtonDown(0))
            {
                this.statsTell();
            }
        }

        public override void moveUnit(int x, int y, int id) //tendr�a que indicarle a qu� tile se mueve (?)
                                                            //cada unidad que se cree que tenga un id y al mover la unidad mira qu� id tiene el pj y a qu� tile lo quieres mover
                                                            //entonces cambia en el mapa la posicion (y se guarda cuales son sus coordenadas actuales maybe?)
        {
            //se muestra los posibles destinos

            Debug.Log("Esta unidad se mueve " + movement + " de distancia");
            //aqu� c�digo (en otro script vendr�a la IA en s� que una vez decidiese el movimiento llamar�a a esta funci�n)
            //tiene que comprobar si el tile al que lo quieres mover es viable en base a su rango de movimiento (aunque eso maybe se hace m�s con el pathfinding), si lo es lo mueve, si no no hace nada


            //poner
        }
        public override void attackUnit()
        {
            
            //comprueba si est� en el rango de ataque


            //si lo est� se inicia el enfrentamiento entre las unidades
        }

    }

    public class Infantry : Character
    {
        public override void setStats()
        {
            movement = 3;
            attackRange = 1;
            health = 3;
            attack = 2;
            type = "infantry";
        }

        public override void moveUnit(int x, int y, int id)
        {

            Debug.Log("Esta unidad se mueve " + movement + " de distancia");
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
        public override void setStats()
        {
            movement = 2;
            attackRange = 2;
            health = 4;
            attack = 3;
            type = "tank";
        }
        public override void moveUnit(int x, int y, int id)
        {

            Debug.Log("Esta unidad se mueve " + movement + " de distancia");
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
        public override void setStats()
        {
            movement = 5;
            attackRange = 2;
            health = 2;
            attack = 2;
            type = "aerial";
        }
        public override void moveUnit(int x, int y, int id)
        {
            movement = 4;
            Debug.Log("Esta unidad se mueve " + movement + " de distancia");
            //aqu� c�digo 
        }
        public override void attackUnit()
        {
            attackRange = 2;
            //comprueba si est� en el rango de ataque


            //si lo est� se inicia el enfrentamiento entre las unidades
        }
    }


*/

