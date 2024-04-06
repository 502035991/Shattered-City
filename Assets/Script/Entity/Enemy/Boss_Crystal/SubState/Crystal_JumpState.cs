using System;
using System.Collections.Generic;
using UnityEngine;

public class Crystal_JumpState : Crystal_AbilityState
{
    public Crystal_JumpState(EnemyStateMachine enemyStateMachine, EnemyData enemyData, Boss_Crystal enemy, string animName, Action<CrystalCD> ac) : base(enemyStateMachine, enemyData, enemy, animName, ac)
    {

    }
    public override void Enter()
    {
        base.Enter();
        if (JumpCounter > 2)
            JumpCounter = 1;
        //enemy.anim.SetInteger("JumpCounter", JumpCounter);

        targetPosition = player.transform.position;
        startPos = enemy.transform.position;

        GetHeightValues();
        jump(JumpCounter == 1? 10 : 15);//一段跳和二段跳的高度
        isAbilityDone = true;
    }
    private int JumpCounter = 1;

    private Vector2 targetPosition;
    private Vector2 startPos;

    private float jumpDuration = 1.2f;

    private Dictionary<float,float> Height_GravityValues = new();//高度 重力
    List<float> sortedHeights = new List<float>();

    //预先计算重力值在范围[0, 100]内的每个值所对应的最大高度 差距1
    private void GetHeightValues()
    {
        Height_GravityValues.Clear();
        sortedHeights.Clear();
        for (int i = 0; i <= 100; i++)
        {
            float gravity = i;
            Vector2 rbVelocity = (targetPosition - startPos) / jumpDuration - 0.5f * Physics2D.gravity * gravity * jumpDuration;//求初始速度
            float maxHeight = (rbVelocity.y * rbVelocity.y) / (2 * -(Physics2D.gravity * gravity).y) * 0.90f;//求最大高度

            Height_GravityValues.Add(maxHeight, gravity);

            sortedHeights = new List<float>(Height_GravityValues.Keys);//排序后的高度列表
            sortedHeights.Sort();
        }
    }
    private void jump(float _maxHeight)
    {
        int iterationCounter = 0;
        float precision = 0.3f;
        Vector2 rbVelocity;

        //初始猜测重力
        float guessGravity = GetNearestGravity(_maxHeight);

        // 计算初始速度所能达到的最大高度
        float maxHeight = CalculateMaxHeight(Physics2D.gravity * guessGravity, out rbVelocity);

        while (Mathf.Abs(maxHeight - _maxHeight) > precision && iterationCounter < 100)
        {
            iterationCounter++;

            //迭代计算最接近的重力值
            maxHeight = CalculateMaxHeight(Physics2D.gravity * guessGravity, out rbVelocity);

            if (maxHeight > _maxHeight)
            {
                guessGravity -= 0.01f;
            }
            else
            {
                guessGravity += 0.01f;
            }
        }
        enemy.RB.gravityScale = guessGravity;
        enemy.SetVelocity(rbVelocity);
    }
    private float GetNearestGravity(float targetHeight)
    {
        // 二分查找最接近目标高度的最大高度
        int left = 0;
        int right = sortedHeights.Count - 1;
        while (left <= right)
        {
            int mid = (left + right) / 2;
            float midHeight = sortedHeights[mid];

            float difference = Mathf.Abs(midHeight - targetHeight);
            if (difference <= 1)
            {
                return mid;
            }
            else if (midHeight < targetHeight)
            {
                left = mid + 1;
            }
            else
            {
                right = mid - 1;
            }
        }
        float bestHeight = sortedHeights[(left - right) /2];
        return Height_GravityValues[bestHeight];
    }
    private float CalculateMaxHeight(Vector2 gravitySum , out Vector2 velocity)
    {
        // 使用给定的重力大小计算抛物线的最大高度
        Vector2 rbVelocity = (targetPosition - startPos) / jumpDuration - 0.5f * gravitySum * jumpDuration;
        velocity = rbVelocity;
        return (rbVelocity.y * rbVelocity.y) / (2 * -gravitySum.y) * 0.90f;//0.1的损耗
    }
    public override void Exit() 
    {        
        base.Exit();
        JumpCounter +=1;
    }
}
