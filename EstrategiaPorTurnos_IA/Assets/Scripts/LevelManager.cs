using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public TilemapVisual tilemapVisual;
    private Tilemap tilemap;
    private Tilemap.TilemapObject.TilemapSprite tilemapSprite;

    private Tilemap.TilemapObject.TilemapSprite[,] spriteMatrix = new Tilemap.TilemapObject.TilemapSprite[grid_x_size, grid_y_size];    //Matriz para guardar el tipo de tile y pasarlo al pathfinding para calcular pesos
    private Grid<Tilemap.TilemapObject> tilemapGrid;    //El grid del tilemap para poder sacar cada uno de los objetos y comprobar su tipo de tile
    
    //public CharacterPathfindingMovementHandler characterPathfinding;
    private Pathfinding _pathfinding;   //Pone que no se usa pero realmente si se usa al pasarle el spriteMatrix
    
   

    int x,y;
    static int grid_x_size;
    static int grid_y_size;
    int DivGridx;
    int DivGridy;
    int total_size;
    int adaptative_Quantity;
    private float _timer;
    private void Start()
    {
        grid_x_size = Random.Range(20, 30);
        grid_y_size = Random.Range(20, 30);

        float fDivGridx = (grid_x_size / 20f)*10f;
        DivGridx = (int)fDivGridx;
        float fDivGridy = (grid_y_size / 20f)*10f;
        float fadaptative_Quantity = ((grid_y_size / 20f) / 2f) *10f;
        adaptative_Quantity = (int)fadaptative_Quantity;
        DivGridy = (int)fDivGridy;
        int total_size = grid_x_size * grid_y_size;

        spriteMatrix = new Tilemap.TilemapObject.TilemapSprite[grid_x_size, grid_y_size];
        tilemap = new Tilemap(grid_x_size, grid_y_size, 10f, Vector3.zero);
        tilemap.SetTilemapVisual(tilemapVisual);
       
        GenerateMap();
        
        /*tilemapGrid = tilemap.GetTilemapGrid();
        for (var i = 0; x < grid_x_size; x++)
        {
            for (var j = 0; y < grid_y_size; y++)
            {
                Tilemap.TilemapObject gridObject = tilemapGrid.GetGridObject(i, j);
                spriteMatrix[i, j] = gridObject.GetTilemapSprite();
                
            }
        }*/
        _pathfinding = new Pathfinding(grid_x_size, grid_y_size, spriteMatrix);
        /*_pathfinding.GetNode(2, 0).isRiver = true;
        _pathfinding.GetNode(5, 0).isRiver = true;*/
        _timer = 0f;
    }
    //Esta comentado esto y el update porque el characterPathfindingMovementHandler ya hace eso
    /*private void CalculatePath()  
    {
        var mouseWorldPosition = GetMouseWorldPosition();
        _pathfinding.GetGrid().GetXY(mouseWorldPosition, out int x, out int y);

        List<PathNode> path = _pathfinding.FindPath(0, 0, x, y);
        if (path != null) {
            for (int i=0; i<path.Count - 1; i++) {
                Debug.DrawLine(new Vector3(path[i].x, path[i].y) * 10f + Vector3.one * 5f, new Vector3(path[i+1].x, path[i+1].y) * 10f + Vector3.one * 5f, Color.green, 5f);
                //Debug.Log("ayo bitches");
            }
        }
        characterPathfinding.SetTargetPosition(mouseWorldPosition);
        if (path == null)
            Debug.Log("No path found");
    }
    */
    private void GenerateMap(){
        StartCoroutine(Timer());
    }

    IEnumerator Timer()
    {
        yield return new WaitForSeconds(1f);
        GenerateGround();
        yield return new WaitForSeconds(1f);
        GenerateMountains();
        yield return new WaitForSeconds(1f);
        GenerateLake();
        yield return new WaitForSeconds(1f);
        GeneratePath();
    }

    private void CheckIfTileInside(int possibleX, int possibleY, Tilemap.TilemapObject.TilemapSprite tilemapSprite)
    {
        if (possibleX >= 0 && possibleX < grid_x_size && possibleY >= 0 && possibleY < grid_y_size)
        {
            tilemap.SetTilemapSprite(possibleX, possibleY, tilemapSprite);
            spriteMatrix[possibleX, possibleY] = tilemapSprite;
        }
    }
    
    private void GenerateLake()
    {
        var cantidad = Random.Range(5 + adaptative_Quantity, 9 + adaptative_Quantity);   //Cantidad de lagos. Aleatoria?
        int lakeType;
        int lakeTypeMid;
        int originXMid, originYMid;
        int originX, originY;  //Origen del primer cuadrado del lago. Se haria aleatoriamente. Quizas entre [2, x-2] [y, 3] 
        tilemapSprite = Tilemap.TilemapObject.TilemapSprite.Water;

        for (int i = 0; i < cantidad; i++)
        {
            lakeType = Random.Range(1, 3);
            lakeTypeMid = Random.Range(1, 2);
            switch (lakeTypeMid)
            {
                case 1: //Center of the map

                originXMid = Random.Range(1, grid_x_size - 2);
                originYMid = Random.Range(DivGridy -1, DivGridy +3);
                /*
                tilemap.SetTilemapSprite(originXMid, originYMid, tilemapSprite);
                spriteMatrix[originXMid, originYMid] = Tilemap.TilemapObject.TilemapSprite.Water;

                tilemap.SetTilemapSprite(originXMid + 1, originYMid, tilemapSprite);
                spriteMatrix[originXMid + 1, originYMid] = Tilemap.TilemapObject.TilemapSprite.Water;
                */
                CheckIfTileInside(originXMid +1, originYMid, Tilemap.TilemapObject.TilemapSprite.Water);
                CheckIfTileInside(originXMid +1, originYMid, Tilemap.TilemapObject.TilemapSprite.Water);
                
                break;

                case 2: //Center of the map

                originXMid = Random.Range(1, grid_x_size - 1);
                originYMid = Random.Range(DivGridy -1, DivGridy +3);

                /*tilemap.SetTilemapSprite(originXMid, originYMid, tilemapSprite);
                spriteMatrix[originXMid, originYMid] = Tilemap.TilemapObject.TilemapSprite.Water;

                tilemap.SetTilemapSprite(originXMid + 1, originYMid, tilemapSprite);
                spriteMatrix[originXMid + 1, originYMid] = Tilemap.TilemapObject.TilemapSprite.Water;

                tilemap.SetTilemapSprite(originXMid , originYMid - 1, tilemapSprite);
                spriteMatrix[originXMid , originYMid - 1] = Tilemap.TilemapObject.TilemapSprite.Water;
                */
                CheckIfTileInside(originXMid , originYMid, Tilemap.TilemapObject.TilemapSprite.Water);
                CheckIfTileInside(originXMid +1, originYMid, Tilemap.TilemapObject.TilemapSprite.Water);
                CheckIfTileInside(originXMid +1, originYMid -1, Tilemap.TilemapObject.TilemapSprite.Water);
                break;
            }
            switch (lakeType)
            {
                //Lago cuadrado
                case 1:
                    originX = Random.Range(1, grid_x_size - 1);
                    originY = Random.Range(DivGridy +2, grid_y_size - 3);

                    
                    CheckIfTileInside(originX, originY, Tilemap.TilemapObject.TilemapSprite.Water);
                    CheckIfTileInside(originX + 1, originY, Tilemap.TilemapObject.TilemapSprite.Water);
                    CheckIfTileInside(originX + 1, originY - 1, Tilemap.TilemapObject.TilemapSprite.Water);
                    CheckIfTileInside(originX, originY - 1, Tilemap.TilemapObject.TilemapSprite.Water);
                    //--------------------------------------------------------------------------------//
                    //simetrico//
                    /*
                    tilemap.SetTilemapSprite(originX, originY - DivGridy, tilemapSprite);
                    spriteMatrix[originX, originY - DivGridy] = Tilemap.TilemapObject.TilemapSprite.Water;

                    tilemap.SetTilemapSprite(originX + 1, originY - DivGridy, tilemapSprite);
                    spriteMatrix[originX + 1, originY - DivGridy] = Tilemap.TilemapObject.TilemapSprite.Water;

                    tilemap.SetTilemapSprite(originX, originY - DivGridy, tilemapSprite);
                    spriteMatrix[originX, originY - DivGridy] = Tilemap.TilemapObject.TilemapSprite.Water;

                    tilemap.SetTilemapSprite(originX + 1, originY - DivGridy, tilemapSprite);
                    spriteMatrix[originX + 1, originY - DivGridy] = Tilemap.TilemapObject.TilemapSprite.Water;
*/

                    CheckIfTileInside(originX, originY - DivGridy, Tilemap.TilemapObject.TilemapSprite.Water);
                    CheckIfTileInside(originX + 1, originY - DivGridy, Tilemap.TilemapObject.TilemapSprite.Water);
                    CheckIfTileInside(originX + 1, originY - DivGridy -1, Tilemap.TilemapObject.TilemapSprite.Water);
                    CheckIfTileInside(originX, originY - DivGridy -1, Tilemap.TilemapObject.TilemapSprite.Water);

                    break;
                //Lago diamante
                case 2:
                    originX = Random.Range(2, grid_x_size - 1);
                    originY = Random.Range(DivGridy +3, grid_y_size -3);
/*
                    tilemap.SetTilemapSprite(originX, originY, tilemapSprite);
                    spriteMatrix[originX, originY] = Tilemap.TilemapObject.TilemapSprite.Water;
                    
                    tilemap.SetTilemapSprite(originX, originY + 1, tilemapSprite);
                    spriteMatrix[originX, originY + 1] = Tilemap.TilemapObject.TilemapSprite.Water;

                    tilemap.SetTilemapSprite(originX + 1, originY, tilemapSprite);
                    spriteMatrix[originX + 1, originY] = Tilemap.TilemapObject.TilemapSprite.Water;
                    
                    tilemap.SetTilemapSprite(originX, originY - 1, tilemapSprite);
                    spriteMatrix[originX, originY - 1] = Tilemap.TilemapObject.TilemapSprite.Water;
                    
                    tilemap.SetTilemapSprite(originX - 1, originY, tilemapSprite);
                    spriteMatrix[originX - 1, originY] = Tilemap.TilemapObject.TilemapSprite.Water;
*/
                    CheckIfTileInside(originX, originY, Tilemap.TilemapObject.TilemapSprite.Water);
                    CheckIfTileInside(originX , originY +1, Tilemap.TilemapObject.TilemapSprite.Water);
                    CheckIfTileInside(originX , originY - 1, Tilemap.TilemapObject.TilemapSprite.Water);
                    CheckIfTileInside(originX -1, originY , Tilemap.TilemapObject.TilemapSprite.Water);
                    CheckIfTileInside(originX +1, originY , Tilemap.TilemapObject.TilemapSprite.Water);
                    //--------------------------------------------------------------------------------//
                    //simetrico//
                    /*
                    tilemap.SetTilemapSprite(originX, originY - DivGridy, tilemapSprite);
                    spriteMatrix[originX, originY - DivGridy] = Tilemap.TilemapObject.TilemapSprite.Water;
                    
                    tilemap.SetTilemapSprite(originX, originY - DivGridy + 1 * DivGridx, tilemapSprite);
                    spriteMatrix[originX, originY - DivGridy + 1] = Tilemap.TilemapObject.TilemapSprite.Water;

                    tilemap.SetTilemapSprite(originX + 1, originY - DivGridy, tilemapSprite);
                    spriteMatrix[originX + 1, originY - DivGridy] = Tilemap.TilemapObject.TilemapSprite.Water;
                    
                    tilemap.SetTilemapSprite(originX, originY - DivGridy - 1, tilemapSprite);
                    spriteMatrix[originX, originY - DivGridy - 1] = Tilemap.TilemapObject.TilemapSprite.Water;
                    
                    tilemap.SetTilemapSprite(originX - 1, originY - DivGridy, tilemapSprite);
                    spriteMatrix[originX - 1, originY - DivGridy] = Tilemap.TilemapObject.TilemapSprite.Water;
*/
                    CheckIfTileInside(originX, originY - DivGridy, Tilemap.TilemapObject.TilemapSprite.Water);
                    CheckIfTileInside(originX , originY - DivGridy + 1, Tilemap.TilemapObject.TilemapSprite.Water);
                    CheckIfTileInside(originX +1, originY - DivGridy, Tilemap.TilemapObject.TilemapSprite.Water);
                    CheckIfTileInside(originX , originY - DivGridy -1, Tilemap.TilemapObject.TilemapSprite.Water);
                    CheckIfTileInside(originX -1 , originY - DivGridy, Tilemap.TilemapObject.TilemapSprite.Water);

                    break;
            }
        }

    }
    
    private void GenerateMountains()
    {
        var cantidad = Random.Range(5 + adaptative_Quantity, 9 + adaptative_Quantity);   //Cantidad de lagos. Aleatoria?
        int MountainType;
        int MountainTypeMid;
        int originXMid, originYMid;
        int originX, originY;  //Origen del primer cuadrado del lago. Se haria aleatoriamente. Quizas entre [2, x-2] [y, 3] 
        tilemapSprite = Tilemap.TilemapObject.TilemapSprite.Mountain;
        
        for (int i = 0; i < cantidad; i++)
        {
            MountainType = Random.Range(1, 4);
            MountainTypeMid = Random.Range(1, 3);
            switch (MountainTypeMid)
            {
                case 1: //Center of the map

                originXMid = Random.Range(1, grid_x_size - 2);
                originYMid = Random.Range(DivGridy -1, DivGridy +3);
                /*
                tilemap.SetTilemapSprite(originXMid, originYMid, tilemapSprite);
                spriteMatrix[originXMid, originYMid] = Tilemap.TilemapObject.TilemapSprite.Mountain;*/
                CheckIfTileInside(originXMid, originYMid, Tilemap.TilemapObject.TilemapSprite.Mountain);
                break;

                case 2: //Center of the map

                originXMid = Random.Range(1, grid_x_size - 2);
                originYMid = Random.Range(DivGridy -1, DivGridy +3);
                /*
                tilemap.SetTilemapSprite(originXMid, originYMid, tilemapSprite);
                spriteMatrix[originXMid, originYMid] = Tilemap.TilemapObject.TilemapSprite.Mountain;
                */
                CheckIfTileInside(originXMid, originYMid, Tilemap.TilemapObject.TilemapSprite.Mountain);
                CheckIfTileInside(originXMid +1, originYMid-1, Tilemap.TilemapObject.TilemapSprite.Mountain);
                break;

                case 3:

                originXMid = Random.Range(1, grid_x_size - 2);
                originYMid = Random.Range(DivGridy -1, DivGridy +3);

                CheckIfTileInside(originXMid, originYMid, Tilemap.TilemapObject.TilemapSprite.Mountain);
                CheckIfTileInside(originXMid +1, originYMid-1, Tilemap.TilemapObject.TilemapSprite.Mountain);
                CheckIfTileInside(originXMid +1, originYMid, Tilemap.TilemapObject.TilemapSprite.Mountain);

                break;

            }

            switch (MountainType)
            {
                //mountaña cuadrada
                case 1:
                    
                    originX = Random.Range(1, grid_x_size-1);
                    originY = Random.Range(DivGridy +2, grid_y_size-1);
                    /*
                    tilemap.SetTilemapSprite(originX, originY, tilemapSprite);
                    spriteMatrix[originX, originY] = Tilemap.TilemapObject.TilemapSprite.Mountain;

                    tilemap.SetTilemapSprite(originX + 1, originY, tilemapSprite);
                    spriteMatrix[originX + 1, originY] = Tilemap.TilemapObject.TilemapSprite.Mountain;

                    tilemap.SetTilemapSprite(originX + 1, originY - 1, tilemapSprite);
                    spriteMatrix[originX + 1, originY - 1] = Tilemap.TilemapObject.TilemapSprite.Mountain;

                    tilemap.SetTilemapSprite(originX, originY - 1, tilemapSprite);
                    spriteMatrix[originX, originY - 1] = Tilemap.TilemapObject.TilemapSprite.Mountain;
                    */
                    CheckIfTileInside(originX, originY, Tilemap.TilemapObject.TilemapSprite.Mountain);
                    CheckIfTileInside(originX +1, originY, Tilemap.TilemapObject.TilemapSprite.Mountain);
                    CheckIfTileInside(originX +1, originY-1, Tilemap.TilemapObject.TilemapSprite.Mountain);
                    CheckIfTileInside(originX , originY -1, Tilemap.TilemapObject.TilemapSprite.Mountain);
                    //--------------------------------------------------------------------------------//
                    //simetrico//
                    /*
                    tilemap.SetTilemapSprite(originX, originY - DivGridy, tilemapSprite);
                    spriteMatrix[originX, originY - DivGridy] = Tilemap.TilemapObject.TilemapSprite.Mountain;

                    tilemap.SetTilemapSprite(originX + 1, originY - DivGridy, tilemapSprite);
                    spriteMatrix[originX + 1, originY - DivGridy] = Tilemap.TilemapObject.TilemapSprite.Mountain;

                    tilemap.SetTilemapSprite(originX + 1, originY - DivGridy - 1 , tilemapSprite);
                    spriteMatrix[originX + 1, originY - DivGridy - 1] = Tilemap.TilemapObject.TilemapSprite.Mountain;

                    tilemap.SetTilemapSprite(originX, originY - DivGridy - 1, tilemapSprite);
                    spriteMatrix[originX, originY - DivGridy - 1] = Tilemap.TilemapObject.TilemapSprite.Mountain;
                    */
                    CheckIfTileInside(originX, originY - DivGridy, Tilemap.TilemapObject.TilemapSprite.Mountain);
                    CheckIfTileInside(originX +1, originY- DivGridy, Tilemap.TilemapObject.TilemapSprite.Mountain);
                    CheckIfTileInside(originX +1, originY - DivGridy-1, Tilemap.TilemapObject.TilemapSprite.Mountain);
                    CheckIfTileInside(originX , originY - DivGridy-1, Tilemap.TilemapObject.TilemapSprite.Mountain);

                    break;
                //Montaña diamante
                case 2:

                    originX = Random.Range(1, grid_x_size -1);
                    originY = Random.Range(DivGridy +2, grid_y_size-1);
                    /*
                    tilemap.SetTilemapSprite(originX, originY, tilemapSprite);
                    spriteMatrix[originX, originY] = Tilemap.TilemapObject.TilemapSprite.Mountain;
                    
                    tilemap.SetTilemapSprite(originX, originY + 1, tilemapSprite);
                    spriteMatrix[originX, originY + 1] = Tilemap.TilemapObject.TilemapSprite.Mountain;

                    tilemap.SetTilemapSprite(originX + 1, originY, tilemapSprite);
                    spriteMatrix[originX + 1, originY] = Tilemap.TilemapObject.TilemapSprite.Mountain;
                    
                    tilemap.SetTilemapSprite(originX, originY - 1, tilemapSprite);
                    spriteMatrix[originX, originY - 1] = Tilemap.TilemapObject.TilemapSprite.Mountain;
                    
                    tilemap.SetTilemapSprite(originX - 1, originY, tilemapSprite);
                    spriteMatrix[originX - 1, originY] = Tilemap.TilemapObject.TilemapSprite.Mountain;
                    */
                    CheckIfTileInside(originX, originY , Tilemap.TilemapObject.TilemapSprite.Mountain);
                    CheckIfTileInside(originX , originY +1, Tilemap.TilemapObject.TilemapSprite.Mountain);
                    CheckIfTileInside(originX +1, originY, Tilemap.TilemapObject.TilemapSprite.Mountain);
                    CheckIfTileInside(originX , originY-1, Tilemap.TilemapObject.TilemapSprite.Mountain);
                    CheckIfTileInside(originX -1, originY, Tilemap.TilemapObject.TilemapSprite.Mountain);
                    //--------------------------------------------------------------------------------//
                    //simetrico//
                    /*
                    tilemap.SetTilemapSprite(originX, originY - DivGridy, tilemapSprite);
                    spriteMatrix[originX, originY - DivGridy] = Tilemap.TilemapObject.TilemapSprite.Mountain;
                    
                    tilemap.SetTilemapSprite(originX, originY - DivGridy + 1, tilemapSprite);
                    spriteMatrix[originX, originY - DivGridy + 1] = Tilemap.TilemapObject.TilemapSprite.Mountain;

                    tilemap.SetTilemapSprite(originX + 1, originY - DivGridy , tilemapSprite);
                    spriteMatrix[originX + 1, originY - DivGridy] = Tilemap.TilemapObject.TilemapSprite.Mountain;
                    
                    tilemap.SetTilemapSprite(originX, originY - DivGridy - 1, tilemapSprite);
                    spriteMatrix[originX, originY - DivGridy - 1] = Tilemap.TilemapObject.TilemapSprite.Mountain;
                    
                    tilemap.SetTilemapSprite(originX - 1, originY - DivGridy, tilemapSprite);
                    spriteMatrix[originX - 1, originY - DivGridy] = Tilemap.TilemapObject.TilemapSprite.Mountain;
*/
                    CheckIfTileInside(originX, originY - DivGridy, Tilemap.TilemapObject.TilemapSprite.Mountain);
                    CheckIfTileInside(originX , originY- DivGridy +1, Tilemap.TilemapObject.TilemapSprite.Mountain);
                    CheckIfTileInside(originX +1, originY - DivGridy-1, Tilemap.TilemapObject.TilemapSprite.Mountain);
                    CheckIfTileInside(originX , originY- DivGridy -1, Tilemap.TilemapObject.TilemapSprite.Mountain);
                    CheckIfTileInside(originX -1, originY - DivGridy, Tilemap.TilemapObject.TilemapSprite.Mountain);

                    break;
                    //Montaña sola
                case 3:

                    originX = Random.Range(1, grid_x_size -1);
                    originY = Random.Range(DivGridy +2, grid_y_size -1);
                    CheckIfTileInside(originX, originY, Tilemap.TilemapObject.TilemapSprite.Mountain);
                    /*
                    spriteMatrix[originX, originY] = Tilemap.TilemapObject.TilemapSprite.Mountain;*/
                    //--------------------------------------------------------------------------------//
                    //simetrico//
                    CheckIfTileInside(originX, - DivGridy, Tilemap.TilemapObject.TilemapSprite.Mountain);
                    /*
                    tilemap.SetTilemapSprite(originX, originY - DivGridy, tilemapSprite);
                    spriteMatrix[originX, originY - DivGridy] = Tilemap.TilemapObject.TilemapSprite.Mountain;
                    */

                    break;
                //Montaña L
                case 4:

                    originX = Random.Range(2, grid_x_size -1);
                    originY = Random.Range(DivGridy +2, grid_y_size -1);
                    /*
                    tilemap.SetTilemapSprite(originX, originY, tilemapSprite);
                    spriteMatrix[originX, originY] = Tilemap.TilemapObject.TilemapSprite.Mountain;

                    tilemap.SetTilemapSprite(originX, originY + 1, tilemapSprite);
                    spriteMatrix[originX, originY + 1] = Tilemap.TilemapObject.TilemapSprite.Mountain;

                    tilemap.SetTilemapSprite(originX + 1, originY, tilemapSprite);
                    spriteMatrix[originX + 1, originY] = Tilemap.TilemapObject.TilemapSprite.Mountain;
                    
                    tilemap.SetTilemapSprite(originX +1, originY + 1, tilemapSprite);
                    spriteMatrix[originX +1, originY + 1] = Tilemap.TilemapObject.TilemapSprite.Mountain;
                    
                    tilemap.SetTilemapSprite(originX - 1, originY +1, tilemapSprite);
                    spriteMatrix[originX - 1, originY +1] = Tilemap.TilemapObject.TilemapSprite.Mountain;
                    */
                    CheckIfTileInside(originX, originY , Tilemap.TilemapObject.TilemapSprite.Mountain);
                    CheckIfTileInside(originX , originY +1, Tilemap.TilemapObject.TilemapSprite.Mountain);
                    CheckIfTileInside(originX +1, originY, Tilemap.TilemapObject.TilemapSprite.Mountain);
                    CheckIfTileInside(originX +1, originY+1, Tilemap.TilemapObject.TilemapSprite.Mountain);
                    CheckIfTileInside(originX -1, originY +1, Tilemap.TilemapObject.TilemapSprite.Mountain);
                    //--------------------------------------------------------------------------------//
                    //simetrico//
                    /*
                    tilemap.SetTilemapSprite(originX, originY - DivGridy, tilemapSprite);
                    spriteMatrix[originX, originY - DivGridy] = Tilemap.TilemapObject.TilemapSprite.Mountain;

                    tilemap.SetTilemapSprite(originX, originY - DivGridy +1, tilemapSprite);
                    spriteMatrix[originX, originY +DivGridy +1] = Tilemap.TilemapObject.TilemapSprite.Mountain;

                    tilemap.SetTilemapSprite(originX + 1, originY - DivGridy, tilemapSprite);
                    spriteMatrix[originX + 1, originY - DivGridy] = Tilemap.TilemapObject.TilemapSprite.Mountain;
                    
                    tilemap.SetTilemapSprite(originX +1, originY - DivGridy + 1, tilemapSprite);
                    spriteMatrix[originX +1, originY - DivGridy +1] = Tilemap.TilemapObject.TilemapSprite.Mountain;
                    
                    tilemap.SetTilemapSprite(originX - 1, originY - DivGridy +1, tilemapSprite);
                    spriteMatrix[originX - 1, originY - DivGridy +1] = Tilemap.TilemapObject.TilemapSprite.Mountain;
                    */
                    CheckIfTileInside(originX, originY - DivGridy, Tilemap.TilemapObject.TilemapSprite.Mountain);
                    CheckIfTileInside(originX , originY - DivGridy+1, Tilemap.TilemapObject.TilemapSprite.Mountain);
                    CheckIfTileInside(originX +1, originY- DivGridy, Tilemap.TilemapObject.TilemapSprite.Mountain);
                    CheckIfTileInside(originX +1, originY+1 - DivGridy, Tilemap.TilemapObject.TilemapSprite.Mountain);
                    CheckIfTileInside(originX -1, originY +1 - DivGridy, Tilemap.TilemapObject.TilemapSprite.Mountain);
                    break;
            }
        }

    }
    private void GenerateGround(){
        tilemapSprite = Tilemap.TilemapObject.TilemapSprite.Ground;
        for(var x = 0; x <grid_x_size; x++)
        {
            for(var y = 0; y < grid_y_size; y++)
            {
                tilemap.SetTilemapSprite(x, y, tilemapSprite);
                spriteMatrix[x, y] = Tilemap.TilemapObject.TilemapSprite.Ground;
            }
        }
    }
    private void GeneratePath(){
        var cantidad = Random.Range(5 + adaptative_Quantity, 9 + adaptative_Quantity);   //Cantidad de lagos. Aleatoria?
        int PathType;
        int PathTypeMid;
        int originXMid, originYMid;
        int originX, originY;  //Origen del primer cuadrado del lago. Se haria aleatoriamente. Quizas entre [2, x-2] [y, 3] 
        tilemapSprite = Tilemap.TilemapObject.TilemapSprite.Path;

        for (int i = 0; i < cantidad; i++)
        {
            PathType = Random.Range(1, 4);
            PathTypeMid = Random.Range(1, 3);
            switch (PathTypeMid)
            {
                case 1: //Center of the map

                originXMid = Random.Range(2, grid_x_size -1);
                originYMid = Random.Range(DivGridy -1, DivGridy +3);

                tilemap.SetTilemapSprite(originXMid, originYMid, tilemapSprite);
                spriteMatrix[originXMid, originYMid] = Tilemap.TilemapObject.TilemapSprite.Path;
                break;

                case 2: //Center of the map

                originXMid = Random.Range(4, grid_x_size -2);
                originYMid = Random.Range(DivGridy -1, DivGridy +3);

                tilemap.SetTilemapSprite(originXMid, originYMid, tilemapSprite);
                spriteMatrix[originXMid, originYMid] = Tilemap.TilemapObject.TilemapSprite.Path;

                tilemap.SetTilemapSprite(originXMid + 1, originYMid, tilemapSprite);
                spriteMatrix[originXMid + 1, originYMid] = Tilemap.TilemapObject.TilemapSprite.Path;

                tilemap.SetTilemapSprite(originXMid , originYMid - 1, tilemapSprite);
                spriteMatrix[originXMid , originYMid - 1] = Tilemap.TilemapObject.TilemapSprite.Path;
                break;

                case 3:

                originXMid = Random.Range(4, grid_x_size -3);
                originYMid = Random.Range(DivGridy -1, DivGridy +3);

                tilemap.SetTilemapSprite(originXMid, originYMid, tilemapSprite);
                spriteMatrix[originXMid, originYMid] = Tilemap.TilemapObject.TilemapSprite.Path;

                tilemap.SetTilemapSprite(originXMid , originYMid +1, tilemapSprite);
                spriteMatrix[originXMid, originYMid +1] = Tilemap.TilemapObject.TilemapSprite.Path;
                break;
            }
            switch (PathType)
            {
                //Path vertical
                case 1:
                    originX = Random.Range(4, grid_x_size -3);
                    originY = Random.Range(DivGridy +2, grid_y_size - 5);

                    tilemap.SetTilemapSprite(originX, originY, tilemapSprite);
                    spriteMatrix[originX, originY] = Tilemap.TilemapObject.TilemapSprite.Path;

                    tilemap.SetTilemapSprite(originX , originY +1, tilemapSprite);
                    spriteMatrix[originX , originY +1] = Tilemap.TilemapObject.TilemapSprite.Path;

                    tilemap.SetTilemapSprite(originX , originY +2, tilemapSprite);
                    spriteMatrix[originX , originY +2] = Tilemap.TilemapObject.TilemapSprite.Path;

                    tilemap.SetTilemapSprite(originX , originY +3, tilemapSprite);
                    spriteMatrix[originX , originY +3] = Tilemap.TilemapObject.TilemapSprite.Path;

                    tilemap.SetTilemapSprite(originX , originY +4, tilemapSprite);
                    spriteMatrix[originX , originY +4] = Tilemap.TilemapObject.TilemapSprite.Path;

                    //--------------------------------------------------------------------------------//
                    //simetrico//
                    
                    tilemap.SetTilemapSprite(originX, originY - DivGridy, tilemapSprite);
                    spriteMatrix[originX, originY - DivGridy] = Tilemap.TilemapObject.TilemapSprite.Path;

                    tilemap.SetTilemapSprite(originX, originY - DivGridy + 1, tilemapSprite);
                    spriteMatrix[originX, originY - DivGridy +1] = Tilemap.TilemapObject.TilemapSprite.Path;
 
                    tilemap.SetTilemapSprite(originX , originY - DivGridy + 2, tilemapSprite);
                    spriteMatrix[originX , originY - DivGridy +3] = Tilemap.TilemapObject.TilemapSprite.Path;

                    tilemap.SetTilemapSprite(originX, originY - DivGridy + 3, tilemapSprite);
                    spriteMatrix[originX, originY - DivGridy +3] = Tilemap.TilemapObject.TilemapSprite.Path;

                    tilemap.SetTilemapSprite(originX, originY - DivGridy + 4, tilemapSprite);
                    spriteMatrix[originX, originY - DivGridy + 4] = Tilemap.TilemapObject.TilemapSprite.Path;

                    break;
                //vertical 3
                case 2:
                    originX = Random.Range(4, grid_x_size -2);
                    originY = Random.Range(DivGridy +2, grid_y_size - 5);


                    tilemap.SetTilemapSprite(originX, originY, tilemapSprite);
                    spriteMatrix[originX, originY] = Tilemap.TilemapObject.TilemapSprite.Path;

                    tilemap.SetTilemapSprite(originX , originY +1, tilemapSprite);
                    spriteMatrix[originX , originY +1] = Tilemap.TilemapObject.TilemapSprite.Path;

                    tilemap.SetTilemapSprite(originX , originY +2, tilemapSprite);
                    spriteMatrix[originX , originY +2] = Tilemap.TilemapObject.TilemapSprite.Path;

                    tilemap.SetTilemapSprite(originX , originY +3, tilemapSprite);
                    spriteMatrix[originX , originY +3] = Tilemap.TilemapObject.TilemapSprite.Path;

                    //--------------------------------------------------------------------------------//
                    //simetrico//
                    tilemap.SetTilemapSprite(originX, originY - DivGridy, tilemapSprite);
                    spriteMatrix[originX, originY - DivGridy ] = Tilemap.TilemapObject.TilemapSprite.Path;

                    tilemap.SetTilemapSprite(originX, originY+ DivGridy +1, tilemapSprite);
                    spriteMatrix[originX , originY - DivGridy +1] = Tilemap.TilemapObject.TilemapSprite.Path;

                    tilemap.SetTilemapSprite(originX  , originY - DivGridy +3, tilemapSprite);
                    spriteMatrix[originX , originY - DivGridy +3] = Tilemap.TilemapObject.TilemapSprite.Path;

                    tilemap.SetTilemapSprite(originX , originY - DivGridy +3, tilemapSprite);
                    spriteMatrix[originX , originY - DivGridy +3] = Tilemap.TilemapObject.TilemapSprite.Path;

                    break;
                //horizontal path
                case 3:
                    originX = Random.Range(4, grid_x_size - 2);
                    originY = Random.Range(DivGridy +2, grid_y_size - 5);
                    tilemap.SetTilemapSprite(originX, originY, tilemapSprite);
                    spriteMatrix[originX, originY] = Tilemap.TilemapObject.TilemapSprite.Path;

                    tilemap.SetTilemapSprite(originX +1, originY , tilemapSprite);
                    spriteMatrix[originX +1, originY ] = Tilemap.TilemapObject.TilemapSprite.Path;

                    tilemap.SetTilemapSprite(originX +2, originY , tilemapSprite);
                    spriteMatrix[originX +2, originY ] = Tilemap.TilemapObject.TilemapSprite.Path;

                    tilemap.SetTilemapSprite(originX +3 , originY , tilemapSprite);
                    spriteMatrix[originX +3, originY ] = Tilemap.TilemapObject.TilemapSprite.Path;
                    //--------------------------------------------------------------------------------//
                    //simetrico//
                    tilemap.SetTilemapSprite(originX , originY - DivGridy, tilemapSprite);
                    spriteMatrix[originX , originY - DivGridy] = Tilemap.TilemapObject.TilemapSprite.Path;

                    tilemap.SetTilemapSprite(originX +1, originY - DivGridy,  tilemapSprite);
                    spriteMatrix[originX + 1, originY - DivGridy] = Tilemap.TilemapObject.TilemapSprite.Path;

                    tilemap.SetTilemapSprite(originX +2, originY +DivGridy, tilemapSprite);
                    spriteMatrix[originX + 2, originY - DivGridy] = Tilemap.TilemapObject.TilemapSprite.Path;

                    tilemap.SetTilemapSprite(originX +3 , originY - DivGridy, tilemapSprite);
                    spriteMatrix[originX +3 , originY - DivGridy] = Tilemap.TilemapObject.TilemapSprite.Path;

                    break;
            }
        }
        
    }
}