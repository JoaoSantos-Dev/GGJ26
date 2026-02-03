using System;
using System.Collections;
using UnityEngine;

namespace StunSystem
{
    public class StunBase : MonoBehaviour, IStunable
    {
        [SerializeField] private ParticleSystem stunEffect;

        public event Action<bool> StunStateChange;

        private Coroutine stunCoroutine;

        public void Apply()
        {
           if(stunCoroutine != null) StopCoroutine(stunCoroutine);
          stunCoroutine =  StartCoroutine(StunBehaviour());
        }

        protected IEnumerator StunBehaviour()
        {
            stunEffect.Play();
            StunStateChange?.Invoke(true);
            yield return new WaitForSeconds(IStunable.Duration);
            StunStateChange?.Invoke(false);
            stunEffect.Stop();
            print("stun STOP");
        }
    }

    public interface IStunable
    {
        public const int Duration = 1;
        public event Action<bool> StunStateChange;

        public void Apply();
    

    }
}