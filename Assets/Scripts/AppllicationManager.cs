using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AppllicationManager : MonoBehaviour
{
    [SerializeField]
    private ushort _targetFPS = 30;
    void Start()
    {
        QualitySettings.vSyncCount = 0;
        Application.targetFrameRate = _targetFPS;
    }
}
