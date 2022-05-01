using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class Screenshot : MonoBehaviour {
 [MenuItem("Screenshot/Take screenshot")]
 static void TakeScreenshot()
 {
     ScreenCapture.CaptureScreenshot("screenshot.png");
 }
}
