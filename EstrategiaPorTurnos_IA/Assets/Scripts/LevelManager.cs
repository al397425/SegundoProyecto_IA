using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public TilemapVisual tilemapVisual;
    private Tilemap tilemap;
    private Tilemap.TilemapObject.TilemapSprite tilemapSprite;
    
    private Tilemap.TilemapObject.TilemapSprite[,] spriteMatrix = new Tilemap.TilemapObject.TilemapSprite[grid_x_size, grid_y_simetrico];    //Matriz para guardar el tipo de tile y pasarlo al pathfinding para calcular pesos
    private Grid<Tilemap.TilemapObject> tilemapGrid;    //El grid del tilemap para poder sacar cada uno de los objetos y comprobar su tipo de tile
    
    public CharacterPathfindingMovementHandler characterPathfinding;
    private Pathfinding _pathfinding;
    
    int x,y;
    static int grid_x_size = 20;
    static int grid_y_simetrico = 10;
    static int grid_y_size = 20;
    static int total_size = grid_x_size * grid_y_size;
    private float _timer;
    private void Start()
    {
        spriteMatrix = new Tilemap.TilemapObject.TilemapSprite[grid_x_size, grid_y_size];
        tilemap = new Tilemap(grid_x_size, grid_y_size, 10f, Vector3.zero);
        tilemap.SetTilemapVisual(tilemapVisual);
        GenerateMap();
        
        /*tilemapGrid = tilemap.GetTilemapGrid();
        for (var i = 0; x < grid_x_size; x++)
        {
            for (var j = 0; y < grid_y_simetrico; y++)
            {
                Tilemap.TilemapObject gridObject = tilemapGrid.GetGridObject(i, j);
                spriteMatrix[i, j] = gridObject.GetTilemapSprite();
                
            }
        }*/
        _pathfinding = new Pathfinding(grid_x_size, grid_y_simetrico, spriteMatrix);
        /*_pathfinding.GetNode(2, 0).isRiver = true;
        _pathfinding.GetNode(5, 0).isRiver = true;*/
        _timer = 0f;
    }
    
    private void CalculatePath()
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
    
    private void Update()
    {
        if (Input.GetMouseButton(1) && _timer <= 0f)
        {
            _timer = .1f;
            CalculatePath();
        }
        _timer -= Time.deltaTime;
    }

    private static Vector3 GetMouseWorldPosition()
    {
        var vec = GetMouseWorldPositionWithZ(Input.mousePosition, Camera.main);
        vec.z = 0f;
        return vec;
    }
    private static Vector3 GetMouseWorldPositionWithZ(Vector3 screenPosition, Camera worldCamera)
    {
        var worldPosition = worldCamera.ScreenToWorldPoint(screenPosition);
        return worldPosition;
    }
    
    private void GenerateMap(){
        //Ground -->> tilemapSprite = Tilemap.TilemapObject.TilemapSprite.Ground;
        //Path  -->> tilemapSprite = Tilemap.TilemapObject.TilemapSprite.Path;
        //Water -->> tilemapSprite = Tilemap.TilemapObject.TilemapSprite.Water;
        //Dirt -->> tilemapSprite = Tilemap.TilemapObject.TilemapSprite.Dirt;
        //Mountain -->> tilemapSprite = Tilemap.TilemapObject.TilemapSprite.Mountain;
        /*Invoke(nameof(GenerateGround), .1f);
        Invoke(nameof(GenerateWater), .1f);
        Invoke(nameof(GenerateMountains), .1f);
        Invoke(nameof(GenerateDirt), .1f);
        Invoke(nameof(GeneratePath), .1f);*/
        /*GenerateGround();
        GenerateMountains();
        GenerateWater();
        GenerateDirt();
        GeneratePath();*/
        
        StartCoroutine(Timer());

    }

    IEnumerator Timer()
    {
        yield return new WaitForSeconds(1f);
        GenerateGround();
        /*yield return new WaitForSeconds(1f);
        GenerateDirt();*/
        yield return new WaitForSeconds(1f);
        GenerateMountains();
        yield return new WaitForSeconds(1f);
        GenerateLake();
        yield return new WaitForSeconds(1f);
        GeneratePath();
    }

    private void GenerateLake()
    {
        var cantidad = Random.Range(5, 11);   //Cantidad de lagos. Aleatoria?
        int lakeType;
        int originX, originY;  //Origen del primer cuadrado del lago. Se haria aleatoriamente. Quizas entre [2, x-2] [y, 3] 
        tilemapSprite = Tilemap.TilemapObject.TilemapSprite.Water;
        
        for (int i = 0; i < cantidad; i++)
        {
            lakeType = Random.Range(1, 3);

            switch (lakeType)
            {
                //Lago cuadrado
                case 1:
                    originX = Random.Range(2, grid_x_size - 2);
                    originY = Random.Range(grid_y_simetrico - 1, 3);

                    tilemap.SetTilemapSprite(originX, originY, tilemapSprite);
                    spriteMatrix[originX, originY] = Tilemap.TilemapObject.TilemapSprite.Water;

                    tilemap.SetTilemapSprite(originX + 1, originY, tilemapSprite);
                    spriteMatrix[originX + 1, originY] = Tilemap.TilemapObject.TilemapSprite.Water;

                    tilemap.SetTilemapSprite(originX + 1, originY - 1, tilemapSprite);
                    spriteMatrix[originX + 1, originY - 1] = Tilemap.TilemapObject.TilemapSprite.Water;

                    tilemap.SetTilemapSprite(originX, originY - 1, tilemapSprite);
                    spriteMatrix[originX, originY - 1] = Tilemap.TilemapObject.TilemapSprite.Water;
                    //--------------------------------------------------------------------------------//
                    //simetrico//
                    
                    tilemap.SetTilemapSprite(originX, originY+10, tilemapSprite);
                    spriteMatrix[originX, originY+10] = Tilemap.TilemapObject.TilemapSprite.Water;

                    tilemap.SetTilemapSprite(originX + 1, originY+10, tilemapSprite);
                    spriteMatrix[originX + 1, originY+10] = Tilemap.TilemapObject.TilemapSprite.Water;

                    tilemap.SetTilemapSprite(originX + 1, originY+10 - 1, tilemapSprite);
                    spriteMatrix[originX + 1, originY+10 - 1] = Tilemap.TilemapObject.TilemapSprite.Water;

                    tilemap.SetTilemapSprite(originX, originY+10 - 1, tilemapSprite);
                    spriteMatrix[originX, originY+10 - 1] = Tilemap.TilemapObject.TilemapSprite.Water;
                    break;
                //Lago diamante
                case 2:
                    originX = Random.Range(3, grid_x_size - 2);
                    originY = Random.Range(grid_y_simetrico - 1, 3);
                    
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
                    //--------------------------------------------------------------------------------//
                    //simetrico//
                    
                    tilemap.SetTilemapSprite(originX, originY+10, tilemapSprite);
                    spriteMatrix[originX, originY+10] = Tilemap.TilemapObject.TilemapSprite.Water;
                    
                    tilemap.SetTilemapSprite(originX, originY+10 + 1, tilemapSprite);
                    spriteMatrix[originX, originY+10 + 1] = Tilemap.TilemapObject.TilemapSprite.Water;

                    tilemap.SetTilemapSprite(originX + 1, originY+10, tilemapSprite);
                    spriteMatrix[originX + 1, originY+10] = Tilemap.TilemapObject.TilemapSprite.Water;
                    
                    tilemap.SetTilemapSprite(originX, originY+10 - 1, tilemapSprite);
                    spriteMatrix[originX, originY+10 - 1] = Tilemap.TilemapObject.TilemapSprite.Water;
                    
                    tilemap.SetTilemapSprite(originX - 1, originY+10, tilemapSprite);
                    spriteMatrix[originX - 1, originY+10] = Tilemap.TilemapObject.TilemapSprite.Water;
                    break;
            }
        }
    }
    
    private void GenerateMountains()
    {
        var cantidad = Random.Range(5, 11);   //Cantidad de lagos. Aleatoria?
        int MountainType;
        int originX, originY;  //Origen del primer cuadrado del lago. Se haria aleatoriamente. Quizas entre [2, x-2] [y, 3] 
        tilemapSprite = Tilemap.TilemapObject.TilemapSprite.Mountain;
        
        for (int i = 0; i < cantidad; i++)
        {
            MountainType = Random.Range(1, 4);

            switch (MountainType)
            {
                //mountaña cuadrada
                case 1:
                    originX = Random.Range(2, grid_x_size - 2);
                    originY = Random.Range(grid_y_simetrico - 1, 3);

                    tilemap.SetTilemapSprite(originX, originY, tilemapSprite);
                    spriteMatrix[originX, originY] = Tilemap.TilemapObject.TilemapSprite.Mountain;

                    tilemap.SetTilemapSprite(originX + 1, originY, tilemapSprite);
                    spriteMatrix[originX + 1, originY] = Tilemap.TilemapObject.TilemapSprite.Mountain;

                    tilemap.SetTilemapSprite(originX + 1, originY - 1, tilemapSprite);
                    spriteMatrix[originX + 1, originY - 1] = Tilemap.TilemapObject.TilemapSprite.Mountain;

                    tilemap.SetTilemapSprite(originX, originY - 1, tilemapSprite);
                    spriteMatrix[originX, originY - 1] = Tilemap.TilemapObject.TilemapSprite.Mountain;
                    //--------------------------------------------------------------------------------//
                    //simetrico//
                    
                    tilemap.SetTilemapSprite(originX, originY+10, tilemapSprite);
                    spriteMatrix[originX, originY+10] = Tilemap.TilemapObject.TilemapSprite.Mountain;

                    tilemap.SetTilemapSprite(originX + 1, originY+10, tilemapSprite);
                    spriteMatrix[originX + 1, originY+10] = Tilemap.TilemapObject.TilemapSprite.Mountain;

                    tilemap.SetTilemapSprite(originX + 1, originY+10 - 1, tilemapSprite);
                    spriteMatrix[originX + 1, originY+10 - 1] = Tilemap.TilemapObject.TilemapSprite.Mountain;

                    tilemap.SetTilemapSprite(originX, originY+10 - 1, tilemapSprite);
                    spriteMatrix[originX, originY+10 - 1] = Tilemap.TilemapObject.TilemapSprite.Mountain;
                    break;
                //Montaña diamante
                case 2:
                    originX = Random.Range(3, grid_x_size - 2);
                    originY = Random.Range(grid_y_simetrico - 1, 3);
                    
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
                    //--------------------------------------------------------------------------------//
                    //simetrico//
                    
                    tilemap.SetTilemapSprite(originX, originY+10, tilemapSprite);
                    spriteMatrix[originX, originY+10] = Tilemap.TilemapObject.TilemapSprite.Mountain;
                    
                    tilemap.SetTilemapSprite(originX, originY+10 + 1, tilemapSprite);
                    spriteMatrix[originX, originY+10 + 1] = Tilemap.TilemapObject.TilemapSprite.Mountain;

                    tilemap.SetTilemapSprite(originX + 1, originY+10, tilemapSprite);
                    spriteMatrix[originX + 1, originY+10] = Tilemap.TilemapObject.TilemapSprite.Mountain;
                    
                    tilemap.SetTilemapSprite(originX, originY+10 - 1, tilemapSprite);
                    spriteMatrix[originX, originY+10 - 1] = Tilemap.TilemapObject.TilemapSprite.Mountain;
                    
                    tilemap.SetTilemapSprite(originX - 1, originY+10, tilemapSprite);
                    spriteMatrix[originX - 1, originY+10] = Tilemap.TilemapObject.TilemapSprite.Mountain;
                    break;
                    //Montaña sola
                case 3:
                    originX = Random.Range(3, grid_x_size - 2);
                    originY = Random.Range(grid_y_simetrico - 1, 3);
    
                    tilemap.SetTilemapSprite(originX, originY, tilemapSprite);
                    spriteMatrix[originX, originY] = Tilemap.TilemapObject.TilemapSprite.Mountain;
                    //--------------------------------------------------------------------------------//
                    //simetrico//
                    tilemap.SetTilemapSprite(originX, originY+10, tilemapSprite);
                    spriteMatrix[originX, originY+10] = Tilemap.TilemapObject.TilemapSprite.Mountain;
                    break;
                //Montaña L
                case 4:
                    originX = Random.Range(3, grid_x_size - 2);
                    originY = Random.Range(grid_y_simetrico - 1, 3);
    
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
                    //--------------------------------------------------------------------------------//
                    //simetrico//
                    tilemap.SetTilemapSprite(originX, originY+10, tilemapSprite);
                    spriteMatrix[originX, originY+10] = Tilemap.TilemapObject.TilemapSprite.Mountain;
                    tilemap.SetTilemapSprite(originX, originY + 11, tilemapSprite);
                    spriteMatrix[originX, originY +11] = Tilemap.TilemapObject.TilemapSprite.Mountain;

                    tilemap.SetTilemapSprite(originX + 1, originY +10, tilemapSprite);
                    spriteMatrix[originX + 1, originY +10] = Tilemap.TilemapObject.TilemapSprite.Mountain;
                    
                    tilemap.SetTilemapSprite(originX +1, originY + 11, tilemapSprite);
                    spriteMatrix[originX +1, originY+11 ] = Tilemap.TilemapObject.TilemapSprite.Mountain;
                    
                    tilemap.SetTilemapSprite(originX - 1, originY +11, tilemapSprite);
                    spriteMatrix[originX - 1, originY +11] = Tilemap.TilemapObject.TilemapSprite.Mountain;

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
        var cantidad = Random.Range(5, 11);   //Cantidad de lagos. Aleatoria?
        int PathType;
        int originX, originY;  //Origen del primer cuadrado del lago. Se haria aleatoriamente. Quizas entre [2, x-2] [y, 3] 
        tilemapSprite = Tilemap.TilemapObject.TilemapSprite.Path;
        
        for (int i = 0; i < cantidad; i++)
        {
            PathType = Random.Range(1, 4);

            switch (PathType)
            {
                //Path vertical
                case 1:
                    originX = Random.Range(2, grid_x_size - 2);
                    originY = Random.Range(grid_y_simetrico - 1, 3);

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
                    
                    tilemap.SetTilemapSprite(originX, originY+10, tilemapSprite);
                    spriteMatrix[originX, originY+10] = Tilemap.TilemapObject.TilemapSprite.Path;

                    tilemap.SetTilemapSprite(originX, originY+11, tilemapSprite);
                    spriteMatrix[originX + 1, originY+11] = Tilemap.TilemapObject.TilemapSprite.Path;

                    tilemap.SetTilemapSprite(originX , originY+12, tilemapSprite);
                    spriteMatrix[originX + 1, originY+12 ] = Tilemap.TilemapObject.TilemapSprite.Path;

                    tilemap.SetTilemapSprite(originX, originY+13, tilemapSprite);
                    spriteMatrix[originX, originY+13 ] = Tilemap.TilemapObject.TilemapSprite.Path;

                    tilemap.SetTilemapSprite(originX, originY+14, tilemapSprite);
                    spriteMatrix[originX, originY+14 ] = Tilemap.TilemapObject.TilemapSprite.Path;
                    break;
                //vertical 3
                case 2:
                    originX = Random.Range(2, grid_x_size - 2);
                    originY = Random.Range(grid_y_simetrico - 1, 3);

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
                    
                    tilemap.SetTilemapSprite(originX, originY+10, tilemapSprite);
                    spriteMatrix[originX, originY+10] = Tilemap.TilemapObject.TilemapSprite.Path;

                    tilemap.SetTilemapSprite(originX, originY+11, tilemapSprite);
                    spriteMatrix[originX + 1, originY+11] = Tilemap.TilemapObject.TilemapSprite.Path;

                    tilemap.SetTilemapSprite(originX , originY+12, tilemapSprite);
                    spriteMatrix[originX + 1, originY+12 ] = Tilemap.TilemapObject.TilemapSprite.Path;

                    tilemap.SetTilemapSprite(originX, originY+13, tilemapSprite);
                    spriteMatrix[originX, originY+13 ] = Tilemap.TilemapObject.TilemapSprite.Path;

                    break;
                //horizontal path
                case 3:
                    originX = Random.Range(2, grid_x_size - 2);
                    originY = Random.Range(grid_y_simetrico - 1, 3);

                    tilemap.SetTilemapSprite(originX, originY, tilemapSprite);
                    spriteMatrix[originX, originY] = Tilemap.TilemapObject.TilemapSprite.Path;

                    tilemap.SetTilemapSprite(originX +1, originY , tilemapSprite);
                    spriteMatrix[originX +1, originY ] = Tilemap.TilemapObject.TilemapSprite.Path;

                    tilemap.SetTilemapSprite(originX +2, originY , tilemapSprite);
                    spriteMatrix[originX +2, originY ] = Tilemap.TilemapObject.TilemapSprite.Path;

                    tilemap.SetTilemapSprite(originX +3 , originY , tilemapSprite);
                    spriteMatrix[originX +3, originY ] = Tilemap.TilemapObject.TilemapSprite.Path;

                    tilemap.SetTilemapSprite(originX +4 , originY, tilemapSprite);
                    spriteMatrix[originX +4 , originY ] = Tilemap.TilemapObject.TilemapSprite.Path;

                    //--------------------------------------------------------------------------------//
                    //simetrico//
                    
                    tilemap.SetTilemapSprite(originX +10, originY, tilemapSprite);
                    spriteMatrix[originX +10, originY] = Tilemap.TilemapObject.TilemapSprite.Path;

                    tilemap.SetTilemapSprite(originX +11, originY, tilemapSprite);
                    spriteMatrix[originX + 11, originY] = Tilemap.TilemapObject.TilemapSprite.Path;

                    tilemap.SetTilemapSprite(originX +12, originY, tilemapSprite);
                    spriteMatrix[originX + 12, originY ] = Tilemap.TilemapObject.TilemapSprite.Path;

                    tilemap.SetTilemapSprite(originX +13 , originY, tilemapSprite);
                    spriteMatrix[originX +13 , originY] = Tilemap.TilemapObject.TilemapSprite.Path;

                    tilemap.SetTilemapSprite(originX +14, originY, tilemapSprite);
                    spriteMatrix[originX +14, originY] = Tilemap.TilemapObject.TilemapSprite.Path;
                    break;

            }
        }
    }
}

/*
    private void GenerateMountains(){
        tilemapSprite = Tilemap.TilemapObject.TilemapSprite.Mountain;
        Vector3 newcoords;
        int RandomMountains = Random.Range(total_size/8, total_size/4 ); //Random Quantity
         for(int i = 0; i < RandomMountains; i++){ //Random position
        Vector3 RandomNumber = new Vector3(Random.Range(10, total_size/2 + total_size/8),
        Random.Range(10, total_size/2 + total_size/8),Random.Range(10, total_size/2 + total_size/8));
            tilemap.SetTilemapSprite(RandomNumber, tilemapSprite);
            
            tilemap.GetTilemapGrid().GetXY(RandomNumber, out int a, out int b);
            if (a < grid_x_size && b < grid_y_simetrico)
                spriteMatrix[a, b] = Tilemap.TilemapObject.TilemapSprite.Mountain;
            
            //Tiles arround the original random tile
            for(int z = 0; z < Random.Range(4,6); z++){
                int random = Random.Range(1,6);
                if(random == 1){
                    tilemap.SetTilemapSprite(newcoords = RandomNumber + new Vector3(1, 1, 1), tilemapSprite);
                    
                    tilemap.GetTilemapGrid().GetXY(RandomNumber, out a, out b);
                    if (a < grid_x_size && b < grid_y_simetrico)
                        spriteMatrix[a, b] = Tilemap.TilemapObject.TilemapSprite.Mountain;
                    
                }else if(random == 2){
                    tilemap.SetTilemapSprite(newcoords = RandomNumber - new Vector3(1, 1, 1), tilemapSprite);
                    
                    tilemap.GetTilemapGrid().GetXY(RandomNumber, out a, out b);
                    if (a < grid_x_size && b < grid_y_simetrico)
                        spriteMatrix[a, b] = Tilemap.TilemapObject.TilemapSprite.Mountain;
                    
                }else if(random == 3){
                    tilemap.SetTilemapSprite(newcoords = RandomNumber + new Vector3(0, 1, 1), tilemapSprite);
                    
                    tilemap.GetTilemapGrid().GetXY(RandomNumber, out a, out b);
                    if (a < grid_x_size && b < grid_y_simetrico)
                        spriteMatrix[a, b] = Tilemap.TilemapObject.TilemapSprite.Mountain;
                
                }else if(random == 4){
                    tilemap.SetTilemapSprite(newcoords = RandomNumber - new Vector3(0, 1, 1), tilemapSprite);
                    
                    tilemap.GetTilemapGrid().GetXY(RandomNumber, out a, out b);
                    if (a < grid_x_size && b < grid_y_simetrico)
                        spriteMatrix[a, b] = Tilemap.TilemapObject.TilemapSprite.Mountain;
                
                }else if(random == 5){
                    tilemap.SetTilemapSprite(newcoords = RandomNumber + new Vector3(1, 0, 1), tilemapSprite);
                    
                    tilemap.GetTilemapGrid().GetXY(RandomNumber, out a, out b);
                    if (a < grid_x_size && b < grid_y_simetrico)
                        spriteMatrix[a, b] = Tilemap.TilemapObject.TilemapSprite.Mountain;
                
                }else if(random == 6){
                    tilemap.SetTilemapSprite(newcoords = RandomNumber - new Vector3(1, 0, 1), tilemapSprite);
                    
                    tilemap.GetTilemapGrid().GetXY(RandomNumber, out a, out b);
                    if (a < grid_x_size && b < grid_y_simetrico)
                        spriteMatrix[a, b] = Tilemap.TilemapObject.TilemapSprite.Mountain;
                }
        }
        }
            
    }*/
/*
    private void GenerateWater(){
        Vector3 newcoords;
        int RandomWater = Random.Range(total_size/8, total_size/4); //Random Quantity
         for(int i = 0; i < RandomWater; i++){ //Random position
         tilemapSprite = Tilemap.TilemapObject.TilemapSprite.Water;
        Vector3 RandomNumber = new Vector3(Random.Range(10, total_size/2 + total_size/8),
        Random.Range(10, total_size/2 + total_size/8),Random.Range(10, total_size/2 + total_size/8));
            tilemap.SetTilemapSprite(RandomNumber, tilemapSprite);
            
            tilemap.GetTilemapGrid().GetXY(RandomNumber, out int a, out int b);
            if (a < grid_x_size && b < grid_y_simetrico)
                spriteMatrix[a, b] = Tilemap.TilemapObject.TilemapSprite.Water;
            
            //Tiles arround the original random tile
            for(int z = 0; z < Random.Range(1,3); z++){
                int random = Random.Range(1,6);
                if(random == 1){
                    tilemapSprite = Tilemap.TilemapObject.TilemapSprite.Water;
                    tilemap.SetTilemapSprite(newcoords = RandomNumber + new Vector3(1, 1, 1), tilemapSprite);
                    
                    tilemap.GetTilemapGrid().GetXY(RandomNumber, out a, out b);
                    if (a < grid_x_size && b < grid_y_simetrico)
                        spriteMatrix[a, b] = Tilemap.TilemapObject.TilemapSprite.Water;
                    //tilemapSprite = Tilemap.TilemapObject.TilemapSprite.Path;
                    //tilemap.SetTilemapSprite(newcoords = RandomNumber + new Vector3(Random.Range(-1,1), Random.Range(-1,1), 1), tilemapSprite);
                }else if(random == 2){
                    tilemapSprite = Tilemap.TilemapObject.TilemapSprite.Water;
                    tilemap.SetTilemapSprite(newcoords = RandomNumber - new Vector3(1, 1, 1), tilemapSprite);
                    
                    tilemap.GetTilemapGrid().GetXY(RandomNumber, out a, out b);
                    if (a < grid_x_size && b < grid_y_simetrico)
                        spriteMatrix[a, b] = Tilemap.TilemapObject.TilemapSprite.Water;

                }else if(random == 3){
                    tilemapSprite = Tilemap.TilemapObject.TilemapSprite.Water;
                    tilemap.SetTilemapSprite(newcoords = RandomNumber + new Vector3(0, 1, 1), tilemapSprite);
                    
                    tilemap.GetTilemapGrid().GetXY(RandomNumber, out a, out b);
                    if (a < grid_x_size && b < grid_y_simetrico)
                        spriteMatrix[a, b] = Tilemap.TilemapObject.TilemapSprite.Water;
                
                }else if(random == 4){
                    tilemapSprite = Tilemap.TilemapObject.TilemapSprite.Water;
                    tilemap.SetTilemapSprite(newcoords = RandomNumber - new Vector3(0, 1, 1), tilemapSprite);
                    
                    tilemap.GetTilemapGrid().GetXY(RandomNumber, out a, out b);
                    if (a < grid_x_size && b < grid_y_simetrico)
                        spriteMatrix[a, b] = Tilemap.TilemapObject.TilemapSprite.Water;
                
                }else if(random == 5){
                    tilemapSprite = Tilemap.TilemapObject.TilemapSprite.Water;
                    tilemap.SetTilemapSprite(newcoords = RandomNumber + new Vector3(1, 0, 1), tilemapSprite);
                    
                    tilemap.GetTilemapGrid().GetXY(RandomNumber, out a, out b);
                    if (a < grid_x_size && b < grid_y_simetrico)
                        spriteMatrix[a, b] = Tilemap.TilemapObject.TilemapSprite.Water;
                
                }else if(random == 6){
                    tilemapSprite = Tilemap.TilemapObject.TilemapSprite.Water;
                    tilemap.SetTilemapSprite(newcoords = RandomNumber - new Vector3(1, 0, 1), tilemapSprite);
                    
                    tilemap.GetTilemapGrid().GetXY(RandomNumber, out a, out b);
                    if (a < grid_x_size && b < grid_y_simetrico)
                        spriteMatrix[a, b] = Tilemap.TilemapObject.TilemapSprite.Water;
                }
        }
        }
    }*/
    /*
    private void GeneratePath(){
        tilemapSprite = Tilemap.TilemapObject.TilemapSprite.Path;
        Vector3 newcoords;
        int RandomMountains = Random.Range(total_size/16, total_size/8 ); //Random Quantity
         for(int i = 0; i < RandomMountains; i++){ //Random position
        Vector3 RandomNumber = new Vector3(Random.Range(10, total_size/2 + total_size/8),
        Random.Range(10, total_size/2 + total_size/8),Random.Range(10, total_size/2 + total_size/8));
            tilemap.SetTilemapSprite(RandomNumber, tilemapSprite);
            
            tilemap.GetTilemapGrid().GetXY(RandomNumber, out int a, out int b);
            if (a < grid_x_size && b < grid_y_simetrico)
                spriteMatrix[a, b] = Tilemap.TilemapObject.TilemapSprite.Path;
            //Tiles arround the original random tile
            int random = Random.Range(1,4);
            for(int z = 0; z < Random.Range(4,6); z++){
                if(random == 1){
                    tilemap.SetTilemapSprite(newcoords = RandomNumber + new Vector3(z, 0, 1), tilemapSprite);
                    
                    tilemap.GetTilemapGrid().GetXY(RandomNumber, out a, out b);
                    if (a < grid_x_size && b < grid_y_simetrico)
                        spriteMatrix[a, b] = Tilemap.TilemapObject.TilemapSprite.Path;
                    
                }else if(random == 2){
                    tilemap.SetTilemapSprite(newcoords = RandomNumber - new Vector3(z, 0, 1), tilemapSprite);
                    
                    tilemap.GetTilemapGrid().GetXY(RandomNumber, out a, out b);
                    if (a < grid_x_size && b < grid_y_simetrico)
                        spriteMatrix[a, b] = Tilemap.TilemapObject.TilemapSprite.Path;
                    
                }else if(random == 3){
                    tilemap.SetTilemapSprite(newcoords = RandomNumber + new Vector3(0, z, 1), tilemapSprite);
                    
                    tilemap.GetTilemapGrid().GetXY(RandomNumber, out a, out b);
                    if (a < grid_x_size && b < grid_y_simetrico)
                        spriteMatrix[a, b] = Tilemap.TilemapObject.TilemapSprite.Path;
                
                }else if(random == 4){
                    tilemap.SetTilemapSprite(newcoords = RandomNumber - new Vector3(0, z, 1), tilemapSprite);
                    tilemap.GetTilemapGrid().GetXY(RandomNumber, out a, out b);
                    if (a < grid_x_size && b < grid_y_simetrico)
                        spriteMatrix[a, b] = Tilemap.TilemapObject.TilemapSprite.Path;
        }
        }
            
    }
}
}
*/
/*originX = Random.Range(3, grid_x_size - 2);
                    originY = Random.Range(grid_y_simetrico - 1, 3);
                    //Origen centro
                    tilemap.SetTilemapSprite(originX, originY, tilemapSprite);
                    spriteMatrix[originX, originY] = Tilemap.TilemapObject.TilemapSprite.Water;
                    
                    //Cuadrado alrededor de origen
                    tilemap.SetTilemapSprite(originX, originY + 1, tilemapSprite);
                    spriteMatrix[originX + 1, originY] = Tilemap.TilemapObject.TilemapSprite.Water;

                    tilemap.SetTilemapSprite(originX + 1, originY + 1, tilemapSprite);
                    spriteMatrix[originX + 1, originY - 1] = Tilemap.TilemapObject.TilemapSprite.Water;

                    tilemap.SetTilemapSprite(originX + 1, originY, tilemapSprite);
                    spriteMatrix[originX, originY - 1] = Tilemap.TilemapObject.TilemapSprite.Water;
                    
                    tilemap.SetTilemapSprite(originX + 1, originY - 1, tilemapSprite);
                    spriteMatrix[originX, originY - 1] = Tilemap.TilemapObject.TilemapSprite.Water;
                    
                    tilemap.SetTilemapSprite(originX, originY - 1, tilemapSprite);
                    spriteMatrix[originX, originY - 1] = Tilemap.TilemapObject.TilemapSprite.Water;
                    
                    tilemap.SetTilemapSprite(originX - 1, originY - 1, tilemapSprite);
                    spriteMatrix[originX, originY - 1] = Tilemap.TilemapObject.TilemapSprite.Water;
                    
                    tilemap.SetTilemapSprite(originX - 1, originY, tilemapSprite);
                    spriteMatrix[originX, originY - 1] = Tilemap.TilemapObject.TilemapSprite.Water;
                    
                    tilemap.SetTilemapSprite(originX - 1, originY + 1, tilemapSprite);
                    spriteMatrix[originX, originY - 1] = Tilemap.TilemapObject.TilemapSprite.Water;
                    
                    //Puntas del diamante
                    tilemap.SetTilemapSprite(originX, originY + 2, tilemapSprite);
                    spriteMatrix[originX, originY - 1] = Tilemap.TilemapObject.TilemapSprite.Water;
                    
                    tilemap.SetTilemapSprite(originX + 2, originY, tilemapSprite);
                    spriteMatrix[originX, originY - 1] = Tilemap.TilemapObject.TilemapSprite.Water;
                    
                    tilemap.SetTilemapSprite(originX, originY - 2, tilemapSprite);
                    spriteMatrix[originX, originY - 1] = Tilemap.TilemapObject.TilemapSprite.Water;
                    
                    tilemap.SetTilemapSprite(originX - 2, originY, tilemapSprite);
                    spriteMatrix[originX, originY - 1] = Tilemap.TilemapObject.TilemapSprite.Water;*/
