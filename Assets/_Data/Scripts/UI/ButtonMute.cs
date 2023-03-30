using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonMute : MonoBehaviour
{
    [SerializeField] private Slider slider;
    [SerializeField] private Sprite[] sprite;
    [SerializeField] private VolumeButton volumeButton;

    private bool isMute;
    private float originalValue;
    private enum VolumeButton { BGM, SE}

    public void OnButtonMute()
    {
        if (!this.isMute) 
            originalValue = this.slider.value;

        this.isMute = !this.isMute;
        
        GetComponent<Image>().sprite = this.isMute ? sprite[1] : sprite[0];
        this.slider.value = this.isMute ? 0 : originalValue;

        if (AudioManager.HasInstance)
        {
            if (this.volumeButton == VolumeButton.BGM)
                AudioManager.Instance.ChangeBGMVolume(this.slider.value);
            else
                AudioManager.Instance.ChangeSEVolume(this.slider.value);
        }
    }
}
