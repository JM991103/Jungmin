using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackBlendTree : StateMachineBehaviour
{
    Player player;

    private void Awake()
    {
        // �÷��̾� �̸� ã�� ����
        player = FindObjectOfType<Player>();
        //Debug.Log($"Awake : {player}");
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        //Debug.Log($"OnStateExit : {player}");
        player.RestoreInputDir();   // �÷��̾��� �̵� ���� ����
    }

}
