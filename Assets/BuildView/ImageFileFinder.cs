//File Name:        ImageFileFinder.cs
//Description:      Provides the buttons on the scene that invoke the file browser. 
//Dependencies:     FilerBrowser.cs, GUILayoutx.cs
//Additional Notes: Currently these use depricated UI functions

using System.IO;
using UnityEngine;

public class ImageFileFinder : MonoBehaviour
{
    protected string m_textPath;
    private bool m_browserEnabled = false;

    protected FileBrowser m_fileBrowser;

    [SerializeField]
    protected Texture2D m_directoryImage,
                        m_fileImage;

    void Awake()
    //Description:  Called bby unity between scene load
    //PRE:          None
    //POST:         The background image uploaded by the user will persist through scenes
    {
        DontDestroyOnLoad(GameObject.Find("UserBackgroundImage"));
    }

    protected void OnGUI()
    //Description:  Depricated Unity UI function
    //PRE:          None
    //POST:         The button for the user to upload will appear on the scene
    {
        if (m_fileBrowser != null)
        {
            m_fileBrowser.OnGUI();
        }
        else
        {
            OnGUIMain();
        }
    }

    protected void OnGUIMain()
    //Description:  Called to invoke the file browser object only once
    //PRE:          None
    //POST:         the file browser object will be created
    {
        GUILayout.BeginHorizontal();
        GUILayout.Label("Background", GUILayout.Width(100));
        GUILayout.FlexibleSpace();
        GUILayout.Label(m_textPath ?? "none selected");
        if (GUILayout.Button("...", GUILayout.ExpandWidth(false)))
        {
            this.m_browserEnabled = true;
            m_fileBrowser = new FileBrowser(
                new Rect(100, 100, 600, 500),
                "Choose Image File",
                FileSelectedCallback
            );

            m_fileBrowser.DirectoryImage = m_directoryImage;
            m_fileBrowser.FileImage = m_fileImage;
        }
        GUILayout.EndHorizontal();
    }

    protected void FileSelectedCallback(string path)
    //Description:  The method called when a file is chosen in the file browser 
    //PRE:          This method will only be called by the filebrowser when a user selects a file
    //POST:         The file path will be set for the scene and the background image will load
    {
        m_fileBrowser = null;
        m_textPath = path;
        this.m_browserEnabled = false;
        LoadImage();
    }

    public bool getBrowserEnabled()
    //Description:  getter for the browser status
    //PRE:          None
    //POST:         Returns the boolean of the browser status
    {
        return this.m_browserEnabled;
    }

    public string getPath()
    //Description:  Getter for the path of the object which the user chose
    //PRE:          None
    //POST:         Returns the string of the path to the image browser
    {
        return this.m_textPath;
    }

    public void LoadImage()
    //Description:  Loads the image to the background
    //PRE:          The user has selected a file to be uploaded as the background
    //POST:         The background the user selected will apear in the scene behind all other gameobjects
    {
        byte[] fileData;

        SpriteRenderer defaultBg = Camera.main.GetComponentInChildren<SpriteRenderer>();
        SpriteRenderer userBg = GameObject.Find("UserBackgroundImage").GetComponent<SpriteRenderer>();
        this.m_textPath = this.m_textPath.Replace('\\', '/');                                                               //unity only likes forward slashes
        Texture2D newTexture = new Texture2D((int)defaultBg.sprite.rect.height, (int)defaultBg.sprite.rect.width);          

        fileData = File.ReadAllBytes(this.m_textPath);                                                                     //read the bytes of the image in raw
        newTexture.LoadImage(fileData);                                                                                     //load image from byte data

        userBg.sprite = Sprite.Create(newTexture,
                                        new Rect(0, 0, newTexture.width, newTexture.height), new Vector2(0.5f, 0.5f), defaultBg.sprite.pixelsPerUnit);

        //the following code scales the image to the camera size
        transform.localScale = new Vector3(1, 1, 1);

        float width = userBg.sprite.bounds.size.x;
        float height = userBg.sprite.bounds.size.y; 

        float worldScreenHeight = Camera.main.orthographicSize * 2f + 1; 
        float worldScreenWidth = worldScreenHeight / Screen.height * Screen.width + 1; 

        Vector3 imgScale = new Vector3(1f, 1f, 1f);

        imgScale.x = worldScreenWidth / width;
        imgScale.y = worldScreenHeight / height;

        // apply change
        userBg.transform.localScale = imgScale;
    }
}