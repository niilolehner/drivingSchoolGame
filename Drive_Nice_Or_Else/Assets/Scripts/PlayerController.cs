using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    // Takes the class and make it public.
    public static PlayerController instance;
    public GameObject player;
    UI_Manager ui_Manager;
    public Rigidbody2D rb;
    private Vector2 playerDirectionY;
    private Vector2 playerDirectionX;
    public float playerSpeed;

    // This function is called when the script instance is being loaded.
    void Awake()
    {
        if (instance == null)
            instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        ui_Manager = FindObjectOfType<UI_Manager>();
    }

    // Update is called once per frame
    void Update()
    {   // Get input and set force to make car moving.
        float directionY = Input.GetAxisRaw("Vertical");
        float directionX = Input.GetAxisRaw("Horizontal");
        playerDirectionY = new Vector2(0, directionY).normalized;
        playerDirectionX = new Vector2(directionX, 0).normalized;
    }

    void FixedUpdate()
    {
        rb.velocity = new Vector2(playerDirectionX.x * playerSpeed, playerDirectionY.y * playerSpeed);    
    }

    public void SwitchCarPosition(bool isRightDirection)
    {
        float positionY = isRightDirection ? 2.35f : -2.5f;
        player.transform.Translate(new Vector3(0, positionY, 0));
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Bad")
        {
            ui_Manager.lifes--;
        }
        else if (collision.tag == "Good")
        {
            if (ui_Manager.lifes != 3)
            {
                ui_Manager.lifes++;
            }
        }
    }
}
