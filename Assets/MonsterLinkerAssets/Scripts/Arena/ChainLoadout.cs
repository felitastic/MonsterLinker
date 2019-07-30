using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChainLoadout : MonoBehaviour
{
    public List<FAChain> FA_Chains;
    public List<Attack> loadedFAs;
    public FeralArtCheck feralartcheck;

    public void ConvertLoadedFeralArts()
    {
        foreach(FeralArt FA in GameStateSwitch.Instance.curProfile.FALoadout)
        {
            loadedFAs.Add(FA);
        }
    }

    public void CheckForChainPossibility()
    {
        foreach(FAChain chain in FA_Chains)
        {
            print("checking chain " + chain.name);                     

            if (loadedFAs.Contains(chain.NeededFeralArts[0]) && loadedFAs.Contains(chain.NeededFeralArts[1]))
            {
                if (chain.ChainInputList.Count < 6)
                {
                    feralartcheck.Chain5.Add(chain);
                }
                else
                {
                    feralartcheck.Chain6.Add(chain);
                }
            }            
        }
    }
}
