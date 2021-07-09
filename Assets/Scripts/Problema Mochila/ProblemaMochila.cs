using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ProblemaMochila : MonoBehaviour
{
    public List<ObjetoMochila> objetos = new List<ObjetoMochila>(); // Lista con los valores V, W, V/W

    public List<Vector2> posicionesLista = new List<Vector2>();

    [SerializeField] Mochila mochila;
    public int n, W, i, peso;
    public List<float> solucion = new List<float>();

    [SerializeField] Text nTexto, WTexto, solucionTexto, iTexto, pesoTexto;

    [SerializeField] GameObject[] highlightsCodigo;

    public void EmpezarEjecucion()
    {
        nTexto.text = "n = " + n.ToString();
        WTexto.text = "W = " + W.ToString();

        StartCoroutine("CalcularVW");
    }

    IEnumerator MostrarVariables()
    {
        yield return new WaitForSeconds(1f);

        ResaltarCodigo(2);
        ActualizarSolucion(0, 0);
        yield return new WaitForSeconds(1f);

        ResaltarCodigo(3);
        pesoTexto.text = "peso = " + peso.ToString();
        yield return new WaitForSeconds(1f);

        ResaltarCodigo(4);
        iTexto.text = "i = " + i.ToString();
        yield return new WaitForSeconds(1f);

        StartCoroutine("ComprobarObjetos");
    }

    // Calcula el valor V/W de todos los objetos
    IEnumerator CalcularVW()
    {
        ResaltarCodigo(0);
        int numeroObjetos = objetos.Count;

        for (int i = 0; i < numeroObjetos; i++)
        {
            yield return new WaitForSeconds(0.6f);

            objetos[i].CalcularVW(); 
        }

        yield return new WaitForSeconds(1f);
        StartCoroutine("OrdenarObjetos");
    }

    // Ordena la lista de objetos según su valor V/W
    IEnumerator OrdenarObjetos()
    {
        ResaltarCodigo(1);
        for(int p = 0; p < objetos.Count; p++)
        {
            posicionesLista.Add(objetos[p].transform.position);
        }

        objetos.Sort((objeto1,objeto2)=>objeto1.vw.CompareTo(objeto2.vw));
        objetos.Reverse();
        
        for(int p = 0; p < objetos.Count; p++)
        {
            objetos[p].layoutElement.ignoreLayout = true;
            objetos[p].transform.SetAsLastSibling();
        }

        // Animacion
        for(int p = 0; p < objetos.Count; p++)
        {
            objetos[p].MoverObjeto(posicionesLista[p]);
        }

        yield return new WaitForSeconds(1.5f);

        foreach(var objeto in objetos)
        {
            objeto.layoutElement.ignoreLayout = false;
        }

        yield return 0;
        StartCoroutine("MostrarVariables");
    }

    IEnumerator ComprobarObjetos()
    {
        while (peso < W && i < objetos.Count)
        {
            // El objeto cabe completo
            if (peso + objetos[i].peso <= W)
            {
                ResaltarCodigo(5);
                peso += objetos[i].peso;
                pesoTexto.text = "peso = " + peso.ToString();
    
                objetos[i].RellenarFilledVerde(objetos[i].filledVerde.fillAmount, 1f);
                mochila.RellenarFilledMochila(mochila.filledMochila.fillAmount, mochila.filledMochila.fillAmount + (float)objetos[i].peso / (float)W, objetos[i].valor);
            
                yield return new WaitForSeconds(1f);
                ResaltarCodigo(6);
                ActualizarSolucion(objetos[i].posOriginal, 1f);

                yield return new WaitForSeconds(1f);
                ResaltarCodigo(7);
                i += 1;
                iTexto.text = "i = " + i.ToString();

                yield return new WaitForSeconds(1f);
            }
            // El objeto no cabe completo
            else {
                ResaltarCodigo(8);
                objetos[i].RellenarFilledVerde(objetos[i].filledVerde.fillAmount, ((float)W - (float)peso) / (float)objetos[i].peso);
                mochila.RellenarFilledMochila(mochila.filledMochila.fillAmount, 1f, objetos[i].valor * ((float)W - (float)peso) / (float)objetos[i].peso);

                ActualizarSolucion(objetos[i].posOriginal, Mathf.Round((((float)W - (float)peso) / (float)objetos[i].peso) * 10) / 10);

                yield return new WaitForSeconds(1f);
                ResaltarCodigo(9);
                peso = W;
                pesoTexto.text = "peso = " + peso.ToString();
            }
        }
        yield return new WaitForSeconds(1f);
        ResaltarCodigo(10);
    }

    void ActualizarSolucion(int posicion, float valor)
    {
        solucion[posicion] = valor;

        string solucionString = "";

        for(int p = 0; p < solucion.Count; p++)
        {
            solucionString += solucion[p].ToString();
            if (p < solucion.Count-1)
            {
                solucionString += ", ";
            }
        }

        solucionTexto.text = "solucion = [" + solucionString + "]";
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
