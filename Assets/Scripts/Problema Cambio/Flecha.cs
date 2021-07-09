using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flecha : MonoBehaviour
{
    public bool moviendo;
    public Animator animator;
    private Vector2 posDestino;

    void Update()
    {
        // La flecha se desplaza a posDestino
        if (moviendo)
        {
            float step =  100f * Time.deltaTime;
            transform.localPosition = Vector2.MoveTowards(transform.localPosition, posDestino, step);
        }
    }

    public void Mover(Vector2 pos)
    {
        posDestino = pos;
        moviendo = true;
    }
}
