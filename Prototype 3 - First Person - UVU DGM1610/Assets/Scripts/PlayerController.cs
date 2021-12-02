using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("Stats")]
    //Player movement speed in units per second
    public float movementSpeed;

    //Force applied upwards
    public float jumpForce;

    //Health Stats
    public int curHp;
    public int maxHp;


    [Header("Mouse Look")]
    //Mouse look sensitivity
    public float lookSensitivity;

    //Limits the mouse/camera's rotation
    public float maxLookX = 50f;
    public float minLookX = -65f;

    //Current x rotation of the camera
    private float rotX;

    //Declares player's camera & RigidBody
    private Camera playerCamera;
    private Rigidbody playerRb;
    public Weapon weaponScript;

    private bool isAiming;

    private void Awake()
    {
        //Disable cursor
        Cursor.lockState = CursorLockMode.Locked;
    }

    // Start is called before the first frame update
    void Start()
    {
        //Calls player's camera & RigidBody
        playerCamera = Camera.main;
        playerRb = GameObject.Find("Player").GetComponent<Rigidbody>();

        GameUI.instance.UpdateHealthBar(curHp, maxHp);
        GameUI.instance.UpdateAmmoAmount(weaponScript.currentAmmo, weaponScript.maxAmmo);
        GameUI.instance.UpdateScoreText(0);
    }

    // Applies damage to the player
    public void TakeDamage(int damage)
    {
        //applies damage upon being shot until health reachs 0 or below
        curHp -= damage;

        if (curHp <= 0)
        {
            Die();
        }

        //Updates player's UI health bar
        GameUI.instance.UpdateHealthBar(curHp, maxHp);
    }

    // Ends game if players curHp <= 0
    void Die()
    {
            GameManager.instance.LoseGame();
    }
    
    void Move()
    {
        //Player movement input
        float x = Input.GetAxis("Horizontal") * movementSpeed;
        float z = Input.GetAxis("Vertical") * movementSpeed;

        //playerRb.velocity = new Vector3(x, playerRb.velocity.y, z);
        Vector3 dir = transform.right * x + transform.forward * z;

        //Jump with direction
        dir.y = playerRb.velocity.y;

        playerRb.velocity = dir;

        //Slows player down when aiming
        if (isAiming == true)
            movementSpeed = 4;
        else
            movementSpeed = 6.5f;
    }

    void Jump()
    {
        //Allows jumping through raycasting
        Ray ray = new Ray(transform.position, Vector3.down);

        if (Physics.Raycast(ray, 1.1f))
        {
            playerRb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        }
    }

    public void GiveHealth(int amountToGive)
    {
        //Gives health from pickup collision
        curHp = Mathf.Clamp(curHp + amountToGive, 0, maxHp);
        GameUI.instance.UpdateHealthBar(curHp, maxHp);
    }

    public void GiveAmmo(int amountToGive)
    {
        //Gives ammo from pickup collision
        weaponScript.currentAmmo = Mathf.Clamp(weaponScript.currentAmmo + amountToGive, 0, weaponScript.maxAmmo);
        GameUI.instance.UpdateAmmoAmount(weaponScript.currentAmmo, weaponScript.maxAmmo);
    }

    //Function creating an aim mechanic
    void Aim()
    {
        //While down, player aims down sights
        if(Input.GetMouseButtonDown(1))
        {
            GameObject.Find("Gun").transform.localPosition = new Vector3(0, -0.2f, 0.66f);
            isAiming = true;
        }
        //When released, weapon returns to hip fire
        if (Input.GetMouseButtonUp(1))
        {
            GameObject.Find("Gun").transform.localPosition = new Vector3(0.17f, -0.26704f, 0.66f);
            isAiming = false;
        }
    }

    void CamLook()
    {
        //Mouse controls player look direction/speed
        float y = Input.GetAxis("Mouse X") * lookSensitivity;
        rotX += Input.GetAxis("Mouse Y") * lookSensitivity;

        //Locks camera rotation from full rotation
        rotX = Mathf.Clamp(rotX, minLookX, maxLookX);
        playerCamera.transform.localRotation = Quaternion.Euler(-rotX, 0, 0);
        transform.eulerAngles += Vector3.up * y;
    }

    // Update is called once per frame
    void Update()
    {
        Move();
        CamLook();
        Aim();

        //Fire Button
        if (Input.GetMouseButton(0))
        {
            if (weaponScript.CanShoot())
                weaponScript.Shoot();
        }
        
        //jump button
        if (Input.GetKeyDown(KeyCode.Space))
            Jump();

        //Don't do anything if the game is paused
        if (GameManager.instance.gamePaused == true)
            return;
    }

}
