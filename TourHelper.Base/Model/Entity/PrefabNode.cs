
using System.Collections.Generic;
using TourHelper.Base.Manager;
using UnityEngine;

namespace TourHelper.Base.Model.Entity
{
    public class PrefabNode
    {
        public string Name { get; set; }

        public GameObject Container { get; set; }

        public ICollection<IPointInRange> Actions { get; set; }
    }
}
