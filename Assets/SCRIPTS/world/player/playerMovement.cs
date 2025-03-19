using UnityEngine;

public class playerMovement : MonoBehaviour
{
	public Rigidbody2D rb;
	public audioManager audioManager;

	[Header("movement stats")]
	public float defaultSpeed;
	[HideInInspector] public float speed;
	public float bounciness; // jump height
	public float agility; // wall jump cap

	[Header("collision layers")]
	public LayerMask ground;
	public LayerMask wall;

	[Header("ground check")]
	public Transform feetTransform;
	public float feetSize = 0.3f;

	[Header("wall check")]
	public Transform armsTransform;
	public float armSize = 0.2f;

	[HideInInspector] public bool isAlive;

	bool hasJumped;
	float coyoteTime = 0.2f; // time allowed to cj
	float coyoteTimeElapsed;
	int wallJumps;

	void Awake()
	{
		Debug.LogError($"assign audioman in inspector");
		audioManager = FindObjectOfType<audioManager>();

		Debug.LogError($"fix slidy movement");

		Debug.LogError($"assign rb in inspector");
		rb = GetComponent<Rigidbody2D>();

		hasJumped = false;
		wallJumps = 0;
		isAlive = true;
		speed = defaultSpeed;
	}

	void Update()
	{
		if (isAlive)
			movement();
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

		if (isOnGround())
			audioManager.playSfx(audioManager.playerSfxSource, audioManager.footSteps);
	}
	void jump()
	{
		dogStuff(); // runs cj timer
		if (Input.GetButtonDown("Jump") && (isOnGround() || coyoteCheck() || squirrelCheck()))
		{
			hasJumped = true;
			rb.velocity = new Vector2(rb.velocity.x, bounciness);
		}
	}

	bool isOnGround()
	{
		bool touchingGround = Physics2D.OverlapCircle(feetTransform.position, feetSize, ground);
		bool touchingWall = Physics2D.OverlapCircle(feetTransform.position, feetSize, wall);

		if (touchingGround || touchingWall)
		{
			wallJumps = 0;
			return true;
		}
		else return false;
	}
	bool squirrelCheck()
	{
		if (isOnWall() && wallJumps < agility)
		{
			wallJumps++;
			return true;
		}
		else return false;

		bool isOnWall()
		{
			foreach (Transform arm in armsTransform.GetComponentsInChildren<Transform>())
			{
				if (Physics2D.OverlapCircle(arm.position, armSize, wall) && hasJumped)
				{
					return true;
				}
			}
			return false;
		}
	}
	void dogStuff()
	{
		if (isOnGround() && rb.velocity.y <= 0) // < only if falling (prevents double jump)
		{
			hasJumped = false;
			coyoteTimeElapsed = coyoteTime;
		}
		else coyoteTimeElapsed -= Time.deltaTime; //? idk why its based on - not +
	}
	bool coyoteCheck()
	{
		if (coyoteTimeElapsed > 0f && !hasJumped)
			return true;
		else return false;
	}

	void OnDrawGizmosSelected() // debug gizmos
	{
		/* Gizmos.color = Color.red;
		Gizmos.DrawWireSphere(feet.position, feetSize);

		Gizmos.color = Color.green;
		foreach (Transform arm in arms.GetComponentsInChildren<Transform>())
			Gizmos.DrawWireSphere(arm.position, armSize); */
	}
}
