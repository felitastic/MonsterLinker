using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public class BlackList : MonoBehaviour
{
    public VideoPlayer videoplayer;
    public VideoClip[] blacklist;

    public IEnumerator PlayBlacklistVideo()
    {
        videoplayer.clip = blacklist[GameStateSwitch.Instance.preloadscript.curSave.Arena];
        videoplayer.Play();
        yield return new WaitForSeconds(5.5f);
        videoplayer.Stop();
    }
}
