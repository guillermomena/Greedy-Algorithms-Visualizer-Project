using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Nodo : MonoBehaviour
{
    [SerializeField] private Text nombreTexto, distanciaMinimaTexto;
    public GameObject circuloRojo, tickVerde;
    public Animator circuloAnimator, distanciaMinimaAnimator;

    public string nombre;
    public int distanciaMinima;
    
    public void ActualizarNombre(string n)
    {
        nombre = n;
        nombreTexto.text = nombre;
    }

    public void ActualizarDistanciaMinima(int n)
    {
        distanciaMinima = n;

        if (distanciaMinima == int.MaxValue)
        {
            distanciaMinimaTexto.text = "Inf";
        } else {
            distanciaMinimaTexto.text = distanciaMinima.ToString();
        }
        
    }
}
