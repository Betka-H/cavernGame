using UnityEngine;

public class playerMovement : MonoBehaviour
{
	private Rigidbody2D rb;
	public bool alive;

	[Header("collision types or smthing")]
	public LayerMask ground;
	public LayerMask wall;

	[Header("movement stats")]
	public float speed = 20f; // 5f
	public float bounciness = 10f; // jump height
	public float agility = 1; // wall jump cap (0 - no jumping allowed >:[)

	[Header("ground check")]
	public Transform feet;
	public float feetSize = 0.3f;

	[Header("wall check")]
	public Transform arms;
	public float armSize = 0.2f;

	private bool hasJumped;
	private float coyoteTime = 0.2f; // time allowed to cj
	private float coyoteTimeElapsed;
	private int wallJumps;

	void Start()
	{
		rb = GetComponent<Rigidbody2D>();
		hasJumped = false;
		wallJumps = 0;
	}
	void FixedUpdate()
	{
		// movement();
		// Debug.LogWarning("player movement changed to fixedupdate. beware of weird shit");
	}
	void Update()
	{
		if (alive)
		{
			movement();
			Time.timeScale = 1;
		}
		else
		{
			rb.velocity = Vector3.zero;
			Time.timeScale = 0;
		}
	}

	// if feet are within a distance from ground (or wall) layer
	bool groundCheck()
	{
		bool groundC = Physics2D.OverlapCircle(feet.position, feetSize, ground);
		bool wallC = Physics2D.OverlapCircle(feet.position, feetSize, wall);

		if (groundC || wallC)
		{
			wallJumps = 0;
			return true;
		}
		else return false;
	}
	// if arms are within a distance from wall layer
	bool wallsCheck()
	{
		// if arms are within a distance from wall layer
		foreach (Transform arm in arms.GetComponentsInChildren<Transform>())
		{
			if (Physics2D.OverlapCircle(arm.position, armSize, wall) && hasJumped)
			{
				return true;
			}
		}
		return false;
	}

	// draws debug gizmos
	void OnDrawGizmosSelected()
	{
		Gizmos.color = Color.red;
		Gizmos.DrawWireSphere(feet.position, feetSize);

		Gizmos.color = Color.green;
		foreach (Transform arm in arms.GetComponentsInChildren<Transform>())
			Gizmos.DrawWireSphere(arm.position, armSize);
	}

	void movement()
	{
		walk();
		jump();
	}

	void walk()
	{
		float moveDir = Input.GetAxis("Horizontal");
		rb.velocity = new Vector2(moveDir * speed, rb.velocity.y);
	}
	void jump()
	{
		dogStuff(); // handles cj timer
		if (Input.GetButtonDown("Jump") && (groundCheck() || coyoteCheck() || squirrelCheck())) // if the player is on the ground, can cj or can wj
		{
			hasJumped = true;
			rb.velocity = new Vector2(rb.velocity.x, bounciness);
		}
	}

	void dogStuff()
	{
		// checks if the player is touching ground while not moving vertically (since feet have a radius idkk)
		if (groundCheck() && rb.velocity.y <= 0)
		{
			hasJumped = false;
			coyoteTimeElapsed = coyoteTime;
		}
		else
		{
			coyoteTimeElapsed -= Time.deltaTime;
		}
	}
	bool coyoteCheck()
	{
		// if the time limit for a coyote jump is not up yet and the player hasnt already jumped
		if (coyoteTimeElapsed > 0f && !hasJumped)
			return true;
		else return false;
	}
	bool squirrelCheck()
	{
		if (wallsCheck() && wallJumps < agility)
		{
			wallJumps++;
			return true;
		}
		else return false;
	}
}
