using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LoadoutTutorial : MonoBehaviour
{
    public List<string> lines;
    public TutorialChan qte;
    public int lineInProgress = 0;
    public GameObject LoadoutButtons;

    public GameObject FAChoiceWindow;
    public GameObject SIChoiceWindow;

    public void Awake()
    {
        qte = FindObjectOfType<TutorialChan>();
    }
    public void TriggerDialogue(int curLine)
    {
        lineInProgress = curLine;

        switch (curLine)
        {
            case 0:
                qte.ChangeQTEBody(eQTEBodyLanguage.armsdown, eQTEFacialExpression.neutral, qte.loadoutTop);
                qte.ChangeQTELine(lines[curLine]);
                LoadoutButtons.SetActive(false);
                qte.QTEchan_Fadein();
                qte.ContinueButton.SetActive(true);
                //qte.ContinueButton.GetComponentInChildren<Button>().Select();

                //get loadout button and disalbe, just blend in a grafik LOOKING like the shit
                break;
            //case 7:
            //    qte.ChangeQTEBody(eQTEBodyLanguage.armsdown, eQTEFacialExpression.excited, qte.mainPos);
            //    qte.ChangeQTELine(lines[curLine]);
            //    qte.QTEchan_Fadein();
            //    qte.ContinueButton.SetActive(false);
            //    break;
        }
        qte.WriteDialogue(lines[lineInProgress]);
    }

    //triggered by player input via continue
    public void NextLine()
    {
        SoundController.Instance.StartSFX(SoundController.SFX.ui_select);
        lineInProgress += 1;
        StartCoroutine(ChangeDialogue());
    }

    public void EndDialogue(bool dissappear)
    {
        if (dissappear)
        {
            qte.QTEchan_Fadeout();
        }
        else
        {

        }
        qte.ContinueButton.SetActive(false);
    }

    public IEnumerator ChangeDialogue()
    {
        switch (lineInProgress)
        {
            case 0:
                break;
            case 1:
                qte.ChangeQTEBody(eQTEBodyLanguage.pointLeft, eQTEFacialExpression.neutral, qte.loadoutTop);
                break;
            case 2:
                break;
            case 3:
                //open fa choice (or fake it)
                FAChoiceWindow.SetActive(true);
                qte.ContinueButton.GetComponentInChildren<Button>().Select();                
                qte.ChangeQTEBody(eQTEBodyLanguage.pointRight, eQTEFacialExpression.neutral, qte.loadoutTop);

                break;
            case 4:
                qte.ChangeQTEBody(eQTEBodyLanguage.armsdown, eQTEFacialExpression.neutral, qte.loadoutTop);

                break;
            case 5:
                break;
            case 6:
                break;
            case 7:
                FAChoiceWindow.SetActive(false);
                qte.ContinueButton.GetComponentInChildren<Button>().Select();
                //close fa choice
                break;
            case 8:
                qte.ChangeQTEBody(eQTEBodyLanguage.pointLeft, eQTEFacialExpression.neutral, qte.loadoutBottom);
                break;
            case 9:
                break;
            case 10:
                SIChoiceWindow.SetActive(true);
                qte.ContinueButton.GetComponentInChildren<Button>().Select();
                //open implant choice
                qte.ChangeQTEBody(eQTEBodyLanguage.pointRight, eQTEFacialExpression.neutral, qte.loadoutBottom);

                break;
            case 11:
                qte.ChangeQTEBody(eQTEBodyLanguage.armsdown, eQTEFacialExpression.excited, qte.loadoutBottom);
                break;
            case 12:
                qte.ChangeQTEBody(eQTEBodyLanguage.armscrossed, eQTEFacialExpression.annoyed, qte.loadoutBottom);
                break;
            case 13: 
                SIChoiceWindow.SetActive(false);
                qte.ContinueButton.GetComponentInChildren<Button>().Select();
                //close implant window
                qte.ChangeQTEBody(eQTEBodyLanguage.armscrossed, eQTEFacialExpression.neutral, qte.loadoutBottom);
                break;
            case 14:
                lines[lineInProgress] = "Good luck, " + FindObjectOfType<PreLoadScript>().curSave.LinkerName + "!";
                qte.ChangeQTEBody(eQTEBodyLanguage.armscrossed, eQTEFacialExpression.neutral, qte.loadoutBottom);
                break;
            case 15:
                EndDialogue(true);
                LoadoutButtons.SetActive(true);
                GameStateSwitch.Instance.preloadscript.curSave.Tutorial = eTutorial.done;
                break;
            default:
                //dont change line text, body, face or pos
                break;
        }
        yield return new WaitForSeconds(0.01f);
        qte.WriteDialogue(lines[lineInProgress]);
    }
}
