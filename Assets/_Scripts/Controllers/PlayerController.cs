using UnityEngine;

namespace _Project
{
    public class PlayerController : MonoBehaviour
    {
        private PlayerArrow _arrow;
        private PlayerMover _mover;
        private PlayerColor _playerColor;

        public void SetupController(PlayerMover mover, PlayerArrow arrow, PlayerColor playerColor)
        {
            _mover = mover;
            _arrow = arrow;
            _playerColor = playerColor;
        }

        public void ResetPlayer()
        {
            _mover.SetStartPosition();
            _arrow.ResetArrow();
            _playerColor.ResetColor();
        }
    }
}