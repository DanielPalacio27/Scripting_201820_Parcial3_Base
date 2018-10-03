using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    private static UIManager instance = null;
    public static UIManager Instance { get { return instance; } }


    [SerializeField] private Text timerText;

    [SerializeField] private Text playerTags;

    [SerializeField] private Text[] AITags;
    [SerializeField] private GameObject AITextContainer, AITextPref;
    [SerializeField] private Text messageBox;

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        if (timerText == null)
        {
            enabled = false;
        }

        for (int i = 0; i < GameController.Instance.NumberOfPlayers; i++)
        {
            GameObject AIText = Instantiate(AITextPref, AITextContainer.transform);
            Text text = AIText.GetComponent<Text>();
            text.text = string.Format("AI {0} TAGS : 0", i + 1);

        }

        AITags = AITextContainer.GetComponentsInChildren<Text>();
    }


    private void Update()
    {
        //TODO: Set text from 
        timerText.text = GameController.Instance.CurrentGameTime.ToString();

    }

    public void UpdateUI()
    {
        int index = 0;
        
        foreach (ActorController a in GameController.Instance.Players)
        {
            if (a is PlayerController)
            {
                playerTags.text = "Player Tags: " + a.TaggedCount.ToString();
                continue;
            }
            
            int aux = index;
            AITags[index].text = string.Format("AI {0} TAGS : {1}", aux + 1, a.TaggedCount);
            index++;

        }
    }

    public void ShowMessage(string txt)
    {
        messageBox.gameObject.SetActive(true);
        messageBox.text = txt;
    }
}