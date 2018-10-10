using UnityEngine;
using UnityEngine.UI;

public class FPS : MonoBehaviour
{
    public Text countText;
    public Text fpsText;

	float deltaTime;
    private int curCount = 0;
	
	void Update ()
	{
		deltaTime += (Time.unscaledDeltaTime - deltaTime) * 0.1f;
        SetFPS();
	}

    public void AddElementCount(int count)
    {
        curCount += count;
        countText.text = "Total Bill: " + curCount.ToString();
    }

	void SetFPS()
	{
		float msec = deltaTime * 1000.0f;
		float fps = 1.0f / deltaTime;
		fpsText.text = string.Format("FPS: {0:00.} ({1:00.0} ms)", fps, msec);
	}
}
