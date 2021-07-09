using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuPrincipal : MonoBehaviour
{
    public void IniciarProblema(string escena)
    {
        SceneManager.LoadScene(escena);
    }
}
