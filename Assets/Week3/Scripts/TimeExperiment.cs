using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class TimeExperiment : MonoBehaviour
{
    public Button toggleButton;
    public Text label;

    private static bool? requestedMode;

    void Start()
    {
        if (FindFirstObjectByType<UpdateMove>() == null)
        {
            if (toggleButton != null)
                toggleButton.gameObject.SetActive(false);
            return;
        }

        if (requestedMode.HasValue)
        {
            foreach (var m in FindObjectsByType<UpdateMove>(FindObjectsSortMode.None))
                m.useDeltaTime = requestedMode.Value;
            foreach (var m in FindObjectsByType<LateUpdateMove>(FindObjectsSortMode.None))
                m.useDeltaTime = requestedMode.Value;
            foreach (var m in FindObjectsByType<FixedUpdateMove>(FindObjectsSortMode.None))
                m.useDeltaTime = requestedMode.Value;
        }

        if (label != null)
            label.text = "Use Delta Time: " + (CurrentMode() ? "ON" : "OFF");
    }

    bool CurrentMode()
    {
        var mover = FindFirstObjectByType<UpdateMove>();
        return mover == null || mover.useDeltaTime;
    }

    public void Toggle()
    {
        requestedMode = !CurrentMode();
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
