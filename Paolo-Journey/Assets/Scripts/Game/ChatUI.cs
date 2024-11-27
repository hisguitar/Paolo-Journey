using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ChatUI : MonoBehaviour
{
    public GameObject videoCanvas;

    [SerializeField] private TMP_Text dialogueText;
    [SerializeField] private GameObject chatUI; // Chat UI ที่จะแสดงข้อความ
    [SerializeField] private GameObject miniGame;
    [SerializeField] private float typingSpeed = 0.025f; // ความเร็วในการพิมพ์ทีละตัว

    [SerializeField] private Sprite[] doctorImages;
    [SerializeField] private Image doctorProfileImage;

    // เพิ่ม AudioClips สำหรับเสียงพากย์
    [SerializeField] private AudioClip[] dialogueAudioClips;
    private AudioSource audioSource;

    private string[] dialogues = {"สวัสดีครับ\nเห็นว่าเล่นสเก็ตบอร์ดล้มมาใช่ไหม\nไม่ต้องห่วงนะครับ\nเดี๋ยวหมอจะช่วยดูแลแผลให้เอง!", 
        "ครับหมอ ผมเจ็บแผลนิดหน่อยครับ", 
        "เข้าใจเลยครับ!\nแผลที่เข่าเจ็บได้ง่ายจริง ๆ\nเดี๋ยวเราทำแผลนิดหน่อยก็จะหายไวขึ้น", 
        "ครับหมอ\nแล้วผมจะกลับไปเล่นได้อีกไหม?",
        "ได้แน่นอนครับ! แค่ต้องดูแลตัวเองให้ดี\nช่วยหมอทำแผลให้สะอาดก่อนนะครับ\nคุณพร้อมไหม?",
        "พร้อมครับ!",
        "สวัสดีครับ! ผมชื่อฟีมอน\nจะมาช่วยคุณหมอในวันนี้นะครับ!\nตอนนี้เรามีแผลที่ต้องดูแล\nนี่เป็นขั้นตอนที่หนึ่งเลย!",
        "โอ้ ดีจัง!\nแล้วเราจะเริ่มทำยังไงครับ?",
        "ก่อนอื่นเลย\nเราต้องทำความสะอาดแผลครับ\nจะได้ไม่มีเชื้อโรคอยู่ในแผลของเรา\nคุณคิดว่าเราควรใช้เครื่องมืออะไรดี"
    };
    private int currentDialogueIndex = 0;
    private bool isTyping = false;
    private bool isDialogueComplete = false;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
        ShowDialogue();
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0)) // เมื่อกดคลิกซ้ายหรือแตะหน้าจอ
        {
            if (isTyping)
            {
                StopAllCoroutines();
                dialogueText.text = dialogues[currentDialogueIndex];
                isTyping = false;
                isDialogueComplete = true;
            }
            else if (isDialogueComplete)
            {
                currentDialogueIndex++;
                if (currentDialogueIndex < dialogues.Length)
                {
                    ShowDialogue();
                }
                else
                {
                    chatUI.SetActive(false);
                    miniGame.SetActive(true);
                }
            }
        }
    }

    private void ShowDialogue()
    {
        dialogueText.text = "";
        if (currentDialogueIndex < doctorImages.Length)
        {
            doctorProfileImage.sprite = doctorImages[currentDialogueIndex];
        }

        // เล่นเสียงพากย์
        if (currentDialogueIndex < dialogueAudioClips.Length && dialogueAudioClips[currentDialogueIndex] != null)
        {
            audioSource.Stop();
            audioSource.PlayOneShot(dialogueAudioClips[currentDialogueIndex]);
        }

        StartCoroutine(TypeDialogue());
    }

    private IEnumerator TypeDialogue()
    {
        isTyping = true;
        isDialogueComplete = false;

        foreach (char letter in dialogues[currentDialogueIndex].ToCharArray())
        {
            dialogueText.text += letter;
            yield return new WaitForSeconds(typingSpeed);
        }

        isTyping = false;
        isDialogueComplete = true;
    }
}
