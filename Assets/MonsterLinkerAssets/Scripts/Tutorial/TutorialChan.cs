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

    [Header("Positions")]
    //For the Title menu
    public Vector2 downLeft_title = new Vector2(800, -350);
    public Vector2 downRight_title = new Vector2(820, 20);
    //public Vector2 downRight_title = new Vector2(820, -350);
    public Vector2 center_title = new Vector2(35, 125);

    //For the Main menu
    public Vector2 downLeft_main = new Vector2(-860, -240);
    public Vector2 downRight_main = new Vector2(815, -240);
    public Vector2 center_main = new Vector2(20, 125);

    //For the loadout Screen
    public Vector2 loadoutTop = new Vector2(-100, 220);
    public Vector2 loadoutBottom = new Vector2(-100, 220);


    public void QTEchan_Fadeout()
    {
        print("qte chan fades out");
        QTEchanAnim.SetTrigger("fadeout");
    }

    public void QTEchan_Fadein()
    {
        print("qte chan fades in");
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
    }
}
