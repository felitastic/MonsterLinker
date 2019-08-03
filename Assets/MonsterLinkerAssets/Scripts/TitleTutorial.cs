using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TitleTutorial : MonoBehaviour
{
    public List<string> lines;
    public TutorialChan tutorialchan;
    [SerializeField] private int linesdone = 0;

    public void ChangeDialogue()
    {
        if (linesdone == 5)
        {
            lines[linesdone - 1] = "Nice to meet you, " + FindObjectOfType<PreLoadScript>().curSave.LinkerName + "!";
            tutorialchan.WriteDialogue(lines[linesdone-1]);
        }
        else
            tutorialchan.WriteDialogue(lines[linesdone]);
    }

    public IEnumerator TriggerDialogue()
    {
        linesdone += 1;
        ChangeDialogue();
        tutorialchan.ContinueButton.SetActive(false);

        switch (linesdone)
        {
            case 1:
                tutorialchan.TutChan_Switch(eQTEBodyLanguage.pointUp, eQTEFacialExpression.happy);
                tutorialchan.QTEposition.anchoredPosition = new Vector2(700, 0);
                tutorialchan.QTEchanAnim.SetTrigger("fadein"); 
                break;
            case 2:
                tutorialchan.QTEchanAnim.SetTrigger("fadeout");
                yield return new WaitForSeconds(0.25f);
                tutorialchan.TutChan_Switch(eQTEBodyLanguage.pointUp, eQTEFacialExpression.shocked);
                tutorialchan.QTEposition.anchoredPosition = new Vector2(-500, 0);
                tutorialchan.QTEchanAnim.SetTrigger("fadein");
                yield return new WaitForSeconds(0.25f);                
                break;
            case 4:
                tutorialchan.QTEchanAnim.SetTrigger("fadeout");
                yield return new WaitForSeconds(0.25f);
                tutorialchan.TutChan_Switch(eQTEBodyLanguage.pointUp, eQTEFacialExpression.sad);
                tutorialchan.QTEposition.anchoredPosition = new Vector2(-800, 170);
                tutorialchan.QTEchanAnim.SetTrigger("fadein");
                yield return new WaitForSeconds(0.25f);
                AllowNameInput();
                break;
            case 5:
                tutorialchan.QTEchanAnim.SetTrigger("fadeout");
                yield return new WaitForSeconds(0.25f);
                tutorialchan.TutChan_Switch(eQTEBodyLanguage.pointUp, eQTEFacialExpression.happy);
                tutorialchan.QTEposition.anchoredPosition = new Vector2(0, 0);
                tutorialchan.QTEchanAnim.SetTrigger("fadein");
                yield return new WaitForSeconds(0.25f);
                break;
            case 6:
                tutorialchan.QTEchanAnim.SetTrigger("fadeout");
                yield return new WaitForSeconds(0.25f);
                SceneManager.LoadScene(2);
                break;
            default:
                tutorialchan.TutChan_Switch(eQTEBodyLanguage.pointUp, eQTEFacialExpression.shocked);
                break;
        }
        tutorialchan.ContinueButton.SetActive(true);
    }

    public void NamesPromptNextLine()
    {
        StartCoroutine(TriggerDialogue());
    }

    public void AllowNameInput()
    {
        tutorialchan.ContinueButton.SetActive(false);
        FindObjectOfType<TitleMenu>().InputPlayerNameWindow.SetActive(true);
    }
}
