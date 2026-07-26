using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class OptionsSliderElement : MonoBehaviour
{
    [SerializeField] private Slider slider;
    public UnityEvent<float> EvtSliderValueChanged = new UnityEvent<float>();

    private void Start()
    {
        slider.onValueChanged.AddListener(OnSliderValueChanged);
    }

    public void Initialize(float minValue, float maxValue, float currentValue)
    {
        slider.minValue = minValue;
        slider.maxValue = maxValue;
        slider.value = currentValue;
    }

    public void OnSliderValueChanged(float newValue)
    {
        if(EvtSliderValueChanged != null)
        {
            EvtSliderValueChanged.Invoke(newValue);
        }
    }

    private void OnDestroy()
    {
        slider.onValueChanged.RemoveAllListeners();
    }
}
