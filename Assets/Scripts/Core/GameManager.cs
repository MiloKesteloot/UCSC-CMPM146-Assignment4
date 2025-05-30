using UnityEngine;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEditor.Experimental.GraphView;

public class GameManager 
{
    public enum GameState
    {
        PREGAME,
        INWAVE,
        WAVEEND,
        COUNTDOWN,
        GAMEOVER,
        PLAYERWIN
    }
    public GameState state;

    public int countdown;
    private static GameManager theInstance;
    public static GameManager Instance {  get
        {
            if (theInstance == null)
                theInstance = new GameManager();
            return theInstance;
        }
    }

    public GameObject player;
    public float startTime;
    
    public ProjectileManager projectileManager;
    public SpellIconManager spellIconManager;
    public EnemySpriteManager enemySpriteManager;
    public PlayerSpriteManager playerSpriteManager;
    public RelicIconManager relicIconManager;

    public Dictionary<string, object> blackboard;

    private List<GameObject> enemies;
    public int enemy_count { get { return enemies.Count; } }

    public void AddEnemy(GameObject enemy)
    {
        enemies.Add(enemy);
    }
    public void RemoveEnemy(GameObject enemy)
    {
        enemies.Remove(enemy);
    }

    public GameObject GetClosestEnemy(Vector3 point)
    {
        if (enemies == null || enemies.Count == 0) return null;
        if (enemies.Count == 1) return enemies[0];
        return enemies.Aggregate((a,b) => (a.transform.position - point).sqrMagnitude < (b.transform.position - point).sqrMagnitude ? a : b);
    }

    public GameObject GetClosestOtherEnemy(GameObject self)
    {
        Vector3 point = self.transform.position;
        if (enemies == null || enemies.Count < 2) return null;
        return enemies.FindAll((a) => a != self).Aggregate((a, b) => (a.transform.position - point).sqrMagnitude < (b.transform.position - point).sqrMagnitude ? a : b);
    }

    public List<GameObject> GetEnemiesInRange(Vector3 point, float distance)
    {
        if (enemies == null || enemies.Count == 0) return null;
        return enemies.FindAll((a) => (a.transform.position - point).magnitude <= distance);
    }

    private GameManager()
    {
        enemies = new List<GameObject>();
        blackboard = new Dictionary<string, object>() {
            { "ATTACK", false }
        };
    }

    public float WinTime()
    {
        return 8 * 60;
    }

    public float CurrentTime()
    {
        return (Time.time - startTime);
    }
}
