using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TitleTutorial : MonoBehaviour
{
    public List<string> lines;
    public TutorialChan qte;
    public int lineInProgress = 0;

    public void Awake()
    {
        qte = FindObjectOfType<TutorialChan>();
    }

    public void TriggerDialogue(int curLine)
    {
        lineInProgress = curLine;

        switch(curLine)
        {
            case 0:
                qte.ChangeQTEBody(eQTEBodyLanguage.pointRight, eQTEFacialExpression.neutral, qte.downRight_title);
                qte.ChangeQTELine(lines[curLine]);
                qte.QTEchan_Fadein();
                break;
            case 4:
                lines[lineInProgress] = "Nice to meet you, " + FindObjectOfType<PreLoadScript>().curSave.LinkerName + "!";
                qte.ChangeQTEBody(eQTEBodyLanguage.armsdown, eQTEFacialExpression.neutral, qte.downRight_title);
                break;
            case 6:
                lines[lineInProgress] = "Welcome back, " + FindObjectOfType<PreLoadScript>().curSave.LinkerName + "!";
                qte.ChangeQTEBody(eQTEBodyLanguage.pointRight, eQTEFacialExpression.excited, qte.downLeft_title);                  
                qte.ChangeQTELine(lines[curLine]);
                qte.QTEchan_Fadein();
                break;
        }
        qte.WriteDialogue(lines[lineInProgress]);
        qte.ContinueButton.SetActive(true);
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
        //if dissappear: tut-chan fadeout
        //else dialogue fadeout, continue button false
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
            case 1:
                qte.ChangeQTEBody(eQTEBodyLanguage.armsdown, eQTEFacialExpression.neutral, qte.downRight_title);
                break;
            case 2:
                qte.ChangeQTEBody(eQTEBodyLanguage.armsdown, eQTEFacialExpression.excited, qte.downRight_title);
                break;
            case 3:
                qte.ChangeQTEBody(eQTEBodyLanguage.pointLeft, eQTEFacialExpression.neutral, qte.downRight_title);
                //yield return new WaitForSeconds(0.5f);
                EndDialogue(false);
                FindObjectOfType<TitleMenu>().InputPlayerNameWindow.SetActive(true);

                break;
            case 4:
                qte.ChangeQTEBody(eQTEBodyLanguage.pointRight, eQTEFacialExpression.excited, qte.downRight_title);
                //yield return new WaitForSeconds(0.5f);

                break;
            case 5:
                EndDialogue(true);
                yield return new WaitForSeconds(0.3f);
                qte.TutStateSwitch(eTutorial.menu);
                SceneManager.LoadScene(2);
                break;
            case 6:
                
                break;
            case 7:
                EndDialogue(true);
                yield return new WaitForSeconds(0.3f);
                SceneManager.LoadScene(2);
                break;
            default:
                //dont change line text, body, face or pos
                break;
        }
        yield return new WaitForSeconds(0.01f);
        qte.WriteDialogue(lines[lineInProgress]);        
    }
}
