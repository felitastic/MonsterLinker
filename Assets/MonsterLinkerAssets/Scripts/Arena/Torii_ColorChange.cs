using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Torii_ColorChange : MonoBehaviour
{
    //public Renderer LeftTorii;
    public Renderer RightTorii;

    public List<Material> ToriiColor;

    public eToriiColor curColor;

    public void ChangeMaterial(eToriiColor toriiColor)
    {
        curColor = toriiColor;
        RightTorii.material = ToriiColor[(int)toriiColor];   
    }
}
