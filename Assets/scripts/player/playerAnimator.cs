using UnityEngine;

public class playerAnimator : MonoBehaviour
{
	private Rigidbody2D rb;
	private Animator animator;

	private enum direction { idle, up, down, right, left, dead };
	private direction dir;

	void Start()
	{
		rb = GetComponentInParent<Rigidbody2D>();
		animator = GetComponentInParent<Animator>();
	}

	void FixedUpdate()
	{
		// if (gameState.State == gameState.gameStates.playing)
		{
			getDir();
			animations();
		}
	}

	void getDir()
	{
		if (!GetComponentInParent<playerMovement>().alive)
		{
			dir = direction.dead;
		}
		else if (rb.velocity.x == 0 && rb.velocity.y == 0)
		{
			dir = direction.idle;
		}
		else if (rb.velocity.y > 0)
		{
			dir = direction.up;
		}
		else if (rb.velocity.y < 0)
		{
			dir = direction.down;
		}
		else if (rb.velocity.x < 0)
		{
			dir = direction.left;
		}
		else if (rb.velocity.x > 0)
		{
			dir = direction.right;
		}
	}

	void animations()
	{
		stop();
		switch (dir)
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
	}
}
