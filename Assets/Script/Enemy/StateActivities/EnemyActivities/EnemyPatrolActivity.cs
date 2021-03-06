﻿using UnityEngine;

public class EnemyPatrolActivity : IEnemyStateActivity
{
    public int wayPointInd = 0;
    public EnemyController controller;

    public EnemyPatrolActivity(EnemyController _controller)
    {
        controller = _controller;
    }

    public void ChangeState(EnemyStates.State state)
    {
        controller.ChangeStateTo(state);
    }

    public void PreStartActivity()
    {
        controller.GetComponent<SphereCollider>().enabled = true;

        controller.ScreemsControll?.StopScreem();

        controller.soundManager.FightOver();
    }

    public void StateActivity()
    {
        if (Vector3.Distance(controller.transform.position, controller.waypoints[wayPointInd].transform.position) >= 1)
        {
            controller.EnemyMoveController.MoveToPoint(controller.waypoints[wayPointInd].transform.position);
        }
        else
        {
            wayPointInd++;
            if (controller.waypoints.Length <= wayPointInd)
            {
                wayPointInd = 0;
            }
        }
    }

    public void TriggerEnterActivity(Collider other)
    {
        if (other.tag == "Player" && other.GetComponent<PlayerMovement>().state == PlayerMovement.State.normal)
        {
            ChangeState(EnemyStates.State.CHASE);
            controller.target = other.gameObject;
        }
    }
}
