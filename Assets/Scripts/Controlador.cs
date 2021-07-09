using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Controlador : MonoBehaviour
{
    [SerializeField] private Image iconoBotonPlay;
    [SerializeField] private Sprite play, stop;
    [SerializeField] private Slider velocidadSlider;
    bool isPlaying = true;

    void Update()
    {
        if (isPlaying)
        {
            Time.timeScale = velocidadSlider.value;
        }   
    }

    public void ReanudarYPausar()
    {
        if (isPlaying)
        {
            isPlaying = false;
            iconoBotonPlay.sprite = play;
            Time.timeScale = 0f;           
        } else {
            isPlaying = true;
            iconoBotonPlay.sprite = stop;       
        }
    }
}
