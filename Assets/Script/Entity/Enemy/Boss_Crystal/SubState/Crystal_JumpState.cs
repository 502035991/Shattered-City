using Cysharp.Threading.Tasks;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using UnityEngine;

public class Crystal_JumpState : Crystal_AbilityState
{
    public Crystal_JumpState(EnemyStateMachine enemyStateMachine, EnemyData enemyData, Boss_Crystal enemy, string animName, Action<CrystalCD> ac) : base(enemyStateMachine, enemyData, enemy, animName, ac)
    {
    }
    private int JumpCounter =1;

    private Vector2 targetPosition;
    private Vector2 startPos;

    private float jumpDuration = 1;

    private AnimationCurve jumpCurve;
    public override void Enter()
    {
        base.Enter();
        if (JumpCounter > 2)
            JumpCounter = 1;
        enemy.anim.SetInteger("JumpCounter", JumpCounter);

        jumpDuration = enemy.jumpDuration;
        targetMaxHeight = enemy.jumpMaxHeight;

        targetPosition = player.transform.position;
        startPos = enemy.transform.position;
        enemy.SetFlip(targetPosition.x < startPos.x ? -1:1);

        Height_GravityValues.Clear();
        sortedHeights.Clear();
        GetHeightValues();
        jump();
    }
    private float targetMaxHeight =20;
    private Dictionary<float,float> Height_GravityValues = new();//�߶� ����
    List<float> sortedHeights = new List<float>();

    //Ԥ�ȼ�������ֵ�ڷ�Χ[0, 100]�ڵ�ÿ��ֵ����Ӧ�����߶�(Ԥ��ת�׶�ʱ�첽���㣩���1
    private void GetHeightValues()
    {
        for (int i = 0; i <= 100; i++)
        {
            float gravity = i;
            Vector2 rbVelocity = (targetPosition - startPos) / jumpDuration - 0.5f * Physics2D.gravity * gravity * jumpDuration;//���ʼ�ٶ�
            float maxHeight = (rbVelocity.y * rbVelocity.y) / (2 * -(Physics2D.gravity * gravity).y) * 0.90f;//�����߶�

            Height_GravityValues.Add(maxHeight, gravity);

            sortedHeights = new List<float>(Height_GravityValues.Keys);//�����ĸ߶��б�
            sortedHeights.Sort();
        }
    }
    private void jump()
    {
        int iterationCounter = 0;
        float precision = 0.3f;
        Vector2 rbVelocity;

        //��ʼ�²�����
        float guessGravity = GetNearestGravity(targetMaxHeight);

        // �����ʼ�ٶ����ܴﵽ�����߶�
        float maxHeight = CalculateMaxHeight(Physics2D.gravity * guessGravity, out rbVelocity);

        while (Mathf.Abs(maxHeight - targetMaxHeight) > precision && iterationCounter < 100)
        {
            iterationCounter++;

            //����������ӽ�������ֵ
            maxHeight = CalculateMaxHeight(Physics2D.gravity * guessGravity, out rbVelocity);

            if (maxHeight > targetMaxHeight)
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
        // ���ֲ�����ӽ�Ŀ��߶ȵ����߶�
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
        // ʹ�ø�����������С���������ߵ����߶�
        Vector2 rbVelocity = (targetPosition - startPos) / jumpDuration - 0.5f * gravitySum * jumpDuration;
        velocity = rbVelocity;
        return (rbVelocity.y * rbVelocity.y) / (2 * -gravitySum.y) * 0.90f;//0.1�����
    }

    public override void Exit() 
    {        
        base.Exit();
        JumpCounter +=1;

        change1 = false;
    }
    private bool change1;
    public override void PhysicUpdate()
    {
        base.PhysicUpdate();

        enemy.SetFlip(targetPosition.x < startPos.x ? -1 : 1);
    }
}
