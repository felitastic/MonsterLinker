using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class HomeMenu : MonoBehaviour
{
    public Animator MainMenuBG;
    public GameObject MenuScreen;
    public GameObject ExitPanel;
    public Text ExitText;
    public GameObject CreditPanel;
    public GameObject CreditTexts;
    public Button CreditsButton;     
    public Button CreditsCancelButton;     
    private Coroutine curRoutine;

    public MenuTutorial menututorial;
    [SerializeField] PreLoadScript preloadscript;

    [Header("For the credits lerp")]
    [Tooltip("Regulates how fast the credits roll, higher no = slower")]
    public float lerpTime = 20f;
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
        SoundController.Instance.StartSFX(SoundController.SFX.ui_select);
        StartCoroutine(FadeOutToArena());               
    }

    public IEnumerator FadeOutToArena()
    {
        StartCoroutine(SoundController.Instance.StopMenuMusic());
        MainMenuBG.SetTrigger("fadeout");
        yield return new WaitForSeconds(0.4f);
        SceneManager.LoadScene(3);
    }

    public void QuitGame()
    {
        SoundController.Instance.StartSFX(SoundController.SFX.ui_select);
        ExitPanel.SetActive(true);
        curRoutine = StartCoroutine(QuitText());
        //disable confirm buttons, only enable b press for cancel
    }    

    public IEnumerator QuitText()
    {
        float waitbetweendots = 0.7f;
        ExitText.text = "Separating link.";
        SoundController.Instance.StartSFX(SoundController.SFX.ui_switchBetweenSlots);
        yield return new WaitForSeconds(waitbetweendots);
        ExitText.text = "Separating link. .";
        SoundController.Instance.StartSFX(SoundController.SFX.ui_switchBetweenSlots);
        yield return new WaitForSeconds(waitbetweendots);
        ExitText.text = "Separating link. . .";
        SoundController.Instance.StartSFX(SoundController.SFX.ui_switchBetweenSlots);
        yield return new WaitForSeconds(waitbetweendots);
        Application.Quit();
    }

    public void AbortQuitting()
    {
        SoundController.Instance.StartSFX(SoundController.SFX.ui_cancel);
        StopCoroutine(curRoutine);
        ExitPanel.SetActive(false);
    }

    public void RunCredits()
    {
        SoundController.Instance.StartSFX(SoundController.SFX.ui_select);
        CreditsCancelButton.Select();
        StartPos = CreditTexts.GetComponent<RectTransform>().anchoredPosition;
        EndPos = new Vector3(0, 3400, 0);
        CreditPanel.SetActive(true);
        lerping = true;
    }

    public void CloseCredits()
    {
        SoundController.Instance.StartSFX(SoundController.SFX.ui_cancel);
        curLerpTime = 0.0f;
        lerping = false;
        CreditPanel.SetActive(false);
        CreditsButton.Select();
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



