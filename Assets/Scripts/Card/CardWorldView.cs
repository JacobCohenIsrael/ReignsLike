using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CardWorldView : MonoBehaviour
{
    [SerializeField] private TextMeshPro title;
    [SerializeField] private TextMeshPro details;
    [SerializeField] private TextMeshPro swipeLeftText;
    [SerializeField] private TextMeshPro swipeRightText;
    [SerializeField] private GameObject art;
    [SerializeField] private Image background;

    [SerializeField] private CardScriptableObject card;

    [SerializeField] private float returnSpeed = 1.0f;
    [SerializeField] private float rotationReturnSpeed = 1.0f;

    [SerializeField] private float sideMargin = 300.0f;

    [Range(-0.5f, 0.0f)] [SerializeField] private float yDragRatio = -0.1f;
    [Range(0, 180)] [SerializeField] private int zRotationRatio;

    [SerializeField] private GameManager gameManager;

    private bool isDragging;
    private Color originalSwipeTextColor;
    private Color swipeColor;

    public CardScriptableObject Card { get => card; set => card = value; }
    public float CardWidth { get; set; } = 3.0f;

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
        art.GetComponent<Renderer>().material.mainTexture = card.Art;
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
            Vector3 mousePosition = new Vector3(Input.mousePosition.x, Input.mousePosition.y, 20);
            Vector2 position = Camera.main.ScreenToWorldPoint(mousePosition);
            position.x = Mathf.Clamp(position.x, -sideMargin, sideMargin);
            position.y = Mathf.Min(Mathf.Abs(position.x / sideMargin), 1) * yDragRatio;
            transform.position = Vector2.MoveTowards(transform.position, position, returnSpeed);
            Vector3 rotation = new Vector3(0, Mathf.Min(position.x / sideMargin, 1) * -zRotationRatio, Mathf.Min(position.x / sideMargin, 1) * -zRotationRatio);
            transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler(rotation), rotationReturnSpeed);
            swipeColor.a = Mathf.Lerp(swipeColor.a, Mathf.Min(Mathf.Abs(position.x / sideMargin), 1), returnSpeed);
        }
        else
        {
            transform.position = Vector2.MoveTowards(transform.position, Vector2.zero, returnSpeed);
            transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.identity, rotationReturnSpeed);
            swipeColor.a = Mathf.Lerp(swipeColor.a, 0, returnSpeed);

        }

        if (transform.position.x > 0)
        {
            swipeLeftText.color = originalSwipeTextColor; // prevent fast moving glitch
            swipeRightText.color = swipeColor;
            if (transform.position.x >= (sideMargin - 0.25) && Input.GetMouseButtonUp(0))
            {
                Debug.Log(transform.position.x);
                gameManager.SwipedRight();
            }
        }
        else if (transform.position.x < 0)
        {
            swipeRightText.color = originalSwipeTextColor; // prevent fast moving glitch
            swipeLeftText.color = swipeColor;
            if (transform.position.x <= (-sideMargin + 0.25) && Input.GetMouseButtonUp(0))
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
