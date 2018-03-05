using System;
using System.Collections;
using System.Collections.Generic;
using Assets.Scripts;
using Newtonsoft.Json;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LoginScript : MonoBehaviour
{
    public InputField EmailField;
    public InputField PasswordField;
    public InputField LoginRegisterField;
    public InputField EmailRegisterField;
    public InputField PasswordRegisterField;
    public GameObject PopupPanel;


    private void Start()
    {
        PopupPanel.gameObject.SetActive(false);
    }

    // Update is called once per frame
    public void OnLogin()
    {
        WWWForm form = new WWWForm();
        form.AddField("email", EmailField.text);
        form.AddField("password", PasswordField.text);
        StartCoroutine(Login(form, "http://localhost:8000/login"));
    }
    public void OnCreateAccount()
    {
        WWWForm form = new WWWForm();
        form.AddField("login", LoginRegisterField.text);
        form.AddField("email", EmailRegisterField.text);
        form.AddField("password", PasswordRegisterField.text);
        StartCoroutine(Register(form, "http://localhost:8000/register"));
    }

    private IEnumerator Login(WWWForm form, string address)
    {
        PopupPanel.gameObject.SetActive(true);

        PopupPanel.GetComponentInChildren<Text>().text = "Connecting to the server...";

        UnityWebRequest www = UnityWebRequest.Post(address, form);
        yield return www.SendWebRequest();

        if (www.isNetworkError || www.isHttpError)
        {
            Debug.Log(www.error);

            PopupPanel.GetComponentInChildren<Text>().text = "Failed to connect";

        }
        else
        {
            var jsonReceived = System.Text.Encoding.UTF8.GetString(www.downloadHandler.data);
            LoginAnswer received;
            try
            {
                received = JsonConvert.DeserializeObject<LoginAnswer>(jsonReceived);
            }
            catch (Exception e)
            {
                Debug.Log(e);
                PopupPanel.GetComponentInChildren<Text>().text = "Failed to receive";
                throw;

            }
            if (received.Ok)
            {
                PopupPanel.GetComponentInChildren<Text>().text = "Done";
                DataHolder.User = new User { Key = received.Data };
                SceneManager.LoadScene(1);
            }
            else
            {
                PopupPanel.GetComponentInChildren<Text>().text = received.Data;
            }
        }
    }
    private IEnumerator Register(WWWForm form, string address)
    {
        PopupPanel.gameObject.SetActive(true);

        PopupPanel.GetComponentInChildren<Text>().text = "Creating account...";

        UnityWebRequest www = UnityWebRequest.Post(address, form);
        yield return www.SendWebRequest();

        if (www.isNetworkError || www.isHttpError)
        {
            Debug.Log(www.error);

            PopupPanel.GetComponentInChildren<Text>().text = "Failed to connect";

        }
        else
        {
            var jsonReceived = System.Text.Encoding.UTF8.GetString(www.downloadHandler.data);
            LoginAnswer received;
            try
            {
                received = JsonConvert.DeserializeObject<LoginAnswer>(jsonReceived);
            }
            catch (Exception e)
            {
                Debug.Log(e);
                PopupPanel.GetComponentInChildren<Text>().text = "Failed to receive";
                throw;

            }
            if (received.Ok)
            {
                PopupPanel.GetComponentInChildren<Text>().text = "Account created!";
            }
            else
            {
                PopupPanel.GetComponentInChildren<Text>().text = received.Data;
            }
        }
    }
}