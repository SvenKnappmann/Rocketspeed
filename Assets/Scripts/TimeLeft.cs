using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeLeft : MonoBehaviour
{
    public SaveTime saveTime;
    // Start is called before the first frame update
    void Start()
    {
        saveTime.ReadString();
    }
}
