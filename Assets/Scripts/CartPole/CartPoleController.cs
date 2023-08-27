using System;
using TMPro;
using UnityEngine;
using Random = UnityEngine.Random;
using DeZeroUnity;
using MathNet.Numerics.LinearAlgebra;

public class CartPoleController : SingletonMonoBehaviour<CartPoleController>
{
	[SerializeField] private GameObject cart;
	[SerializeField] private GameObject pole;
	private Rigidbody cartRb;
	private Rigidbody poleRb;
	private HingeJoint poleHingeJoint;
	[SerializeField] private float _force = 15f;
	[SerializeField] private ControlMode mode = ControlMode.Manual;
	[SerializeField] private TextMeshProUGUI episodeLabel;
	[SerializeField] private TextMeshProUGUI timeLabel;
	[SerializeField] private TextMeshProUGUI angleLabel;
	private int episode = 1;
	private float startTime;
	private float elapsedTime;
	private SimpleNN model;

	protected override void doAwake()
	{
		cartRb = cart.GetComponent<Rigidbody>();
		poleRb = pole.GetComponent<Rigidbody>();
		poleHingeJoint = pole.GetComponent<HingeJoint>();
		startTime = Time.time;
		elapsedTime = startTime;
		episodeLabel.text = "Episode: " + episode;
		model = new SimpleNN(4, 8, 1);
	}


	private void Update()
	{
		switch (mode)
		{
			case ControlMode.Manual:
			{
				if (Input.GetKey(KeyCode.LeftArrow))
				{
					cartRb.AddForce(Vector3.left * _force);
				}
		
				if (Input.GetKey(KeyCode.RightArrow))
				{
					cartRb.AddForce(Vector3.right * _force);
				}

				break;
			}
			case ControlMode.Random:
			{
				var force = RandomAction();
				Debug.Log("force: " + force);
				cartRb.AddForce(Vector3.right * force);
				break;
			}
			case ControlMode.NN:
			{
				var force = NNAction();
				Debug.Log("force: " + force);
				cartRb.AddForce(Vector3.right * force);
				break;
			}
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

	// ランダム行動を実行
	private float RandomAction()
	{
		var force = UnityEngine.Random.Range(-15f, 15f);
		return force * 10;
	}
	
	// ニューラルネットワークによる行動を実行
	private float NNAction()
	{
		// 状態を取得
		var state = GetState();
		// 状態を入力として渡し、行動を取得
		var action = model.Forward(new Variable(Matrix<float>.Build.Dense(1, 4, state)));
		// 行動を実行
		return action.Data[0, 0] * 10;
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
		episode++;
		episodeLabel.text = "Episode: " + episode;
	}
}