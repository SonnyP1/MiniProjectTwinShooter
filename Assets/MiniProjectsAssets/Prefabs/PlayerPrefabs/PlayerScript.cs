using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
public class PlayerScript : MonoBehaviour
{
    //Movement
    MovementComp movementComp;
    //Gun Stuff
    [SerializeField] List<Weapon> tempWeapons = new List<Weapon>();
    [SerializeField] Transform WeaponsSpawnLoc;
    int currentWeaponSelection;
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
    public Transform GetWeaponSpawnLoc() { return WeaponsSpawnLoc; }
    public void AddToWeaponList(Weapon weaponToAdd)
    {
        tempWeapons.Add(weaponToAdd);
    }
    private void Start()
    {
        ChooseWeaponsVisability();
        movementComp = GetComponent<MovementComp>();
        movementComp.SetInput(playerInput);

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
    void ChooseWeaponsVisability()
    {
        Weapon[] currentHeldWeapons = tempWeapons.ToArray();
        foreach (Weapon var in tempWeapons)
        {
            if(var == currentHeldWeapons[currentWeaponSelection])
            {
                var.SetGunVisibility(true);
            }
            else
            {
                var.SetGunVisibility(false);
            }
        }
    }
    void OnInteractButtonPressed(InputAction.CallbackContext ctx)
    {
        InteractComp interactComp = GetComponentInChildren<InteractComp>();
        if(interactComp != null)
        {
            interactComp.InteractWithInteractable();
        }
    }
    void OnReloadButtonPressed(InputAction.CallbackContext ctx)
    {
        Weapon[] currentHeldWeapons = tempWeapons.ToArray();
        currentHeldWeapons[currentWeaponSelection].Reload();
    }
    void OnDodgePressedUpdated(InputAction.CallbackContext ctx)
    {
        movementComp.MainDodge();
    }

    void OnMouseWheelUpdated(InputAction.CallbackContext ctx)
    {
        float convertMouseWheelToIntOneToNegOne = Mathf.Clamp(ctx.ReadValue<float>(), -1, 1);
        currentWeaponSelection = Mathf.Clamp((int)convertMouseWheelToIntOneToNegOne + currentWeaponSelection, 0, tempWeapons.Count - 1);
        ChooseWeaponsVisability();
    }
    void OnLeftClickUpdated(InputAction.CallbackContext ctx)
    {
        Weapon[] currentHeldWeapons = tempWeapons.ToArray();
        currentHeldWeapons[currentWeaponSelection].Attack();
    }
    void OnRightClickedUpdated(InputAction.CallbackContext ctx)
    {
        Weapon[] currentHeldWeapons = tempWeapons.ToArray();
        currentHeldWeapons[currentWeaponSelection].SecoundaryAttack();
    }
    void OnMouseUpdate(InputAction.CallbackContext ctx)
    {
        movementComp.SetMouseLoc(ctx.ReadValue<Vector2>());
        movementComp.UpdateRotation();
    }
    void OnMoveInputUpdate(InputAction.CallbackContext ctx)
    {
        movementComp.SetMoveInput(ctx.ReadValue<Vector2>());
    }
}
