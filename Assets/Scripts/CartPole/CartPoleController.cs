using System;
using TMPro;
using UnityEngine;
using Random = UnityEngine.Random;

public class CartPoleController : SingletonMonoBehaviour<CartPoleController>
{
	[SerializeField] private GameObject cart;
	[SerializeField] private GameObject pole;
	private Rigidbody cartRb;
	private Rigidbody poleRb;
	private HingeJoint poleHingeJoint;
	[SerializeField] private float _force = 15f;
	[SerializeField] private bool isHeuristic = true;
	[SerializeField] private TextMeshProUGUI timeLabel;
	[SerializeField] private TextMeshProUGUI angleLabel;
	private float startTime;
	private float elapsedTime;

	protected override void doAwake()
	{
		cartRb = cart.GetComponent<Rigidbody>();
		poleRb = pole.GetComponent<Rigidbody>();
		poleHingeJoint = pole.GetComponent<HingeJoint>();
		startTime = Time.time;
		elapsedTime = startTime;
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
		else
		{
			var force = AgentAction();
			Debug.Log("force: " + force);
			cartRb.AddForce(Vector3.right * force);
		}

		timeLabel.text = "Time: " + (Time.time - elapsedTime).ToString("F2");
		angleLabel.text = "Angle: " + poleHingeJoint.angle.ToString("F2");
	}

	private void FixedUpdate()
	{
		if (Mathf.Approximately(poleHingeJoint.angle, 60f) || Mathf.Approximately(poleHingeJoint.angle, -60f))
		{
			Reset();
		}
	}

	// カートポールの状態を取得
	public float GetState()
	{
		var state = new float[4];
		state[0] = cart.transform.position.x; // カートの位置
		state[1] = cartRb.velocity.x; // カートの速度
		state[2] = poleHingeJoint.angle; // ポールの角度
		state[3] = poleRb.angularVelocity.x; // ポールの角速度
		return state[0];
	}

	// AIの行動を実行
	private float AgentAction()
	{
		var force = UnityEngine.Random.Range(-15f, 15f);
		return force * 10;
	}

	public void Reset()
	{
		// カートの位置をリセット
		cart.transform.position = Vector3.zero;
		// カートの速度をリセット
		cartRb.velocity = Vector3.zero;
		// ポールの位置をリセット
		pole.transform.position = new Vector3(0, 2.5f, 0);
		// ポールの角度をリセット
		pole.transform.rotation = Quaternion.identity;
		// ポールの速度をリセット
		poleRb.velocity = Vector3.zero;
		// ポールの角速度をリセット
		poleRb.angularVelocity = Vector3.zero;
		// タイマーをリセット
		elapsedTime = Time.time;
		Debug.Log("Reset完了");
	}
}