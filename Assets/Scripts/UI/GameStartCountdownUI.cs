using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameStartCountdownUI : MonoBehaviour
{
    private const string NUMBER_POPUP = "NumberPopup";

    [SerializeField] private TextMeshProUGUI countdownText;

    private Animator animator;
    private int previousCountdownNumber;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    private void Start()
    {
        GameManager.Instance.OnStateChange += GameManager_OnStateChange;
        previousCountdownNumber = Mathf.CeilToInt(GameManager.Instance.GetCountdownToStartTimer() - 1);
    }

    private void GameManager_OnStateChange(object sender, System.EventArgs e)
    {
        if(GameManager.Instance.IsCountdownToStartActive()) {
            SoundManager.Instance.PlayCountdownSound(1);
            Show();
        }
        else
        {
            Hide();
        }
    }

    private void Update()
    {
        int countdownNumber = Mathf.CeilToInt(GameManager.Instance.GetCountdownToStartTimer() - 1);
        if (countdownNumber <= 0)
            countdownText.text = "GO!";
        else
            countdownText.text = countdownNumber.ToString();
           
        if(previousCountdownNumber != countdownNumber)
        {
            animator.SetTrigger(NUMBER_POPUP);
            previousCountdownNumber = countdownNumber;
            
            if (countdownNumber <= 0)
                SoundManager.Instance.PlayCountdownSound(0);
            else
                SoundManager.Instance.PlayCountdownSound(1);
        }
    }

    private void Show()
    {
        gameObject.SetActive(true);
    }
    
    private void Hide()
    {
        gameObject.SetActive(false);
    }
}
