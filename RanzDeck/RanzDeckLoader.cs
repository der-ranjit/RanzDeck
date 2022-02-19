using System.Collections.Generic;
using Photon.Pun;
using RanzDeck.Cards;
using RanzDeck.MonoBehaviours;
using UnboundLib;
using UnboundLib.Cards;
using UnboundLib.Utils;
using UnityEngine;

namespace RanzDeck
{
    public static class RanzDeckLoader
    {
        public static List<CardInfo> buildCards = new List<CardInfo>();

        public static void Load()
        {
            DevMode.Log("Loading start");
            RanzDeckLoader.LoadCards();
        }

        public static void Unload()
        {
            DevMode.Log("Unloading start");
            RanzDeck.CardArtBundle.Unload(true);
            RanzDeckLoader.UnloadBuildCards();
            RanzDeckLoader.DestroyAllPlayersRanzBehaviours();
        }

        public static void DestroyRanzBehaviours(GameObject gameObject)
        {
            RanzBehavior[] components = gameObject.GetComponents<RanzBehavior>();
            foreach (RanzBehavior component in components)
            {
                if (component != null)
                {
                    component.Destroy();
                    DevMode.Log($"Destroyed '{component.GetType()}'");
                }
            }
        }

        private static void LoadCards()
        {
            CustomCard.BuildCard<DrSmollBot>(RanzDeckLoader.HandleCardBuild);
            CustomCard.BuildCard<DrFatBot>(RanzDeckLoader.HandleCardBuild);
            CustomCard.BuildCard<CockyBlocky>(RanzDeckLoader.HandleCardBuild);
            CustomCard.BuildCard<KrazyKevin>(RanzDeckLoader.HandleCardBuild);
        }

        private static void HandleCardBuild(CardInfo cardInfo)
        {
            DevMode.Log($"Loaded card '{cardInfo.name}'");
            RanzDeckLoader.buildCards.Add(cardInfo);
            CardManager.EnableCard(cardInfo);
        }

        private static void UnloadBuildCards()
        {
            foreach (CardInfo cardInfo in RanzDeckLoader.buildCards)
            {
                DevMode.Log($"Unloading Card '{cardInfo.name}'"); ;
                CardManager.DisableCard(cardInfo);
                CardManager.cards.Remove(cardInfo.name);
                CustomCard.cards.Remove(cardInfo);
                ((DefaultPool)PhotonNetwork.PrefabPool).ResourceCache.Remove(cardInfo.name);
                PhotonNetwork.Destroy(cardInfo.gameObject);
            }
            RanzDeckLoader.buildCards.Clear();
        }

        private static void DestroyAllPlayersRanzBehaviours()
        {
            if (PlayerManager.instance != null)
            {
                PlayerManager.instance.InvokeMethod("ResetCharacters");
            }
        }
    }

}