//
//RandomWind.cs for unity-chan!
//
//Original Script is here:
//ricopin / RandomWind.cs
//Rocket Jump : http://rocketjump.skr.jp/unity3d/109/
//https://twitter.com/ricopin416
//
//修正2014/12/20
//風の方向変化/重力影響を追加.
//

using UnityEngine;
using System.Collections;

namespace UnityChan
{
	public class RandomWind : MonoBehaviour
	{
		private SpringBone[] springBones;
		public bool isWindActive = false;

		private bool isMinus = false;				//風方向反転用.
		public float threshold = 0.5f;				// ランダム判定の閾値.
		public float interval = 5.0f;				// ランダム判定のインターバル.
		public float windPower = 1.0f;				//風の強さ.
		public float gravity = 0.98f;				//重力の強さ.
        public bool isGUI = true;


		// Use this for initialization
		void Start ()
		{
			springBones = GetComponent<SpringManager> ().springBones;
			StartCoroutine ("RandomChange");
		}





		// Update is called once per frame
		void Update ()
		{

			Vector3 force = Vector3.zero;
			if (isWindActive) {
				if(isMinus){
					force = new Vector3 (Mathf.PerlinNoise (Time.time, 0.0f) * windPower * -0.001f , gravity * -0.001f , 0);
				}else{
					force = new Vector3 (Mathf.PerlinNoise (Time.time, 0.0f) * windPower * 0.001f, gravity * -0.001f, 0);
				}

				for (int i = 0; i < springBones.Length; i++) {
					springBones [i].springForce = force;
				}
			
			}
		}

		void OnGUI ()
		{
            if (isGUI)
            {
                Rect rect1 = new Rect(10, Screen.height - 40, 400, 30);
                isWindActive = GUI.Toggle(rect1, isWindActive, "Random Wind");
            }
		}

		// ランダム判定用関数.
		IEnumerator RandomChange ()
		{
			// 無限ループ開始.
			while (true) {
				//ランダム判定用シード発生.
				float _seed = Random.Range (0.0f, 1.0f);

				if (_seed > threshold) {
					//_seedがthreshold以上の時、符号を反転する.
					isMinus = true;
				}else{
					isMinus = false;
				}

				// 次の判定までインターバルを置く.
				yield return new WaitForSeconds (interval);
			}
		}


	}
}