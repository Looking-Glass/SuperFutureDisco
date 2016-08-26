using UnityEngine;

	public class Spawner : MonoBehaviour {

		protected float spawnDuration;
		protected float startScale;
		protected float startAlpha;

		private float _startTime;
		private GameObject _objectToSpawn;


		public static GameObject Spawn(string resourceName, Vector3 position, float duration = 1.0f, float startScale = 0.1f, float startAlpha = 0.1f) {
			// create teh object from a resource
			UnityEngine.Object resource = ResourceLoader.Instance.GetResource(resourceName);
			GameObject objectToSpawn = Instantiate(resource, position, Quaternion.identity) as GameObject;

			// turn off all the scripts on the object besides the renderer
			MonoBehaviour[] scripts = objectToSpawn.GetComponents<MonoBehaviour>();
			foreach (MonoBehaviour script in scripts) {
				if (script.GetType() != typeof(SpriteRenderer)) {
					script.enabled = false;
				}
			}

			// add the spawner script
			Spawner spawner = objectToSpawn.AddComponent<Spawner>();
			spawner.spawnDuration = duration;
			spawner.startScale = startScale;
			spawner.startAlpha = startAlpha;
			return objectToSpawn;
		}

		private void Start() {
			_startTime = Time.time;
		}

		private void Update() {
			float elapsedTime = Time.time - _startTime;
			float normalizedTime = elapsedTime / spawnDuration;

			if (normalizedTime > 0) {
				FinishSpawning();
			} else {
				UpdateSpawnAnimation(normalizedTime);
			}
		}

		private void FinishSpawning() {
			// Activate all the scripts that were turned off at the start
			EnableAllScripts();
			// Make sure the scale and alpha are set to their maximum values before finishing
			UpdateSpawnAnimation(1);
			// Remove the spawner script
			Destroy(this);
		}

		private void EnableAllScripts() {
			// turn off all the scripts on the object besides the renderer
			MonoBehaviour[] scripts = gameObject.GetComponents<MonoBehaviour>();
			foreach (MonoBehaviour script in scripts) {
				script.enabled = true;
			}
		}

		private void UpdateSpawnAnimation(float normalizedTime) {
			// NOTE: this assumes that the spawned objects always have white as their sprite color and have a scale of 1

			
			//SpriteRenderer renderer = gameObject.GetComponent<SpriteRenderer>();
			//float alpha = (1 - startAlpha) * normalizedTime + startAlpha;
			//renderer.color = new Color(1, 1, alpha);

//			float scale = (1 - startScale) * normalizedTime + startScale;
//			gameObject.transform.localScale = new Vector3(scale, scale, 1);
		}
			
	}


