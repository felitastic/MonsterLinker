using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuTutorial : MonoBehaviour
{
    public List<string> lines;
    public TutorialChan qte;
    public int lineInProgress = 0;
    public GameObject MenuButtons;

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
                qte.ChangeQTEBody(eQTEBodyLanguage.armsdown, eQTEFacialExpression.excited, qte.downLeft_main);
                qte.ChangeQTELine(lines[curLine]);
                qte.QTEchan_Fadein();
                qte.ContinueButton.SetActive(false);
                break;
            case 1:
                MenuButtons.SetActive(false);
                qte.ChangeQTEBody(eQTEBodyLanguage.armscrossed, eQTEFacialExpression.annoyed, qte.center_main);
                qte.ChangeQTELine(lines[curLine]);
                qte.QTEchan_Fadein();
                qte.ContinueButton.SetActive(true);
                break;
            case 7:
                qte.ChangeQTEBody(eQTEBodyLanguage.armsdown, eQTEFacialExpression.neutral, qte.downLeft_main);
                qte.ChangeQTELine(lines[curLine]);
                qte.QTEchan_Fadein();
                qte.ContinueButton.SetActive(false);
                break;
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
                EndDialogue(false);

                break;
            case 1:
                break;
            case 2:
                qte.ChangeQTEBody(eQTEBodyLanguage.pointRight, eQTEFacialExpression.neutral, qte.center_main);

                break;
            case 3:
                qte.ChangeQTEBody(eQTEBodyLanguage.armscrossed, eQTEFacialExpression.concerned, qte.center_main);

                break;
            case 4:
                qte.ChangeQTEBody(eQTEBodyLanguage.armscrossed, eQTEFacialExpression.excited, qte.center_main);

                break;
            case 5:
                qte.ChangeQTEBody(eQTEBodyLanguage.armscrossed, eQTEFacialExpression.neutral, qte.downRight_main);
                qte.TutStateSwitch(eTutorial.loadout);
                break;
            case 6:
                EndDialogue(false);
                MenuButtons.SetActive(true);
                //end menu tutorial                
                break;
            case 7:
                EndDialogue(false);
                break;

            default:
                //dont change line text, body, face or pos
                break;
        }
        yield return new WaitForSeconds(0.01f);
        qte.WriteDialogue(lines[lineInProgress]);
    }
}
