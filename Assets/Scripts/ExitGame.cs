using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ExitGame : MonoBehaviour
{
    public AudioSource click;
    public void Exit()
    {
        Application.Quit();
        click.Play();
    }
}
