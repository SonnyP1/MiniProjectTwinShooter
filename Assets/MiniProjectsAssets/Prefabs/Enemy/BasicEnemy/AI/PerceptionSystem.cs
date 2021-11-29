using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PerceptionSystem : MonoBehaviour
{
    private List<PerceptionComp> _listerners = new List<PerceptionComp>();
    private List<PerceptionStimuli> _stimulies = new List<PerceptionStimuli>();
    public void AddListener(PerceptionComp perceptionComp)
    {
        _listerners.Add(perceptionComp);
    }

    public void RemoveListener(PerceptionComp perceptionComp)
    {
        _listerners.Remove(perceptionComp);
    }

    public void RegisterStimuli(PerceptionStimuli stimuli)
    {
        _stimulies.Add(stimuli);
    }

    public void UnRegisterStimuli(PerceptionStimuli stimuli)
    {
        _stimulies.Remove(stimuli);
    }

    private void Update()
    {
        for (int i = 0; i < _stimulies.Count; i++)
        {
            for (int j = 0; j < _listerners.Count; j++)
            {
                PerceptionComp listener = _listerners[i];
                if (listener)
                {
                    listener.EvaluatePerception(_stimulies[i]);
                }
            }
        }
    }
}
