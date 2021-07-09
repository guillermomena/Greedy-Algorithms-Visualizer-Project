using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ProblemaCambioInicio : MonoBehaviour
{
    [SerializeField] private InputField cantidadInput;
    [SerializeField] private ProblemaCambio problemaCambio;
    [SerializeField] private Button cantidadBoton, iniciarBoton;
    [SerializeField] private GameObject canvasInicio, canvasEjecucion;
    private bool cantidadAñadida;

    void Start()
    {
        canvasInicio.SetActive(true);
        canvasEjecucion.SetActive(false);
        
        ComprobarBotonesDisponibles();
    }

    public void Iniciar()
    {
        canvasInicio.SetActive(false);
        canvasEjecucion.SetActive(true);

        problemaCambio.EmpezarEjecucion();
    }

    public void AñadirCantidad()
    {
        if (cantidadInput.text != "")
        {
            problemaCambio.cantidad = int.Parse(cantidadInput.text);
            cantidadBoton.interactable = false;
            cantidadInput.text = null;
            cantidadAñadida = true;
            ComprobarBotonesDisponibles();
        }
    }

    public void ComprobarBotonesDisponibles()
    {
        if (cantidadAñadida)
        {
            iniciarBoton.interactable = true;
        } else {
            iniciarBoton.interactable = false;
        }
    }
}
