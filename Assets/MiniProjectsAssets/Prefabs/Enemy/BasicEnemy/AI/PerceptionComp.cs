using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public delegate void OnPerceptionUpdated(bool successfullySensed, PerceptionStimuli stimuli);
public abstract class PerceptionComp : MonoBehaviour
{ 
    public abstract bool EvaluatePerception(PerceptionStimuli stimuli);
    public OnPerceptionUpdated onPerceptionUpdated;

    private void Start()
    {
        FindObjectOfType<PerceptionSystem>().AddListener(this);
    }

    private void OnDestroy()
    {
        PerceptionSystem system = FindObjectOfType<PerceptionSystem>();
        if (system != null)
        {
            system.RemoveListener(this);
        }
    }
}
