using UnityEngine;
using UnityEngine.UI;

public class CollectionCuratorText : MonoBehaviour, ScreenInitializedListener
{
    [GetComponent] public Text text;

    public void OnScreenInitialized()
    {
        var screen = this.GetScreenParent<CollectionDetailsScreen>();
        screen.onScreenPayloadLoaded.AddListener(() =>
        {
            text.text = screen.LoadedPayload.Collection.owner.Uid;
        });
    }
    
}