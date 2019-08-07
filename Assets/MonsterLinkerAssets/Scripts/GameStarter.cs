using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameStarter : MonoBehaviour
{
    public Animator Logo;

    void Start()
    {
        StartCoroutine(PlayLogo());
    }

    IEnumerator PlayLogo()
    {
        yield return new WaitForSeconds(0.1f);
        SoundController.Instance.StartSFX(SoundController.SFX.logo_giggle);
        Logo.SetTrigger("play");
        yield return new WaitForSeconds(2.25f);
        SceneManager.LoadScene(1);
    }
}
