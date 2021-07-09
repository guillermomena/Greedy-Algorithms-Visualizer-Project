using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class ProblemaCaballo : MonoBehaviour
{
    [SerializeField] private GameObject tableroParent, casillaPrefab, caballoPrefab;
    [SerializeField] private Text nTexto, filaIniTexto, colIniTexto, seleccionadoTexto;
    [SerializeField] private GameObject[] highlightsCodigo;

    public int N, colIni, filaIni;

    private Casilla[,] tablero;
    private Casilla seleccionado;
    private Caballo caballo;

    private List<Casilla> casillasCandidatasAux = null;
    private Casilla casillaSeleccionadaAux = null;

    // Inicia la ejecución del algoritmo
    public void EmpezarEjecucion()
    {
        nTexto.text = "n = " + N.ToString();
        colIniTexto.text = "colIni = " + colIni.ToString();
        filaIniTexto.text = "filaIni = " + filaIni.ToString();
        StartCoroutine("GenerarTablero");
    }

    // Inicializa la visualización del tablero y coloca al caballo en su casilla
    IEnumerator GenerarTablero()
    {
        yield return new WaitForSeconds(0.2f);

        ResaltarCodigo(0);

        tablero = new Casilla[N,N];

        float tamañoTablero = tableroParent.GetComponent<RectTransform>().rect.width;
        float tamañoCasilla = tamañoTablero / N;

        for (int j = 0; j < N; j++)
        {
            for (int i = 0; i < N; i++)
            {
                GameObject casillaObject = Instantiate(casillaPrefab, new Vector2(0,0), Quaternion.identity, tableroParent.transform);
                Casilla casilla = casillaObject.GetComponent<Casilla>();

                casilla.ColocarEnTablero(i, j, tamañoCasilla);

                tablero[i,j] = casilla;

                yield return new WaitForSeconds(0.05f);
            }
        }

        yield return new WaitForSeconds(0.5f);
        
        ResaltarCodigo(1);
        ActualizarSeleccionado(tablero[colIni, filaIni]);

        // Coloca al caballo
        GameObject caballoInstanciado = Instantiate(caballoPrefab, new Vector2(0,0), Quaternion.identity, tableroParent.transform);
        caballo = caballoInstanciado.GetComponent<Caballo>();
        caballo.ColocarEnTablero(tablero, seleccionado, tamañoCasilla);

        yield return new WaitForSeconds(0.5f);

        StartCoroutine("ComprobarCasillas");
    }

    IEnumerator ComprobarCasillas()
    {
        for(int i = 0; i < N*N; i++)
        {
            // Se mueve el caballo (en el primer loop el caballo ya aparece en su posición)
            if (i > 0)
            {   
                caballo.Mover(seleccionado);
                // Espera a que el caballo haya terminado de moverse
                yield return new WaitUntil(() => Vector2.Distance(caballo.transform.position, seleccionado.transform.position) < 0.0025f);
                seleccionado.Atenuar();
            }

            yield return new WaitForSeconds(0.25f);  

            ResaltarCodigo(2);
            tablero[seleccionado.x, seleccionado.y].ActualizarNum(i+1);

            yield return new WaitForSeconds(0.5f);  

            ResaltarCodigo(3);
            StartCoroutine(CasillasAccesibles(seleccionado));
            yield return new WaitUntil(() => casillasCandidatasAux != null);
            List<Casilla> candidatos = casillasCandidatasAux;
            casillasCandidatasAux = null;
            //List<Casilla> candidatos = CasillasAccesibles(seleccionado);

            yield return new WaitForSeconds(0.5f);  
  
            ResaltarCodigo(4);

            yield return new WaitForSeconds(0.5f);  
            StartCoroutine(SeleccionarCasilla(candidatos));
            yield return new WaitUntil(() => casillaSeleccionadaAux != null);
            ActualizarSeleccionado(casillaSeleccionadaAux);
            casillaSeleccionadaAux = null;
            //ActualizarSeleccionado(SeleccionarCasilla(candidatos));

            yield return new WaitForSeconds(0.75f);

            // Dibuja la casilla seleccionada y atenúa el resto
            ResaltarCodigo(15);
            for(int c = 0; c < candidatos.Count; c++)
            {
                if (candidatos[c] == seleccionado)
                {
                    candidatos[c].Seleccionar();
                } else {
                    candidatos[c].Atenuar();
                }
            }

            yield return new WaitForSeconds(0.25f);
        }
    }

    // Calcula todas las casillas no visitadas accesibles desde casillaActual
    IEnumerator CasillasAccesibles(Casilla casillaActual)
    {
        ResaltarCodigo(5);
        Vector2Int[] desplCaballo = {new Vector2Int(-1,-2), new Vector2Int(-2,-1), new Vector2Int(1,-2), new Vector2Int(2,-1),
                                     new Vector2Int(-1,2), new Vector2Int(-2,1), new Vector2Int(1,2), new Vector2Int(2,1)};

        yield return new WaitForSeconds(0.1f);

        ResaltarCodigo(6);
        List<Casilla> casillasCandidatas = new List<Casilla>();

        yield return new WaitForSeconds(0.1f);

        for(int i = 0; i < desplCaballo.Length; i++)
        {
            ResaltarCodigo(7);
            int c = casillaActual.x + desplCaballo[i].x;
            int f = casillaActual.y + desplCaballo[i].y;

            //yield return new WaitForSeconds(0.125f);

            if (f>=0 && f<N && c>=0 && c<N && tablero[c,f].num == 0)
            {
                //yield return new WaitForSeconds(0.1f);
                //ResaltarCodigo(8);
                casillasCandidatas.Add(tablero[c,f]);

                
                // Solo se resaltan las candidatas desde la casilla actual del caballo
                if (caballo.x == casillaActual.x && caballo.y == casillaActual.y)
                {
                    yield return new WaitForSeconds(0.2f);
                    tablero[c,f].Resaltar();         
                }     
            }
        }

        yield return new WaitForSeconds(0.2f);
        ResaltarCodigo(9);
        //yield return new WaitForSeconds(0.25f);

        casillasCandidatasAux = casillasCandidatas;

        //return casillasCandidatas;
    }

    // Selecciona entre las casillas candidatas, la que tenga menor número de casillas
    // accesibles desde ella
    IEnumerator SeleccionarCasilla(List<Casilla> casillasCandidatas)
    {
        ResaltarCodigo(10);
        int menorNumCasillasAccesibles = 9;

        yield return new WaitForSeconds(0.25f);

        ResaltarCodigo(11);
        Casilla casillaSeleccionada = null;

        for(int i = 0; i < casillasCandidatas.Count; i++)
        {
            yield return new WaitForSeconds(0.15f);
            ResaltarCodigo(12);
            StartCoroutine(CasillasAccesibles(casillasCandidatas[i]));
            yield return new WaitUntil(() => casillasCandidatasAux != null);
            List<Casilla> accesibles = casillasCandidatasAux;
            casillasCandidatasAux = null;
            //List<Casilla> accesibles = CasillasAccesibles(casillasCandidatas[i]); 
            
            yield return new WaitForSeconds(0.1f);
            ResaltarCodigo(13);
            int numCasillasAccesibles = accesibles.Count;

            // Muestra el número de casillas accesibles desde cada casilla
            casillasCandidatas[i].MostrarNumCasillasAccesibles(numCasillasAccesibles);

            if (numCasillasAccesibles < menorNumCasillasAccesibles)
            {
                yield return new WaitForSeconds(0.25f);
                ResaltarCodigo(14);
                menorNumCasillasAccesibles = numCasillasAccesibles;
                casillaSeleccionada = casillasCandidatas[i];
            }
        }

        casillaSeleccionadaAux = casillaSeleccionada;
    }

    void ActualizarSeleccionado(Casilla casilla)
    {
        if (casilla != null)
        {
            seleccionado = casilla;
            seleccionadoTexto.text = "seleccionado = [" + casilla.x + ", " + casilla.y + "]";
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
}
