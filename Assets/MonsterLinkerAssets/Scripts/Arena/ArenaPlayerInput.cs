using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

/// <summary>
/// Handles Player Input (except for QTEs?)
/// </summary>
public class ArenaPlayerInput : MonoBehaviour
{
    public InputBarHandler inputbarhandler;
    public ArenaUIHandler arenaui;
    public FeralArtCheck feralartcheck;    
    public void AddBaseattack(BaseAttack baseAttack)
    {
        SoundController.Instance.StartSFX(SoundController.SFX.ui_select);
        inputbarhandler.Add(baseAttack);
    }

    public void ExitGame()
    {
        StartCoroutine(Quit());
    }

    public IEnumerator Quit()
    {
        yield return new WaitForSeconds(0.5f);
        Application.Quit();
    }

    public void ResetPlayerInput()
    {
        SoundController.Instance.StartSFX(SoundController.SFX.ui_cancel);
        inputbarhandler.Reset();       
    }


    public void RetryFight()
    {
        SoundController.Instance.StopFightMusic();
        SoundController.Instance.StartSFX(SoundController.SFX.ui_select);
        Scene curScene = SceneManager.GetActiveScene();
        SoundController.Instance.StartMenuMusic();
        SceneManager.LoadScene(curScene.name);
    }

    public void NextFight()
    {
        SoundController.Instance.StopFightMusic();
        SoundController.Instance.StartSFX(SoundController.SFX.ui_loadoutEquip);
        Scene curScene = SceneManager.GetActiveScene();
        SoundController.Instance.StartMenuMusic();
        SceneManager.LoadScene(curScene.name);
    }

    public void BackToHome()
    {
        SoundController.Instance.StopFightMusic();
        SoundController.Instance.StartSFX(SoundController.SFX.ui_cancel);
        SceneManager.LoadScene(2);
    }

    public void CallCheatWindow()
    {
        arenaui.UseCheatWindow();
    }

    public void HP25()
    {
        GameStateSwitch.Instance.baeffectshandler.curPlayerHP = GameStateSwitch.Instance.baeffectshandler.maxPlayerHP * 0.25f;
        GameStateSwitch.Instance.baeffectshandler.UpdateHPandRPCounter();
        GameStateSwitch.Instance.baeffectshandler.UpdateHPandRPbars();
        arenaui.UseCheatWindow();
    }

    public void RP100()
    {
        GameStateSwitch.Instance.baeffectshandler.curPlayerRP = 100;
        GameStateSwitch.Instance.baeffectshandler.UpdateHPandRPCounter();
        GameStateSwitch.Instance.baeffectshandler.UpdateHPandRPbars();
        arenaui.UseCheatWindow();
    }

    public void TestStuff()
    {
        //VFXController.Instance.SpawnEffect(VFXController.VFX.TestVFX, VFXController.Position.TestMiddle);

        //float duration = Random.Range(0.1f, 0.2f);
        //float magnitude = Random.Range(1.5f, 2.25f);
        //StartCoroutine(GameStateSwitch.Instance.camshake.Shake(duration, magnitude));
    }

    //public void ChooseProfile(Save saveSlot)
    //{
    //    if (saveSlot.Used)
    //    {
    //        updateui.OpenDialogue(updateui.OverwriteProfileDialogue);
    //    }
    //    else
    //    {
    //        WriteProfile(saveSlot);
    //    }
    //}

    //public void WriteProfile(Save saveSlot)
    //{
    //    updateui.OpenDialogue(updateui.InsertPlayerDataDialogue);
    //}

    //public void DoNotOverwriteProfile()
    //{
    //    updateui.CloseDialogue(updateui.OverwriteProfileDialogue);
    //}

    //public void StartGame()
    //{
    //    //updateui.UIState(eHomeUI.Home);
    //}

    //-----------------------------------

    //public void ComboConfirm()
    //{
    //    ArenaManager.Instance.ArenaState = eArena.EnemyInput;
    //    //TODO state ändern
    //    //    print("combo confirmed");
    //    //    bool wasPressed = comboManager.Confirm();
    //    //    //TODO evaluate combo, go to fight animation screen

    //}

    //public void ExitArena()
    //{
    //    //TODO Go back to home menu - currently reloading scene
    //    GlobalManager.Instance.News.text += ("Demo end");
    //    Scene scene = SceneManager.GetActiveScene();
    //    SceneManager.LoadScene(scene.name);
    //}

    //public void StartFight()
    //{
    //    ArenaManager.Instance.ArenaState = eArena.Intro;
    //}

    //public void HideInfo()
    //{
    //    if (!hidden)
    //    {
    //        print("Hiding Info Panel");
    //        hidden = true;
    //        InputAnim.Play("hide");
    //        InputAnim.SetBool("normal", false);
    //        InputAnim.SetBool("hidden", true);
    //        InfoButtonText.text = "SHOW";
    //    }
    //    else
    //    {
    //        print("Showing Info panel");
    //        hidden = false;
    //        InputAnim.Play("show");
    //        InputAnim.SetBool("hidden", false);
    //        InputAnim.SetBool("normal", true);
    //        InfoButtonText.text = "HIDE";
    //    }
    //}
}
