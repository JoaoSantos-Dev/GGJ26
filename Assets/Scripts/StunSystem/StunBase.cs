using System;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace StunSystem
{
    public class StunBase : MonoBehaviour, IStunable
    {
        [SerializeField] private ParticleSystem stunEffect;

        public event Action<bool> StunStateChange;


        public void Apply()
        {
            StunBehaviour();
        }

        protected async virtual void StunBehaviour()
        {
            stunEffect.Play();
            StunStateChange?.Invoke(true);
            await UniTask.Delay((int)(IStunable.Duration*1000));
            StunStateChange?.Invoke(false);
            stunEffect.Stop();
        }
    }

    public interface IStunable
    {
        public const int Duration = 1;
        public event Action<bool> StunStateChange;

        public void Apply();
    

    }
}