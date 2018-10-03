using System.Collections;
using UnityEngine;
using System;
using UnityEngine.AI;

public class GameController : MonoBehaviour
{
    private static GameController instance = null;
    public static GameController Instance { get { return instance; } }

    public delegate void GameOverHandler();
    public static event GameOverHandler OnGameOver;

    public event Func<string> OnWinner;

    private ActorController previousTagged;

    [SerializeField] private ActorController[] players;

    [SerializeField] private GameObject AIPrefab;
    [SerializeField] private int numberOfPlayers;
    [SerializeField] private Collider planeCollider;
    [SerializeField] LayerMask walkable;
    [SerializeField] private float gameTime = 10F;

    public float CurrentGameTime { get; private set; }
   
    public int NumberOfPlayers
    {
        get
        {
            return Mathf.Clamp(numberOfPlayers, 0, 4);
        }
    }

    public ActorController[] Players
    {
        get
        {
            return players;
        }

        private set
        {
            players = value;
        }
    }

    public ActorController currentTagged { get; set; }
    public ActorController PreviousTagged
    {
        get
        {
            return previousTagged;
        }

        set
        {
            previousTagged = value;
        }
    }
    
    Vector3 randomPos;

    private void Awake()
    {

        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }

        for (int i = 0; i < NumberOfPlayers; i++)
        {
            Instantiate(AIPrefab, randomPos, Quaternion.identity);
        }

    }

    // Use this for initialization
    private IEnumerator Start()
    {
        OnWinner += TheWinnerIs;
        OnGameOver += GameOver;
        CurrentGameTime = gameTime;

       
        // Sets the first random tagged player
        Players = FindObjectsOfType<ActorController>();

        foreach (ActorController p in Players)
        {
            yield return StartCoroutine(GetRandom(p));
        }

        yield return new WaitForSeconds(0.5F);

        Players[UnityEngine.Random.Range(0, Players.Length)].onActorTagged(true);
    }

    public IEnumerator GetRandom(ActorController actor)
    {
        randomPos = new Vector3(UnityEngine.Random.Range(planeCollider.bounds.min.x, planeCollider.bounds.max.x), UnityEngine.Random.Range(planeCollider.bounds.min.y, planeCollider.bounds.max.y), UnityEngine.Random.Range(planeCollider.bounds.min.z, planeCollider.bounds.max.z));
        RaycastHit hit;
        Ray ray = new Ray(new Vector3(randomPos.x, planeCollider.bounds.max.y + 30, randomPos.z), Vector3.down);

        while (!(Physics.Raycast(ray, out hit, Mathf.Infinity, walkable)))
        {
            ray = new Ray(new Vector3(randomPos.x, planeCollider.bounds.max.y + 30, randomPos.z), Vector3.down);
            yield return null;
        }

        actor.transform.position = hit.point;
    }

    private void Update()
    {
        CurrentGameTime -= Time.deltaTime;

        if (CurrentGameTime <= 0F)
        {
            //TODO: Send GameOver event.
            OnGameOver();
        }
    }

    public void GameOver()
    {
        CurrentGameTime = 0;
        UIManager.Instance.ShowMessage(OnWinner());
        foreach (ActorController p in Players)
        {
            p.Agent.speed = 0;
        }
    }

    public string TheWinnerIs()
    {
        int lessTagged = Players[0].TaggedCount;
        int pos = 1;
        ActorController winnerActor = Players[0];
        string message;

        for(int i = 0; i < players.Length; i++)
        {            
            if(players[i].TaggedCount < lessTagged)
            {
                lessTagged = players[i].TaggedCount;
                pos = i + 1;
                winnerActor = players[i];
            }
        }
        
        if(winnerActor is PlayerController)
        {
            message = "Congratulations, you WIN!!!";
            return message;
        }
        else
        {
            message = string.Format("The AI {0} is the winner ", pos);
            return message;
        }
        
    }
}