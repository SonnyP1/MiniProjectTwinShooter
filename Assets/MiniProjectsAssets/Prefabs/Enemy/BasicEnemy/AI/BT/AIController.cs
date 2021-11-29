using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
public delegate void OnBlackBoardKeyUpdated(string key, object value);
public class AIController : MonoBehaviour
{
    [SerializeField] BehaviorTree _behaviorTree;
    [SerializeField] PerceptionComp _perceptionComp;
    public OnBlackBoardKeyUpdated onBlackBoardKeyUpdated;
    public AttackTarget onAttack;
    private Dictionary<string, object> _blackBoard = new Dictionary<string, object>();
    
    public void AddBlackBoardKey(string key,object defaultVal = null)
    {
        if (!_blackBoard.ContainsKey(key))
        {
            _blackBoard.Add(key,defaultVal);
        }
    }

    public object GetBlackBoardVal(string key)
    {
        return _blackBoard[key];
    }

    public void SetBlackBoardKey(string key, object val)
    {
        if (_blackBoard.ContainsKey(key))
        {
            _blackBoard[key] = val;
            if (onBlackBoardKeyUpdated != null)
            {
                onBlackBoardKeyUpdated.Invoke(key,val);
            }
        }
    }

    public BehaviorTree GetBehaviorTree()
    {
        return _behaviorTree;
    }
    void Start()
    {
        if (_behaviorTree != null)
        {
            _behaviorTree.Init(this);
        }

        if (_perceptionComp != null)
        {
            _perceptionComp.onPerceptionUpdated += PerceptionUpdated;
        }
    }

    private void PerceptionUpdated(bool successfullysensed, PerceptionStimuli stimuli)
    {
        if (successfullysensed)
        {
            SetBlackBoardKey("Target",stimuli.gameObject);
        }
        else
        {
            SetBlackBoardKey("Target",null);
            SetBlackBoardKey("LastKnownLoc",stimuli.gameObject.transform.position);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (_behaviorTree)
        {
            _behaviorTree.Run();
        }
    }
}
