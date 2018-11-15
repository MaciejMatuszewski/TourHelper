
using TourHelper.Base.Model.Entity;
using UnityEngine;

namespace TourHelper.Base.Logic
{
    public interface IPlayer
    {
        double AccumulatedDistance { get; }
        void InitializePlayer(Camera camera);
        void UpdatePlayer();
        void RebasePlayer(Coordinate _origin);

    }
}
