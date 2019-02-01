
using UnityEngine;

public class PlayerState : MonoBehaviour {

    private static PlayerState _Instance;
    public static PlayerState Instance {
        get {
            if (_Instance == null)
                _Instance = new GameObject("PlayerState").AddComponent<PlayerState>();
            return _Instance;
        }
    }

    public Horizontal Horizontal;
    public Vertical Vertical;
    public DirectionFacing DirectionFacing;
    public Attack attack;
}

public enum Horizontal {
    Idle = 0,
    MovingLeft = -1,
    MovingRight = 1
}

public enum Vertical
{
    Grounded,
    AirBorn
}

public enum DirectionFacing {
    Left = -1,
    Right = 1
}

public enum Attack {
    passive,
    punch,
    projectile
}
