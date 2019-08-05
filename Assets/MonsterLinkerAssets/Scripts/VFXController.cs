using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VFXController : MonoBehaviour
{
    public static VFXController Instance;

    [Header("List of Positions to spawn the VFX")]
    public GameObject[] SpawnPosition;
    //One enum for each Position, needs to be the same order
    public enum Position
    {
        TestMiddle,

        //NumberofPositions needs to be the last in list!
        NumberofPositions
    }

    [Header("List of VFX prefabs")]
    public GameObject[] VFXEffect;
    //One enum name for each prefab, needs to be the same order
    public enum VFX
    {
        TestVFX,

        //NumberofVFX needs to be the last in list!
        NumberofVFX
    }    
    void Start()
    {
        #region Singleton
        if (Instance != null)
        {
            print("there is already a VFX controller!");
            Destroy(this);
        }
        else
        {
            Instance = this;
        }
        #endregion

        if (VFXEffect.Length != (int)VFX.NumberofVFX)
        {
            Debug.LogError("Not enough VFX assigned");
        }
        if (SpawnPosition.Length != (int)Position.NumberofPositions)
        {
            Debug.LogError("Not enough Transforms assigned");
        }
    }

    public void SpawnEffect(VFX effect, Position position)
    {
        GameObject newVFX = GameObject.Instantiate(this.VFXEffect[(int)effect], transform.position, transform.rotation) as GameObject;
        newVFX.name = "" + effect;
        //newVFX.transform.position = this.SpawnPosition[(int)position].transform.position;
    }
    
    public void SpawnEffectViaInt(VFX effect, int position)
    {
        GameObject newVFX = GameObject.Instantiate(this.VFXEffect[(int)effect], transform.position, transform.rotation) as GameObject;
        newVFX.name = "" + effect;
        newVFX.transform.position = this.SpawnPosition[position].transform.position;
    }
}
