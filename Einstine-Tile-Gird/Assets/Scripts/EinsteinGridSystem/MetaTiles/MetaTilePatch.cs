using System.Collections.Generic;
using UnityEngine;

namespace DaBu.EGS.MetaTiles
{
    public class MetaTilePatch
    {
        public List<MetaTile> Childeren { get; private set; }

        public MetaTilePatch()
        {
            Childeren = new List<MetaTile>();
        }
        
        public void AddChild(MetaTile metaTile) => Childeren.Add(metaTile);
        
        public void AddChild(MetaTile metaTile, Matrix4x4 transform)
        {
            metaTile.SetTransform(transform);
            AddChild(metaTile);
        }
    }
}