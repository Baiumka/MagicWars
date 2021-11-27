using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class LobbyDialog : MonoBehaviour
{
    [SerializeField]
    Image currentTypeImage;
    [SerializeField]
    Transform currentButtonPanel;

    [SerializeField]
    Button yesButton;
    [SerializeField]
    Button noButton;
    public enum DialogType
    {
        dtError,
        dtAlert,
        dtInfo,
        dtQuest
    }

    public enum DialogResult
    {
        none,
        btYes,
        btNo
    }

    [SerializeField]
    Sprite errorImage;
    [SerializeField]
    Sprite alertImage;
    [SerializeField]
    Sprite infoImage;
    [SerializeField]
    Sprite questImage;

    DialogResult _result = DialogResult.none;
    int _inviteGroupId = 0;

    public DialogResult result { get => _result; set => _result = value; }
    public int inviteGroupId { get => _inviteGroupId; set => _inviteGroupId = value; }

    public IEnumerator ShowDialog(string message, DialogType type, System.Action action)
    {

        switch (type)
        {
            case DialogType.dtError:
                currentTypeImage.sprite = errorImage;
                break;
            case DialogType.dtAlert:
                currentTypeImage.sprite = alertImage;
                break;
            case DialogType.dtInfo:
                currentTypeImage.sprite = infoImage;
                break;
            case DialogType.dtQuest:
                currentTypeImage.sprite = questImage;
                break;
        }

        transform.Find("Text").GetComponent<Text>().text = message;
        gameObject.SetActive(true);

        yesButton.onClick.AddListener(PressedYesButton);
        noButton.onClick.AddListener(PressedNoButton);

        yield return new WaitUntil(() => _result != DialogResult.none);
        action();
    }


    void PressedYesButton()
    {
        _result = DialogResult.btYes;
    }
    void PressedNoButton()
    {
        _result = DialogResult.btNo;
    }
}
