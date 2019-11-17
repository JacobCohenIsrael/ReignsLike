using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private CardWorldView cardView;

    public void SwipedLeft()
    {
        cardView.LoadCard(cardView.Card.LeftSwipeCard);
    }

    public void SwipedRight()
    {
        cardView.LoadCard(cardView.Card.RightSwipeCard);
    }
}
