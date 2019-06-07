using ColossalFramework.UI;
using UnityEngine;

namespace WatchIt
{
    public class UIUtils
    {
        public static UIPanel CreatePanel(string name)
        {
            UIPanel panel = UIView.GetAView().AddUIComponent(typeof(UIPanel)) as UIPanel;
            panel.name = name;

            return panel;
        }

        public static UIPanel CreatePanel(UIComponent parent, string name)
        {
            UIPanel panel = parent.AddUIComponent<UIPanel>();
            panel.name = name;

            return panel;
        }

        public static UILabel CreateLabel(UIComponent parent, string name, string text)
        {
            UILabel label = parent.AddUIComponent<UILabel>();
            label.name = name;
            label.text = text;

            return label;
        }

        public static UISprite CreateSprite(UIComponent parent, string name, string spriteName)
        {
            UISprite sprite = parent.AddUIComponent<UISprite>();
            sprite.name = name;
            sprite.spriteName = spriteName;

            return sprite;
        }

        public static UISprite CreateSprite(UIComponent parent, string name, UITextureAtlas atlas, string spriteName)
        {
            UISprite sprite = parent.AddUIComponent<UISprite>();
            sprite.name = name;
            sprite.atlas = atlas;
            sprite.spriteName = spriteName;

            return sprite;
        }

        public static UIButton CreateLargeButton(string name)
        {
            UIButton button = UIView.GetAView().AddUIComponent(typeof(UIButton)) as UIButton;
            button.name = name;

            button.normalBgSprite = "RoundBackBig";
            button.focusedBgSprite = "RoundBackBigFocused";
            button.hoveredBgSprite = "RoundBackBigHovered";
            button.pressedBgSprite = "RoundBackBigPressed";
            button.disabledBgSprite = "RoundBackBigDisabled";

            return button;
        }

        public static UIButton CreateLargeButton(UIComponent parent, string name)
        {
            UIButton button = parent.AddUIComponent<UIButton>();
            button.name = name;

            button.normalBgSprite = "RoundBackBig";
            button.focusedBgSprite = "RoundBackBigFocused";
            button.hoveredBgSprite = "RoundBackBigHovered";
            button.pressedBgSprite = "RoundBackBigPressed";
            button.disabledBgSprite = "RoundBackBigDisabled";

            return button;
        }

        public static UIButton CreateButton(UIComponent parent, string name, UITextureAtlas atlas, string spriteName)
        {
            UIButton button = parent.AddUIComponent<UIButton>();
            button.name = name;
            button.atlas = atlas;

            button.normalBgSprite = spriteName + "Normal";
            button.focusedBgSprite = spriteName + "Focused";
            button.hoveredBgSprite = spriteName + "Hovered";
            button.pressedBgSprite = spriteName + "Pressed";
            button.disabledBgSprite = spriteName + "Disabled";

            return button;
        }

        public static UIDragHandle CreateDragHandle(UIComponent parent)
        {
            UIDragHandle dragHandle = parent.AddUIComponent<UIDragHandle>();
            dragHandle.name = "DragHandle";
            dragHandle.target = parent;

            return dragHandle;
        }

        public static UIDragHandle CreateDragHandle(UIComponent parent, UIComponent target)
        {
            UIDragHandle dragHandle = parent.AddUIComponent<UIDragHandle>();
            dragHandle.name = "DragHandle";
            dragHandle.target = target;

            return dragHandle;
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
    }
}
