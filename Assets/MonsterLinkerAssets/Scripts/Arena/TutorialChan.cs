using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TutorialChan : MonoBehaviour
{
    [Tooltip("Different Positions for her to appear")]
    public List<GameObject> Positions;

    public List<Image> QTEFace;
    public enum QTEFacialExpression
    {
        happy,
        excited,
        sad,
        angry
    }

    public List<Image> QTEBody;
    public enum QTEBodyLanguage
    {
        pointUp,
        pointDown,
        pointMiddle,
        noPoint
    }

    public void StateSwitch(eTutorial tutorial)
    {
        GameStateSwitch.Instance.curProfile.Tutorial = tutorial;

        switch (tutorial)
        {
            case eTutorial.menu:
                break;
            case eTutorial.loadout:
                break;
            case eTutorial.input1:
                break;
            case eTutorial.inicheck:
                break;
            case eTutorial.infight:
                break;
            case eTutorial.input2:
                break;
            case eTutorial.done:
                break;
            default:
                break;
        }
    }
}
