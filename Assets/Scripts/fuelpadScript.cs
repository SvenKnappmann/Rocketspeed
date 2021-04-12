using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class fuelpadScript : MonoBehaviour
{
    public SpriteRenderer spriteRenderer;

    public Sprite fuelpAnim0;
    public Sprite fuelpAnim1;
    public Sprite fuelpAnim2;
    private bool animate;
    public float timer = 0f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (animate)
        {
            timer += Time.deltaTime;
            if (timer % 1.875 < 0.625)
            {
                spriteRenderer.sprite = fuelpAnim0;
            }
            else if (timer % 1.875 > 1.25)
            {
                spriteRenderer.sprite = fuelpAnim2;
            }
            else
            {
                spriteRenderer.sprite = fuelpAnim1;
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
       if(collision.gameObject.transform.tag == "Player")
        {
            animate = true;
            collision.transform.GetComponent<PlayerController>().isOnFuel = true;
        }
    }


    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.transform.tag == "Player")
        {
            animate = false;
            collision.transform.GetComponent<PlayerController>().isOnFuel = false;
        }
    }
}
