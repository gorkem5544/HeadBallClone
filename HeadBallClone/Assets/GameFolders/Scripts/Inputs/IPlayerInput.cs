using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assembly_CSharp.Assets.GameFolders.Scripts.Inputs
{
    public interface IPlayerInput
    {
        float HorizontalInput { get; }
        bool JumpKeyDown { get; }
        bool ShootKeyDown { get; }
    }

}