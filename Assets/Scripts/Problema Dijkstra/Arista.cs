using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Arista : MonoBehaviour
{
    public Text pesoTexto;
    [SerializeField] private RectTransform barra;
    public int nodoInit, nodoFin;
    public Animator animator;

    public int peso;
    public float tamañoBarra;

    public void ActualizarArista(int p, float t)
    {
        peso = p;
        pesoTexto.text = peso.ToString();

        tamañoBarra = t;
        barra.sizeDelta = new Vector2(tamañoBarra, barra.sizeDelta.y);
    }
}
