using System;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GameEvents : MonoBehaviour
{
    public static GameEvents current;


    private void Awake()
    {
        current = this;
    }

    public event Action onKnifeCollision;

    public void OnKnifeCollision()
    {
        Debug.Log("Entro al metodo");
        if (onKnifeCollision != null)
        {
            Debug.Log("entro al bucle");
            onKnifeCollision.Invoke();
        }
    }
}