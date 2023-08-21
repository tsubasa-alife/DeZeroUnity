using System;
using UnityEngine;

public class CartPoleController : SingletonMonoBehaviour<CartPoleController>
{
	[SerializeField] private GameObject cart;
	[SerializeField] private GameObject pole;
	private Rigidbody cartRb;
	private Rigidbody poleRb;
	[SerializeField] private float _force = 15f;
	[SerializeField] private bool isHeuristic = true;

	protected override void doAwake()
	{
		cartRb = cart.GetComponent<Rigidbody>();
		poleRb = pole.GetComponent<Rigidbody>();
	}


	private void Update()
	{
		if (isHeuristic)
		{
			if (Input.GetKey(KeyCode.LeftArrow))
			{
				cartRb.AddForce(Vector3.left * _force);
			}
		
			if (Input.GetKey(KeyCode.RightArrow))
			{
				cartRb.AddForce(Vector3.right * _force);
			}
		}
	}

	public void Reset()
	{
		// カートの位置をリセット
		cart.transform.position = Vector3.zero;
	}
}