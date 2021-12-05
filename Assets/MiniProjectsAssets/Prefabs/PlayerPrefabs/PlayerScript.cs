using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
public class PlayerScript : MonoBehaviour
{
    //Movement
    private MovementComp _movementComp;
    private Vector2 _moveInput;
    //Gun Stuff
    private WeaponInventorySystem weaponInventorySystem;
    //Player Needs Stuff
    private PlayerInputs playerInput;
    //Animation Stuff
    private Animator _animator;
    //Health
    private HealthComp _healthComp;
    private UIHealth _uiHealth;

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
       
        _movementComp = GetComponent<MovementComp>();
        _movementComp.SetInput(playerInput);
        
        weaponInventorySystem = GetComponent<WeaponInventorySystem>();
        _animator = GetComponent<Animator>();
        
        _healthComp = GetComponent<HealthComp>();
        _healthComp.onDamageTaken += DmgTaken;
        _healthComp.onHitPointDepleted += Death;
        _uiHealth = FindObjectOfType<UIHealth>();
        if (_uiHealth)
        {
            _uiHealth.SetMaxHealth(_healthComp.GetMaxHitPoints());
            _uiHealth.UpdateHeartFillContainers(_healthComp.GetMaxHitPoints());
        }
        else{Debug.Log("UI Health doesnt exist!");}

        playerInput.Gameplay.Move.performed += OnMoveInputUpdate;
        playerInput.Gameplay.Move.canceled += OnMoveInputUpdate;
        playerInput.Gameplay.MouseLoc.performed += OnMouseUpdate;
        playerInput.Gameplay.OnLeftClick.performed += OnLeftClickUpdated;
        playerInput.Gameplay.OnRightClick.performed += OnRightClickedUpdated;
        playerInput.Gameplay.Dodge.performed += OnDodgePressedUpdated;
        playerInput.Gameplay.MouseWheel.performed += OnMouseWheelUpdated;
        playerInput.Gameplay.Reload.performed += OnReloadButtonPressed;
        playerInput.Gameplay.Interact.performed += OnInteractButtonPressed;
        playerInput.DeathCtrl.Restart.performed += OnRestartButtonPressed;
        playerInput.DeathCtrl.Restart.Disable();
    }

    private void OnRestartButtonPressed(InputAction.CallbackContext obj)
    {
        SceneManager.LoadScene(1);
    }


    private void Update()
    {
        UpdateAnimationParameters();
    }

    //INTERACT INPUTS
    void OnInteractButtonPressed(InputAction.CallbackContext ctx)
    {
        InteractComp interactComp = GetComponentInChildren<InteractComp>();
        if(interactComp != null)
        {
            interactComp.InteractWithInteractable();
        }
    }
    
    //MOVEMENT INPUTS
    void OnMouseUpdate(InputAction.CallbackContext ctx)
    {
        _movementComp.SetMouseLoc(ctx.ReadValue<Vector2>());
        _movementComp.UpdateRotation();
    }
    void OnMoveInputUpdate(InputAction.CallbackContext ctx)
    {
        _moveInput = ctx.ReadValue<Vector2>();
        _movementComp.SetMoveInput(_moveInput);
    }
    void OnDodgePressedUpdated(InputAction.CallbackContext ctx)
    {
        _movementComp.MainDodge();
        
    }
    
    //WEAPON INPUTS
    void OnMouseWheelUpdated(InputAction.CallbackContext ctx)
    {
        float convertMouseWheelToOneToNegOne = Mathf.Clamp(ctx.ReadValue<float>(), -1, 1);
        weaponInventorySystem.ChangeCurrentWeaponSelectionAndVisibility((int)convertMouseWheelToOneToNegOne);
    }
    void OnLeftClickUpdated(InputAction.CallbackContext ctx)
    {
        if (!weaponInventorySystem.IsWeaponListEmpty())
        {
            weaponInventorySystem.AttackPrimaryCurrentWeapon();
        }
    }
    void OnReloadButtonPressed(InputAction.CallbackContext ctx)
    {
        if (!weaponInventorySystem.IsWeaponListEmpty())
        {
            weaponInventorySystem.ReloadCurrentWeapon();
        }
    }
    void OnRightClickedUpdated(InputAction.CallbackContext ctx)
    {
        if (!weaponInventorySystem.IsWeaponListEmpty())
        {
            weaponInventorySystem.AttackSecondaryCurrentWeapon();
        }
    }
    
    //Animation
    void UpdateAnimationParameters()
    {
        if (_moveInput == Vector2.zero)
        {
            _animator.SetFloat("MovementActive",0);
        }
        else
        {
            //make timer to lerp between 0 and 1
            _animator.SetFloat("MovementActive",0.168f);
        }
    }
    
    private void DmgTaken(int newamt, int oldamt, object attacker)
    {
        //change UI
        if (_uiHealth)
        {
            _uiHealth.UpdateHeartFillContainers(newamt);
        }
    }
    
    private void Death()
    {
        playerInput.Gameplay.Disable();
        _uiHealth.UpdateHeartFillContainers(0);
        _animator.SetTrigger("DeathTrigger");
        Destroy(FindObjectOfType<PerceptionSystem>().gameObject);
        playerInput.DeathCtrl.Restart.Enable();
    }

}
