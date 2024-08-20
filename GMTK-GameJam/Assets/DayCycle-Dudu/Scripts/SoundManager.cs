using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SoundManager : MonoBehaviour
{
    public static SoundManager Instance;

    [SerializeField] private AudioClipsRefsSO audioClipsRefsSO;
    [Range(1f, 30f)][SerializeField] private float volume;
    // [SerializeField] private Slider volumeSlider;

    private float startVolume;

    private void Awake() 
    {
        Instance = this;   
    }

    private void Start() 
    {
        startVolume = volume;
        // volumeSlider.value = volume;      
    }

    private void PlaySound(AudioClip[] audioClipArray, Vector3 position, float volume)
    {
        PlaySound(audioClipArray[Random.Range(0, audioClipArray.Length)], position, volume);
    }

    private void PlaySound(AudioClip audioClip, Vector3 position, float volume)
    {
        AudioSource.PlayClipAtPoint(audioClip, position, volume);
    }

    //

    public void PlayDropGetItemSound(Vector3 position, float volumeMultiplier)
    {
        PlaySound(audioClipsRefsSO.dropGetItem, position, volumeMultiplier * volume);
    }
    public void PlaySpawnEggSound(Vector3 position, float volumeMultiplier)
    {
        PlaySound(audioClipsRefsSO.spawnEgg, position, volumeMultiplier * volume);
    }
    public void PlaySellEggSound(Vector3 position, float volumeMultiplier)
    {
        PlaySound(audioClipsRefsSO.sellEgg, position, volumeMultiplier * volume);
    }
    public void PlayBeanTreeGrowSound(Vector3 position, float volumeMultiplier)
    {
        PlaySound(audioClipsRefsSO.beanTreeGrow, position, volumeMultiplier * volume);
    }
    public void PlayCoinSound(Vector3 position, float volumeMultiplier)
    {
        PlaySound(audioClipsRefsSO.coin, position, volumeMultiplier * volume);
    }
    public void PlayBeanTreeDamageSound(Vector3 position, float volumeMultiplier)
    {
        PlaySound(audioClipsRefsSO.beanTreeDamage, position, volumeMultiplier * volume);
    }
    public void PlayBeanTreeBreakSound(Vector3 position, float volumeMultiplier)
    {
        PlaySound(audioClipsRefsSO.beanTreeBreak, position, volumeMultiplier * volume);
    }
    public void PlayBeanTreeFootstepSound(Vector3 position, float volumeMultiplier)
    {
        PlaySound(audioClipsRefsSO.beanTreeFootstep, position, volumeMultiplier * volume);
    }
    public void PlayVillagerFootstepSound(Vector3 position, float volumeMultiplier)
    {
        PlaySound(audioClipsRefsSO.villagerFootstep, position, volumeMultiplier * volume);
    }

    /* public void SetVolumeSlider()
    {
        volume = volumeSlider.value * startVolume;
    } */


}
