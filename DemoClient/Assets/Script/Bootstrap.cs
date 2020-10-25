using UnityEngine;
using FigNet.Core;

public class Bootstrap : MonoBehaviour
{
    void Start()
    {
        Invoke(nameof(RegisterHandlers), 0.03f);
    }

    private void RegisterHandlers() 
    {
        FN.HandlerCollection.RegisterHandler(new RoomJoinHandler());
    }
 
}
