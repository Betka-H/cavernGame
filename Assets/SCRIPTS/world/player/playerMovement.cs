using UnityEngine;

public class playerMovement : MonoBehaviour
{
	public Rigidbody2D rb;
	audioManager audioManager;

	[Header("movement stats")]
	public float defaultSpeed;
	[HideInInspector] public float speed;
	public float bounciness; // jump height
	public float agility; // wall jump cap

	[HideInInspector] public bool isAlive;

	bool hasJumped;
	float coyoteTime = 0.15f; // time allowed to cj
	float coyoteTimeElapsed;
	int wallJumps;

	gameController gameController;

	void Awake()
	{
		audioManager = FindObjectOfType<audioManager>();
		gameController = FindObjectOfType<gameController>();

		hasJumped = false;
		wallJumps = 0;
		isAlive = true;

		string oldMovementKey = "oldMovement";
		if (PlayerPrefs.GetInt(oldMovementKey) == 1)
		{
			oldMovement = true;
		}
		else
			oldMovement = false;

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
	bool oldMovement;
	void walk()
	{
		float moveDir;

		switch (oldMovement)
		{
			case true:
				moveDir = Input.GetAxis("Horizontal");
				if (gameController.roomController.currentLevel == gameController.level.cavern)
					speed = 8;
				rb.velocity = new Vector2(moveDir * speed, rb.velocity.y);
				break;
			case false:
				moveDir = Input.GetAxisRaw("Horizontal");
				if (moveDir != 0)
					rb.velocity = new Vector2(moveDir * speed, rb.velocity.y);
				else
				{
					if (rb.velocity.magnitude > 0.5f)
						rb.velocity = new Vector2(rb.velocity.x * 0.9f, rb.velocity.y); // chatgpt
					else rb.velocity = Vector3.zero;
				}
				break;
		}

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

	[Header("collision layers")]
	public LayerMask ground;
	public LayerMask wall;
	[Header("ground check")]
	public Transform feetTransform;
	public float feetSize = 0.3f;
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

	[Header("wall check")]
	public Transform armsTransform;
	public float armSize = 0.2f;
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
