using UnityEngine;

public class AnimationAudioRelay : MonoBehaviour
{
    public void EventoSomVestirMascara()
    {
        if (AudioManager.Instance != null)
        {
            AudioManager.Instance.PlayEffect(AudioManager.Instance.listaDeSons.vestirMascara, transform.position);
        }
    }

    public void EventoSomPasso()
    {
        if (AudioManager.Instance != null)
        {
            AudioManager.Instance.PlayPasso(transform.position);
        }
    }
}