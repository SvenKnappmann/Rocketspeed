using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Clock : MonoBehaviour
{

    public Text miliSecondsText;
    public Text secondsText;
    public Text minutesText;

    private float timer = 450f; 
    public int miliSeconds = 0;
    public int seconds = 0;
    public int minutes = 10;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Converter();
    }
    private void Converter()
    {
        timer -= Time.deltaTime;
        miliSeconds = (int)Mathf.Floor((timer % 1) * 1000);
        seconds = (int)Mathf.Floor(timer % 60);
        minutes = (int)Mathf.Floor(timer / 60);

        miliSecondsText.text = miliSeconds.ToString("000");
        secondsText.text = seconds.ToString("00");
        minutesText.text = minutes.ToString("00");
        if (timer <= 0f)
        {
            GetComponent<LoadScene>().LoadGameOverScene();
        }
    }
}
