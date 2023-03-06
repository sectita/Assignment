using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnManager : MonoBehaviour
{
    static Dictionary<string, List<BFSMove>> units = new Dictionary<string, List<BFSMove>>(); //string for tag
    static Queue<string> turnKey = new Queue<string>();
    static Queue<BFSMove> turnTeam = new Queue<BFSMove>();


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (turnTeam.Count == 0)
        {
            TeamTurnQueue();
        }
    }

    static void TeamTurnQueue()
    {
        List<BFSMove> teamList = units[turnKey.Peek()];

        foreach (BFSMove unit in teamList)
        {
            turnTeam.Enqueue(unit);
        }
        StartingTurn();
    }

    static void StartingTurn()
    {
        if (turnTeam.Count > 0)
        {
            turnTeam.Peek().BeginTurn(); ;
        }
    }
    public static void EndTurn()
    {
        BFSMove unit = turnTeam.Dequeue();
        unit.EndTurn();

        if (turnTeam.Count > 0)
        {
            StartingTurn();
        }
        else
        {
            string team = turnKey.Dequeue();
            turnKey.Enqueue(team);
            TeamTurnQueue();
        }
    }
    public static void AddUnit(BFSMove unit)
    {
        List<BFSMove> list;
        if (!units.ContainsKey(unit.tag))
        {
            list = new List<BFSMove>();
            units[unit.tag] = list;

            if (!turnKey.Contains(unit.tag))
            {
                turnKey.Enqueue(unit.tag);
            }

        }
        else
        {
            list = units[unit.tag];
        }
        list.Add(unit);
    }
}
