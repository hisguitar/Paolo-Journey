using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class MiniGameManager : MonoBehaviour
{
    public GameObject miniGame;
    public GameObject correctImage;
    public TMP_Text questionText;
    public TMP_Text responseText;
    public GameObject response;
    public Button[] optionButtons;

    public Sprite[] optionSprites; // อาร์เรย์ของรูปภาพสำหรับตัวเลือกแต่ละคำตอบ
    public GameObject[] wrongText; // อาร์เรย์ สำหรับแสดงข้อความ "ผิด" บนแต่ละปุ่มที่กดผิด

    private List<QuestionStep> steps;
    private int currentStepIndex = 0;
    private bool isResponseVisible = false;

    public GameObject trueImage; // ภาพเครื่องหมายถูก
    public GameObject falseImage;   // ภาพเครื่องหมายผิด
    public float floatDistance = 500f; // ระยะทางที่เครื่องหมายลอยขึ้น
    public float fadeDuration = 1f; // ระยะเวลาที่เครื่องหมายจางหายไป
    public float stayDuration = 0.5f; // ระยะเวลาที่เครื่องหมายแสดงก่อนเริ่มจางหาย
    
    // AudioClips สำหรับเสียงพากย์ของปุ่มแต่ละตัวเลือก
    [SerializeField] private AudioClip[] optionAudioClips; // เสียงพากย์สำหรับตัวเลือก
    private AudioSource audioSource;
    private AudioClip currentPlayingClip = null;
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        InitializeQuestions();
        LoadStep(currentStepIndex);
    }
    
    // แสดงเครื่องหมายถูก
    public void ShowCorrectIndicator()
    {
        StartCoroutine(FloatingEffect(trueImage));
    }
    // แสดงเครื่องหมายผิด
    public void ShowWrongIndicator()
    {
        StartCoroutine(FloatingEffect(falseImage));
    }

    private IEnumerator FloatingEffect(GameObject indicator)
    {
        // เปิดใช้งานเครื่องหมาย
        indicator.SetActive(true);

        // ตั้งค่าเริ่มต้น
        RectTransform rectTransform = indicator.GetComponent<RectTransform>();
        CanvasGroup canvasGroup = indicator.GetComponent<CanvasGroup>();

        if (canvasGroup == null)
        {
            canvasGroup = indicator.AddComponent<CanvasGroup>();
        }

        Vector3 startPosition = rectTransform.anchoredPosition;
        Vector3 targetPosition = startPosition + new Vector3(0, floatDistance, 0);

        // เคลื่อนที่ขึ้น
        float elapsedTime = 0f;
        while (elapsedTime < fadeDuration)
        {
            float t = elapsedTime / fadeDuration;

            // เลื่อนตำแหน่ง
            rectTransform.anchoredPosition = Vector3.Lerp(startPosition, targetPosition, t);

            // รอเฟรมถัดไป
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        // รอช่วงเวลาหนึ่งก่อนเริ่มจางหาย
        yield return new WaitForSeconds(stayDuration);

        // เริ่มจางหาย
        elapsedTime = 0f;
        while (elapsedTime < fadeDuration)
        {
            float t = elapsedTime / fadeDuration;

            // ลดค่า Alpha เพื่อจางหาย
            canvasGroup.alpha = Mathf.Lerp(1f, 0f, t);

            // รอเฟรมถัดไป
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        // ซ่อนเครื่องหมาย
        indicator.SetActive(false);

        // รีเซ็ตค่า
        rectTransform.anchoredPosition = startPosition;
        canvasGroup.alpha = 1f;
    }
    
    
    void InitializeQuestions()
    {
        steps = new List<QuestionStep>
        {
            new QuestionStep("ขั้นตอนที่ 1: ทำความสะอาดแผล", new List<Option>
            {
                new Option("ผ้าก๊อซชุบน้ำยาฆ่าเชื้อ", true, "นี่คือผ้าก๊อซชุบน้ำยาฆ่าเชื้อ!\nเราใช้มันทำความสะอาดแผลให้สะอาด ป้องกันเชื้อโรคที่อาจจะเข้ามาในแผลได้", optionSprites[4], optionAudioClips[0]),
                new Option("ผ้าพันแผล", false, "นี่คือผ้าพันแผล ใช้ตอนปิดแผล\nแต่ตอนนี้เรายังไม่ได้ปิดแผลนะ ต้องทำความสะอาดก่อน!", optionSprites[3], optionAudioClips[1]),
                new Option("ปลาสเตอร์ยา", false, "ปลาสเตอร์ยานี้ไว้ใช้ปิดแผลเล็กๆ\nเมื่อทำความสะอาดเสร็จแล้ว เราค่อยใช้มันปิดแผลนะ!", optionSprites[1], optionAudioClips[2])
            }),
            new QuestionStep("ขั้นตอนที่ 2: ห้ามเลือด", new List<Option>
            {
                new Option("สำลี", false, "สำลีใช้กับการทำความสะอาด\nแต่ไม่เหมาะกับการห้ามเลือด\nเพราะมันซับเลือดได้ไม่ดี!", optionSprites[6], optionAudioClips[3]),
                new Option("ผ้าก๊อซสะอาด", true, "นี่แหละ! ผ้าก๊อซสะอาด ใช้ห้ามเลือดได้ดีเลย\nแค่กดมันลงไปที่แผลก็ช่วยหยุดเลือดได้!", optionSprites[4], optionAudioClips[4]),
                new Option("แอลกอฮอล์", false, "แอลกอฮอล์ใช้ทำความสะอาด\nเครื่องมือได้ แต่ห้ามใช้ห้ามเลือด\nเพราะมันจะแสบและ\nไม่ช่วยหยุดเลือดนะ!", optionSprites[5], optionAudioClips[5])
            }),
            new QuestionStep("ขั้นตอนที่ 3: ทายาใส่แผล", new List<Option>
            {
                new Option("ยาฆ่าเชื้อ", true, "ยาฆ่าเชื้อช่วยป้องกันเชื้อโรคจากแผล\nเราทาลงไปบนแผล\nเพื่อให้แน่ใจว่าแผลจะไม่ติดเชื้อ!", optionSprites[2], optionAudioClips[6]),
                new Option("น้ำเกลือ", false, "น้ำเกลือใช้ล้างแผลได้\nแต่ตอนนี้เราต้องการยาฆ่าเชื้อ\nที่มีพลังในการป้องกันเชื้อโรคมากกว่า!", optionSprites[0], optionAudioClips[7]),
                new Option("ปลาสเตอร์ยา", false, "ปลาสเตอร์ยาไว้ปิดแผลเล็กๆ\nแต่ว่ายังไม่ได้ทายาเลย\nต้องทายาก่อนแล้วค่อยปิดแผลนะ!", optionSprites[1], optionAudioClips[8])
            }),
            new QuestionStep("ขั้นตอนที่ 4: ปิดแผล", new List<Option>
            {
                new Option("ปลาสเตอร์ยา", true, "นี่แหละ! ปลาสเตอร์ยาช่วยปิดแผลเล็กๆ\nให้แผลสะอาดและไม่โดนเชื้อโรค!", optionSprites[1], optionAudioClips[9]),
                new Option("ผ้าก๊อซสะอาด", false, "ผ้าก๊อซสะอาดเหมาะกับแผลใหญ่\nใช้ปิดแผลที่ทำความสะอาด\nแล้วเพื่อป้องกันไม่ให้เชื้อโรคเข้า!", optionSprites[4], optionAudioClips[10]),
                new Option("ผ้าพันแผล", false, "ถ้าแผลใหญ่กว่านั้น\nผ้าพันแผลจะช่วยพันแผล\nทั้งหมดได้\nและช่วยปกป้องแผล!", optionSprites[3 ], optionAudioClips[11])
            })
        };
    }

    void LoadStep(int stepIndex)
    {
        QuestionStep step = steps[stepIndex];
        questionText.text = step.QuestionText;
        responseText.text = "";

        for (int i = 0; i < optionButtons.Length; i++)
        {
            optionButtons[i].GetComponent<Image>().sprite = step.Options[i].OptionSprite;  // ตั้งค่ารูปภาพของปุ่ม
            optionButtons[i].GetComponentInChildren<TMP_Text>().text = step.Options[i].Text;

            
            wrongText[i].gameObject.SetActive(false);

            int index = i; // ทำสำเนา local variable สำหรับ callback
            optionButtons[i].onClick.RemoveAllListeners();
            optionButtons[i].onClick.AddListener(() => CheckAnswer(step.Options[index], index));
        }
    }


/*    public void CheckAnswer(Option option)
    {
        responseText.text = option.Response;
        
        if (option.IsCorrect)
        {
            currentStepIndex++;
            if (currentStepIndex < steps.Count)
            {
                LoadStep(currentStepIndex); // โหลดขั้นตอนถัดไป
            }
            else
            {
                responseText.text = "จบขั้นตอนทั้งหมดแล้ว! เก่งมาก!";
            }
        }
        else
        {
            responseText.text = "คำตอบผิด! ลองใหม่อีกครั้ง!";
        }
    }*/
    public void CheckAnswer(Option option, int optionIndex)
    {
        // แสดงข้อความ Response ของปุ่มที่กดเสมอ
        responseText.text = option.Response;
        // เล่นเสียงพากย์ของตัวเลือกที่เลือก
        audioSource.PlayOneShot(option.voiceClip);
        
        // ตรวจสอบว่าคำตอบถูกหรือผิด
        if (option.IsCorrect)
        {
            SoundManager.Instance.Play(SoundManager.SoundName.True);
            ShowCorrectIndicator();
            currentStepIndex++;
            if (currentStepIndex < steps.Count)
            {
                LoadStep(currentStepIndex); // โหลดขั้นตอนถัดไป
            }
            else
            {
                responseText.text = "จบขั้นตอนทั้งหมดแล้ว! เก่งมาก!";
                correctImage.gameObject.SetActive(true);
                miniGame.gameObject.SetActive(false);
            }
        }
        else
        {
            SoundManager.Instance.Play(SoundManager.SoundName.False);
            ShowWrongIndicator();
            // แสดงข้อความ "ผิด" และทำให้ปุ่มมืดลง
            wrongText[optionIndex].gameObject.SetActive(true);
            
            // แสดง Response และตั้งสถานะให้พร้อมตรวจจับการคลิก
            response.SetActive(true);
            isResponseVisible = true; // เปิดสถานะว่ากำลังแสดง response
            // เล่นเสียงพากย์ที่เกี่ยวข้องกับตัวเลือกที่เลือก
        }
    }
    
    private void Update()
    {
        // ตรวจสอบว่า response กำลังแสดงและมีการคลิกหน้าจอ
        if (isResponseVisible && Input.GetMouseButtonDown(0))
        {
            response.SetActive(false); // ซ่อน response
            isResponseVisible = false; // ปิดสถานะ
            audioSource.Stop();

        }
    }
    
    public class Option
    {
        public string Text;
        public bool IsCorrect;
        public string Response;
        public Sprite OptionSprite;  // ตัวแปรสำหรับเก็บรูปของตัวเลือก
        public AudioClip voiceClip;  // เสียงพากย์สำหรับตัวเลือกนี้


        public Option(string text, bool isCorrect, string response, Sprite optionSprite, AudioClip clip)
        {
            Text = text;
            IsCorrect = isCorrect;
            Response = response;
            OptionSprite = optionSprite;
            voiceClip = clip;

        }
    }

    public class QuestionStep
    {
        public string QuestionText;
        public List<Option> Options;

        public QuestionStep(string questionText, List<Option> options)
        {
            QuestionText = questionText;
            Options = options;
        }
    }

}
