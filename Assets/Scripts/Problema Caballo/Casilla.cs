using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Casilla : MonoBehaviour
{
    [SerializeField] private Text numTexto, casillasAccesiblesTexto;
    [SerializeField] private Animator colorAnimator, casillasAccesiblesAnimator;
    
    public int num, x, y;
    public float posX, posY;
    
    void Start()
    {
        posX = transform.position.x;
        posY = transform.position.y;
    }

    public void ActualizarNum(int n)
    {
        num = n;
        numTexto.text = num.ToString();
    }

    public void Resaltar()
    {
        colorAnimator.SetTrigger("Resaltar");
    }

    public void Seleccionar()
    {
        colorAnimator.SetTrigger("Seleccionar");
    }

    public void Atenuar()
    {
        colorAnimator.SetTrigger("Atenuar");
        casillasAccesiblesAnimator.SetTrigger("Atenuar");
    }

    // Muestra el número de casillas accesibles desde la casilla
    public void MostrarNumCasillasAccesibles(int casillas)
    {
        casillasAccesiblesTexto.text = casillas.ToString();
        casillasAccesiblesAnimator.SetTrigger("Resaltar");
    }

    // Coloca la casilla en el tablero
    public void ColocarEnTablero(int i, int j, float tamañoCasilla)
    {
        x = i;
        y = j;

        float tamañoInicialCasilla = GetComponent<RectTransform>().sizeDelta.x;

        // Ajusta el tamaño
        GetComponent<RectTransform>().SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, tamañoCasilla);
        GetComponent<RectTransform>().SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, tamañoCasilla);
    
        // Ajusta el tamaño de los objetos hijos de la casilla
        int children = transform.childCount;
        for (int p = 0; p < children; ++p)
        {
            float escalaChild = (1/tamañoInicialCasilla) * tamañoCasilla;
            transform.GetChild(p).transform.localScale = new Vector3(escalaChild, escalaChild, escalaChild);
        }

        // Ajusta la posición
        transform.localPosition = new Vector2(0,0) + new Vector2((tamañoCasilla/2) + tamañoCasilla * i, - ((tamañoCasilla/2) + tamañoCasilla * j));

        // Asigna el color de la casilla
        if ((i%2 == 0 && j%2 == 0) || (i%2 != 0 && j%2 != 0))
        {
            GetComponent<Image>().color = Color.white;
        }

    }
}
