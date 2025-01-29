using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Ebac.Core.Singleton;

public class VFXManager : Singleton<VFXManager>
{
    public enum VFXType
    {
        JUMP,
        VFX_2,
    }

    public List<VFXManagerSetup> vfxSetups;
        
}

[System.Serializable]
public class VFXManagerSetup
{
    public VFXManager.VFXType vfxtype;
    public GameObject prefab;


}
