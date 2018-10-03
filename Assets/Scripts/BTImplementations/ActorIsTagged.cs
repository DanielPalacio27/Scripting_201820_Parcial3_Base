/// <summary>
/// Selector that succeeeds if ControlledAI is marked as 'tagged'
/// </summary>
public class ActorIsTagged : Selector
{
    protected override bool CheckCondition()
    {
        ControlledAI.Agent.speed = 10;
        bool isTagged = ControlledAI.IsTagged;
        return isTagged;
    }
}