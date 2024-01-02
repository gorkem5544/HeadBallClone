using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IEntityController
{
    Transform transform { get; }
}
public interface IFootController : IEntityController
{

}
public class FootController : MonoBehaviour
{

}
