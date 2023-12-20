using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TMPro;
using UnityEngine;
using Random = UnityEngine.Random;
using DeZeroUnity;
using DeZeroUnity.Algebra;

public class CartPoleController : SingletonMonoBehaviour<CartPoleController>
{
	[SerializeField] private GameObject cart;
	[SerializeField] private GameObject pole;
	private Rigidbody cartRb;
	private Rigidbody poleRb;
	private HingeJoint poleHingeJoint;
	[SerializeField] private float _force = 15f;
	[SerializeField] private ControlMode mode = ControlMode.Manual;
	[SerializeField] private bool isTrainMode = false;
	private bool shouldTrain = false;
	private bool onTraining = false;
	private bool isEndTraining = false;
	[SerializeField] private TextMeshProUGUI episodeLabel;
	[SerializeField] private TextMeshProUGUI timeLabel;
	[SerializeField] private TextMeshProUGUI angleLabel;
	private int episode = 1;
	[SerializeField] private int maxEpisode = 100;
	private float episodeTime = 10f;
	private int interval = 5;
	private int epochs = 100;
	private float startTime;
	private float elapsedTime;
	private float getObservationSpan = 1f;
	private float currentTime = 0f;
	private SimpleNN model;
	private float lr = 0.2f;
	private SGD optimizer;

	private Dictionary<float[],float> trainData = new Dictionary<float[], float>();

	protected override void doAwake()
	{
		cartRb = cart.GetComponent<Rigidbody>();
		poleRb = pole.GetComponent<Rigidbody>();
		poleHingeJoint = pole.GetComponent<HingeJoint>();
		startTime = Time.time;
		elapsedTime = startTime;
		episodeLabel.text = "Episode: " + episode;
		if (mode == ControlMode.NN)
		{
			model = new SimpleNN(4, 8, 1);
			optimizer = new SGD(lr);
			optimizer.Setup(model);
		}
		else
		{
			// モードがNNでない場合は訓練モードをオフにする
			isTrainMode = false;
		}
	}


	private void Update()
	{
		if (!onTraining)
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
					cartRb.AddForce(Vector3.right * force);
					break;
				}
				case ControlMode.NN:
				{
					var force = NNAction();
					cartRb.AddForce(Vector3.right * force);
					break;
				}
			}

			if (isTrainMode)
			{
				currentTime += Time.deltaTime;
				if (currentTime > getObservationSpan)
				{
					GetObservation();
					currentTime = 0f;
				}
			}

			timeLabel.text = "Time: " + (Time.time - elapsedTime).ToString("F2");
			angleLabel.text = "Angle: " + poleHingeJoint.angle.ToString("F2");
		}
		else
		{
			if (isEndTraining)
			{
				Debug.Log("シミュレーションを再開");
				onTraining = false;
				episodeLabel.text = "Episode: " + episode;
				elapsedTime = Time.time;
				// 物理演算を再開
				Physics.autoSimulation = true;
				isEndTraining = false;
			}
		}
	}

	private void FixedUpdate()
	{
		if (!onTraining)
		{
			if (Mathf.Approximately(poleHingeJoint.angle, 60f) || Mathf.Approximately(poleHingeJoint.angle, -60f))
			{
				Reset();
			}

			if (Time.time - elapsedTime > episodeTime)
			{
				Reset();
			}
		}
	}

	private void LateUpdate()
	{
		if (shouldTrain)
		{
			// 一旦物理演算を止める
			Physics.autoSimulation = false;
			onTraining = true;
			episodeLabel.text = "Episode: Training...";
			_ = Task.Run(() => TrainNN());
			shouldTrain = false;
		}
	}

	// カートポールの状態を取得
	public float[] GetState()
	{
		var state = new float[4];
		state[0] = cart.transform.position.x; // カートの位置
		state[1] = cartRb.velocity.x; // カートの速度
		state[2] = poleHingeJoint.angle; // ポールの角度
		state[3] = poleRb.angularVelocity.x; // ポールの角速度
		return state;
	}
	
	// 一定時間ごとに訓練用データを作成
	public void GetObservation()
	{
		var state = GetState();
		var action = model.Forward(new Variable(new Matrix(1, 4, state)));
		// 教師データは、ポールが傾いている方向へより大きな力を加えるようなものを作成
		var groundTruth = action.Data.Elements[0, 0] - (state[2] / 60f);
		trainData.Add(state, groundTruth);
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
		var action = model.Forward(new Variable(new Matrix(1, 4, state)));
		// 行動を実行
		return action.Data.Elements[0, 0] * 150f;
	}

	// NNの学習を実行
	private void TrainNN()
	{
		Debug.Log("CartPole 学習開始");
		var inputData = trainData.Keys.ToList();
		var groundTruth = trainData.Values.ToList();
		
		for (int i = 0; i < epochs; i++)
		{
			for (int j = 0; j < inputData.Count; j++)
			{
				var x = new Variable(new Matrix(1, 4, inputData[j]));
				var t = new Variable(new Matrix(1, 1, new float[] { groundTruth[j] }));
				
				var y = model.Forward(x);
				var loss = Dzf.MeanSquaredError(y, t);
				
				model.ClearGrads();
				loss[0].Backward();
				
				optimizer.Update();
			}
		}
		
		Debug.Log("CartPole 学習終了");
		trainData.Clear();
		isEndTraining = true;
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
		if (episode > maxEpisode)
		{
			// ゲームを終了
#if UNITY_EDITOR
			UnityEditor.EditorApplication.isPlaying = false;
#else
			Application.Quit();
#endif
		}
		// 一定エピソードごとに学習フラグを立てる
		if (episode % interval == 0 && isTrainMode)
		{
			shouldTrain = true;
		}
	}
}