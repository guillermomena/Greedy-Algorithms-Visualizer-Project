using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;

public class ProblemaDijkstra : MonoBehaviour
{
    public Grafo grafo;
    [SerializeField] private Text solTexto, candidatosTexto, seleccionadoTexto;

    private int[] sol;
    private List<int> candidatos = new List<int>();
    private int seleccionado;

    [SerializeField] private GameObject[] highlightsCodigo;

    // Inicia la ejecución del algoritmo
    public void EmpezarEjecucion()
    {
        grafo.DibujarGrafo();
        StartCoroutine("Dijkstra");
        
    }

    // Bucle principal
    IEnumerator Dijkstra()
    {
        yield return new WaitForSeconds(2f);
        ResaltarCodigo(0);
        yield return StartCoroutine(InicializarSol(grafo.distancias, grafo.nodoInicial));

        yield return new WaitForSeconds(2f);
        ResaltarCodigo(1);
        yield return StartCoroutine(InicializarCandidatos(grafo.distancias.Length, grafo.nodoInicial));

        while (candidatos.Count > 1)
        {
            yield return new WaitForSeconds(3f); 

            ResaltarCodigo(2);
            yield return StartCoroutine("Seleccionar");

            yield return new WaitForSeconds(2f);  
            ResaltarCodigo(3);
            yield return StartCoroutine(Actualizar());

            grafo.nodos[seleccionado].circuloAnimator.SetTrigger("White");
        }

        ResaltarCodigo(4);
    }

    IEnumerator InicializarSol(int[][] distancias, int nodoInic)
    {
        sol = distancias[nodoInic];
        ActualizarSolTexto();

        for (int i = 0; i < sol.Length; i++)
        {
            grafo.nodos[i].ActualizarDistanciaMinima(sol[i]);
            grafo.nodos[i].distanciaMinimaAnimator.SetTrigger("Red");
            yield return new WaitForSeconds(1f);
        }    
    }

    IEnumerator InicializarCandidatos(int n, int nodoInic)
    {
        for(int i=0; i < n; i++)
        {
            candidatos.Add(i);
        }

        candidatos.RemoveAt(nodoInic);
        ActualizarCandidatosTexto();

        yield return null;
    }

    // Selecciona el mejor nodo entre los candidatos
    IEnumerator Seleccionar()
    {
        ResaltarCodigo(5);
        int mejor = 0;

        yield return new WaitForSeconds(0.5f);

        ResaltarCodigo(6);
        // El mejor candidato es el que tenga menor distancia minima
        for(int i=0; i < candidatos.Count; i++)
        {
            grafo.nodos[candidatos[i]].circuloAnimator.SetTrigger("Yellow");
            if (sol[candidatos[i]] < sol[candidatos[mejor]])
            {
                mejor = i;
            }
        }

        yield return new WaitForSeconds(2f);

        ResaltarCodigo(7);
        seleccionado = candidatos[mejor];
        seleccionadoTexto.text = "seleccionado = " + seleccionado;

        grafo.nodos[seleccionado].circuloAnimator.SetTrigger("Green");

        yield return new WaitForSeconds(0.5f);
        ResaltarCodigo(8);
        candidatos.RemoveAt(mejor);

        yield return new WaitForSeconds(0.2f);

        for(int i=0; i < candidatos.Count; i++)
        {
            grafo.nodos[candidatos[i]].circuloAnimator.SetTrigger("White");
        }

        yield return new WaitForSeconds(0.5f);

        ResaltarCodigo(9);
        ActualizarCandidatosTexto();  
    }

    // Actualiza las distancias mínimas de cada nodo candidato
    IEnumerator Actualizar()
    {
        for (int i=0; i < candidatos.Count; i++)
        {
            ResaltarCodigo(10);
            grafo.nodos[candidatos[i]].circuloRojo.SetActive(true);

            yield return new WaitForSeconds(1f); 

            // La nueva distancia minima será la menor entre la actual o la suma de la distancia mínima del nodo seleccionado
            // y el peso de la arista que los une
            int min = Mathf.Min(sol[candidatos[i]], SumarValores(sol[seleccionado], grafo.distancias[seleccionado][candidatos[i]]));
            
            if (min == sol[candidatos[i]])
            {
                grafo.nodos[candidatos[i]].distanciaMinimaAnimator.SetTrigger("Green");
            } else {
                Arista arista = grafo.GetArista(candidatos[i], seleccionado);
                arista.animator.SetTrigger("Green");
            }

            sol[candidatos[i]] = min;

            yield return new WaitForSeconds(1f); 

            ResaltarCodigo(11);
            ActualizarSolTexto();
            grafo.nodos[candidatos[i]].ActualizarDistanciaMinima(sol[candidatos[i]]); 

            yield return new WaitForSeconds(0.5f);  

            grafo.nodos[candidatos[i]].circuloRojo.SetActive(false);
            yield return new WaitForSeconds(2f);         
        }
        
        grafo.nodos[seleccionado].tickVerde.SetActive(true);
    }

    int CalcularValorMinimo(int n1, int n2)
    {
        if (n1 < n2)
        {
            return n1;
        } else {
            return n2;
        }
    }

    int SumarValores(int n1, int n2)
    {
        if (n1 == int.MaxValue || n2 == int.MaxValue)
        {
            return int.MaxValue;
        } else {
            return n1+n2;
        }
    }

    void ActualizarSolTexto()
    {
        string texto = "";

        for (int i=0; i < sol.Length; i++)
        {
            if (sol[i] == int.MaxValue)
            {
                texto += "Inf";
            } else {
                texto += sol[i].ToString();
            }
            
            if (i < sol.Length-1)
            {
                texto += ", ";
            }
        }
        solTexto.text = "sol = [" + texto + "]";
    }

    void ActualizarCandidatosTexto()
    {
        string texto = "";

        for (int i=0; i < candidatos.Count; i++)
        {
            texto += candidatos[i].ToString();
            if (i < candidatos.Count-1)
            {
                texto += ", ";
            }
        }
        candidatosTexto.text = "candidatos = [" + texto + "]";
    }

    // Resalta la parte del código que se está ejecutando
    void ResaltarCodigo(int parte)
    {
        for (int i=0; i < highlightsCodigo.Length; i++)
        {
            highlightsCodigo[i].SetActive(false);
        }
        highlightsCodigo[parte].SetActive(true);
    }
}
