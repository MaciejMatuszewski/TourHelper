
using TourHelper.Base.Model.Entity;
using UnityEngine;

namespace TourHelper.Base.Logic
{
    public interface IPlayer
    {
        void InitializePlayer(Camera camera);
        void UpdatePlayer();
        void RebasePlayer(Coordinate _origin);

    }
}
