using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ExitGame : MonoBehaviour
{
    public AudioSource click;
    // Exits game
    public void Exit()
    {
        click.Play();
        Application.Quit();
    }
}
