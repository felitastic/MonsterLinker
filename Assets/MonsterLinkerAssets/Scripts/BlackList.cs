using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public class BlackList : MonoBehaviour
{
    public VideoPlayer videoplayer;
    public VideoClip[] blacklist;


    private void Update()
    {
        //if ((int)videoplayer.frame == (int)videoplayer.frameCount)
        //{
        //    StartCoroutine(EndVideo());
        //}
    }
    public void PlayBlacklistVideo()
    {
        print("starting video");
        videoplayer.clip = blacklist[GameStateSwitch.Instance.preloadscript.curSave.Arena-1];
        videoplayer.Play();
        StartCoroutine(EndVideo());
        //float waitTime = (float)videoplayer.clip.length;
        //yield return new WaitForSeconds(waitTime);
        //int framcount = (int)videoplayer.frameCount;
        //int frame = (int)videoplayer.frame;
    }

    public IEnumerator EndVideo()
    {
        yield return new WaitForSeconds(5.75f);
        videoplayer.Stop();
        //GameStateSwitch.Instance.cameramovement.SetCamPosition(eCamPosition.loadout);
        GameStateSwitch.Instance.SwitchState(eGameState.Loadout);
    }
}
