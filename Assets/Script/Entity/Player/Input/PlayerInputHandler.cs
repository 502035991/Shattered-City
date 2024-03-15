using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInputHandler : MonoBehaviour
{
    public Vector2 movementInput {  get; private set; }

    public int normInputX {  get; private set; }
    public int normInputY {  get; private set; }

    public bool jumpInput { get; private set; }
    public bool isDash {  get; private set; }
    public bool isAttack {  get; private set; }
    public void OnMoveInput(InputAction.CallbackContext context)
    {
        movementInput = context.ReadValue<Vector2>();

        normInputX = (int)(movementInput * Vector2.right).normalized.x;
        normInputY = (int)(movementInput* Vector2.up).normalized.y;
    }
    public void OnJumpInput(InputAction.CallbackContext context) 
    { 
        if(context.started && !isDash &&!isAttack)
        {
            jumpInput = true;
        }
    }
    public void OnDashInput(InputAction.CallbackContext context)
    {
         if (context.started && !isAttack && SkillManager.instance.dash.CanUseSkill())
        {
            isDash = true;
        }
    }
    public void OnNormalAttack(InputAction.CallbackContext context)
    {
        if (context.started && !isDash)
        {
            isAttack = true;
        }
    }
    public void UseJumpInput() => jumpInput = false;
    public void UseDashInput() => isDash = false;
    public void UseAttackInput() => isAttack = false;
}
