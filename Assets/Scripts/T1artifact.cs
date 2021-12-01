using UnityEngine;

public class T1artifact : MonoBehaviour
{
    public GameObject text;
    public static System.EventHandler<System.EventArgs> onConclusion;
    // Update is called once per frame
    void Start()
    {
        text.SetActive(false);
    }

    [System.Obsolete]
    void Update()
    {
        if (this.gameObject.active)
        {
            if (Input.GetKey(KeyCode.E))
            {
                GroundTile.complete = true;
                OnConclusion();
            }
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        text.SetActive(true);
    }
    private void OnTriggerExit(Collider other)
    {
        text.SetActive(false);
    }
    private void OnConclusion()
    {
        //print(count);
        onConclusion?.Invoke(this, new System.EventArgs());
    }
}
