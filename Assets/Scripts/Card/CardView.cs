using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CardView : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI title;
    [SerializeField] private TextMeshProUGUI details;
    [SerializeField] private TextMeshProUGUI swipeLeftText;
    [SerializeField] private TextMeshProUGUI swipeRightText;
    [SerializeField] private Image art;
    [SerializeField] private Image background;

    [SerializeField] private CardScriptableObject card;

    [SerializeField] private float returnSpeed = 1.0f;
    [SerializeField] private float rotationReturnSpeed = 1.0f;

    [SerializeField] private float sideMargin = 300.0f;

    [Range(-50, 0)] [SerializeField] private int yDragRatio = -50;
    [Range(0, 15)] [SerializeField] private int zRotationRatio;

    [SerializeField] private GameManager gameManager;

    private bool isDragging;
    private Color originalSwipeTextColor;
    private Color swipeColor;

    public CardScriptableObject Card { get => card; set => card = value; }
    public float CardWidth { get; set; } = 100.0f;

    private void Start()
    {
        LoadCard(card);
        originalSwipeTextColor = swipeLeftText.color;
        swipeColor = originalSwipeTextColor;
    }

    public void LoadCard(CardScriptableObject card)
    {
        this.title.text = card.CardName;
        this.details.text = card.CardDetails;
        //this.art.sprite = card.Art;
        this.swipeLeftText.text = card.SwipeLeftText;
        this.swipeRightText.text = card.SwipeRightText;
        this.Card = card;
        transform.position = Vector3.zero;
        transform.rotation = Quaternion.identity;

    }
    
    private void Update()
    {
        if (isDragging)
        {
            Vector2 position = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            position.x = Mathf.Clamp(position.x, -sideMargin, sideMargin);
            position.y = Mathf.Min(Mathf.Abs(position.x / sideMargin), 1) * yDragRatio;
            transform.position = position;
            Vector3 rotation = new Vector3(0, 0, Mathf.Min(position.x / sideMargin, 1) * -zRotationRatio);
            transform.rotation = Quaternion.Euler(rotation);
            swipeColor.a = Mathf.Min(Mathf.Abs(position.x / sideMargin), 1);
        }
        else
        {
            transform.position = Vector2.MoveTowards(transform.position, Vector2.zero, returnSpeed);
            transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.identity, rotationReturnSpeed);
            
        }

        if (transform.position.x > 0)
        {
            swipeLeftText.color = originalSwipeTextColor; // prevent fast moving glitch
            swipeRightText.color = swipeColor;
            if (transform.position.x >= (sideMargin - CardWidth / 2 - 5) && Input.GetMouseButtonUp(0))
            {
                gameManager.SwipedRight();
            }
        }
        else if (transform.position.x < 0)
        {
            swipeRightText.color = originalSwipeTextColor; // prevent fast moving glitch
            swipeLeftText.color = swipeColor;
            if (transform.position.x <= (-sideMargin + CardWidth / 2 + 5) && Input.GetMouseButtonUp(0))
            {
                gameManager.SwipedLeft();
            }
        }
        else
        {
            swipeLeftText.color = originalSwipeTextColor;
            swipeRightText.color = originalSwipeTextColor;
        }
    }

    private void OnMouseOver()
    {
        if (Input.GetMouseButton(0))
        {
            this.isDragging = true;
        }
        
    }

    private void OnMouseUp()
    {
        this.isDragging = false;
    }
}
