using System.Collections.Generic;
using MarblesTD.Towers;
using UnityEngine;

public class Bootstrap : MonoBehaviour
{
    public Transform StartingPosition;
    public Transform EndPosition;
    
    public List<Tower> Towers = new List<Tower>();
    public List<Marble> Marbles = new List<Marble>();
    public List<Projectile> Projectiles = new List<Projectile>();

    public static Bootstrap Instance;
    private void Awake()
    {
        Instance = this;
    }

    void Update()
    {
        for (int i = Towers.Count - 1; i >= 0; i--)
        {
            Towers[i].Update(GetMarblePlacements(), Time.deltaTime);
        }

        for (int i = Projectiles.Count - 1; i >= 0; i--)
        {
            Projectiles[i].Update(Time.deltaTime);
        }
        
        for (int i = Marbles.Count - 1; i >= 0; i--)
        {
            Marbles[i].Update(EndPosition, Time.deltaTime);
        }
    }

    private IEnumerable<MarblePlacement> GetMarblePlacements()
    {
        int marblesCount = Marbles.Count;
        for (int i = 0; i < marblesCount; i++)
        {
            var marble = Marbles[i];
            yield return new MarblePlacement(marble, marble.Position);
        }
    }
}
