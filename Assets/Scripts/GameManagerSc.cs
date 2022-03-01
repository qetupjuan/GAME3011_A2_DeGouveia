using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameManagerSc : MonoBehaviour
{
    public LockpickBehaviour lockpickBehaviour;
    public TextMeshProUGUI difficultyText;
    public TextMeshProUGUI timeText;
    public TextMeshProUGUI numberSyringes;
    public GameObject losePanel;
    public GameObject Lock;
    public float time = 50f;
    public float timeLimit;
    public bool isPlaying;

    void Start()
    {
        lockpickBehaviour = FindObjectOfType<LockpickBehaviour>();
    }
    void Update()
    {
        Clock();

        if(lockpickBehaviour)
        {
            difficultyText.text = "" + lockpickBehaviour.currentSyringes;
            timeText.text = "" + lockpickBehaviour.currentSyringes;
            numberSyringes.text = "" + lockpickBehaviour.currentSyringes;
        }
    }

    void CheckWin()
    {

    }


    void Clock()
    {
        if (isPlaying)
        {
            if (time > 0.0f)
            {
                time -= Time.deltaTime;
                timeLimit = time;
            }
        }
        if (time <= 0.0f)
        {
            time = 0.0f;
            isPlaying = false;
            //losePanel.SetActive(false);
        }
    }
}
