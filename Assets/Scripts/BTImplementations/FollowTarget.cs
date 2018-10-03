/// <summary>
/// Task that instructs ControlledAI to follow its designated 'target'
/// </summary>
using UnityEngine;
public class FollowTarget : Task
{
    [SerializeField]
    Transform targetPos;
    public override bool Execute()
    {
        targetPos = GetComponent<GetNearestTarget>().Target;
        ControlledAI.Agent.SetDestination(targetPos.position);
        //return true;
        return base.Execute();
    }
}