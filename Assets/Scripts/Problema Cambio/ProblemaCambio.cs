using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ProblemaCambio : MonoBehaviour
{
    [Header("Variables")]
    public int cantidad; // Cantidad a la que hay que llegar
    private int cambio, x, i, posArray;
    private bool esSolucion;
    [Header("Textos")]
    [SerializeField] private Text cantidadTexto;
    [SerializeField] private Text cambioTexto;
    [SerializeField] private Text monedasTexto, esSolucionTexto, iTexto, xTexto, AddSubstractTexto;
    [Header("Mostrador")]
    [SerializeField] private GameObject[] monedasMostrador;
    [SerializeField] private Transform mostradorBilletes, mostradorMonedas;
    private float separadorMostrador = 30f; // Separación entre los billetes y monedas del mostrador
    [Header("Otros")]
    [SerializeField] private Dinero[] monedas;
    [SerializeField] private GameObject[] highlightsCodigo;
    [SerializeField] private Flecha flecha;
    [SerializeField] private GameObject canvasEjecucion;

    public void EmpezarEjecucion()
    {
        StartCoroutine("ComprobarCantidad");
    }

    IEnumerator ComprobarCantidad()
    {
        ActualizarCambio(cantidad);
        cantidadTexto.text = "cantidad = " + cantidad.ToString();

        yield return new WaitForSeconds(0.8f);
    
        ResaltarCodigo(0);
        monedasTexto.text = "monedas = (500, 200, 100, 50, 25, 5, 2, 1)";
        yield return new WaitForSeconds(0.8f);

        ResaltarCodigo(1);
        esSolucionTexto.text = "esSolucion = " + esSolucion.ToString();
        yield return new WaitForSeconds(0.8f);

        ResaltarCodigo(2);
        iTexto.text = "i = " + posArray.ToString();
        yield return new WaitForSeconds(0.8f);

        while (!esSolucion)
        {
            ResaltarCodigo(3);
            xTexto.text = "x = " + monedas[posArray].valor.ToString();

            // La flecha se desplaza hasta la siguiente moneda/billete
            Vector2 posDestino = new Vector2(flecha.transform.localPosition.x, monedas[posArray].posY);
            flecha.Mover(posDestino);
            yield return new WaitUntil(() => Vector2.Distance(flecha.transform.localPosition, posDestino) < 0.0025f);     
            flecha.moviendo = false;

            // Se suma el valor de la moneda al cambio que llevamos
            monedas[posArray].animator.SetTrigger("Out");
            yield return new WaitForSeconds(0.7f);

            // Se comprueba si la moneda nos sirve, nos hemos pasado o ya hemos llegado a la cantidad    
            if ((cambio - monedas[posArray].valor) >= 0)
            {
                ResaltarCodigo(4);
                // No hemos llegado a la cantidad, pero la moneda nos sirve
                AñadirAlMostrador(posArray);
                flecha.animator.SetTrigger("Green");
                yield return new WaitForSeconds(0.5f); 
                monedas[posArray].animator.SetTrigger("In");

                yield return new WaitForSeconds(0.5f);
                ResaltarCodigo(5);
                ActualizarCambio(cambio - monedas[posArray].valor);
                AddSubstractTexto.text = "-" + monedas[posArray].valor;
                AddSubstractTexto.GetComponent<Animator>().SetTrigger("Popup");
                cambioTexto.GetComponent<Animator>().SetTrigger("Green");

                // Hemos llegado a la cantidad
                if (cambio == 0)
                {
                    yield return new WaitForSeconds(0.5f);
                    ResaltarCodigo(6);
                    esSolucion = true;
                    esSolucionTexto.text = "esSolucion = " + esSolucion.ToString();
                    yield return new WaitForSeconds(0.5f); 
                    ResaltarCodigo(8);
                }
            } else {
                ResaltarCodigo(7);
                // Nos hemos pasado, la moneda no sirve
                flecha.animator.SetTrigger("Red");
                yield return new WaitForSeconds(0.5f);       
                monedas[posArray].animator.SetTrigger("In");
                posArray++;
                iTexto.text = "i = " + posArray.ToString();
            }
            yield return new WaitForSeconds(0.5f);
        }
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

    // Muestra el billete o moneda escogido en el centro de la pantalla
    void AñadirAlMostrador(int posArray)
    {
        if (monedas[posArray].valor == 2 || monedas[posArray].valor == 1)
        {
            Instantiate(monedasMostrador[posArray], mostradorMonedas.position, Quaternion.identity, canvasEjecucion.transform);
            mostradorMonedas.position = new Vector2(mostradorMonedas.position.x, mostradorMonedas.position.y + separadorMostrador);
        } else {
            Instantiate(monedasMostrador[posArray], mostradorBilletes.position, Quaternion.identity, canvasEjecucion.transform);
            mostradorBilletes.position = new Vector2(mostradorBilletes.position.x, mostradorBilletes.position.y + separadorMostrador);
        }       
    }

    // Actualiza el cambio restante y lo muestra en pantalla
    void ActualizarCambio(int n)
    {
        cambio = n;
        cambioTexto.text = cambio.ToString();
    }
}
