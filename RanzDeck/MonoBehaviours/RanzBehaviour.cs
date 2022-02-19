using UnityEngine;

namespace RanzDeck.MonoBehaviours
{
    public abstract class RanzBehavior : MonoBehaviour
    {
        public void Destroy()
        {
            UnityEngine.GameObject.Destroy(this);
        }
    }
}
