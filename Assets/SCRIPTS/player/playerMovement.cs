using UnityEngine;

public class playerMovement : MonoBehaviour
{
	public Rigidbody2D rb;
	[HideInInspector] public bool isAlive;

	[Header("collision types or smthing")]
	public LayerMask ground;
	public LayerMask wall;

	audioManager audioManager;

	[Header("movement stats")]
	public float defaultSpeed;
	public float speed; // 5f
	public float bounciness; // jump height
	public float agility; // wall jump cap (0 - no jumping allowed >:[)

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

	void Awake()
	{
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
		dogStuff();
		if (Input.GetButtonDown("Jump") && (isOnGround() || coyoteCheck() || squirrelCheck()))
		{
			hasJumped = true;
			rb.velocity = new Vector2(rb.velocity.x, bounciness);
		}
	}

	bool isOnGround()
	{
		bool touchingGround = Physics2D.OverlapCircle(feet.position, feetSize, ground);
		bool touchingWall = Physics2D.OverlapCircle(feet.position, feetSize, wall);

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
			foreach (Transform arm in arms.GetComponentsInChildren<Transform>())
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
		else
			coyoteTimeElapsed -= Time.deltaTime;
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
