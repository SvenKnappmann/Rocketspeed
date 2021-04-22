using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.IO;
using UnityEngine.UI;

public class SaveTime : MonoBehaviour
{
    public Clock clock;
    public Text timeLeft;

    //[MenuItem("Tools/Write file")]
    public void WriteString()
    {
        string path = "Assets/Saves/test.txt";
        File.Create(path).Close();

        //Write some text to the test.txt file
        StreamWriter writer = new StreamWriter(path, true);
        writer.WriteLine(clock.minutes + ":" + clock.seconds + ":" + clock.miliSeconds);
        writer.Close();
    }
    //[MenuItem("Tools/Read file")]
    public void ReadString()
    {
        string path = "Assets/Saves/test.txt";

        //Read the text from directly from the test.txt file
        StreamReader reader = new StreamReader(path);
        timeLeft.text = "Time left: " + reader.ReadToEnd();
        reader.Close();
    }
}
