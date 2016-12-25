using UnityEngine;

public class LevelSelector : MonoBehaviour
{
    public SceneFader SceneFader;
    public void select(string levelName)
    {
        SceneFader.FadeTo(levelName);
    }
}
