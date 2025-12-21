using System.Collections.Generic;
using UnityEngine;

public class SortController : MonoBehaviour
{
    public int PlayerIndex;
    public SortRandomizer Randomizer;
    public Vector2Int BarSizeInPixels;
    public GameObject BarPrefab;
    
    private readonly List<Bar> bars = new();
    private int selection;
    private BongoInput input;

    private float BarWidth => BarSizeInPixels.x * 0.01f;

    private void Start()
    {
        var numbers = Randomizer.GetNumbers();
        
        for(var i = 0; i < Randomizer.NumberOfBars; i++)
        {
            var barObject = Instantiate(BarPrefab, transform);
            barObject.transform.position = GetBarPosition(i);
            var bar = barObject.GetComponent<Bar>();
            bar.SetHeight(0.2f + (float)numbers[i] / Randomizer.NumberOfBars * 0.8f);
            bars.Add(bar);
        }
        
        bars[0].Select();

        input = BongoInput.GetByIndex(PlayerIndex);
        input.LeftPressed += MoveLeft;
        input.RightPressed += MoveRight;
        input.ClapPressed += Selection;
    }

    private Vector3 GetBarPosition(int index)
    {
        return transform.position + new Vector3(index * BarWidth, 0, 0);
    }

    private void OnDestroy()
    {
        input.LeftPressed -= MoveLeft;
        input.RightPressed -= MoveRight;
        input.ClapPressed -= Selection;
    }

    private void MoveLeft() => Move(-1);
    private void MoveRight() => Move(1);

    private void Move(int change)
    {
        var oldSelection = selection;
        
        selection = Mathf.Clamp(selection + change, 0, Randomizer.NumberOfBars - 1);
        if(oldSelection != selection)
        {
            if(bars[oldSelection].IsMoveActive)
            {
                bars[oldSelection].Position = GetBarPosition(selection);
                bars[selection].Position = GetBarPosition(oldSelection);
                (bars[oldSelection], bars[selection]) = (bars[selection], bars[oldSelection]);
            }
            else
            {
                bars[oldSelection].Deselect();
                bars[selection].Select();
            }
        }
    }

    private void Selection()
    {
        if(bars[selection].IsMoveActive)
            bars[selection].Select();
        else
            bars[selection].StartMove();
    }
}