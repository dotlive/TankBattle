using UnityEngine;

namespace Main
{
    public static class Utils
    {
        public static void PlayParticle(string resName, Vector3 pos)
        {
            Object obj = Resources.Load(resName);
            if(obj == null)
            {
                return;
            }

            GameObject effect = (GameObject)GameObject.Instantiate(obj);
            if(effect != null)
            {
                effect.transform.position = pos;
            }
        }

        public static void ChangeTankColor(GameObject tank, Color color)
        {
            var mesh = tank.GetComponentsInChildren<MeshRenderer>();
            foreach (var m in mesh)
            {
                m.material.color = color;
            }
        }
    }
}
