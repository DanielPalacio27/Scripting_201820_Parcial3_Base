using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetNearestTarget : Task {

    [SerializeField]
    public Transform target;

    [SerializeField]
    float distance;

    
    public Transform Target
    {
        get
        {
            return target;
        }

        set
        {
            target = value;
        }
    }
    
    public override bool Execute()
    {


        distance = Vector3.Distance(transform.position, GameController.Instance.Players[0].transform.position);
        Target = GameController.Instance.Players[0].transform;

        if(distance == 0)
        {
            distance = Mathf.Infinity;
        }

        for (int i = 0; i < GameController.Instance.Players.Length; i++)
        {
            if (GameController.Instance.Players[i] != GameController.Instance.PreviousTagged)
            {
                float aux = Vector3.Distance(transform.position, GameController.Instance.Players[i].transform.position);

                if (aux < distance && aux > 0)
                {
                    distance = aux;
                    Target = GameController.Instance.Players[i].transform;
                }
            }

        }

        return base.Execute();  
    }
}
