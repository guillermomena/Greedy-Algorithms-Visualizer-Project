using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dinero : MonoBehaviour
{
    public int valor;
    public float posY;
    public Animator animator;
    
    void Awake()
    {
        posY = transform.localPosition.y;
        animator = GetComponent<Animator>();
    }
}
