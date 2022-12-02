using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tilemap {
    
    private Grid<TilemapObject> grid;

    public Tilemap(int width, int height, float cellSize, Vector3 originPosition) {
        grid = new Grid<TilemapObject>(width, height, cellSize, originPosition, (g, x, y) => new TilemapObject(g, x, y));
    }

    public void SetTilemapSprite(Vector3 worldPosition, TilemapObject.TilemapSprite tilemapSprite) {
        TilemapObject tilemapObject = grid.GetGridObject(worldPosition);
        if (tilemapObject != null) {
            tilemapObject.SetTilemapSprite(tilemapSprite);
        }
    }

    public void SetTilemapVisual(TilemapVisual tilemapVisual) {
        tilemapVisual.SetGrid(this, grid);
    }
    
    public class TilemapObject {

        public enum TilemapSprite {
            None,
            Ground,
            Path,
            Dirt,
            Water,
            Mountain
        }

        private Grid<TilemapObject> grid;
        private int x;
        private int y;
        private TilemapSprite tilemapSprite;

        public TilemapObject(Grid<TilemapObject> grid, int x, int y) {
            this.grid = grid;
            this.x = x;
            this.y = y;
        }

        public void SetTilemapSprite(TilemapSprite tilemapSprite) {
            this.tilemapSprite = tilemapSprite;
            grid.TriggerGridObjectChanged(x, y);
        }

        public TilemapSprite GetTilemapSprite() {
            return tilemapSprite;
        }

        public override string ToString() {
            return tilemapSprite.ToString();
        }
    }
}
