using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Caballo : MonoBehaviour
{
    public int x, y;

    // Inicia la corutina de mover el caballo (esta función es necesaria porque no se puede
    // iniciar una corutina desde otro script)
    public void Mover(Casilla casilla)
    {
        StartCoroutine("MoverCoroutine", casilla);
    }

    IEnumerator MoverCoroutine(Casilla casilla)
    {
        float duracion;

        // Primero se desplaza en el eje X
        Vector2 posInicial = transform.position;
        Vector2 posFinal = new Vector2(casilla.posX, transform.position.y);

        // Si el desplazamiento en el eje X es de 2 casillas, duracion = 0.5
        // si es de una casilla, duracion = 0.25
        if (Math.Abs(x - casilla.x) > 1) {
            duracion = 0.5f;
        } else {
            duracion = 0.25f;
        }

        // Desplazamiento en el eje X
        for (float t = 0f; t < duracion; t += Time.deltaTime)
        {
            transform.position = Vector2.Lerp(posInicial, posFinal, t / duracion);
            yield return null;
        }
        transform.position = posFinal;

        // Ahora solo se desplaza en el eje Y
        posInicial = transform.position;
        posFinal = new Vector2(transform.position.x, casilla.posY);

        if (Math.Abs(y - casilla.y) > 1) {
            duracion = 0.5f;
        } else {
            duracion = 0.25f;
        }

        // Desplazamiento en el eje Y
        for (float t = 0f; t < duracion; t += Time.deltaTime)
        {
            transform.position = Vector2.Lerp(posInicial, posFinal, t / duracion);
            yield return null;
        }
        transform.position = posFinal;

        x = casilla.x;
        y = casilla.y;
    }

    // Coloca el caballo en el tablero al inicio de la ejecución
    public void ColocarEnTablero(Casilla[,] tablero, Casilla seleccionado, float tamañoCasilla)
    {
        x = seleccionado.x;
        y = seleccionado.y;

        GetComponent<RectTransform>().SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, tamañoCasilla);
        GetComponent<RectTransform>().SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, tamañoCasilla);
    
        transform.position = tablero[seleccionado.x, seleccionado.y].transform.position;
    }
}
