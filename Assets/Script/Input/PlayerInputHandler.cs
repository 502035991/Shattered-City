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
    public bool isAir {  get; private set; }
    public bool isDash {  get; private set; }
    public bool isAttack {  get; private set; }
    public bool isAirAttack {  get; private set; }

    //技能
    public bool isCloneDsah {  get; private set; }
    public bool isCloneDashEnable {  get; private set; }
    public bool isTimeStopEnable {  get; private set; }

    private Player player;
    private void Start()
    {
        player = GetComponentInParent<Player>();        
    }
    public void OnMoveInput(InputAction.CallbackContext context)
    {
        movementInput = context.ReadValue<Vector2>();

        normInputX = (int)(movementInput * Vector2.right).normalized.x;
        normInputY = (int)(movementInput* Vector2.up).normalized.y;
    }
    public void OnJumpInput(InputAction.CallbackContext context) 
    { 
        if(context.started && !isDash &&!isAttack && !isAirAttack && !player.isControlled)
        {
            jumpInput = true;
        }
    }
    public void OnDashInput(InputAction.CallbackContext context)
    {
         if (context.started && !isAttack && !isAirAttack && SkillManager.instance.dash.CanUseSkill() && !player.isControlled)
        { 
            isDash = true;
        }
    }
    public void OnNormalAttack(InputAction.CallbackContext context)
    {
        if (context.started && !isDash && !isCloneDashEnable && !player.isControlled)
        {
            if(!isAir && !isAirAttack)
            {
                isAttack = true;
                //地面上的攻击
            }
            else
            {
                isAirAttack = true;
            }

        }
    }
    public void OnCloneDash(InputAction.CallbackContext context)
    {
        if (player.isControlled)
            return;

        if (context.started && !isDash && !isAttack && !isAirAttack && SkillManager.instance.cloneDash.CanUseSkill())
        {
            isCloneDsah = true;
            isCloneDashEnable = false;
        }      
        else if(context.started && SkillManager.instance.cloneDash.ObjStats())
        {
            isCloneDashEnable = true;
        }
    }
    public void OnTimeStop(InputAction.CallbackContext context)
    {
        if(context.started)
        {
            isTimeStopEnable = true;
        }
        else if(context.canceled)
        {
            isTimeStopEnable = false;
        }

    }

    public void ClearMovementInput() => movementInput = Vector2.zero;

    public void UseJumpInput() => jumpInput = false;
    public void UseDashInput() => isDash = false;
    public void UseAttackInput() => isAttack = false;
    public void UseAirAttackInput () => isAirAttack = false;

    public void SetAirState(bool isAir) => this.isAir = isAir;

    //技能
    public void UseCloneDashInput() => isCloneDsah =false;
    public void UseCloneDashMoveInput() => isCloneDashEnable = false;
}
