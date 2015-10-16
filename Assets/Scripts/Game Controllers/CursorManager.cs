using UnityEngine;
using System.Collections;

public class CursorManager : MonoBehaviour
{

    static CursorManager instance;

    public Texture2D cursorNormal, cursorCanGrab, cursorHasGrabbed;
	Vector2 hotspotNormal = new Vector2(23.75f, 9.5f);
	Vector2 hotspotCanGrab = new Vector2(26.25f, 20f);
	Vector2 hotspotHasGrabbed = new Vector2(31.25f, 25f);
    public bool canGrab = false, hasGrabbed = false;

    void Awake()
    {
        if (instance == null)
        {
            //Debug.Log("Assigning instance of Audio Controller");
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
        DontDestroyOnLoad(this);
    }

    public void OnApplicationQuit()
    {
        //Debug.Log("Audio Controller destroyed");
        instance = null;
        Destroy(this);
    }

    void Update()
    {
        if (hasGrabbed)
        {
            SetCursor(2);
        }
        else if(canGrab)
        {
            SetCursor(1);
        }
        else
        {
            SetCursor(0);
        }
    }

    public static void SetCanGrab(bool value)
    {
        if (instance != null) instance.canGrab = value;
    }
    public static void SetHasGrabbed(bool value)
    {
        if (instance != null) instance.hasGrabbed = value;
    }

    void SetCursor(int type = 0)
    {
        switch (type)
        {
            case (1):
                Cursor.SetCursor(instance.cursorCanGrab, instance.hotspotCanGrab, CursorMode.Auto);
                break;
            case (2):
                Cursor.SetCursor(instance.cursorHasGrabbed, instance.hotspotHasGrabbed, CursorMode.Auto);
                break;
            default:
                Cursor.SetCursor(instance.cursorNormal, instance.hotspotNormal, CursorMode.Auto);
                break;
        }
    }
}
