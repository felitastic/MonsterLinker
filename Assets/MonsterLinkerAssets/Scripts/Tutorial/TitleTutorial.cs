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
                qte.ChangeQTEBody(eQTEBodyLanguage.armsdown, eQTEFacialExpression.neutral, new Vector2(0, 0));
                qte.ChangeQTELine(lines[curLine]);
                qte.QTEchan_Fadein();
                break;
            case 4:
                lines[lineInProgress] = "Nice to meet you, " + FindObjectOfType<PreLoadScript>().curSave.LinkerName + "!";
                qte.ChangeQTEBody(eQTEBodyLanguage.armsdown, eQTEFacialExpression.neutral, new Vector2(0, 0));
                break;
            case 6:
                lines[lineInProgress] = "Welcome back, " + FindObjectOfType<PreLoadScript>().curSave.LinkerName + "!";
                qte.ChangeQTEBody(eQTEBodyLanguage.armsdown, eQTEFacialExpression.neutral, new Vector2(750, 150));                  
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
        //if (lineInProgress == 3)
        //{
        //    EndDialogue(false);
        //}
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

                break;
            case 2:

                break;
            case 3:
                qte.ChangeQTEBody(eQTEBodyLanguage.armsdown, eQTEFacialExpression.neutral, new Vector2(750, 150));
                //yield return new WaitForSeconds(0.5f);
                EndDialogue(false);
                FindObjectOfType<TitleMenu>().InputPlayerNameWindow.SetActive(true);

                break;
            case 4:
                qte.ChangeQTEBody(eQTEBodyLanguage.armsdown, eQTEFacialExpression.neutral, new Vector2(0, 0));
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

    //public IEnumerator ChangePosition(Vector2 newPos)
    //{
    //    tutorialchan.QTEchanAnim.SetTrigger("fadeout");
    //    yield return new WaitForSeconds(0.3f);
    //    tutorialchan.QTEposition.anchoredPosition = newPos;        
    //    tutorialchan.QTEchanAnim.SetTrigger("fadein");
    //}

    //public IEnumerator TriggerDialogue()
    //{
    //    linesdone += 1;
    //    ChangeDialogue();
    //    tutorialchan.ContinueButton.SetActive(false);

    //    switch (linesdone)
    //    {            
    //        case 0:
    //            tutorialchan.TutChan_Switch(eQTEBodyLanguage.pointUp, eQTEFacialExpression.happy);
    //            tutorialchan.QTEposition.anchoredPosition = new Vector2(700, 0);
    //            tutorialchan.QTEchanAnim.SetTrigger("fadein"); 
    //            break;
    //        case 1:
    //            StartCoroutine(ChangePosition(new Vector2(-200, 0)));
    //            tutorialchan.TutChan_Switch(eQTEBodyLanguage.pointUp, eQTEFacialExpression.shocked);    

    //            tutorialchan.QTEchanAnim.SetTrigger("fadeout");
    //            yield return new WaitForSeconds(0.25f);
    //            tutorialchan.QTEposition.anchoredPosition = new Vector2(-500, 0);
    //            tutorialchan.QTEchanAnim.SetTrigger("fadein");
    //            yield return new WaitForSeconds(0.25f);                
    //            break;
    //        case 2:
    //            tutorialchan.QTEchanAnim.SetTrigger("fadeout");
    //            yield return new WaitForSeconds(0.25f);
    //            tutorialchan.TutChan_Switch(eQTEBodyLanguage.pointUp, eQTEFacialExpression.sad);
    //            tutorialchan.QTEposition.anchoredPosition = new Vector2(-800, 170);
    //            tutorialchan.QTEchanAnim.SetTrigger("fadein");
    //            yield return new WaitForSeconds(0.25f);
    //            AllowNameInput();
    //            break;
    //        case 3:
    //            tutorialchan.QTEchanAnim.SetTrigger("fadeout");
    //            yield return new WaitForSeconds(0.25f);
    //            tutorialchan.TutChan_Switch(eQTEBodyLanguage.pointUp, eQTEFacialExpression.happy);
    //            tutorialchan.QTEposition.anchoredPosition = new Vector2(0, 0);
    //            tutorialchan.QTEchanAnim.SetTrigger("fadein");
    //            yield return new WaitForSeconds(0.25f);
    //            break;
    //        case 4:
    //            tutorialchan.QTEchanAnim.SetTrigger("fadeout");
    //            yield return new WaitForSeconds(0.25f);
    //            tutorialchan.TutChan_Switch(eQTEBodyLanguage.pointUp, eQTEFacialExpression.sad);
    //            tutorialchan.QTEposition.anchoredPosition = new Vector2(-800, 170);
    //            tutorialchan.QTEchanAnim.SetTrigger("fadein");
    //            yield return new WaitForSeconds(0.25f);
    //            break;
    //        case 5:
    //            tutorialchan.QTEchanAnim.SetTrigger("fadeout");
    //            yield return new WaitForSeconds(0.25f);
    //            SceneManager.LoadScene(2);
    //            break;
    //        case 7:
    //            tutorialchan.TutChan_Switch(eQTEBodyLanguage.pointUp, eQTEFacialExpression.happy);
    //            tutorialchan.QTEposition.anchoredPosition = new Vector2(0, 0);
    //            tutorialchan.QTEchanAnim.SetTrigger("fadein");
    //            break;
    //            case 8:
    //            tutorialchan.QTEchanAnim.SetTrigger("fadeout");
    //            yield return new WaitForSeconds(0.25f);
    //            SceneManager.LoadScene(2);
    //            break;
    //        default:
    //            tutorialchan.TutChan_Switch(eQTEBodyLanguage.pointUp, eQTEFacialExpression.shocked);
    //            break;
    //    }
    //    tutorialchan.ContinueButton.SetActive(true);
    //}

    //public void NamesPromptNextLine()
    //{
    //    StartCoroutine(TriggerDialogue());
    //}

    //public void AllowNameInput()
    //{
    //    tutorialchan.ContinueButton.SetActive(false);
    //    FindObjectOfType<TitleMenu>().InputPlayerNameWindow.SetActive(true);
    //}
}
