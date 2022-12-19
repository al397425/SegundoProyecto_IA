using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    public float tamañoMaximo;
    public float tamañoMinimo;
    public float tamañoInicial;
    public float velocidadZoom;

    [SerializeField] private Camera cam;
    
    private float tamaño;
    private Vector3 origenClic;

    void Start()
    {
        tamaño = tamañoInicial;
    }

    private void Update()
    {
        MoverCamera();
        ZoomCamera();
    }

    private void MoverCamera()
    {
        if(Input.GetMouseButtonDown(2))
        {
            origenClic = cam.ScreenToWorldPoint(Input.mousePosition);
        }

        if(Input.GetMouseButton(2))
        {
            Vector3 diferencia = origenClic - cam.ScreenToWorldPoint(Input.mousePosition);

            cam.transform.position += diferencia;
        }
    }

    private void ZoomCamera()
    {
        if(Input.GetAxis("Mouse ScrollWheel") > 0)
        {
            if(tamaño > tamañoMinimo)
                tamaño -= velocidadZoom;
        }

        if(Input.GetAxis("Mouse ScrollWheel") < 0)
        {
            if(tamaño < tamañoMaximo)
                tamaño += velocidadZoom;
        }

        cam.orthographicSize = tamaño;
    }
}
