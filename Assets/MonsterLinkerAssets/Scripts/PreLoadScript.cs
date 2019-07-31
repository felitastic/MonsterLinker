using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PreLoadScript : MonoBehaviour
{
    public Save Save1;

    void Awake()
    {
        DontDestroyOnLoad(this);
    }

}
