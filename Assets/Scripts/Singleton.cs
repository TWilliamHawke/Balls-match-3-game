using UnityEngine;

abstract public class Singleton<T> : MonoBehaviour where T: MonoBehaviour
{
    static T _instance;
    public static T instance
    {
        get
        {
            if(_instance == null)
            {
                var obj = GameObject.FindObjectOfType<T>();
                if(obj == null) {
                    Debug.LogError($"You forgot to add {typeof(T)} into scene");
                }
                _instance = obj;
            }
            return _instance;
        }
    }

    protected virtual void Awake() {
        if(_instance != null)
        {
            print("Wtf???");
            Destroy(this);
        }
    }

}
