using UnityEngine;

public class PlayerMovement : MonoBehaviour {
    public float speed = 6f;

    public Vector3 movment;
    public Animator anim;
    public Rigidbody playerRigidbody;
    public int floorMask;
    public float camRayLength = 100f;

	void Awake()
    {
        floorMask = LayerMask.GetMask ("Floor");
		anim = GetComponent <Animator> ();
		playerRigidbody = GetComponent <Rigidbody> ();
	}

    void FixedUpdate()
    {
        float h = Input.GetAxisRaw ("Horizontal");
		float v = Input.GetAxisRaw ("Vertical");

        Move(h, v);
        Turning();
        Animation(h, v);
    }

	void Move (float h, float v)
    {
        movment.Set (h, 0f, v);
        movment = movment.normalized * speed * Time.deltaTime;
        playerRigidbody.MovePosition (transform.position + movment);
    }
   
    void Turning()
    {
        Ray camRay = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit floorHit;

        if ( Physics.Raycast (camRay, out floorHit, camRayLength, floorMask)) {
            Vector3 playerToMouse = floorHit.point - transform.position;
            playerToMouse.y = 0f;
            Quaternion newRotation = Quaternion.LookRotation(playerToMouse);
            playerRigidbody.MoveRotation(newRotation);
        }
    }

    void Animation(float h, float v)
    {
        bool walking = (h != 0f || v != 0f);
        anim.SetBool("IsWalking", walking);
    }		
}