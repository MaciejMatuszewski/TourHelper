
using TourHelper.Base.Model.Entity;
using UnityEngine;

namespace TourHelper.Base.Logic
{
    public interface IPlayer
    {
        void InitializePlayer(GameObject camera);
        void UpdatePlayer();
        void RebasePlayer(Coordinates _origin);

    }
}
