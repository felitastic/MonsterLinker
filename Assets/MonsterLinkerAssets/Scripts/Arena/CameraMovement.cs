using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    public List<GameObject> CamTransforms;
    public GameObject Camera;

    public float speed = 5f;
    public bool moving;

    Vector3 StartPos;
    Vector3 EndPos;

    public void SetPositions(eCamPosition camEndPos)
    {
        StartPos = Camera.transform.position;
        //set endposition depending on gamestate
        EndPos = CamTransforms[(int)camEndPos].transform.position;
    }

    public void MoveCamera()
    {
        moving = true;
    }

    void Update()
    {
        if (moving)
        {
            Camera.transform.position = Vector3.MoveTowards(StartPos, EndPos, speed * Time.deltaTime);

            if (Camera.transform.position == EndPos)
                moving = false;
        }
    }

    ////Time for the whole movement, the higher, the slower the object moves
    //public float lerpTime = 5f;
    ////Is set to the current time as long as the lerp is running
    //float curLerpTime;
    ////Start and End Position of the object
    //Transform StartPos;
    //Transform EndPos;
    ////To start the lerp
    //private bool lerping;

    //public void Update()
    //{
    //    if (lerping)
    //    {
    //        curLerpTime += Time.deltaTime;

    //        if (curLerpTime > lerpTime)
    //            curLerpTime = lerpTime;

    //        float percentage = curLerpTime / lerpTime;
    //        Camera.GetComponent<Transform>() = Vector3.Lerp(StartPos, EndPos, percentage); 
    //    }
    //}

    //public void StartLerp(eCamPosition camEndPos)
    //{
    //    StartPos = Camera.GetComponent<Transform>();
    //    //set endposition depending on gamestate
    //    EndPos = CamTransforms[(int)camEndPos];

    //    lerping = true;
    //}

    //public void EndLerp()
    //{
    //    //setting curtime back to zero
    //    curLerpTime = 0.0f;
    //    //ending lerp
    //    lerping = false;
    //}

}
