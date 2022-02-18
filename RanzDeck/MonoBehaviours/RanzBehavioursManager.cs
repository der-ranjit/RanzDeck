using UnityEngine;

namespace RanzDeck.MonoBehaviours
{
    public static class RanzBehaviorsManager
    {

        public static void DestroyAllRanzDeckMonoBehaviours(GameObject gameObject)
        {
            RanzDeck.Log("[RanzDeck] Destroying MonoBehaviours");
            RanzBehaviorsManager.DestroyRanzBehaviours(gameObject);
        }

        private static void DestroyRanzBehaviours(GameObject gameObject)
        {
            // TODO do i really need the concrete type here ? :(
            RanzBehavior[] components = gameObject.GetComponents<RanzBehavior>();
            foreach (RanzBehavior component in components)
            {
                if (component != null)
                {
                    RanzDeck.Log($"[RanzDeck] Destroyed '{component.name}'");
                    UnityEngine.Object.Destroy(component);
                }
            }
        }
    }
}