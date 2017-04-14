using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;


[CustomEditor(typeof(RadialControllerBehaviour))]
public class MenuItemEditor : Editor
{
    // Constants 
    private const int MAX_BUTTON_TEXT = 10;

    // String literals
    private const string PRECISION_HELP_TEXT = "Precision for rotation events in degrees.  Recommendation is between 1 and 10 degrees.";
    private const string PRECISION_LABEL = "Precision";
    private const string HAPTIC_FEEDBACK_LABEL = "Haptic Feedback";
    private const string MENU_ITEM_HELP_TEXT = "Menu items for the RadialController.  Try to keep to a minimum for better user experience.  Minimum is 1.";
    private const string MENU_ITEMS_TITLE = "Menu Items";
    private const string MENU_TITLE_LABEL = "Menu Title";
    private const string MENU_ICON_LABEL = "Menu Icon";
    private const string MENU_ADD_TEXT = "Add";
    private const string MENU_REMOVE_TEXT = "Remove";
    private const string MENU_REMOVE_TEXT_FORMAT = "Remove '{0}'";
    private const string ELLIPSIS = "...";

    // Editor state
    bool showMenuItems = true;
   
    public override void OnInspectorGUI()
    {
        var radialControllerBehaviour = (RadialControllerBehaviour)target;

        // initialise
        
        // Basic parameters - rotation resolution, and haptic feedback
        EditorGUILayout.HelpBox(PRECISION_HELP_TEXT, MessageType.Info);
        radialControllerBehaviour.RotationResolution = EditorGUILayout.Slider(PRECISION_LABEL, radialControllerBehaviour.RotationResolution, 1, 360);
        radialControllerBehaviour.UseAutomaticHapticFeedback = EditorGUILayout.Toggle(HAPTIC_FEEDBACK_LABEL, radialControllerBehaviour.UseAutomaticHapticFeedback);

        // Menu items - titles and icons, and ability to add and remove
        EditorGUILayout.Space();
        showMenuItems = EditorGUILayout.Foldout(showMenuItems, MENU_ITEMS_TITLE);
        if (showMenuItems)
        {
            EditorGUILayout.HelpBox(MENU_ITEM_HELP_TEXT, MessageType.Info);

            for (var i = 0; i < radialControllerBehaviour.MenuItems.Count; i++)
            {
                EditorGUILayout.Space();
                var m = radialControllerBehaviour.MenuItems[i];
                var r = EditorGUILayout.BeginVertical();
                r = Pad(r, 5);

                EditorGUI.DrawRect(r, new Color(0.5f, 0.5f, 0.5f, 0.2f));

                m.Title = EditorGUILayout.TextField(MENU_TITLE_LABEL, m.Title);
                m.Icon = (Texture2D)EditorGUILayout.ObjectField(MENU_ICON_LABEL,
                        m.Icon,
                        typeof(Texture2D), true);
                if (m.Icon != null)
                {
                    if (m.Icon.width != 64 || m.Icon.height != 64)
                    {
                        var templatex = "Image is {0}x{0}.  Icons must be 64x64 pixels in size.";
                        var msg = string.Format(templatex, m.Icon.width, m.Icon.height);
                        EditorGUILayout.HelpBox(msg, MessageType.Error);
                    }
                }
                
                string removeButtonText = GetRemoveButtonText(m.Title);

                if (radialControllerBehaviour.MenuItems.Count > 1)
                {
                    if (GUILayout.Button(removeButtonText))
                    {
                        radialControllerBehaviour.MenuItems.RemoveAt(i);
                        i--;
                    }
                }
                EditorGUILayout.EndVertical();
                
                EditorGUILayout.Space();
            }

            if (GUILayout.Button(MENU_ADD_TEXT))
            {
                radialControllerBehaviour.MenuItems.Add(new MenuItem());
            }
        }
    }

    /// <summary>
    /// Simple padding for the rect
    /// </summary>
    /// <param name="r">Rect to pad</param>
    /// <param name="pad">Amount to pad by</param>
    /// <returns></returns>
    private static Rect Pad(Rect r, int pad)
    {
        r.x -= pad;
        r.width += pad*2;
        r.y -= pad;
        r.height += pad*2;
        return r;
    }

    /// <summary>
    /// Remove buttons have the title of the menu item on them 
    /// as users were removing the wrong ones.  This method generates
    /// the text.
    /// </summary>
    /// <param name="menuTitle">Menu Title</param>
    /// <returns></returns>
    private static string GetRemoveButtonText(String menuTitle)
    {
        // If it's empty, we just show 'remove'
        string removeButtonText = MENU_REMOVE_TEXT;

        // Otherwise we show the truncated labal text 
        // to make it obvious for users 
        if (!string.IsNullOrEmpty(menuTitle))
        {
            if (menuTitle.Length > MAX_BUTTON_TEXT)
            {
                menuTitle = menuTitle.Substring(0, MAX_BUTTON_TEXT) + ELLIPSIS;
            }
            removeButtonText = string.Format(MENU_REMOVE_TEXT_FORMAT, (object)menuTitle);
        }

        return removeButtonText;
    }
}
