using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class SortRandomizer : MonoBehaviour
{
    public int NumberOfBars = 10;
    List<int> numbers;
    
    public List<int> GetNumbers()
    {
        if(numbers == null)
            Randomize();

        return numbers;
    }

    private void Randomize()
    {
        numbers = Enumerable.Range(1, NumberOfBars).ToList();
        for (var i = numbers.Count - 1; i > 0; i--)
        {
            var j = Random.Range(0, i + 1);
            (numbers[i], numbers[j]) = (numbers[j], numbers[i]);
        }
    }
}
