using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArenaStageChanger : MonoBehaviour
{
    public GameObject Enemy1;
    public GameObject Enemy2;
    public GameObject Enemy3;
    public GameObject Enemy4;
    public GameObject Enemy5;

    public void CheckArenaStage(int arena)
    {
        switch (arena)
        {
            case 1:
                Enemy1.SetActive(true);

                break;
            case 2:
                Enemy2.SetActive(true);

                break;
            case 3:
                Enemy3.SetActive(true);

                break;
            case 4:
                Enemy4.SetActive(true);

                break;
            case 5:
                Enemy5.SetActive(true);

                break;
            default:
                Debug.LogError("could not find stage");
                Enemy1.SetActive(true);
                break;
        }
    }
}
