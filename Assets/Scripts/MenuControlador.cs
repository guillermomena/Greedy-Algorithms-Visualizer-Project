using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuControlador : MonoBehaviour
{
    [SerializeField] GameObject canvasInicio, canvasEjecucion;

    void Start()
    {
        
    }

    void Update()
    {
        
    }

    public void Back()
    {
        if (canvasInicio.activeSelf == true)
        {
            SceneManager.LoadScene(0);
        } else {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }
}
