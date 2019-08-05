using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    public List<GameObject> CamTransforms;
    public GameObject CamHolder;

    Quaternion StartQuaternion;
    Vector3 StartPos;
    Quaternion EndQuaternion;
    Vector3 EndPos;

    //Time for the whole movement, the higher, the slower the object moves
    public float lerpTime = 5f;
    //Is set to the current time as long as the lerp is running
    float curLerpTime;
    //To start the lerp
    public bool lerping;
    
    public void Update()
    {
        if (lerping)
        {
            curLerpTime += Time.deltaTime;

            if (curLerpTime > lerpTime)
                curLerpTime = lerpTime;

            float percentage = curLerpTime / lerpTime;
            CamHolder.transform.position = Vector3.Lerp(StartPos, EndPos, percentage);
            CamHolder.transform.rotation = Quaternion.Lerp(StartQuaternion, EndQuaternion, percentage);

            if (Mathf.Approximately(percentage, 1.0f))
            {
                EndLerp();
            }
        } 
    }

    public void SetCamPosition(eCamPosition camEndPos)
    {
        CamHolder.transform.position = CamTransforms[(int)camEndPos].transform.position;
        CamHolder.transform.rotation = CamTransforms[(int)camEndPos].transform.rotation;
    }

    public void SetPositions(eCamPosition camEndPos)
    {
        StartPos = CamHolder.transform.position;
        StartQuaternion = CamHolder.transform.rotation;
        EndPos = CamTransforms[(int)camEndPos].transform.position;
        EndQuaternion = CamTransforms[(int)camEndPos].transform.rotation;
    }

    public void StartLerp(float curSpeed)
    {
        //StartPos = Camera.GetComponent<Transform>();
        ////set endposition depending on gamestate
        //EndPos = CamTransforms[(int)camEndPos];
        lerpTime = curSpeed;
        lerping = true;
    }

    public void EndLerp()
    {
        //setting curtime back to zero
        curLerpTime = 0.0f;
        //ending lerp
        lerping = false;
    }

}
