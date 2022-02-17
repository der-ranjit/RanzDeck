using UnityEngine;

namespace RanzDeck.MonoBehaviours
{
    public static class RanzBehaviorsManager
    {

        public static void DestroyAllRanzDeckMonoBehaviours(GameObject gameObject)
        {
            RanzBehaviorsManager.DestroyRanzBehaviours(gameObject);
        }

        private static void DestroyRanzBehaviours(GameObject gameObject)
        {
            RanzBehavior[] components = gameObject.GetComponents<RanzBehavior>();
            foreach (RanzBehavior component in components)
            {
                if (component != null)
                {
                    UnityEngine.Object.Destroy(component);
                }
            }
        }
    }
}