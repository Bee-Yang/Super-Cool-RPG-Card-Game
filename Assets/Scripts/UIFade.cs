using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIFade : MonoBehaviour
{
    public CanvasGroup uiElement;

    public void FadeIn()
    {
	StartCoroutine(FadeCanvasGroup(uiElement, uiElement.alpha, 1));
    }

    public void FadeOut()
    {
	StartCoroutine(FadeCanvasGroup(uiElement, uiElement.alpha, 0));
    }

    public IEnumerator FadeCanvasGroup(CanvasGroup cg, float start, float end, float lerpTime = 1)
    {

	float _timeStartedLerping = Time.time;
	float timeSinceStarted = Time.time - _timeStartedLerping;
	float percentageComplete = timeSinceStarted / lerpTime;
	
	// set to Visible immediately
	cg.alpha = 1;
	
	while(true)
	{
		timeSinceStarted = Time.time - _timeStartedLerping;
		percentageComplete = timeSinceStarted / lerpTime;
		float current = Mathf.Lerp(start, end, percentageComplete);
		cg.alpha = current;

		if(percentageComplete >= 1)
		{
			break;
		}

		yield return new WaitForEndOfFrame();
	}

	print("done");
    }
}
