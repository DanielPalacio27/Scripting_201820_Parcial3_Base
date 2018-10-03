using UnityEngine;
/// <summary>
/// Selector that succeeds if 'tagged' player is within a 'acceptableDistance' radius.
/// </summary>
public class IsTaggedActorNear : Selector
{
    [SerializeField]
    private float acceptableDistance = 20F;
    bool isNear;

    protected override bool CheckCondition()
    {
        ActorController taggedActor = null;
        float distance = acceptableDistance + 1;
       

        foreach(ActorController a in GameController.Instance.Players)
        {
            if(a.IsTagged)
            {
                taggedActor = a;
                break;
            }
        }

        if(taggedActor != null)
        {
            distance = Vector3.Distance(transform.position, taggedActor.transform.position);
        }

        isNear = (distance <= acceptableDistance) ? true : false;
        return isNear;

    }
}