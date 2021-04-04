using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    public bool MouseLook = true;
    public string HorzAxis = "Horizontal";
    public string VertAxis = "Vertical";
    public string FireAxis = "Fire1";
    // .3 seconds must elapse before player can fire again
    public float ReloadDelay = 0.3F;
    public bool CanFire = true;
    // an array of Transforms is used in case we ever add more turrets to the player spaceship
    public Transform[] TurretTransforms;
    public float MaxSpeed = 5f;

    private Rigidbody ThisBody = null;

    private void Awake()
    {
        ThisBody = GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        // FixedUpdate is called once before physics system is update
        // GetAxis returns -1 to 0 to 1 if appropriate key is pressed. If left/up and right/down keys both pressed, returns 0
        float Horz = Input.GetAxis(HorzAxis);
        float Vert = Input.GetAxis(VertAxis);
        Vector3 MoveDirection = new Vector3(Horz, 0.0f, Vert);
        //AddForce applies a physical force to the Player object, moving it in a specific direction
        ThisBody.AddForce(MoveDirection.normalized * MaxSpeed);
        ThisBody.velocity = new Vector3
            (Mathf.Clamp(ThisBody.velocity.x, -MaxSpeed, MaxSpeed),
            Mathf.Clamp(ThisBody.velocity.y, -MaxSpeed, MaxSpeed),
            Mathf.Clamp(ThisBody.velocity.z, -MaxSpeed, MaxSpeed));

        if (MouseLook)
        {
            // ScreenToWorldPoint converts the screen position of the cursor to a position in the Game World Makes the player always look at the mouse cursor
            Vector3 MousePosWorld = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 0.0F));
            MousePosWorld = new Vector3(MousePosWorld.x, 0.0F, MousePosWorld.z);
            Vector3 LookDirection = MousePosWorld - transform.position;

            transform.localRotation = Quaternion.LookRotation(LookDirection.normalized,Vector3.up);
        }

        if (Input.GetButtonDown(FireAxis) && CanFire)
        {
            foreach (Transform T in TurretTransforms)
            {
                AmmoManager.SpawnAmmo(T.position, T.rotation);
            }
            CanFire = false;
            Invoke("EnableFire", ReloadDelay);
        }
    }

    void EnableFire()
    {
        CanFire = true;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnDestroy()
    {
        Debug.Log("Trying to set GameOver");
;        GameController.GameOver();
    }
}
