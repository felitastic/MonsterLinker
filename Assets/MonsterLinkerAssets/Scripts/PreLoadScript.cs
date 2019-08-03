using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PreLoadScript : MonoBehaviour
{
    public Save curSave;
    public Save Save1;
    public Save Save2;
    public Save Save3;

    void Awake()
    {
        DontDestroyOnLoad(this);
    }

}
