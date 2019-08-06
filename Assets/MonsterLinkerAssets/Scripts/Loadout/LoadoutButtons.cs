using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class LoadoutButtons : MonoBehaviour
{
    [Header("Drag n drop")]
    public eLoadout Window;
    
    [Header("All available FAs")]
    public List<FeralArt> AllFAs;
    [Header("All available SIs")]
    public List<Implant> AllSIs;
    [Header("All FA Choice GameObject")]
    public List<GameObject> FAChoiceButtons;
    [Header("All SI Choice GameObject")]
    public List<GameObject> SIChoiceButtons;
    [Header("FA Field Prefab")]
    public GameObject FAField;
    [Tooltip("Icon Prefab, same as used for FA Info Window")]
    public GameObject Icon;
    [Tooltip("Object FA_Grid")]
    public GameObject FAFieldParent;

    public GameObject FeralArtChoice;
    public GameObject ImplantChoice;
    public Animator FeralArtChoiceAnim;
    public Animator ImplantChoiceAnim;

    public GameObject EventSystem_Normal;

    public Button FAChoiceButton1;
    public Button ImplantChoiceButton1;
    public List<Button> MenuButtons;

    public List<FeralArt> LoadedFAs = new List<FeralArt>(3);
    public List<Button> FAChoiceButton;
    public List<Button> SIChoiceButton;
    public List<Text> ChoosenTexts = new List<Text>(4);

    [Header("No touchie")]
    public Button curLeftButton;
    public Text curLeftText;
    public Text ImplantText;

    public void Update()
    {
        if (Input.GetButtonDown("Cancel") && GameStateSwitch.Instance.GameState == eGameState.Loadout)
        {
            switch (Window)
            {
                case eLoadout.LoadoutOnly:
                    print("all good, nothing to cancel here");
                    break;
                case eLoadout.FeralArtChoice:
                    //disable back button
                    SoundController.Instance.StartSFX(SoundController.SFX.ui_cancel);
                    WindowSwitch(eLoadout.LoadoutOnly);
                    break;
                case eLoadout.ImplantChoice:
                    //disable back button
                    SoundController.Instance.StartSFX(SoundController.SFX.ui_cancel);
                    WindowSwitch(eLoadout.LoadoutOnly);
                    break;
            }
        }
    }

    public void WriteFAList()
    {
        foreach (FeralArt feralart in AllFAs)
        {
            GameObject FAfield = GameObject.Instantiate(FAField, transform.position, transform.rotation) as GameObject;
            FAfield.transform.parent = FAFieldParent.transform;
            FAfield.transform.localScale = new Vector3(1, 1, 1);

            Text Name = FAfield.GetComponentInChildren<FaNameField>().GetComponent<Text>();
            Text Cost = FAfield.GetComponentInChildren<FaCostField>().GetComponent<Text>();
            Name.text = feralart.FAName;
            Cost.text = "" + feralart.RPCost;

            //adding the button function
            FAfield.GetComponent<Button>().onClick.AddListener(delegate { ChooseFA(feralart); });

            //adding buttons to the list for description BS check
            FAChoiceButtons.Add(FAfield);

            //getting the first button in line
            if (Name.text == AllFAs[0].FAName)
            {
                FAChoiceButton1 = FAfield.GetComponentInChildren<Button>();
            }

            GameObject IconParent = FAfield.GetComponentInChildren<FaInputField>().gameObject;

            //Spawn input icons
            int n = 0;
            while (n < feralart.FeralArtInput.Count)
            {
                GameObject icon = GameObject.Instantiate(Icon, transform.position, transform.rotation) as GameObject;

                icon.transform.parent = IconParent.transform;
                icon.transform.localScale = new Vector3(1, 1, 1);

                Image iconImg = icon.GetComponent<Image>();
                iconImg.sprite = feralart.FeralArtInput[n].InfoPanelIcon;
                n += 1;
            }
        }
    }

    public void SetInitialTexts()
    {
        LoadedFAs = GameStateSwitch.Instance.curProfile.FALoadout;

        ChoosenTexts[0].text = LoadedFAs[0].FAName;
        ChoosenTexts[1].text = LoadedFAs[1].FAName;
        ChoosenTexts[2].text = LoadedFAs[2].FAName;

        if (GameStateSwitch.Instance.curProfile.curImplant != null)
            ChoosenTexts[3].text = GameStateSwitch.Instance.curProfile.curImplant.ImplantName;

        //if (LoadedFAs[2] != null && GameStateSwitch.Instance.curProfile.curImplant != null)
        //    StartButton.Select();
        //else
        //    curLeftButton.Select();
    }

    public void ConfirmLoadout() 
    {
        if (LoadedFAs[0] != null && LoadedFAs[1] != null && LoadedFAs[2] != null && GameStateSwitch.Instance.curProfile.curImplant != null)
        {
            //SoundController.Instance.StartSFX(SoundController.SFX.ui_select);
            //StartCoroutine(SoundController.Instance.StopMenuMusic());
            StartCoroutine(WaitForButtonAnim());
        }
        else
        {
            SoundController.Instance.StartSFX(SoundController.SFX.ui_error);
            print("not enough FAs chosen");
        }
    }

    public IEnumerator WaitForButtonAnim()
    {
        SoundController.Instance.StartSFX(SoundController.SFX.ui_select);
        GameStateSwitch.Instance.Implant = GameStateSwitch.Instance.curProfile.curImplant.ImplantType;
        GameStateSwitch.Instance.curProfile.FALoadout = LoadedFAs;
        yield return new WaitForSeconds(0.5f);
        //SoundController.Instance.AudioSourceBGM_Menu.mute = true;
        //SoundController.Instance.AudioSourceBGM_Fight.mute = false;
        GameStateSwitch.Instance.arenaui.FALoadout.SetActive(false);
        GameStateSwitch.Instance.SwitchState(eGameState.Intro);
    }

    public void OpenFAChoice(Button thisButton)
    {
        SoundController.Instance.StartSFX(SoundController.SFX.ui_select);
        curLeftButton = thisButton;
        curLeftText = curLeftButton.GetComponentInChildren<Text>();        
        WindowSwitch(eLoadout.FeralArtChoice);
        FAChoiceButton1.Select();        
    }
    public void ChooseFA(FeralArt thisFA)
    {
        if (!LoadedFAs.Contains(thisFA))
        {
            SoundController.Instance.StartSFX(SoundController.SFX.ui_loadoutEquip);
            int slotNo = int.Parse(curLeftButton.name);
            LoadedFAs[slotNo] = thisFA;
            print("pressed button for FA choice "+thisFA.name);
            curLeftText.text = thisFA.FAName;
        }
        else
        {
            SoundController.Instance.StartSFX(SoundController.SFX.ui_error);
            print("FA already in list");
        }
        WindowSwitch(eLoadout.LoadoutOnly);
        curLeftButton.Select();
    }

    public void OpenImplantChoice(Button thisButton)
    {
        SoundController.Instance.StartSFX(SoundController.SFX.ui_select);
        curLeftButton = thisButton;
        curLeftText = curLeftButton.GetComponentInChildren<Text>();        
        WindowSwitch(eLoadout.ImplantChoice);
        ImplantChoiceButton1.Select();
    }

    public void ChooseImplant(Implant implant)
    {
        if (GameStateSwitch.Instance.curProfile.curImplant != implant)
        {
            SoundController.Instance.StartSFX(SoundController.SFX.ui_loadoutEquip);
            GameStateSwitch.Instance.curProfile.curImplant = implant;
            curLeftText.text = implant.ImplantName;
        }
        else
        {
            print("implant already chosen");
            SoundController.Instance.StartSFX(SoundController.SFX.ui_error);
        }
        WindowSwitch(eLoadout.LoadoutOnly);
        curLeftButton.Select();
    }

    public void WindowSwitch(eLoadout window)
    {
        Window = window;

        switch (Window)
        {
            case eLoadout.LoadoutOnly:
                foreach (Button button in MenuButtons)
                {
                    button.enabled = true;
                }
                curLeftButton.GetComponentInChildren<Animator>().SetBool("Cursor", false);
                CloseWindows();
                break;
            case eLoadout.FeralArtChoice:
                foreach (Button button in MenuButtons)
                {
                    button.enabled = false;
                }
                curLeftButton.GetComponentInChildren<Animator>().SetBool("Cursor", true);
                FeralArtChoice.SetActive(true);
                break;
            case eLoadout.ImplantChoice:
                foreach (Button button in MenuButtons)
                {
                    button.enabled = false;
                }
                curLeftButton.GetComponentInChildren<Animator>().SetBool("Cursor", true);
                ImplantChoice.SetActive(true);
                break;
        }
    }

    public void CloseWindows()
    {
        FeralArtChoice.SetActive(false);
        ImplantChoice.SetActive(false);
        curLeftButton.Select();
    }
}
