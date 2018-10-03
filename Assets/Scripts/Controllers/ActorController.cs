using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
[RequireComponent(typeof(Collider))]
public abstract class ActorController : MonoBehaviour
{
    protected NavMeshAgent agent;

    [SerializeField]
    protected Color baseColor = Color.blue;

    protected Color taggedColor = Color.red;

    protected MeshRenderer renderer;

    public delegate void OnActorTagged(bool val);

    public OnActorTagged onActorTagged;


    [SerializeField] private int taggedCount;
    //public int TaggedCount { get; set; }

    public bool IsTagged { get; protected set; }

    public NavMeshAgent Agent
    {
        get
        {
            return agent;
        }

        set
        {
            agent = value;
        }
    }

    public int TaggedCount
    {
        get
        {
            return taggedCount;
        }

        set
        {
            taggedCount = value;
        }
    }

    // Use this for initialization
    protected virtual void Start()
    {
        Agent = GetComponent<NavMeshAgent>();
        renderer = GetComponent<MeshRenderer>();
        SetTagged(false);

        onActorTagged += SetTagged;
    }

    protected abstract Vector3 GetTargetLocation();

    protected void MoveActor()
    {
        Agent.SetDestination(GetTargetLocation());
    }

    protected void OnCollisionEnter(Collision collision)
    {
        ActorController otherActor = collision.gameObject.GetComponent<ActorController>();

        if (otherActor != null)
        {
            if (otherActor.IsTagged)
            {
                TaggedCount++;
                otherActor.onActorTagged(false);
                GameController.Instance.PreviousTagged = otherActor;
                Invoke("DelayToTagged", 0.1f);
                GameController.Instance.currentTagged = this;
                UIManager.Instance.UpdateUI();
            }
        }
    }

    public void DelayToTagged()
    {
        onActorTagged(true);
    }

    protected virtual void OnDestroy()
    {
        Agent = null;
        renderer = null;
        onActorTagged -= SetTagged;
    }

    private void SetTagged(bool val)
    {
        IsTagged = val;

        if (renderer)
        {
            //print(string.Format("Changing color to {0}", gameObject.name));
            renderer.material.color = val ? taggedColor : baseColor;
        }
    }
}