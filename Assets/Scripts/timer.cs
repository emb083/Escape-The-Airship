using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class timer : MonoBehaviour
{
    public float startingTime = 600f;
    public Light[] lights;
    public MonoBehaviour PlayerMovement;
    public MonoBehaviour PlayerCam;
    public float delayBefore = 2f;

    private float curTime;
    private bool isGameOver;
    private int nextLight = 0;

    void Start()
    {
        curTime = startingTime;

        
    }

    void Update()
    {
        if (isGameOver)
            return;

        curTime -= Time.deltaTime;

        lightsOut();

        if (curTime <= 0f && !isGameOver)
        {
            curTime = 0f;
            StartCoroutine(TheEnd());
        }
    }

    void lightsOut()
    {
        float[] timesOff = { 480f, 360f, 240f, 120f, 0f };

        while (nextLight < timesOff.Length && curTime <= timesOff[nextLight])
        {
            

            if (nextLight < lights.Length && lights[nextLight] != null)
            {
                lights[nextLight].enabled = false; 
            }

            nextLight++;
        }
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


        SceneManager.LoadScene("Ending");
        EscapeShipAnimation.won = false;

    }
}