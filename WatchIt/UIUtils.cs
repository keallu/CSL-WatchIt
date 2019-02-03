using ColossalFramework.UI;
using UnityEngine;

namespace WatchIt
{
    public class UIUtils
    {
        public static UIPanel CreatePanel(string name)
        {
            UIPanel panel = UIView.GetAView().AddUIComponent(typeof(UIPanel)) as UIPanel;
            panel.name = name + "Panel";

            return panel;
        }

        public static UIPanel CreatePanel(UIComponent parent, string name)
        {
            UIPanel panel = parent.AddUIComponent<UIPanel>();
            panel.name = name + "Panel";

            return panel;
        }

        public static UILabel CreateLabel(UIComponent parent, string name, string text)
        {
            UILabel label = parent.AddUIComponent<UILabel>();
            label.name = name + "Label";
            label.text = text;

            return label;
        }

        public static UILabel CreateMenuPanelTitle(UIComponent parent, string title)
        {
            UILabel label = parent.AddUIComponent<UILabel>();
            label.name = "Title";
            label.text = title;
            label.textAlignment = UIHorizontalAlignment.Center;
            label.relativePosition = new Vector3(parent.width / 2f - label.width / 2f, 11f);

            return label;
        }

        public static UIButton CreateMenuPanelCloseButton(UIComponent parent)
        {
            UIButton button = parent.AddUIComponent<UIButton>();
            button.name = "CloseButton";
            button.relativePosition = new Vector3(parent.width - 37f, 2f);

            button.normalBgSprite = "buttonclose";
            button.hoveredBgSprite = "buttonclosehover";
            button.pressedBgSprite = "buttonclosepressed";

            button.eventClick += (component, eventParam) =>
            {
                parent.Hide();
            };

            return button;
        }

        public static UIDragHandle CreateMenuPanelDragHandle(UIComponent parent)
        {
            UIDragHandle dragHandle = parent.AddUIComponent<UIDragHandle>();
            dragHandle.name = "DragHandle";
            dragHandle.width = parent.width - 40f;
            dragHandle.height = 40f;
            dragHandle.relativePosition = Vector3.zero;
            dragHandle.target = parent;

            return dragHandle;
        }

        public static UISprite CreateWatchSprite(UIComponent parent, string name, int index, UITextureAtlas atlas)
        {
            UISprite sprite = parent.AddUIComponent<UISprite>();
            sprite.name = name + "Sprite";
            sprite.atlas = atlas;
            sprite.AlignTo(parent, UIAlignAnchor.TopRight);
            sprite.size = new Vector2(34f, 34f);
            sprite.relativePosition = new Vector3(-0.5f, (34f * index) - 0.5f);
            sprite.isInteractive = false;

            return sprite;
        }

        public static UIButton CreateWatchButton(UIComponent parent, string name, int index, UITextureAtlas atlas, string spriteName, string toolTip)
        {
            UIButton button = parent.AddUIComponent<UIButton>();
            button.name = name + "Button";
            button.atlas = atlas;
            button.tooltip = toolTip;
            button.AlignTo(parent, UIAlignAnchor.TopRight);
            button.size = new Vector2(33f, 33f);
            button.relativePosition = new Vector3(0f, 34f * index);

            button.normalBgSprite = "InfoIconBaseNormal";
            button.hoveredBgSprite = "InfoIconBaseHovered";
            button.pressedBgSprite = "InfoIconBasePressed";
            button.disabledBgSprite = "InfoIconBaseDisabled";

            button.foregroundSpriteMode = UIForegroundSpriteMode.Stretch;
            button.normalFgSprite = spriteName;
            button.hoveredFgSprite = spriteName;
            button.pressedFgSprite = spriteName;
            button.disabledFgSprite = spriteName;

            return button;
        }
    }
}
