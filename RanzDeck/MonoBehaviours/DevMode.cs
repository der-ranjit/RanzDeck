using System.Collections;
using System.Linq;
using RanzDeck.Cards;
using RanzDeck.Utils;
using UnboundLib.GameModes;
using UnboundLib.Utils;
using UnityEngine;

namespace RanzDeck.MonoBehaviours
{
    public class DevMode : RanzBehavior
    {
        public static DevMode? instance;

        public static void Log(string message)
        {
            if (RanzDeck.devMode)
            {
                UnityEngine.Debug.Log($"[RanzDeck] {message}");
            }
        }

        private void Awake()
        {
            DevMode.instance = this;
        }

        private void Update()
        {
            if (RanzDeck.devMode && Input.GetKeyDown(KeyCode.F5))
            {
                this.ToggleSandBox();
            }
            else if (RanzDeck.devMode && Input.GetKeyDown(KeyCode.F8))
            {
                this.GiveCardsToPlayers();
            }
        }

        public void OnDestroy()
        {
            // destroy remaining cards 
            this.DestroyActiveCardObjects();
        }

        public void EnterSandbox()
        {
            base.StartCoroutine(this.LoadSandboxAndSpawnPlayer());
        }

        public void EnterMainMenu()
        {
            base.StartCoroutine(this.LoadMainMenu());
        }

        public IEnumerator ApplyCardsToPlayerID(int id, string[] cardNames)
        {
            foreach (CardInfo card in CardChoice.instance.cards.Where(cardInfo => cardNames.Contains(cardInfo.name)))
            {
                GameObject cardToPick = CardChoice.instance.AddCard(card);
                yield return WaitFor.Frames(2);
                ApplyCardStats cardStats = cardToPick.GetComponent<ApplyCardStats>();
                if (cardStats != null)
                {
                    cardStats.Pick(id);
                    UnityEngine.Object.Destroy(cardToPick);
                }
            }
        }

        private void ToggleSandBox()
        {
            bool activeGame = GameModeManager.CurrentHandler != null;
            if (activeGame)
            {
                this.EnterMainMenu();
            }
            else
            {
                this.EnterSandbox();
            }
        }

        private IEnumerator LoadSandboxAndSpawnPlayer()
        {
            // TODO first time entering sandbox programatically does not spawn map
            if (MainMenuHandler.instance != null && MapManager.instance != null && PlayerAssigner.instance != null)
            {
                string sandboxMap = "MovingParts2_M";
                MainMenuHandler.instance.Close();
                LevelManager.EnableLevel(sandboxMap);
                MapManager.instance.forceMap = sandboxMap;
                GameModeManager.SetGameMode("Sandbox");
                if (GameModeManager.CurrentHandler != null)
                {
                    GameModeManager.CurrentHandler.StartGame();
                    this.EnterSandboxCardChoiceFix();
                }
                yield return new WaitForSeconds(1);
                if (PlayerAssigner.instance != null)
                {
                    yield return base.StartCoroutine(PlayerAssigner.instance.CreatePlayer(null, false));
                    this.GiveCardsToPlayers();
                }
            }
        }

        private void GiveCardsToPlayers()
        {
            string[] cardsForP1 = {
                    KrazyKevin.CardName,
                    DrFatBot.CardName,
                    DrFatBot.CardName,
                    DrFatBot.CardName,
                    DrSmollBot.CardName
                };
            string[] cardsForP2 = {
                    KrazyKevin.CardName,
                    DrFatBot.CardName
                };
            int player1ID = PlayerManager.instance.players.First().playerID;
            int player2ID = player1ID + 1;
            base.StartCoroutine(this.ApplyCardsToPlayerID(player1ID, cardsForP1));
        }

        private void EnterSandboxCardChoiceFix()
        {
            // change activeCard collection to trigger CardChoice.instance.cards change; seems to be a bug in unbound
            CardManager.DisableCard(RanzDeckLoader.buildCards[0]);
            CardManager.EnableCard(RanzDeckLoader.buildCards[0]);
        }

        private IEnumerator LoadMainMenu()
        {
            yield return new WaitForSeconds(0);
            GameModeManager.SetGameMode(null);
            if (NetworkConnectionHandler.instance != null)
            {
                NetworkConnectionHandler.instance.NetworkRestart();
            }
        }

        private void DestroyActiveCardObjects()
        {
            bool activeGame = GameModeManager.CurrentHandler != null;
            if (activeGame)
            {
                foreach (GameObject gameObject in GameObject.FindObjectsOfType<GameObject>())
                {
                    if (gameObject.activeInHierarchy)
                    {
                        foreach (ApplyCardStats cardStats in gameObject.GetComponents<ApplyCardStats>())
                        {
                            DevMode.Log(cardStats.gameObject.name);
                            UnityEngine.Object.Destroy(cardStats.gameObject);
                        };
                    }
                }
            }
        }
    }
}