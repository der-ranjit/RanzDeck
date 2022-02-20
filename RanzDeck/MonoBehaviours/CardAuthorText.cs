using System.Linq;
using TMPro;
using UnityEngine;

namespace RanzDeck.MonoBehaviours
{
    public class CardAuthorText : RanzBehavior
    {
        public void Awake()
        {
            // add mod name text
            // create blank object for text, and attach it to the canvas
            GameObject modNameObj = new GameObject("ModNameText");
            // find bottom left edge object
            RectTransform[] allChildrenRecursive = base.GetComponentsInChildren<RectTransform>();
            GameObject BottomLeftCorner = allChildrenRecursive.Where(obj => obj.gameObject.name == "EdgePart (1)").FirstOrDefault().gameObject;
            modNameObj.gameObject.transform.SetParent(BottomLeftCorner.transform);
            TextMeshProUGUI modText = modNameObj.gameObject.AddComponent<TextMeshProUGUI>();
            modText.text = "RANZ";
            modNameObj.transform.Rotate(new Vector3(0f, 0f, -1f), 45f);
            modNameObj.transform.Rotate(new Vector3(0f, -1f, 0f), 180f);
            modNameObj.transform.localScale = new Vector3(1f, 1f, 1f);
            modNameObj.transform.localPosition = new Vector3(-50f, -50f, 0f);
            modText.alignment = TextAlignmentOptions.Bottom;
            modText.alpha = 0.1f;
            modText.fontSize = 54;
        }
    }
}