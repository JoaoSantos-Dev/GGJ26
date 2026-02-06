using System;
using System.Collections;
using UnityEngine;

namespace StunSystem
{
    public class StunBase : MonoBehaviour, IStunable
    {
        public bool StunActive { get; set; } = false;
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
            StunActive = true;
            yield return new WaitForSeconds(IStunable.Duration);
            StunStateChange?.Invoke(false);
            StunActive = false;
            stunEffect.Stop(true, ParticleSystemStopBehavior.StopEmittingAndClear);
        }
    }

    public interface IStunable
    {
        public const int Duration = 1;
        public event Action<bool> StunStateChange;

        public void Apply();
    

    }
}