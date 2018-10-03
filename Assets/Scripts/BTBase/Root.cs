using UnityEngine;

public class Root : Node
{
    [SerializeField]
    private Node child;

    public override bool Execute()
    {
        print("Ejecutado");
        return child.Execute();
    }

    public override void SetControlledAI(AIController newControlledAI)
    {
        child.SetControlledAI(newControlledAI);
    }
}