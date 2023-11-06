using System.Collections;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections.Generic;
using System.Linq;

public class DialoguesManager : MonoBehaviour
{
    public static DialoguesManager dialoguesManager;
    public GameObject dialogueUI;
    public Animator dialogueUiAn;
    public TextMeshProUGUI Name;
    public TextMeshProUGUI text;
    public TextMeshProUGUI choice1;
    public TextMeshProUGUI choice2;
    public GameObject[] img;
    [SerializeField] string[] img_sources_keys;
    [SerializeField] Sprite[] img_sources;
    Dictionary<string, Sprite> img_sources_dict = new Dictionary<string, Sprite>();
    public GameObject UIpointer;
    public string[] choices;
    public bool cinematic;
    [SerializeField] GameObject storyTeller;
    public bool skipDialog;
    private Coroutine executeDialogCoroutine;
    private void Awake() {
        dialoguesManager = this;
        dialogueUiAn = dialogueUI.GetComponent<Animator>();
        for(int i = 0; i < img_sources_keys.Length; i++)
        {
            img_sources_dict.Add(img_sources_keys[i], img_sources[i]);
        }
    }

    public void ExecuteDialog(string dialog)
    {
        if(executeDialogCoroutine != null) StopCoroutine(executeDialogCoroutine);
        executeDialogCoroutine = StartCoroutine(_ExecuteDialog(dialog));
    }

    public void ExecuteDialogViaKey(string key)
    {
        if(executeDialogCoroutine != null) StopCoroutine(executeDialogCoroutine);
        executeDialogCoroutine = StartCoroutine(_ExecuteDialog(Dialogues.dialogues.texts[key]));
    }

    IEnumerator _ExecuteDialog(string key)
    {
        text.text = "";
        if(key == "")
        {
            dialogueUI.SetActive(false);
            yield break;
        }
        dialogueUI.SetActive(true);
        bool next = false;
        bool skipText = false;
        foreach(string line in key.Split("*"))
        {
            UIpointer.SetActive(false);

            if(line.Contains("-choice-"))
            {
                choices = key.Split("-choice-");
                choice1.transform.parent.gameObject.SetActive(true);
                choice2.transform.parent.gameObject.SetActive(true);
                choice1.text = choices[1].Split(">>")[0];
                choice2.text = choices[2].Split(">>")[0];
                break;
            }

            if(line.Contains("[TXT]"))
            {
                ExecuteDialog(Dialogues.dialogues.texts[line.Split("[TXT]")[1]]);
                break;
            }

            if(line.Contains("<Event>"))
            {
                // while(!Input.GetKeyDown(KeyCode.Space)) yield return null;
                SceneManager.LoadScene(line.Split("<Event>")[1], LoadSceneMode.Single);
                break;
            }

            if(line.Contains("[NOCINE]"))
            {
                cinematic = false;
                continue;
            }

            if(line.Contains("[END]"))
            {
                GameManager.GM.GameElementsAreActive(true);
                cinematic = false;
                dialogueUI.SetActive(false);
                break;
            }

            if(line.Contains("[An]"))
            {
                dialogueUiAn.Play(line.Split("[An]")[1]);
                continue;
            }

            if(line.Contains("[trAn]"))
            {
                dialogueUiAn.SetTrigger(line.Split("[trAn]")[1]);
                continue;
            }

            if(line.Contains("[AN]"))
            {
                GameObject.Find(line.Split("[AN]")[1].Split(",")[0]).GetComponent<Animator>().Play(line.Split("[AN]")[1].Split(",")[1]);
                continue;
            }

            if(line.Contains("[IMG]"))
            {
                if(line.Split("[IMG]")[1] == "none")
                {
                    storyTeller.SetActive(false);
                    continue;
                }

                storyTeller.SetActive(true);
                dialogueUI.GetComponent<StoryImgController>().num = int.Parse(line.Split("[IMG]")[1]);
                dialogueUiAn.Play("ImgChange", 0, 0);
                continue;
            }

            if(line.Contains("[GM]"))
            {
                GameManager.GM.Invoke(line.Split("[GM]")[1], 0);
                continue;
            }

            if(line.Contains("[UIAn]"))
            {
                dialogueUiAn.Play(line.Split("[UIAn]")[1]);
                continue;
            }

            if(line.Contains("[NAME]"))
            {
                Name.text = line.Split("[NAME]")[1];
                continue;
            }

            if(line.Contains("[Img]"))
            {
                string sprite = line.Split("[Img]")[1];
                Transform img_transform = img[int.Parse(line.Split(")")[0])].transform;
                if(sprite != "")
                {
                    img_transform.gameObject.SetActive(false);
                    img_transform.GetComponentInChildren<Image>().sprite = img_sources_dict[sprite];
                }
                else
                {
                    img_transform.gameObject.SetActive(false);
                }
            }

            if(line.Contains("[NOTGAME]"))
            {
                GameManager.GM.GameElementsAreActive(false);
                continue;
            }

            if(line.Contains("[HIDE]"))
            {
                Name.text = " ";
                text.transform.parent.gameObject.SetActive(false);
                continue;
            }

            if(line.Contains("[UNHIDE]"))
            {
                text.transform.parent.gameObject.SetActive(true);
                continue;
            }


            if(line.Contains("[Wait]"))
            {
                if(!skipDialog)
                    yield return new WaitForSecondsRealtime(float.Parse(line.Split("[Wait]")[1]));
                else
                    yield return new WaitForSecondsRealtime(float.Parse(line.Split("[Wait]")[1]) / 10);

                continue;
            }

            text.text = "";

            foreach(char character in line)
            {
                if(character == "<"[0])
                {
                    next = true;
                    if(line.Contains("<until>"))
                    {
                        while(!Input.GetButtonDown(line.Split("<until>")[1])) yield return null;
                        break;
                    }
                    if(line.Contains("<wait>"))
                    {
                        yield return new WaitForSecondsRealtime(float.Parse(line.Split("<wait>")[1]));
                        break;
                    }
                }

                text.text = text.text + character;
                if(!skipDialog)
                {
                    if(!skipText)
                    {
                        if(Input.GetMouseButtonDown(0) || Input.GetKeyDown(KeyCode.Space)){skipText = true;}
                        else{yield return new WaitForSecondsRealtime(0.05f);}
                    }
                    else
                    {
                        yield return new WaitForSecondsRealtime(0.000015f);
                    }
                }
            }
            UIpointer.SetActive(true);
            while(!(Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(0) || skipDialog || next)) {yield return null;}
            skipText = false;
            next = false;
        }
        skipDialog = false;
    }

    public void ExecuteChoice(int num)
    {
        choice1.transform.parent.gameObject.SetActive(false);
        choice2.transform.parent.gameObject.SetActive(false);
        ExecuteDialog(choices[num].Split(">>")[1]);
    }
}
