using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class EndgameScreenManager : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI _endgameText = null;
    [SerializeField]
    private Button _replayButton = null;

    public string Text
    {
        set => _endgameText.text = value;
    }

    public event EventHandler ReplayEvent = null;

    private void Start()
    {
        _replayButton.onClick.AddListener(() => ReplayEvent?.Invoke(this, EventArgs.Empty));
    }
}
