using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assembly_CSharp.Assets.GameFolders.Scripts.Inputs
{
    public class PlayerInput : IPlayerInput
    {

        public float HorizontalInput => Input.GetAxis("Horizontal");

        public bool JumpKeyDown => Input.GetKeyDown(KeyCode.W);

        public bool ShootKeyDown => Input.GetKey(KeyCode.Space);
    }

}