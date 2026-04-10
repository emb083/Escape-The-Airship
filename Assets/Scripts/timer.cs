using UnityEngine;
using System.Collections;
using TMPro;

public class timer : MonoBehaviour
{
    public float startingTime = 600f; // 10 minutes
    public TextMeshProUGUI timerHUD;
    public MonoBehaviour PlayerMovement;
    public MonoBehaviour PlayerCam;

    public string gameOverTrigger = "GameOver";
    public float delayBefore = 2f;

    private float curTime;
    private bool isGameOver;

    void Start()
    {
        curTime = startingTime;
        UpdateText();
    }

    void Update()
    {
        if (isGameOver)
            return;

        curTime -= Time.deltaTime;

        if (curTime <= 0f)
        {
            curTime = 0f;
            UpdateText();
            StartCoroutine(TheEnd());
            return;
        }

        UpdateText();
    }

    private void UpdateText()
    {
        int minutes = Mathf.FloorToInt(curTime / 60f);
        int seconds = Mathf.FloorToInt(curTime % 60f);
        timerHUD.text = $"{minutes:00}:{seconds:00}";
    }

    private IEnumerator TheEnd()
    {
        isGameOver = true;

        if (PlayerMovement != null)
            PlayerMovement.enabled = false;

        if (PlayerCam != null)
            PlayerCam.enabled = false;

        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

        yield return new WaitForSeconds(delayBefore);

        Time.timeScale = 0f;
    }
}