using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ProblemaDijkstraInicio : MonoBehaviour
{
    [SerializeField] private ProblemaDijkstra problemaDijkstra;
    [SerializeField] private Button iniciarBoton, añadirTamañoBoton, añadirNodoInicialBoton;
    [SerializeField] private GameObject canvasInicio, canvasEjecucion;
    [SerializeField] private InputField tamañoInput, nodoInicialInput;
    [SerializeField] private List<InputField> inputsList;
    private InputField[,] inputs = new InputField[8,8];
    private int tamañoGrafo, nodoInicial;
    private bool nodosRellenados, nodoInicialAñadido, tamañoGrafoAñadido;

    void Start()
    {
        canvasInicio.SetActive(true);
        canvasEjecucion.SetActive(false);
        iniciarBoton.interactable = false;

        ColocarInputs();
    }

    void AñadirDistancias()
    {
        problemaDijkstra.grafo.distancias = new int[tamañoGrafo][];
        for (int i = 0; i < tamañoGrafo; i++)
        {
            for (int j = 0; j < tamañoGrafo; j++)
            {
                if (int.Parse(inputs[i,j].text) != (-1))
                    problemaDijkstra.grafo.distancias[i][j] = int.Parse(inputs[i,j].text);
                else
                    problemaDijkstra.grafo.distancias[i][j] = int.MaxValue;
            }
        }       
    }

    public void Iniciar()
    {
        problemaDijkstra.grafo.AñadirDistancias(inputs, tamañoGrafo);

        canvasInicio.SetActive(false);
        canvasEjecucion.SetActive(true);

        problemaDijkstra.EmpezarEjecucion();
    }

    void ColocarInputs()
    {
        for (int i = 0; i < 8; i++)
        {
            for (int j = 0; j < 8; j++)
            {
                InputField input = inputsList[0];
                inputs[i,j] = input;
                inputsList.RemoveAt(0);
            }
        }
    }

    public void AñadirTamañoGrafo()
    {
        if (nodoInicialAñadido && int.Parse(tamañoInput.text) <= nodoInicial)
            return;

        if (int.Parse(tamañoInput.text) <= 0 || int.Parse(tamañoInput.text) > 8)
            return;

        tamañoGrafo = int.Parse(tamañoInput.text);
        tamañoGrafoAñadido = true;

        for (int i = 0; i < inputs.GetLength(0); i++)
        {
            for (int j = 0; j < inputs.GetLength(1); j++)
            {
                inputs[i,j].gameObject.SetActive(false);
            }
        }   

        for (int i = 0; i < tamañoGrafo; i++)
        {
            for (int j = 0; j < tamañoGrafo; j++)
            {
                inputs[i,j].gameObject.SetActive(true);
            }
        }   

        ComprobarBotonesDisponibles();
        añadirTamañoBoton.interactable = false;
    }

    public void GrafoEjemplo()
    {
        tamañoInput.text = "5";
        AñadirTamañoGrafo();

        nodoInicialInput.text = "0";
        AñadirNodoInicial();

        int[] fila0 = new int[] {0, 5, -1, 3, -1};
        int[] fila1 = new int[] {-1, 0, -1, -1, 2};
        int[] fila2 = new int[] {-1, -1, 0, -1, -1};
        int[] fila3 = new int[] {-1, 1, 11, 0, 6};
        int[] fila4 = new int[] {-1, -1, 1, -1, 0};

        for (int i=0; i < tamañoGrafo; i++)
        {
            inputs[0,i].text = fila0[i].ToString();
            inputs[1,i].text = fila1[i].ToString();
            inputs[2,i].text = fila2[i].ToString();
            inputs[3,i].text = fila3[i].ToString();
            inputs[4,i].text = fila4[i].ToString();
        }
    }

    public void AñadirNodoInicial()
    {
        if (tamañoGrafoAñadido && int.Parse(nodoInicialInput.text) >= tamañoGrafo)
            return;

        if (int.Parse(nodoInicialInput.text) < 0)
            return;

        problemaDijkstra.grafo.nodoInicial = int.Parse(nodoInicialInput.text);
        nodoInicialAñadido = true;

        ComprobarBotonesDisponibles();
        añadirNodoInicialBoton.interactable = false;
    }

    public void ComprobarInputsRellenados()
    {
        for (int i = 0; i < tamañoGrafo; i++)
        {
            for (int j = 0; j < tamañoGrafo; j++)
            {
                if (inputs[i,j].text == "" || inputs[i,j].text == null || int.Parse(inputs[i,j].text) < (-1) || int.Parse(inputs[i,j].text) > 99)
                {
                    return;
                }
            }
        }

        Debug.Log("Inputs rellenados");
        nodosRellenados = true;
        ComprobarBotonesDisponibles();
    }

    public void ComprobarBotonesDisponibles()
    {
        if (tamañoGrafoAñadido && nodoInicialAñadido && nodosRellenados)
        {
            iniciarBoton.interactable = true;
        } else {
            iniciarBoton.interactable = false;
        }
    }
}