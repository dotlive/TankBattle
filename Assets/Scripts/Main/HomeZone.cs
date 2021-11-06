using UnityEngine;

namespace Main
{
    class HomeZone : MonoBehaviour
    {
        public ETeam Team = ETeam.A;

        private float m_SqrRadius;

        void Start()
        {
            m_SqrRadius = Match.instance.GlobalSetting.HomeZoneRadius * Match.instance.GlobalSetting.HomeZoneRadius;
        }

        void Update()
        {
            var t = Match.instance.GetTank(Team);
            if (t == null)
            {
                return;
            }

            var homeZonePos = Match.instance.GetRebornPos(Team);
            if ((homeZonePos - t.Position).sqrMagnitude < m_SqrRadius)
            {
                t.HPRecovery(Time.deltaTime * Match.instance.GlobalSetting.HPRecoverySpeed);
            }
            else
            {
                t.HPRecovery(0);
            }
        }
    }
}