using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadZone : MonoBehaviour
{
    public Transform gateA;
    public Transform gateB;

    public bool zone1;
    public bool zone2;
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
        // Detects the gate
        if (zone1)
        {
            // Detects if the player is standing on the fuelpad
            if (collision.gameObject.transform.tag == "Player")
            {
                //Swaps places of gates
                gateA.transform.position = new Vector2(95,120);
                gateB.transform.position = new Vector2(-25,-20);
            }
        }
        else if (zone2)
        {
            if (collision.gameObject.transform.tag == "Player")
            {
                gateA.transform.position = new Vector2(83.264f, 308.96f);
                gateB.transform.position = new Vector2(-25, -20);
            }
        }
    }
}
