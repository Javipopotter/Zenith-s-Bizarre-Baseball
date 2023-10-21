using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class DialoguesManager : MonoBehaviour
{
    public static DialoguesManager dialoguesManager;
    [SerializeField]GameObject dialogueUI;
    public Animator dialogueUiAn;
    public TextMeshProUGUI Name;
    public TextMeshProUGUI text;
    public TextMeshProUGUI choice1;
    public TextMeshProUGUI choice2;
    public GameObject[] img;
    public GameObject UIpointer;
    public string[] choices;
    public bool cinematic;
    [SerializeField] GameObject storyTeller;
    public bool skip;

    private void Awake() {
        dialoguesManager = this;
        dialogueUiAn = dialogueUI.GetComponent<Animator>();
    }

    public void ExecuteDialog(string dialog)
    {
        StartCoroutine(_ExecuteDialog(dialog));
    }

    public void ExecuteDialogViaKey(string key)
    {
        StartCoroutine(_ExecuteDialog(Dialogues.dialogues.texts[key]));
    }

    IEnumerator _ExecuteDialog(string key)
    {
        if(key == ""){yield break;}
        dialogueUI.SetActive(true);
        text.text = "";
        foreach(string line in key.Split("*"))
        {
            cinematic = true;
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
                img[int.Parse(line.Split(")")[0])].transform.GetComponentInChildren<Image>().sprite = Resources.Load("Sprites/" + line.Split("[Img]")[1]) as Sprite;
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


            if(line.Contains("[Wait]") && !skip)
            {
                yield return new WaitForSecondsRealtime(float.Parse(line.Split("[Wait]")[1]));
                continue;
            }

            text.text = "";

            foreach(char character in line)
            {
                if(character == "<"[0])
                {
                    if(line.Contains("<until>"))
                    {
                        while(!Input.GetKeyDown(line.Split("<until>")[1])) yield return null;
                        break;
                    }
                    if(line.Contains("<Wait>"))
                    {
                        yield return new WaitForSeconds(float.Parse(line.Split("<Wait>")[1]));
                        break;
                    }
                }

                text.text = text.text + character;
                if(!skip){
                    if(Input.GetKey(KeyCode.Space)){yield return new WaitForSeconds(0.015f);}
                    else if(Input.GetMouseButton(0)){}
                    else{yield return new WaitForSeconds(0.05f);}
                }
                else
                {
                    yield return new WaitForSeconds(0.0015f);
                }
            }
            UIpointer.SetActive(true);
            while(!(Input.GetKeyDown(KeyCode.Space) || skip)) yield return null;
        }
        skip = false;
    }

    public void ExecuteChoice(int num)
    {
        choice1.transform.parent.gameObject.SetActive(false);
        choice2.transform.parent.gameObject.SetActive(false);
        ExecuteDialog(choices[num].Split(">>")[1]);
    }
}
