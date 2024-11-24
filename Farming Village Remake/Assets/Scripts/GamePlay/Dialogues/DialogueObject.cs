using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(menuName = "Dialogue/DialogueObject")]
public class DialogueObject : ScriptableObject
{
    [SerializeField] private string charactername;
    [SerializeField] Sprite characterSprite; 
    [SerializeField][TextArea] private string[] dialogue;
    [SerializeField] private Response[] responses;

    public string[] Dialogue => dialogue;

    public bool HasResponses => Responses != null && Responses.Length > 0;

    public Response[] Responses => responses;

    public string CharacterName => charactername;

    public Sprite CharacterSprite => characterSprite;


}

