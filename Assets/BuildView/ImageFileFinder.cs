using System.IO;
using UnityEngine;
using UnityEditor;

public class ImageFileFinder : MonoBehaviour
{

    protected string m_textPath;
    private bool m_browserEnabled = false;

    protected FileBrowser m_fileBrowser;

    [SerializeField]
    protected Texture2D m_directoryImage,
                        m_fileImage;

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
            //m_fileBrowser.SelectionPattern = "*.*";
            m_fileBrowser.DirectoryImage = m_directoryImage;
            m_fileBrowser.FileImage = m_fileImage;
        }
        GUILayout.EndHorizontal();
    }

    protected void FileSelectedCallback(string path)
    {
        m_fileBrowser = null;
        m_textPath = path;
        this.m_browserEnabled = false;
        LoadImage();
    }

    public bool getBrowserEnabled()
    {
        return this.m_browserEnabled;
    }

    public string getPath()
    {
        return this.m_textPath;
    }

    public void LoadImage()
    {
        byte[] fileData;

        SpriteRenderer defaultBg = Camera.main.GetComponentInChildren<SpriteRenderer>();   
        SpriteRenderer userBg = GameObject.Find("BackgroundImage").GetComponent<SpriteRenderer>();
        this.m_textPath = this.m_textPath.Replace('\\', '/');                                                               //unity only likes forward slashes
        Texture2D newTexture = new Texture2D((int)defaultBg.sprite.rect.height, (int)defaultBg.sprite.rect.width);  //i want to scale uploaded image to current background size 
                                                                                                                    //  but it isn't working?
        fileData = File.ReadAllBytes(this.m_textPath);
        newTexture.LoadImage(fileData);

        userBg.sprite = Sprite.Create(newTexture,
                                        new Rect(0, 0, newTexture.width, newTexture.height), new Vector2(0.5f, 0.5f), defaultBg.sprite.pixelsPerUnit);
    }
}