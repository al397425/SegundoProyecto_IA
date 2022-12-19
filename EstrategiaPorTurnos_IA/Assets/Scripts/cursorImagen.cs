using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cursorImagen : MonoBehaviour
{
    public Texture2D cursorTextura;

    void Start() {
        {
            Cursor.SetCursor(cursorTextura, Vector2.zero, CursorMode.ForceSoftware);
        }
    }
}
