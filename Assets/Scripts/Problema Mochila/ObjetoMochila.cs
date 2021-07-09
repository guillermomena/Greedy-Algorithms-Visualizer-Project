using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ObjetoMochila : MonoBehaviour
{
    public int peso;
    public int valor;
    public float vw;

    public Text pesoTexto, valorTexto, vwTexto, idTexto;
    public Image filledVerde;

    float fillInicio, fillTarget;
    public int posOriginal; // Posicion original en la lista de objetos, antes de ordenarla
    public LayoutElement layoutElement;

    public Vector2 targetPos;

    public void CalcularVW()
    {
        vw = (float)valor / (float)peso;
        vw = (float)System.Math.Round(vw,1);

        vwTexto.text = vw.ToString();
    }

    public void RellenarFilledVerde(float inicio, float target)
    {
        fillInicio = inicio;
        fillTarget = target;
        StartCoroutine("RellenarFilled");
    }

    public void MoverObjeto(Vector2 target)
    {
        targetPos = target;
        StartCoroutine("MoverObjetoCoroutine");
    }
    
    IEnumerator RellenarFilled()
    {
        float duracion = 1f;
        for (float t = 0f; t < duracion; t += Time.deltaTime)
        {
            filledVerde.fillAmount = Mathf.Lerp(fillInicio, fillTarget, t / duracion);
            yield return null;
        }
        filledVerde.fillAmount = fillTarget;
    }

    IEnumerator MoverObjetoCoroutine()
    {
        Vector2 initPos = gameObject.transform.position;
        float duracion = 1f;
        for (float t = 0f; t < duracion; t += Time.deltaTime)
        {
            gameObject.transform.position = Vector2.Lerp(initPos, targetPos, t / duracion);
            yield return null;
        }
        gameObject.transform.position = targetPos;
    }
    
}
