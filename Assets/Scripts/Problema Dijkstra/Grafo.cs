using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Grafo : MonoBehaviour
{
    [SerializeField] private RectTransform manecilla;
    [SerializeField] private GameObject nodoPrefab, aristaPrefab;

    public int[][] distancias;

    public List<Nodo> nodos = new List<Nodo>();
    public List<Arista> aristas = new List<Arista>();

    private int nNodos;
    public int nodoInicial;

    public void DibujarGrafo()
    {
        nNodos = distancias.Length;

        string letrasNodos = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
        float angulo = 360 / nNodos;

        Transform manecillaChild = manecilla.GetChild(0).transform;

        // Instancia los nodos
        for (int i=0; i < nNodos; i++)
        {
            GameObject nodoObject = Instantiate(nodoPrefab, manecillaChild.transform.position, Quaternion.identity, this.transform);
            manecilla.eulerAngles = new Vector3(manecilla.rotation.x, manecilla.rotation.y, 0 - angulo*(i+1));
            
            Nodo nodo = nodoObject.GetComponent<Nodo>();
            nodos.Add(nodo);
            nodo.ActualizarNombre(letrasNodos.ToCharArray()[i].ToString());
        }

        // Instancia las aristas
        for (int i = 0; i < distancias.Length; i++)
        {
            for (int j = 0; j < distancias.Length; j++)
            {
                if (distancias[i][j] != 0 && distancias[i][j] != int.MaxValue)
                {
                    GameObject aristaObject = Instantiate(aristaPrefab, nodos[i].transform.position, Quaternion.identity, this.transform);
                    aristaObject.transform.SetSiblingIndex(0);

                    aristaObject.transform.right = nodos[j].transform.position - aristaObject.transform.position;

                    Arista arista = aristaObject.GetComponent<Arista>();
                    aristas.Add(arista);
                    arista.nodoInit = i;
                    arista.nodoFin = j;
                    arista.ActualizarArista(distancias[i][j], Vector2.Distance(aristaObject.transform.localPosition, nodos[j].transform.localPosition));
                
                    arista.pesoTexto.transform.rotation = Quaternion.Euler(0f, 0f, 0f);
                }
            }
        }
    }

    // Dados dos nodos, devuelve la arista que los une
    public Arista GetArista(int n1, int n2)
    {
        for (int i = 0; i < aristas.Count; i++)
        {
            if (aristas[i].nodoInit == n1 && aristas[i].nodoFin == n2)
                return aristas[i];

            if (aristas[i].nodoInit == n2 && aristas[i].nodoFin == n1)
                return aristas[i];
        }
        return aristas[0];
    }

    public void AñadirDistancias(InputField[,] inputs, int tamaño)
    {
        distancias = new int[tamaño][];

        for (int i = 0; i < tamaño; i++)
        {
            int[] n = new int[tamaño];
            for (int j = 0; j < tamaño; j++)
            {
                if (int.Parse(inputs[i,j].text) != (-1))
                    n[j] = int.Parse(inputs[i,j].text);
                else
                    n[j] = int.MaxValue;
            }
            distancias[i] = n;
        }  
    }
}
