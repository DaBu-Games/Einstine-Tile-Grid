using System;
using System.Collections.Generic;
using UnityEngine;

public class MetaTileGenerator : MonoBehaviour
{
    [SerializeField]private List<TileDefinition> tileDefinitions = new List<TileDefinition>();
    [SerializeField]private List<Rule> rules;

    public MetaTile GenerateMetaTile(MetaTileType metaTileType, int level)
    {
        MetaTile metaTile = new MetaTile(metaTileType, level);
        
        return metaTile;
    }

    private MetaTile CreateStartMetaTile(MetaTileType metaTileType)
    {
        MetaTile metaTile = new MetaTile(metaTileType, 0);
        
        return metaTile;
    }
}