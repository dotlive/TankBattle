using AI.SensorSystem;
using UnityEngine;

namespace Main
{
    /// <summary>
    /// 星星
    /// </summary>
    public class Star : MonoBehaviour
    {
        private static int _idGen = 0;

        private static int GetNextID()
        {
            return _idGen++;
        }

        public int ID { get; private set; }

        public Vector3 Position => transform.position;

        public bool IsSuperStar { get; private set; } = false;

        private bool m_Taken = false;
        private float m_NextJingleTime;

        internal void Init(Vector3 pos, bool isSuperStar)
        {
            ID = GetNextID();
            transform.position = pos;
            m_Taken = false;
            IsSuperStar = isSuperStar;
        }

        public void Update()
        {
            if (Time.time >= m_NextJingleTime)
            {
                Match.instance.SendStim(
                    Stimulus.CreateStimulus((int) EStimulusType.StarJingle, ESensorType.Hearing, Position, this,
                        IsSuperStar ? 1f : 0.5f));
                m_NextJingleTime = Time.time + 1f;
            }
        }

        private void OnTriggerEnter(Collider other)
        {
            if (PhysicsUtils.IsFireCollider(other) == false)
            {
                return;
            }

            if (m_Taken == true)
            {
                return;
            }

            m_Taken = true;
            FireCollider fc = other.GetComponent<FireCollider>();
            if (fc != null && fc.Owner != null)
            {
                fc.Owner.TakeStar(IsSuperStar);
                Match.instance.SendStim(
                    Stimulus.CreateStimulus((int) EStimulusType.StarTaken, ESensorType.Hearing, Position, this));
                Match.instance.RemoveStar(this);
            }
        }
    }
}