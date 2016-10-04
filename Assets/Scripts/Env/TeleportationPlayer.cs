using UnityEngine;
using System.Collections;

// TODO Add variable to know should we switch enemy direction
//TODO: refactor this to the base class
public class TeleportationPlayer : MonoBehaviour
{

	public bool isLeft;

	void OnCollisionEnter2D (Collision2D other)
	{
		Collider2D collider = other.collider;

		Vector2 initialVelocity = collider.attachedRigidbody.velocity;
		collider.attachedRigidbody.velocity = new Vector3 (
			-initialVelocity.x,
			initialVelocity.y
		);

		if (other.gameObject.tag != "Player")
			return;
			
		Camera camera = Camera.main;
		float height = 2f * camera.orthographicSize;
		float width = height * camera.aspect;

		Vector3 initialPosition = other.transform.position;
		float newX = 0;
		if (isLeft) {
			newX = width - collider.bounds.extents.x;
		} else {
			newX = collider.bounds.extents.x;
		}

		other.transform.position = new Vector3 (
			newX,
			initialPosition.y,
			initialPosition.z
		);


	}
}