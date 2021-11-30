using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class MovementComp : MonoBehaviour
{
    [Header("Movement")]
    [SerializeField] float WalkingSpeed;
    [SerializeField] float DodgeMultiplierOfWalkingSpeed;
    [SerializeField] float DodgeLengthOfTime;
    [Header("Disable&Reenable during dodge for player")]
    [SerializeField] BoxCollider characterHitBox;
    [SerializeField] TrailRenderer trailRenderer;
    IInputActionCollection InputActions;
    Vector3 Velocity;
    const float GRAVITY = -9.8f;
    Vector2 MoveInput;
    Vector2 MouseLoc;
    Coroutine DodgeCoroutine = null;
    CharacterController playerController;
    private void Start()
    {
        playerController = GetComponent<CharacterController>();
        playerController.detectCollisions = false;
    }
    private void Update()
    {
        CalcuateWalkingVelocity();
        playerController.Move(Velocity * Time.deltaTime);
    }
    public void UpdateRotation()
    {
        if (DodgeCoroutine == null)
        {
            RaycastHit hit;
            Ray pointToRayCast = Camera.main.ScreenPointToRay(MouseLoc);
            if (Physics.Raycast(pointToRayCast, out hit))
            {
                //Debug.Log("Hit Point: " + hit.point);
                Quaternion playerDir = Quaternion.LookRotation(hit.point - transform.position);
                playerDir.x = 0;
                playerDir.z = 0;
                transform.rotation = playerDir;
            }
        }
    }
    void CalcuateWalkingVelocity()
    {
        if (DodgeCoroutine == null)
        {
            if (playerController.isGrounded)
            {
                Velocity.y = -0.2f;
            }
            Velocity.x = PlayerMovementDir().x * WalkingSpeed;
            Velocity.z = PlayerMovementDir().z * WalkingSpeed;
        }
        Velocity.y += GRAVITY * Time.deltaTime;
    }
    public void MainDodge()
    {
        if (DodgeCoroutine == null)
        {
            DodgeCoroutine = StartCoroutine(DodgeToDirection(DodgeLengthOfTime));
        }
        else { Debug.Log("I DIDNT DODGE"); }
    }



    IEnumerator DodgeToDirection(float MaxTime)
    {
        Vector3 LastInputs = PlayerMovementDir();
        InputActions.Disable();
        characterHitBox.enabled = false;
        trailRenderer.enabled = true;


        float startTime = 0;
        while (startTime < MaxTime)
        {
            //Calcuate Dodge Movement
            startTime += Time.deltaTime;
            if (playerController.isGrounded)
            {
                Velocity.y = -0.2f;
            }
            Velocity.x = LastInputs.x * DodgeMultiplierOfWalkingSpeed * WalkingSpeed;
            Velocity.z = LastInputs.z * DodgeMultiplierOfWalkingSpeed * WalkingSpeed;
            yield return new WaitForEndOfFrame();
        }


        DodgeCoroutine = null;
        trailRenderer.enabled = false;
        characterHitBox.enabled = true;
        
        InputActions.Enable();
    }


    Vector3 PlayerMovementDir()
    {
        return new Vector3(-MoveInput.y, 0, MoveInput.x).normalized;
    }
    public void SetMoveInput(Vector2 newMoveInput)
    {
        MoveInput = newMoveInput;
    }
    public void SetMouseLoc(Vector2 newMouseLoc)
    {
        MouseLoc = newMouseLoc;
    }

    public void SetInput(IInputActionCollection inputAction)
    {
        InputActions = inputAction;
    }


}
