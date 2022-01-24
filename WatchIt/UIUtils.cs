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

        public static UIScrollablePanel CreateScrollablePanel(UIComponent parent, string name)
        {
            UIScrollablePanel scrollablePanel = parent.AddUIComponent<UIScrollablePanel>();
            scrollablePanel.name = name;

            return scrollablePanel;
        }

        public static UIScrollbar CreateScrollbar(UIComponent parent, string name)
        {
            UIScrollbar scrollbar = parent.AddUIComponent<UIScrollbar>();
            scrollbar.name = name;

            return scrollbar;
        }
        public static UISlicedSprite CreateSlicedSprite(UIComponent parent, string name)
        {
            UISlicedSprite slicedSprite = parent.AddUIComponent<UISlicedSprite>();
            slicedSprite.name = name;

            return slicedSprite;
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
        public static UIButton CreateButton(UIComponent parent, string name, string spriteName)
        {
            UIButton button = parent.AddUIComponent<UIButton>();
            button.name = name;

            button.normalBgSprite = spriteName + "Normal";
            button.focusedBgSprite = spriteName + "Focused";
            button.hoveredBgSprite = spriteName + "Hovered";
            button.pressedBgSprite = spriteName + "Pressed";
            button.disabledBgSprite = spriteName + "Disabled";

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

            button.eventClicked += (component, eventParam) =>
            {
                if (!eventParam.used)
                {
                    parent.Hide();

                    eventParam.Use();
                }
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

        public static UITabstrip CreateTabStrip(UIComponent parent)
        {
            UITabstrip tabstrip = parent.AddUIComponent<UITabstrip>();
            tabstrip.name = "TabStrip";
            tabstrip.clipChildren = true;

            return tabstrip;
        }

        public static UITabContainer CreateTabContainer(UIComponent parent)
        {
            UITabContainer tabContainer = parent.AddUIComponent<UITabContainer>();
            tabContainer.name = "TabContainer";

            return tabContainer;
        }

        public static UIButton CreateTabButton(UIComponent parent)
        {
            UIButton button = parent.AddUIComponent<UIButton>();
            button.name = "TabButton";

            button.height = 26f;
            button.width = 120f;

            button.textHorizontalAlignment = UIHorizontalAlignment.Center;
            button.textVerticalAlignment = UIVerticalAlignment.Middle;

            button.normalBgSprite = "GenericTab";
            button.disabledBgSprite = "GenericTabDisabled";
            button.focusedBgSprite = "GenericTabFocused";
            button.hoveredBgSprite = "GenericTabHovered";
            button.pressedBgSprite = "GenericTabPressed";

            button.textColor = new Color32(255, 255, 255, 255);
            button.disabledTextColor = new Color32(111, 111, 111, 255);
            button.focusedTextColor = new Color32(16, 16, 16, 255);
            button.hoveredTextColor = new Color32(255, 255, 255, 255);
            button.pressedTextColor = new Color32(255, 255, 255, 255);

            return button;
        }
    }
}
