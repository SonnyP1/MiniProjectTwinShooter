using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
public class PlayerScript : MonoBehaviour
{
    //Movement
    [Header("Movement")]
    [SerializeField] float WalkingSpeed;
    [SerializeField] float DodgeMultiplierOfWalkingSpeed;
    [SerializeField] float DodgeLengthOfTime;
    [SerializeField] BoxCollider playerHitBox;
    Vector3 Velocity;
    const float GRAVITY = -9.8f;
    Vector2 MoveInput;
    Vector2 MouseLoc;
    Coroutine DodgeCoroutine = null;
    CharacterController playerController;
    //Gun Stuff
    [SerializeField] Weapon tempWeapon;
    //Player Needs Stuff
    PlayerInputs playerInput;

    private void Awake()
    {
        playerInput = new PlayerInputs();
    }

    private void OnEnable()
    {
        playerInput.Enable();
    }
    private void OnDisable()
    {
        playerInput.Disable();
    }

    private void Start()
    {
        playerController = GetComponent<CharacterController>();
        playerInput.Gameplay.Move.performed += OnMoveInputUpdate;
        playerInput.Gameplay.Move.canceled += OnMoveInputUpdate;
        playerInput.Gameplay.MouseLoc.performed += OnMouseUpdate;
        playerInput.Gameplay.OnLeftClick.performed += OnLeftClickUpdated;
        playerInput.Gameplay.OnRightClick.performed += OnRightClickedUpdated;
        playerInput.Gameplay.Dodge.performed += OnDodgePressedUpdated;
    }

    void OnDodgePressedUpdated(InputAction.CallbackContext ctx)
    {
        MainDodge();
    }
    void OnRightClickedUpdated(InputAction.CallbackContext ctx)
    {
        tempWeapon.SecoundaryAttack();
    }
    void OnLeftClickUpdated(InputAction.CallbackContext ctx)
    {
        //check what weapons is using

        //in weapons type then fire
        tempWeapon.Attack();
    }
    void OnMouseUpdate(InputAction.CallbackContext ctx)
    {
        MouseLoc = ctx.ReadValue<Vector2>();
        if (DodgeCoroutine == null)
        {
            UpdateRotation();
        }
    }
    void OnMoveInputUpdate(InputAction.CallbackContext ctx)
    {
        MoveInput = ctx.ReadValue<Vector2>();
    }

    private void Update()
    {
        CalcuateWalkingVelocity();
        playerController.Move(Velocity * Time.deltaTime);
    }

    void MainDodge()
    {
        if (DodgeCoroutine == null)
        {
            DodgeCoroutine = StartCoroutine(DodgeToDirection(DodgeLengthOfTime));
        }
        else { Debug.Log("I DIDNT DODGE"); }
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

    void UpdateRotation()
    {
        RaycastHit hit;
        Ray pointToRayCast = Camera.main.ScreenPointToRay(MouseLoc);
        if(Physics.Raycast(pointToRayCast,out hit))
        {
            //Debug.Log("Hit Point: " + hit.point);
            Quaternion playerDir = Quaternion.LookRotation( hit.point - transform.position);
            playerDir.x = 0;
            playerDir.z = 0;
            transform.rotation = playerDir;
        }
    }

    Vector3 PlayerMovementDir()
    {
        return new Vector3(-MoveInput.y, 0, MoveInput.x).normalized;
    }


    IEnumerator DodgeToDirection(float MaxTime)
    {
        Vector3 LastInputs = PlayerMovementDir();
        playerInput.Gameplay.Move.Disable();
        playerHitBox.enabled = false;
        DisableShooting();


        float startTime = 0;
        while(startTime < MaxTime)
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
        playerHitBox.enabled = true;
        playerInput.Gameplay.Move.Enable();
        EnabledShooting();
    }

    void DisableShooting()
    {
        playerInput.Gameplay.OnLeftClick.Disable();
        playerInput.Gameplay.OnRightClick.Disable();
    }
    void EnabledShooting()
    {
        playerInput.Gameplay.OnLeftClick.Enable();
        playerInput.Gameplay.OnRightClick.Enable();
    }
}
