using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonSounds : MonoBehaviour
{
    //TODO checken ob buttons für "switch between" sound?
    //public void OnDeselect(BaseEventData data)
    //{
    //    Debug.Log("Deselected");
    //}

    public void ConfirmSound()
    {
        SoundController.Instance.StartSFX(SoundController.SFX.ui_select);
    }

    public void CancelSound()
    {
        SoundController.Instance.StartSFX(SoundController.SFX.ui_cancel);

    }

    public void ErrorSound()
    {       

    }

    public void DeSelect()
    {
        SoundController.Instance.StartSFX(SoundController.SFX.ui_switchBetweenSlots);
    }
}
