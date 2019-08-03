using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class TitleMenu : MonoBehaviour
{
    [Tooltip("Initiate Link Button at Game Start")]
    public GameObject TitleButton;
    public GameObject SaveLoadWindow;
    public GameObject InputPlayerNameWindow;
    public GameObject Save1;
    public GameObject Save2;
    public GameObject Save3;
    public InputField PlayerName;
    public Button curSaveButton;
    private PreLoadScript preloadscript;
    private TitleTutorial titletutorial;

    public void Start()
    {
        TitleButton.SetActive(true);
        SaveLoadWindow.SetActive(false);
        preloadscript = FindObjectOfType<PreLoadScript>();
        titletutorial = FindObjectOfType<TitleTutorial>();
        WriteSaveData();
    }

    public void PressLoadButton()
    {
        SelectedSaveSlot();
        LoadSave(SelectedSaveSlot());
    }

    public void PressDeleteButton()
    { 
        SelectedSaveSlot();
        DeleteSave(SelectedSaveSlot());
    }

    public void WriteSaveData()
    {
        if (preloadscript.Save1.Empty)
        {
            Save1.GetComponentInChildren<Text>().text = "Save 1: New Game";
        }
        else
        {
            Save1.GetComponentInChildren<Text>().text = "" + preloadscript.Save1.LinkerName;
        }

        if (preloadscript.Save2.Empty)
        {
            Save2.GetComponentInChildren<Text>().text = "Save 2: New Game";
        }
        else
        {
            Save2.GetComponentInChildren<Text>().text = "" + preloadscript.Save2.LinkerName;
        }

        if (preloadscript.Save3.Empty)
        {
            Save3.GetComponentInChildren<Text>().text = "Save 3: New Game";
        }
        else
        {
            Save3.GetComponentInChildren<Text>().text = "" + preloadscript.Save3.LinkerName;
        }
    }

    public void InitiatingLink()
    {
        TitleButton.GetComponentInChildren<Button>().interactable = false;
        StartCoroutine(WaitForGlitchyButton());
    }

    public IEnumerator WaitForGlitchyButton()
    {
        yield return new WaitForSeconds(0.8f);
        TitleButton.SetActive(false);
        SaveLoadWindow.SetActive(true);
    }

    public int SelectedSaveSlot()
    {
        int SlotNo = 0;
        if (Save1 == EventSystem.current.currentSelectedGameObject.gameObject)
        {
            curSaveButton = Save1.GetComponentInChildren<Button>();
            SlotNo = 1;
        }
        if (Save2 == EventSystem.current.currentSelectedGameObject.gameObject)
        {
            curSaveButton = Save2.GetComponentInChildren<Button>();
            SlotNo = 2;
        }
        if (Save3 == EventSystem.current.currentSelectedGameObject.gameObject)
        {
            curSaveButton = Save3.GetComponentInChildren<Button>();
            SlotNo = 3;
        }
        print("slotno " + SlotNo);
        return SlotNo;
    }

    public void LoadSave(int slotNo)
    {
        switch (slotNo)
        {
            case 1:
                preloadscript.curSave = preloadscript.Save1;
                break;
            case 2:
                preloadscript.curSave = preloadscript.Save2;
                break;
            case 3:
                preloadscript.curSave = preloadscript.Save3;
                break;
            default:
                Debug.LogError("could not find save, loading default 1");
                preloadscript.curSave = preloadscript.Save1;
                break;
        }

        if (preloadscript.curSave.Empty)
        {
            SaveLoadWindow.SetActive(false);
            StartCoroutine(titletutorial.TriggerDialogue());
        }
        else
        {
            SceneManager.LoadScene(2);
        }
    }

    public void DeleteSave(int slotNo)
    {
        switch (slotNo)
        {
            case 1:
                preloadscript.Save1.ResetSave();
                break;
            case 2:
                preloadscript.Save2.ResetSave();
                break;
            case 3:
                preloadscript.Save3.ResetSave();
                break;
            default:
                Debug.LogError("could not find save to delete");
                break;
        }
        WriteSaveData();
        curSaveButton.Select();
    }

    public void ConfirmPlayerName()
    {
        preloadscript.curSave.LinkerName = PlayerName.text;
        InputPlayerNameWindow.SetActive(false);
        titletutorial.TriggerDialogue();
    }
}
