using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ProblemaCaballoInicio : MonoBehaviour
{
    [SerializeField] private ProblemaCaballo problemaCaballo;
    [SerializeField] private Button añadirTamañoBoton, añadirCasillaBoton, iniciarBoton;
    [SerializeField] private InputField tamañoInput, casillaXInput, casillaYInput;
    [SerializeField] private GameObject canvasInicio, canvasEjecucion;
    private bool tamañoAñadido, casillaAñadida;

    void Start()
    {
        canvasInicio.SetActive(true);
        canvasEjecucion.SetActive(false);
        ComprobarBotonesDisponibles();
    }
    
    public void AñadirTamaño()
    {
        // N tiene que ser mayor que 4 y par
        if (int.Parse(tamañoInput.text) > 4 && int.Parse(tamañoInput.text)%2 == 0)
        {
            problemaCaballo.N = int.Parse(tamañoInput.text);
            añadirTamañoBoton.interactable = false;
            tamañoAñadido = true;
            ComprobarBotonesDisponibles();
        }
    }

    public void AñadirCasillaInicial()
    {
        // La casilla inicial tiene que estar dentro del tablero
        if (int.Parse(casillaXInput.text) >= 0 && int.Parse(casillaXInput.text) < problemaCaballo.N)
        {
            if (int.Parse(casillaYInput.text) >= 0 && int.Parse(casillaYInput.text) < problemaCaballo.N)
            {
                problemaCaballo.colIni = int.Parse(casillaXInput.text);
                problemaCaballo.filaIni = int.Parse(casillaYInput.text);
                casillaAñadida = true;
                ComprobarBotonesDisponibles();
            }
        }
    }

    public void Iniciar()
    {
        canvasInicio.SetActive(false);
        canvasEjecucion.SetActive(true);

        problemaCaballo.EmpezarEjecucion();
    }

    void ComprobarBotonesDisponibles()
    {
        // Hay que haber añadido el tamaño y la casilla inicial para iniciar la ejecución
        if (tamañoAñadido && casillaAñadida)
        {
            iniciarBoton.interactable = true;
        } else {
            iniciarBoton.interactable = false;
        }

        // Hay que haber añadido el tamaño para añadir la casilla inicial
        if (tamañoAñadido && !casillaAñadida)
        {
            añadirCasillaBoton.interactable = true;
        } else {
            añadirCasillaBoton.interactable = false;
        }
    }
}