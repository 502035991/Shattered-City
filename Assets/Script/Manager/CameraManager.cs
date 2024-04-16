using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    public static CameraManager instance;
    private void Awake()
    {
        if (instance == null)
            instance = this;
    }
    public void CameraShake(CinemachineImpulseSource source , float force)
    {
        source.GenerateImpulseWithForce(force);
    }
}
