using UnityEngine;

public class PlayerController : ActorController
{
    [SerializeField]
    private LayerMask walkable;

    protected override Vector3 GetTargetLocation()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        Debug.DrawRay(ray.origin, ray.direction * 1000);

        if (Physics.Raycast(ray, out hit, Mathf.Infinity, walkable))
        {
            return hit.point;
        }
        else
        {
            print("Couldn't find point");
            return transform.position;
        }
    }

    // Update is called once per frame
    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            MoveActor();
        }

    }
}