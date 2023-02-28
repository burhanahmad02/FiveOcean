using UnityEngine;
using UnityEngine.UI;

public class SlideNumber : MonoBehaviour
{
    public Text text;
    public float textAnimationTime = 2f;

    public float desiredNumber = 0;
    public float initialNumber = 0;
    public float currentNumber = 0;

    bool animate = false;
    public bool _isTime = false;
    
    private void Update()
    {
        if (animate)
        {
            if (currentNumber != desiredNumber)
            {
                if (initialNumber < desiredNumber)
                {
                    currentNumber += (textAnimationTime * Time.unscaledDeltaTime) * (desiredNumber - initialNumber);
                    if (currentNumber >= desiredNumber)
                    {
                        currentNumber = desiredNumber;
                    }
                }
                else
                {
                    currentNumber -= (textAnimationTime * Time.unscaledDeltaTime) * (initialNumber - desiredNumber);

                    if (currentNumber <= desiredNumber)
                    {
                        currentNumber = desiredNumber;
                        animate = false;
                    }
                }

                if (_isTime)
                {
                    int minutes = Mathf.FloorToInt(currentNumber / 60f);
                    int seconds = Mathf.RoundToInt(currentNumber % 60f);

                    if (seconds == 60)
                    {
                        seconds = 0;
                        minutes += 1;
                    }
                    if (minutes <= 0)
                    {
                        minutes = 0;
                    }
                    text.text = minutes.ToString("00") + ":" + seconds.ToString("00");
                }
                else
                {
                    text.text = currentNumber.ToString("0");
                }
            }
        }
    }

    public void SetNumberForSliding(float _current, float _desired)
    {
        initialNumber = _current;
        currentNumber = initialNumber;
        desiredNumber = _desired;
        animate = true;
    }

    public void SetNumber(float value)
    {
        initialNumber = currentNumber;
        desiredNumber = value;

        animate = true;
    }

    public void SetNumberForTime(float value)
    {
        initialNumber = value;
        currentNumber = initialNumber;
        desiredNumber = 0;
        animate = true;
    }

    public void AddToNumber(float value)
    {
        initialNumber = currentNumber;
        desiredNumber += value;
        animate = true;
    }
}