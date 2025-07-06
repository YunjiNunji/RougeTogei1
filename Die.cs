using UnityEngine;

[System.Serializable]
public class Die
{
    public string name = "Unnamed Die";
    public int sides = 6;

    public Die(string name, int sides)
    {
        this.name = name;
        this.sides = sides;
    }

    public int Roll()
    {
        int result = Random.Range(1, sides + 1);
        Debug.Log($"{name} rolled a {result}");
        return result;
    }
}