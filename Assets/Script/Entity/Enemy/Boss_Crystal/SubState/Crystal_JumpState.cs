using System;
using System.Collections.Generic;
using UnityEngine;

public class Crystal_JumpState : Crystal_AbilityState
{
    private Dictionary<float, float> Height_GravityValues = new Dictionary<float, float>();//高度_速度
    private int JumpCounter;
    private Vector2 targetPosition;
    private Vector2 startPos;

    public Crystal_JumpState(EnemyStateMachine enemyStateMachine, EnemyData enemyData, Boss_Crystal enemy, string animName, Action<CrystalAttackMenu> ac) : base(enemyStateMachine, enemyData, enemy, animName, ac)
    {

    }
    public override void Enter()
    {
        base.Enter();
        enemy.anim.SetInteger("JumpCounter", JumpCounter);

        targetPosition = player.transform.position;
        startPos = enemy.transform.position;

        CalculateJump(JumpCounter);
        isAbilityDone = true;
    }
    private void CalculateJump(int jumpCounter)
    {
        float jumpDuration = UnityEngine.Random.Range(0.9f, 1.3f);
        float maxHeight = UnityEngine.Random.Range(7, 15);

        float gravity = 0f;

        switch (jumpCounter)
        {           
            case 1:
                GetHeightValues(jumpDuration);
                gravity = IterationNearestGravity(GetNearestGravity(maxHeight), jumpDuration, maxHeight);
                ac?.Invoke(CrystalAttackMenu.Jump_1);
                break;
            case 2:
                GetHeightValues(jumpDuration);
                gravity = IterationNearestGravity(GetNearestGravity(maxHeight), jumpDuration, maxHeight);
                ac?.Invoke(CrystalAttackMenu.Jump_2);
                break;
            default:
                break;
        }
        Jump(gravity, jumpDuration);
    }
    private void Jump(float gravity, float jumpDuration)
    {
        Vector2 rbVelocity;
        CalculateMaxHeightToVelocity(Physics2D.gravity * gravity, jumpDuration, out rbVelocity);

        enemy.RB.gravityScale = gravity;
        //enemy.SetVelocity(rbVelocity);
        enemy.RB.AddForce(rbVelocity, ForceMode2D.Impulse);
    }
    //预先计算重力值在范围[1, 100]内的每个值所对应的最大高度 差距1
    private void GetHeightValues(float jumpDuration)
    {
        Height_GravityValues.Clear();
        for (int i = 1; i <= 100; i++)
        {
            float gravity = i;
            var gravitySum = Physics2D.gravity * gravity;

            Vector2 rbVelocity = (targetPosition - startPos) / jumpDuration - 0.5f * gravitySum * jumpDuration;//求初始速度
            float maxHeight = (rbVelocity.y * rbVelocity.y) / (2 * -(gravitySum).y);//求最大高度

            Height_GravityValues.Add(maxHeight, gravity);
        }
    }
    //找到与目标高度差距最小的预设重力
    private float GetNearestGravity(float targetHeight)
    {
        float nearestGravity = 0f;
        float minDifference = float.MaxValue;

        foreach (var kvp in Height_GravityValues)
        {
            float difference = Mathf.Abs(kvp.Key - targetHeight);
            if (difference < minDifference)
            {
                minDifference = difference;
                nearestGravity = kvp.Value;
            }
        }
        return nearestGravity;
    }
    //计算抛物线的初始速度
    private void CalculateMaxHeightToVelocity(Vector2 gravitySum, float jumpDuration, out Vector2 velocity)
    {
        Vector2 rbVelocity = (targetPosition - startPos) / jumpDuration - 0.5f * gravitySum * jumpDuration;
        velocity = rbVelocity;
    }
    //迭代得到最合适的重力
    private float IterationNearestGravity(float gravity , float jumpDuration ,float targetHeight)
    {
        int iterations = 0;
        while (iterations < 100)
        {
            var currentHeight = CalculateMaxHeight(gravity, jumpDuration);

            if (Mathf.Abs(currentHeight - targetHeight) < 0.2f)
            {
                // 如果当前高度已经接近目标高度，则返回当前重力值
                return gravity;
            }

            // 根据当前高度与目标高度的比较，调整重力值
            if (currentHeight > targetHeight)
            {
                gravity -= 0.01f;
            }
            else
            {
                gravity += 0.01f;
            }

            iterations++;
        }
        return gravity;

    }
    // 使用给定的重力大小计算抛物线的最大高度
    private float CalculateMaxHeight(float gravity , float jumpDuration)
    {
        Vector2 gravitySum = gravity * Physics2D.gravity;
        Vector2 rbVelocity = (targetPosition - startPos) / jumpDuration - 0.5f * gravitySum * jumpDuration;
        return (rbVelocity.y * rbVelocity.y) / (2 * -gravitySum.y);
    }
    public override void SetAdditionalData(object value)
    {
        if (value != null && value is int)
        {
            JumpCounter = (int)value;
        }
    }
}
