using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TutorialChan : MonoBehaviour
{
    public Image curFace;
    public Image curBody;
    public Text curDialogue;

    public Animator QTEchanAnim;
    public RectTransform QTEposition;
    public GameObject ContinueButton;

    public List<Sprite> QTEFace;
    public List<Sprite> QTEBody;

    public void TutChan_Switch(eQTEBodyLanguage body, eQTEFacialExpression face)
    {
        print("face: " + face);
        print("body: " + body);
        curFace.sprite = QTEFace[(int)face];
        curBody.sprite = QTEBody[(int)body];
    }

    public void WriteDialogue(string dialogue)
    {
        curDialogue.text = dialogue;
    }

    public void TutStateSwitch(eTutorial tutorial)
    {
        FindObjectOfType<PreLoadScript>().curSave.Tutorial = tutorial;

        switch (tutorial)
        {
            case eTutorial.notstarted:
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
