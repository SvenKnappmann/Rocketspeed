using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TriggerEnd : MonoBehaviour
{
    public SaveTime saveTime;
    private bool toggle = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.transform.tag == "Player" && !toggle)
        {
            toggle = true;
            saveTime.WriteString();
            SceneManager.LoadScene(3);
        }
    }
}
