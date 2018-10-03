/// <summary>
/// Task that instructs ControlledAI to stop its current movement.
/// </summary>
public class StopMovement : Task
{
    public override bool Execute()
    {
        ControlledAI.Agent.speed = 0;
        return base.Execute();
    }
}