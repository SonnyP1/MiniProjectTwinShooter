using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
public class PlayerScript : MonoBehaviour
{
    //Movement
    private MovementComp movementComp;
    //Gun Stuff
    private WeaponInventorySystem weaponInventorySystem;
    //Player Needs Stuff
    private PlayerInputs playerInput;

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
        movementComp = GetComponent<MovementComp>();
        movementComp.SetInput(playerInput);
        weaponInventorySystem = GetComponent<WeaponInventorySystem>();

        playerInput.Gameplay.Move.performed += OnMoveInputUpdate;
        playerInput.Gameplay.Move.canceled += OnMoveInputUpdate;
        playerInput.Gameplay.MouseLoc.performed += OnMouseUpdate;
        playerInput.Gameplay.OnLeftClick.performed += OnLeftClickUpdated;
        playerInput.Gameplay.OnRightClick.performed += OnRightClickedUpdated;
        playerInput.Gameplay.Dodge.performed += OnDodgePressedUpdated;
        playerInput.Gameplay.MouseWheel.performed += OnMouseWheelUpdated;
        playerInput.Gameplay.Reload.performed += OnReloadButtonPressed;
        playerInput.Gameplay.Interact.performed += OnInteractButtonPressed;
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
        movementComp.SetMouseLoc(ctx.ReadValue<Vector2>());
        movementComp.UpdateRotation();
    }
    void OnMoveInputUpdate(InputAction.CallbackContext ctx)
    {
        movementComp.SetMoveInput(ctx.ReadValue<Vector2>());
    }
    void OnDodgePressedUpdated(InputAction.CallbackContext ctx)
    {
        movementComp.MainDodge();
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

}
