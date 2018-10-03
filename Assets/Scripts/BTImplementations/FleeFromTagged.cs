/// <summary>
/// Task that instructs ControlledAI to move away from 'tagged' player
/// </summary>
public class FleeFromTagged : Task
{
    public override bool Execute()
    {
        ControlledAI.MoveAI();
        return base.Execute();
    }
}