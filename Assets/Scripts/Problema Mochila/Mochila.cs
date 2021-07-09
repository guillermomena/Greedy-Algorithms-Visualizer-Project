using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Mochila : MonoBehaviour
{
    public Image filledMochila;
    [SerializeField] Text pesoTotalTexto, valorTotalTexto;
    [SerializeField] float valorTotal, nuevoValorTotal;
    float fillInicio, fillTarget;
    [SerializeField] ProblemaMochila problemaMochila;

    void Update()
    {
        pesoTotalTexto.text = ((filledMochila.fillAmount * problemaMochila.W).ToString("F0") + " / " + problemaMochila.W);
        valorTotalTexto.text = valorTotal.ToString("F0");
    }

    public void RellenarFilledMochila(float inicio, float target, float valor)
    {
        fillInicio = inicio;
        fillTarget = target;
        nuevoValorTotal = valorTotal + valor;
        StartCoroutine("RellenarFilled");
    }

    IEnumerator RellenarFilled()
    {
        float duracion = 1f;
        for (float t = 0f; t < duracion; t += Time.deltaTime)
        {
            filledMochila.fillAmount = Mathf.Lerp(fillInicio, fillTarget, t / duracion);
            valorTotal = (int)Mathf.Lerp(valorTotal, nuevoValorTotal, t / duracion);

            yield return null;
        }
        filledMochila.fillAmount = fillTarget;  
        valorTotal = nuevoValorTotal;
    }
}
