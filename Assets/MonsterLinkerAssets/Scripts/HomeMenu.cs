using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class HomeMenu : MonoBehaviour
{
    public GameObject MenuScreen;
    public GameObject ExitPanel;
    public Text ExitText;
    public GameObject CreditPanel;
    public GameObject CreditTexts;     
    private Coroutine curRoutine;

    public MenuTutorial menututorial;
    [SerializeField] PreLoadScript preloadscript;

    [Header("For the credits lerp")]
    [Tooltip("Regulates how fast the credits roll, higher no = slower")]
    public float lerpTime = 5f;
    float curLerpTime;
    Vector3 StartPos;
    Vector3 EndPos;
    private bool lerping;

    public void Start()
    {
        preloadscript = FindObjectOfType<PreLoadScript>();

        if (preloadscript.curSave.Tutorial == eTutorial.menu)
        {
            menututorial.TriggerDialogue(1);
        }
        else if (preloadscript.curSave.Tutorial == eTutorial.loadout)
        {
            menututorial.TriggerDialogue(7);
        }
        else if (preloadscript.curSave.Tutorial == eTutorial.done)
        {
            menututorial.TriggerDialogue(0);
        }
    }
    public void GoToArena()
    {
        StartCoroutine(SoundController.Instance.StopMenuMusic());
        SceneManager.LoadScene(3);
    }

    public void QuitGame()
    {
        ExitPanel.SetActive(true);
        curRoutine = StartCoroutine(QuitText());
        //disable confirm buttons, only enable b press for cancel
    }    

    public IEnumerator QuitText()
    {
        float waitbetweendots = 0.7f;
        ExitText.text = "Separating link.";
        yield return new WaitForSeconds(waitbetweendots);
        ExitText.text = "Separating link. .";
        yield return new WaitForSeconds(waitbetweendots);
        ExitText.text = "Separating link. . .";
        yield return new WaitForSeconds(waitbetweendots);
        Application.Quit();
    }

    public void AbortQuitting()
    {
        StopCoroutine(curRoutine);
        ExitPanel.SetActive(false);
    }

    public void RunCredits()
    {
        StartPos = CreditTexts.GetComponent<RectTransform>().anchoredPosition;
        EndPos = StartPos + new Vector3(0, 1410, 0);
        CreditPanel.SetActive(true);
        lerping = true;
    }

    public void CloseCredits()
    {
        curLerpTime = 0.0f;
        lerping = false;
        CreditPanel.SetActive(false);
        CreditTexts.GetComponent<RectTransform>().anchoredPosition = StartPos;
    }

    public void FixedUpdate()
    {
        if (lerping)
        {
            curLerpTime += Time.deltaTime;

            if (curLerpTime > lerpTime)
                curLerpTime = lerpTime;

            float percentage = curLerpTime / lerpTime;
            CreditTexts.GetComponent<RectTransform>().anchoredPosition = Vector3.Lerp(StartPos, EndPos, percentage);

            if (CreditTexts.GetComponent<RectTransform>().anchoredPosition == new Vector2(EndPos.x, EndPos.y))
            {
                CloseCredits();
            }
        }
    }
}



