using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class LoadoutInfoWindow : MonoBehaviour
{
    public LoadoutButtons loadoutbuttons;
    public Text FA_InfoWindowText;
    public Text SI_InfoWindowText;

    public void Update()
    {
        switch (loadoutbuttons.Window)
        {
            case eLoadout.LoadoutOnly:
                break;
            case eLoadout.FeralArtChoice:
                if (EventSystem.current.currentSelectedGameObject == loadoutbuttons.FAChoiceButtons[0])
                {
                    FA_InfoWindowText.text = loadoutbuttons.AllFAs[0].Description;
                }
                if (EventSystem.current.currentSelectedGameObject == loadoutbuttons.FAChoiceButtons[1])
                {
                    FA_InfoWindowText.text = loadoutbuttons.AllFAs[1].Description;
                }
                if (EventSystem.current.currentSelectedGameObject == loadoutbuttons.FAChoiceButtons[2])
                {
                    FA_InfoWindowText.text = loadoutbuttons.AllFAs[2].Description;
                }
                if (EventSystem.current.currentSelectedGameObject == loadoutbuttons.FAChoiceButtons[3])
                {
                    FA_InfoWindowText.text = loadoutbuttons.AllFAs[3].Description;
                }
                if (EventSystem.current.currentSelectedGameObject == loadoutbuttons.FAChoiceButtons[4])
                {
                    FA_InfoWindowText.text = loadoutbuttons.AllFAs[4].Description;
                }
                if (EventSystem.current.currentSelectedGameObject == loadoutbuttons.FAChoiceButtons[5])
                {
                    FA_InfoWindowText.text = loadoutbuttons.AllFAs[5].Description;
                }
                if (EventSystem.current.currentSelectedGameObject == loadoutbuttons.FAChoiceButtons[6])
                {
                    FA_InfoWindowText.text = loadoutbuttons.AllFAs[6].Description;
                }
                break;
            case eLoadout.ImplantChoice:

                if (EventSystem.current.currentSelectedGameObject == loadoutbuttons.SIChoiceButtons[0])
                {
                    SI_InfoWindowText.text = loadoutbuttons.AllSIs[0].Description;
                }
                if (EventSystem.current.currentSelectedGameObject == loadoutbuttons.SIChoiceButtons[1])
                {
                    SI_InfoWindowText.text = loadoutbuttons.AllSIs[1].Description;
                }
                if (EventSystem.current.currentSelectedGameObject == loadoutbuttons.SIChoiceButtons[2])
                {
                    SI_InfoWindowText.text = loadoutbuttons.AllSIs[2].Description;
                }
                if (EventSystem.current.currentSelectedGameObject == loadoutbuttons.SIChoiceButtons[3])
                {
                    SI_InfoWindowText.text = loadoutbuttons.AllSIs[3].Description;
                }

                break;
        }
    }
}
