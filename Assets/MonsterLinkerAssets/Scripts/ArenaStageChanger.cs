using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArenaStageChanger : MonoBehaviour
{
    //public GameObject Enemy1;
    //public GameObject Enemy2;
    //public GameObject Enemy3;
    //public GameObject Enemy4;
    //public GameObject Enemy5;

    public Torii_ColorChange toriicolorchange;

    public void CheckArenaStage(int arena)
    {
        switch (arena)
        {
            case 1:
                //Enemy2.SetActive(false);
                //Enemy5.SetActive(false);
                //Enemy1.SetActive(true);
                toriicolorchange.ChangeMaterial(eToriiColor.violett, eEnemySkin.Slicer);
                break;
            case 2:
                //Enemy1.SetActive(false);
                //Enemy3.SetActive(false);
                //Enemy2.SetActive(true);
                toriicolorchange.ChangeMaterial(eToriiColor.pink, eEnemySkin.Crystalfang);

                break;
            case 3:
                //Enemy2.SetActive(false);
                //Enemy4.SetActive(false);
                //Enemy3.SetActive(true);
                toriicolorchange.ChangeMaterial(eToriiColor.yellow, eEnemySkin.Pyro);

                break;
            case 4:
                //Enemy3.SetActive(false);
                //Enemy5.SetActive(false);
                //Enemy4.SetActive(true);
                toriicolorchange.ChangeMaterial(eToriiColor.green, eEnemySkin.Xensor);

                break;
            case 5:
                //Enemy4.SetActive(false);
                //Enemy1.SetActive(false);
                //Enemy5.SetActive(true);
                toriicolorchange.ChangeMaterial(eToriiColor.orange, eEnemySkin.Eldritch);

                break;
            default:
                Debug.LogError("could not find stage");
                toriicolorchange.ChangeMaterial(eToriiColor.violett, eEnemySkin.Slicer);
                //Enemy1.SetActive(true);
                break;
        }
    }
}
