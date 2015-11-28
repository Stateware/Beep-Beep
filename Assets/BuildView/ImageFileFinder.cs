// File Name:        ImageFileFinder.cs
// Description:      Provides the buttons on the scene that invoke the file browser. 
// Dependencies:     FilerBrowser.cs, GUILayoutx.cs
// Additional Notes: Currently these use depricated UI functions

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

    // Description:  Called bby unity between scene load
    // PRE:          N/A
    // POST:         The background image uploaded by the user will persist through scenes
    void Awake()
    {
        DontDestroyOnLoad(GameObject.Find("UserBackgroundImage"));
    }

    // Description:  Depricated Unity UI function
    // PRE:          N/A
    // POST:         The button for the user to upload will appear on the scene
    protected void OnGUI()
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

    // Description:  Called to invoke the file browser object only once
    // PRE:          N/A
    // POST:         The file browser object will be created
    protected void OnGUIMain()
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

    // Description:  The method called when a file is chosen in the file browser 
    // PRE:          This method will only be called by the filebrowser when a user selects a file
    // POST:         The file path will be set for the scene and the background image will load
    protected void FileSelectedCallback(string path)
    {
        m_fileBrowser = null;
        m_textPath = path;
        this.m_browserEnabled = false;
        LoadImage();
    }

    //Description:  Getter for the browser status
    //PRE:          N/A
    //POST:         Returns the boolean of the browser status
    public bool GetBrowserEnabled()
    {
        return this.m_browserEnabled;
    }

    // Description:  Getter for the path of the object which the user chose
    // PRE:          N/A
    // POST:         Returns the string of the path to the image browser
    public string GetPath()
    {
        return this.m_textPath;
    }

    // Description:  Loads the image to the background
    // PRE:          The user has selected a file to be uploaded as the background
    // POST:         The background the user selected will apear in the scene behind all other gameobjects
    public void LoadImage()
    {
        byte[] fileData;

        SpriteRenderer defaultBg = Camera.main.GetComponentInChildren<SpriteRenderer>();
        SpriteRenderer userBg = GameObject.Find("UserBackgroundImage").GetComponent<SpriteRenderer>();
        this.m_textPath = this.m_textPath.Replace('\\', '/');                                                               // Unity only likes forward slashes
        Texture2D newTexture = new Texture2D((int)defaultBg.sprite.rect.height, (int)defaultBg.sprite.rect.width);          

        fileData = File.ReadAllBytes(this.m_textPath);                                                                     // Read the bytes of the image in raw
        newTexture.LoadImage(fileData);                                                                                     // Load image from byte data

        userBg.sprite = Sprite.Create(newTexture,
                                        new Rect(0, 0, newTexture.width, newTexture.height), new Vector2(0.5f, 0.5f), defaultBg.sprite.pixelsPerUnit);

        // The following code scales the image to the camera size
        transform.localScale = new Vector3(1, 1, 1);

        float width = userBg.sprite.bounds.size.x;
        float height = userBg.sprite.bounds.size.y; 

        float worldScreenHeight = Camera.main.orthographicSize * 2f + 1; 
        float worldScreenWidth = worldScreenHeight / Screen.height * Screen.width + 1; 

        Vector3 imgScale = new Vector3(1f, 1f, 1f);

        imgScale.x = worldScreenWidth / width;
        imgScale.y = worldScreenHeight / height;

        // Apply change
        userBg.transform.localScale = imgScale;
    }
}