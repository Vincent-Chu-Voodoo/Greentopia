#if UNITY_EDITOR
using System.IO;
using UnityEngine;
using System.Collections;
using UnityEditor;

namespace EmptyGame.Misc {

	public class ScreenshotController : MonoBehaviour {
		public string ScreenshotFolder = "Screenshots";
		private int count = 1;

		public string[] resolutions;

		private bool locked = false;

		private Vector2 currentResolution;
		private float currentTimeScale;

		void Start() {
			if (!Directory.Exists(ScreenshotFolder)) {
				Debug.Log("Created directory for screenshots: " + ScreenshotFolder);
				Directory.CreateDirectory(ScreenshotFolder);
			}
			count = Directory.GetFiles(ScreenshotFolder).Length / resolutions.Length;

			currentResolution = GameViewUtils.GetMainGameViewSize();
			currentTimeScale = Time.timeScale;
		}

		void Update() {
			if ((Input.GetMouseButtonDown(1) || Input.GetKeyDown(KeyCode.Space)) && !locked) {
				StartCoroutine(CaptureMultipleResolutions());
			}
		}

		public IEnumerator CaptureMultipleResolutions() {
			locked = true;
			Time.timeScale = 0;
			count++;
			yield return new WaitForEndOfFrame();
			
			for (int i = 0; i < resolutions.Length; i++)
			{
				print($"akaCK0 {resolutions[i]}");
				if (GameViewUtils.SizeExists(resolutions[i])) {
					GameViewUtils.SetSize(resolutions[i]);									
					yield return new WaitForEndOfFrame();
					print($"akaCK1");
					Capture(resolutions[i]);
				}
			}
			GameViewUtils.SetSize(currentResolution);
			Time.timeScale = currentTimeScale;
			locked = false;
   		}

		void Capture(string name) {
			print($"akaCK2 {name}");
			string screenshotName = ScreenshotFolder + "/" + Application.productName + "_" + count + "_" + name + ".png";
			ScreenCapture.CaptureScreenshot(screenshotName);
			Debug.Log("Saved screenshot : \"" + screenshotName + "\"");
		}
	}
}
#endif