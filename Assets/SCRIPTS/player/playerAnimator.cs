using UnityEngine;

public class playerAnimator : MonoBehaviour
{
	public playerMovement playerMovement;
	public Rigidbody2D rb;
	public Animator animator;

	private enum direction { idle, up, down, right, left, dead };

	void Start()
	{
		Debug.LogError($"assign playermovement manually");
		playerMovement = FindObjectOfType<playerMovement>();

		Debug.LogError($"assign rb manually");
		rb = GetComponentInParent<Rigidbody2D>();

		Debug.LogError($"assign animator manually");
		animator = GetComponentInParent<Animator>();
	}

	void FixedUpdate()
	{
		animate();
	}

	void animate()
	{
		stop(); //* only stop on dir change
		switch (getDir()) //? combine?
		{
			case direction.idle:
				animator.SetBool("idle", true);
				break;
			case direction.up:
				animator.SetBool("up", true);
				break;
			case direction.down:
				animator.SetBool("down", true);
				break;
			case direction.right:
				animator.SetBool("right", true);
				break;
			case direction.left:
				animator.SetBool("left", true);
				break;
			case direction.dead:
				animator.SetBool("dead", true);
				break;
		}
		void stop()
		{
			animator.SetBool("idle", false);
			animator.SetBool("left", false);
			animator.SetBool("right", false);
			animator.SetBool("up", false);
			animator.SetBool("down", false);
			animator.SetBool("dead", false);
		}
		direction getDir()
		{
			if (!playerMovement.isAlive)
				return direction.dead;
			else if (rb.velocity.x == 0 && rb.velocity.y == 0)
				return direction.idle;
			else if (rb.velocity.y > 0)
				return direction.up;
			else if (rb.velocity.y < 0)
				return direction.down;
			else if (rb.velocity.x < 0)
				return direction.left;
			else // if (rb.velocity.x > 0)
				return direction.right;
		}
	}
}
