using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SightPerceptionComp : PerceptionComp
{
	List<PerceptionStimuli> CurrentlySeeingStimuli = new List<PerceptionStimuli>();
    [SerializeField] float sightRadius = 5f;
    [SerializeField] float lostSightRadius = 6f;
	[SerializeField] float peripheralAngleDegree = 80f;
	[SerializeField] float eyeHeight = 1.6f;
	[SerializeField] float attackRadius = 1.6f;

	public override bool EvaluatePerception(PerceptionStimuli stimuli)
    {
	    bool InSightRange = this.InSightRange(stimuli);
		bool IsNotBlocked = this.IsNotBlocked(stimuli);
		bool IsInPeripheralAngle = this.IsInPeripheralAngle(stimuli);

		bool Percepted = InSightRange && IsNotBlocked && IsInPeripheralAngle;
		if(Percepted && !CurrentlySeeingStimuli.Contains(stimuli))
		{
			CurrentlySeeingStimuli.Add(stimuli);
			if(onPerceptionUpdated != null)
			{
				Debug.Log($"I HAVE SEEN {stimuli.gameObject}");
				
				onPerceptionUpdated.Invoke(true, stimuli);
			}
		}

		if(!Percepted && CurrentlySeeingStimuli.Contains(stimuli))
		{
			CurrentlySeeingStimuli.Remove(stimuli);
			if(onPerceptionUpdated != null)
			{
				Debug.Log($"I HAVE LOST TRACK {stimuli.gameObject}");
				onPerceptionUpdated.Invoke(false, stimuli);
			}	
		}
		
		return Percepted;
    }
	
	bool InSightRange(PerceptionStimuli stimuli)
	{
		Vector3 ownerPos = transform.position;
		Vector3 stimuliPos = stimuli.transform.position;
		float checkRadius = sightRadius;
		if(CurrentlySeeingStimuli.Contains(stimuli))
		{
			checkRadius = lostSightRadius;
		}
		return Vector3.Distance(ownerPos,stimuliPos) < checkRadius;
	}

	bool IsNotBlocked(PerceptionStimuli stimuli)
	{
		Vector3 stimuliCheckPos = stimuli.GetComponent<Collider>().bounds.center;
		Vector3 EyePos = transform.position+Vector3.up*eyeHeight;
		Ray ray = new Ray(EyePos,(stimuliCheckPos-EyePos).normalized);
		if(Physics.Raycast(ray,out RaycastHit HitResult,lostSightRadius))
		{
			if(HitResult.collider.gameObject == stimuli.gameObject)
			{
				return true;
			}
			else
			{
				return false;
			}
		}

		return false;
	}

	bool IsInPeripheralAngle(PerceptionStimuli stimuli)
	{
		float AngleToStimuli = Mathf.Rad2Deg * Mathf.Acos(Vector3.Dot(transform.forward,(stimuli.transform.position - transform.position).normalized));
		return AngleToStimuli < peripheralAngleDegree/2;
	}
	
	private void OnDrawGizmos()
	{
	    Gizmos.DrawWireSphere(transform.position,sightRadius);
	    Gizmos.DrawWireSphere(transform.position,lostSightRadius);
	    Gizmos.DrawWireSphere(transform.position,attackRadius);
	    Vector3 forward = transform.forward;
	    Quaternion RotateRight = Quaternion.AngleAxis(peripheralAngleDegree / 2, Vector3.up);
	    Quaternion RotateLeft = Quaternion.AngleAxis(-peripheralAngleDegree / 2, Vector3.up);
	    Gizmos.DrawLine(transform.position,transform.position + RotateLeft * forward * lostSightRadius);
	    Gizmos.DrawLine(transform.position,transform.position + RotateRight * forward * lostSightRadius);
	    foreach (var stimuli in CurrentlySeeingStimuli)
	    {
		    Gizmos.DrawLine(transform.position,stimuli.transform.position);
	    }
	}
}
