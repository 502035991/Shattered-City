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
        //不执行父类
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
            CameraManager.instance.CameraShake(player.MyInpulse, 1);
            landObj = enemy.CreatSkill(enemyData.Skill[CrystalAttackMenu.Jump_1].obj);
        }
        else
        {
            CameraManager.instance.CameraShake(player.MyInpulse, 2f);
            landObj = enemy.CreatSkill(enemyData.Skill[CrystalAttackMenu.Jump_2].obj);
        }
        // 射线检测地面
        RaycastHit2D hit = Physics2D.Raycast(landObj.transform.position, Vector2.down ,10,enemyData.layerToGround);
        var coll = landObj.GetComponent<BoxCollider2D>();
        var pos = new Vector2(enemy.transform.position.x, hit.point.y + coll.size.y /2 * coll.transform.localScale.y);//scale不能小于1

        var controller = landObj.GetComponent<CrystalSkillLandController>();
        controller.Init(0.2f, pos, AttackedPlayerCallback);
    }
    private void AttackedPlayerCallback()
    {
        isAttackedPlayer =true;

        if (landCounter == 1)
            player.KnockBack(0,enemy.facingDirection,0.5f);
        else
            player.KnockBack(0, enemy.facingDirection, 1f);
    }
    public override void PhysicUpdate()
    {
        base.PhysicUpdate();
        enemy.SetVelocityX(0);
    }

}
