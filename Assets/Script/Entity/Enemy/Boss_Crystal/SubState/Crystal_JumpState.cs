using System;
using System.Collections.Generic;
using UnityEngine;

public class Crystal_JumpState : Crystal_AbilityState
{
    private Dictionary<float, float> Height_GravityValues = new Dictionary<float, float>();//�߶�_�ٶ�
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
    //Ԥ�ȼ�������ֵ�ڷ�Χ[1, 100]�ڵ�ÿ��ֵ����Ӧ�����߶� ���1
    private void GetHeightValues(float jumpDuration)
    {
        Height_GravityValues.Clear();
        for (int i = 1; i <= 100; i++)
        {
            float gravity = i;
            var gravitySum = Physics2D.gravity * gravity;

            Vector2 rbVelocity = (targetPosition - startPos) / jumpDuration - 0.5f * gravitySum * jumpDuration;//���ʼ�ٶ�
            float maxHeight = (rbVelocity.y * rbVelocity.y) / (2 * -(gravitySum).y);//�����߶�

            Height_GravityValues.Add(maxHeight, gravity);
        }
    }
    //�ҵ���Ŀ��߶Ȳ����С��Ԥ������
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
    //���������ߵĳ�ʼ�ٶ�
    private void CalculateMaxHeightToVelocity(Vector2 gravitySum, float jumpDuration, out Vector2 velocity)
    {
        Vector2 rbVelocity = (targetPosition - startPos) / jumpDuration - 0.5f * gravitySum * jumpDuration;
        velocity = rbVelocity;
    }
    //�����õ�����ʵ�����
    private float IterationNearestGravity(float gravity , float jumpDuration ,float targetHeight)
    {
        int iterations = 0;
        while (iterations < 100)
        {
            var currentHeight = CalculateMaxHeight(gravity, jumpDuration);

            if (Mathf.Abs(currentHeight - targetHeight) < 0.2f)
            {
                // �����ǰ�߶��Ѿ��ӽ�Ŀ��߶ȣ��򷵻ص�ǰ����ֵ
                return gravity;
            }

            // ���ݵ�ǰ�߶���Ŀ��߶ȵıȽϣ���������ֵ
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
    // ʹ�ø�����������С���������ߵ����߶�
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
