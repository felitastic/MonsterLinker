using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TutorialChan : MonoBehaviour
{
    public Image curFace;
    public Image curBody;
    public Text curDialogue;
    public Vector2 curPos;

    public Animator QTEchanAnim;
    public RectTransform QTEposition;
    public GameObject ContinueButton;

    public List<Sprite> QTEFace;
    public List<Sprite> QTEBody;

    public void QTEchan_Fadeout()
    {
        QTEchanAnim.SetTrigger("fadeout");
    }

    public void QTEchan_Fadein()
    {
        QTEchanAnim.SetTrigger("fadein");
    }

    public void ChangeQTEBody(eQTEBodyLanguage body, eQTEFacialExpression face, Vector2 newPos)
    {
        print("face: " + face);
        print("body: " + body);
        curFace.sprite = QTEFace[(int)face];
        curBody.sprite = QTEBody[(int)body];
        ChangeQTEPos(newPos);
    }    

    public void ChangeQTEPos(Vector2 newPos)
    {
        QTEposition.anchoredPosition = newPos;
        curPos = QTEposition.anchoredPosition;
    }

    public void ChangeQTELine(string curLine)
    {
        curDialogue.text = curLine;
    }

    public void WriteDialogue(string curLine)
    {
        curDialogue.text = curLine;
    }

    public void TutStateSwitch(eTutorial tutorial)
    {
        FindObjectOfType<PreLoadScript>().curSave.Tutorial = tutorial;

        //switch (tutorial)
        //{
        //    case eTutorial.notstarted:
        //        break;
        //    case eTutorial.menu:
        //        break;
        //    case eTutorial.loadout:
        //        break;
        //    case eTutorial.input1:
        //        break;
        //    case eTutorial.inicheck:
        //        break;
        //    case eTutorial.infight:
        //        break;
        //    case eTutorial.input2:
        //        break;
        //    case eTutorial.done:
        //        break;
        //    default:
        //        break;
        //}
    }
}
