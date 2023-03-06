using System.Collections.Generic;
using UnityEngine;

public interface IBFSMove
{
    void BeginTurn();
    void CalculateAdjecencyList(Tile target);
    Tile EndTileFind(Tile t);
    void EndTurn();
    void FindSelectableTiles();
    void GetCurrentTile();
    Tile GetTargetTile(GameObject tar);
    Tile LowestFCost(List<Tile> openList);
    void MoveToTarget(Tile tile);
    void MoveUnits();
    void PathFind(Tile target);
}