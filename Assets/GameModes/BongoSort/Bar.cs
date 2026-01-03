using KBCore.Refs;
using UnityEngine;

public class Bar : ValidatedMonoBehaviour
{
    public Sprite Normal;
    public Sprite Selected;
    public Sprite Move;
    
    [SerializeField, Child] SpriteRenderer SpriteRenderer;
    
    public bool IsMoveActive { get; set; }
    public Vector3 Position
    {
        get => transform.position;
        set => transform.position = value;
    }

    public int Number { get; set; }

    public void Select()
    {
        IsMoveActive = false;
        SpriteRenderer.transform.localPosition = Vector3.zero;
        SpriteRenderer.sprite = Selected;
    }

    public void Deselect()
    {
        SpriteRenderer.sprite = Normal;
    }

    public void StartMove()
    {
        IsMoveActive = true;
        SpriteRenderer.sprite = Move;
        SpriteRenderer.transform.localPosition = new Vector3(0, -0.05f, 0);
    }

    public void SetHeight(float height)
    {
        SpriteRenderer.size = new Vector2(SpriteRenderer.size.x, height);
    }
}
