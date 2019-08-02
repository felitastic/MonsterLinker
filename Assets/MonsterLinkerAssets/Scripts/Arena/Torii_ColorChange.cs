using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Torii_ColorChange : MonoBehaviour
{
    public Renderer LeftTorii;
    public Renderer RightTorii;
    public Renderer Enemy;

    public List<Material> ToriiColor;
    public List<Material> EnemySkin;

    public eToriiColor curColor;
    public eEnemySkin curSkin;

    public void ChangeMaterial(eToriiColor toriiColor, eEnemySkin enemySkin)
    {
        curColor = toriiColor;
        curSkin = enemySkin;
        RightTorii.material = ToriiColor[(int)toriiColor];
        Enemy.material = EnemySkin[(int)enemySkin];
    }
}
