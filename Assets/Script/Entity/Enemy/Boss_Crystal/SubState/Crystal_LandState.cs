using Cysharp.Threading.Tasks;
using UnityEngine;

public class Crystal_LandState : Crystal_GroundedState
{

    public Crystal_LandState(EnemyStateMachine enemyStateMachine, EnemyData enemyData, Boss_Crystal enemy, string animName) : base(enemyStateMachine, enemyData, enemy, animName)
    {
    }

    public override void Enter()
    {
        base.Enter();
        landCounter = enemy.anim.GetInteger("JumpCounter");
        isAttackedPlayer = false;
    }
    public override void DoCheck()
    {
        //≤ª÷¥––∏∏¿‡
    }
    private GameObject landObj;

    private int landCounter;
    private bool isAttackedPlayer;

    public override async void AnimationFinishTrigger()
    {
        base.AnimationFinishTrigger();

        await UniTask.Delay(600);
        Object.Destroy(landObj);
        enemyStateMachine.ChangeState(enemy.twoBattleState);
    }
    public override void AnimationSkillEffect()
    {
        base.AnimationSkillEffect();
        if(landCounter ==1)
        {
            landObj = enemy.CreatSkill(enemyData.Skill[CrystalAttackMenu.Jump_1].obj);
        }
        else
        {
            landObj = enemy.CreatSkill(enemyData.Skill[CrystalAttackMenu.Jump_2].obj);
        }
        var controller = landObj.GetComponent<CrystalSkillLandController>();
        controller.Init(0.2f, enemy.skillLandPos.position, AttackedPlayerCallback);
    }
    private void AttackedPlayerCallback(bool isAttacked)
    {
        isAttackedPlayer =true;
/*
        if(landCounter ==1)
            player.KnockBackUp(enemy.facingDirection, 0, 5, 0.6f);
        else
            player.KnockBackUp(enemy.facingDirection, 0, 10, 0.6f);*/
    }
    public override void PhysicUpdate()
    {
        base.PhysicUpdate();
        enemy.SetVelocityX(0);
    }

}
