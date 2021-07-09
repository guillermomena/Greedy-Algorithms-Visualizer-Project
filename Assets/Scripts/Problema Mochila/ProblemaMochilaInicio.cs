using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ProblemaMochilaInicio : MonoBehaviour
{
    [SerializeField] private ProblemaMochila problemaMochila;
    [SerializeField] private InputField pesoInput, valorInput, pesoMaximoInput;
    [SerializeField] private GameObject objetoPrefab, posicionPrefab;
    [SerializeField] private Button pesoMaximoBoton, iniciarBoton;
    [SerializeField] private Transform listaObjetosParent;
    [SerializeField] private GameObject canvasInicio, canvasEjecucion;
    private bool pesoAñadido, objetoAñadido;

    void Start()
    {
        canvasInicio.SetActive(true);
        canvasEjecucion.SetActive(false);
        
        ComprobarBotonesDisponibles();
    }

    public void AñadirObjeto()
    {
        if (pesoInput.text != "" && valorInput.text != "")
        {
            GameObject objeto = Instantiate(objetoPrefab, new Vector3(0,0,0), Quaternion.identity, listaObjetosParent);

            ObjetoMochila objetoMochila = objeto.GetComponent<ObjetoMochila>();

            problemaMochila.objetos.Add(objetoMochila);

            objetoMochila.peso = int.Parse(pesoInput.text);
            objetoMochila.valor = int.Parse(valorInput.text);
            objetoMochila.pesoTexto.text = objetoMochila.peso.ToString();
            objetoMochila.valorTexto.text = objetoMochila.valor.ToString();

            pesoInput.text = null;
            valorInput.text = null;

            objetoMochila.posOriginal = problemaMochila.n;
            objetoMochila.idTexto.text = (objetoMochila.posOriginal + 1).ToString();
            problemaMochila.n += 1;

            problemaMochila.solucion.Add(0f);

            objetoAñadido = true;

            ComprobarBotonesDisponibles();
        }
    }

    public void Iniciar()
    {
        canvasInicio.SetActive(false);
        canvasEjecucion.SetActive(true);

        problemaMochila.EmpezarEjecucion();
    }

    public void AñadirPesoMaximo()
    {
        if (pesoMaximoInput.text != "")
        {
            problemaMochila.W = int.Parse(pesoMaximoInput.text);
            pesoMaximoInput.text = null;
            pesoMaximoBoton.interactable = false;
            pesoAñadido = true;

            ComprobarBotonesDisponibles();
        } 
    }

    public void ComprobarBotonesDisponibles()
    {
        if (pesoAñadido && objetoAñadido)
        {
            iniciarBoton.interactable = true;
        } else {
            iniciarBoton.interactable = false;
        }
    }
}
