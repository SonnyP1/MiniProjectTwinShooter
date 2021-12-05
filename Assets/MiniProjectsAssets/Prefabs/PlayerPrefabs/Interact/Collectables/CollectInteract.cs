using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectInteract : Interactable
{
    private ScoreKeeper _scoreKeeper;
    private InteractComp _playerInteractComp;

    void Start()
    {
        _scoreKeeper = FindObjectOfType<ScoreKeeper>();
        if (_scoreKeeper == null)
        {
            Debug.Log("Cannot find scoreKeeper type");
        }
    }
    public override void Interacting()
    {
        if (_scoreKeeper)
        {
            _scoreKeeper.AddOrSubScore(-1);
            _playerInteractComp.RemoveInteractable(this);
            Destroy(gameObject);
        }
    }
    
    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("PlayerInteract"))
        {
            _playerInteractComp = other.transform.GetComponent<InteractComp>();
        }
    }
}
