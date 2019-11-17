using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Card")]
public class CardScriptableObject : ScriptableObject
{
    [SerializeField] private string cardName;
    [SerializeField] private Texture art;
    [SerializeField] private string cardDetails;
    [SerializeField] private string swipeRightText;
    [SerializeField] private string swipeLeftText;

    [SerializeField] private CardScriptableObject rightSwipeCard;
    [SerializeField] private CardScriptableObject leftSwipeCard;

    public string CardName { get => cardName; set => cardName = value; }
    public Texture Art { get => art; set => art = value; }
    public string CardDetails { get => cardDetails; set => cardDetails = value; }
    public string SwipeRightText { get => swipeRightText; set => swipeRightText = value; }
    public string SwipeLeftText { get => swipeLeftText; set => swipeLeftText = value; }
    public CardScriptableObject RightSwipeCard { get => rightSwipeCard; set => rightSwipeCard = value; }
    public CardScriptableObject LeftSwipeCard { get => leftSwipeCard; set => leftSwipeCard = value; }
}
