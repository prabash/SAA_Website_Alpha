using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;

namespace TEST_ASP_ALPHA_1.Common
{
    public class HTMLControlsManager
    {
        public static HtmlGenericControl GetDivTag(string[] classNames, string innerText = null, string style = null, Dictionary<string, string> additionAtt = null)
        {
            HtmlGenericControl itemDivControl = (HtmlGenericControl)GetHTMLControl(HTMLControls.div, null, classNames, innerText, style, null, null, null, null, null, null, additionAtt);
            return itemDivControl;
        }

        public static HtmlAnchor GetAnchorTag(string href, string[] classNames = null, string innerText = null, string title = null, string style = null, Dictionary<string, string> additionAtt = null)
        {
            HtmlAnchor anchorControl = (HtmlAnchor)GetHTMLControl(HTMLControls.anchor, null, classNames, innerText, style, href, null, null, null, title, null, additionAtt);
            return anchorControl;
        }

        public static HtmlImage GetImageTag(string src, string[] classNames = null, string alt = null, string style = null, Dictionary<string, string> additionAtt = null)
        {
            HtmlImage imageControl = (HtmlImage)GetHTMLControl(HTMLControls.image, null, classNames, null, style, null, src, alt, null, null, null, additionAtt);
            return imageControl;
        }

        public static HtmlGenericControl GetCustomTag(string tagName, string[] classNames, string innerText = null, string style = null, string id = null, string title = null, string type = null, Dictionary<string, string> additionAtt = null)
        {
            HtmlGenericControl itemDivControl = (HtmlGenericControl)GetHTMLControl(HTMLControls.generic, tagName, classNames, innerText, style, null, null, null, id, title, type, additionAtt);
            return itemDivControl;
        }

        private static HtmlControl GetHTMLControl(HTMLControls requiredControl, 
                                                    string tagName = null, 
                                                    string[] classNames = null, 
                                                    string innerText = null, 
                                                    string style = null, 
                                                    string href = null, 
                                                    string src = null,
                                                    string alt = null,
                                                    string id = null,
                                                    string title = null,
                                                    string type = null,
                                                    Dictionary<string,string> additionAtt = null)
    {
            dynamic control = null;
            switch (requiredControl)
            {
                case HTMLControls.div:
                    {
                        control = new HtmlGenericControl();
                        control.TagName = "div";
                        break;
                    }
                case HTMLControls.anchor:
                    {
                        control = new HtmlAnchor();
                        control.HRef = href;
                        break;
                    }
                case HTMLControls.image:
                    {
                        control = new HtmlImage();
                        control.Src = src;

                        if (!String.IsNullOrEmpty(alt))
                            control.Alt = alt;

                        break;
                    }
                case HTMLControls.generic:
                    {
                        control = new HtmlGenericControl();
                        control.TagName = tagName;
                        break;
                    }
                default:
                    break;
            }

            if (classNames != null && classNames.Count() > 0)
                control.Attributes["class"] = getClassInfo(classNames);

            if (!String.IsNullOrEmpty(innerText))
                control.InnerText = innerText;

            if (!String.IsNullOrEmpty(style))
                control.Attributes["style"] = style;

            if (!String.IsNullOrEmpty(id))
                control.Attributes["id"] = id;

            if (!String.IsNullOrEmpty(title))
                control.Attributes["title"] = title;

            if (!String.IsNullOrEmpty(type))
                control.Attributes["type"] = type;

            if (additionAtt != null)
            {
                if (additionAtt.Count() > 0)
                {
                    foreach (var item in additionAtt)
                    {
                        control.Attributes[item.Key] = item.Value;
                    }
                }
            }

            return control;
        }

        private static string getClassInfo(string[] classNames)
        {
            if (classNames != null && classNames.Count() > 0)
            {
                StringBuilder classAtt = new StringBuilder();
                foreach (var className in classNames)
                    classAtt.Append(className + " ");

                return classAtt.ToString().Trim();
            }
            else
                return "";
        }

    }

    enum HTMLControls
    {
        div,
        anchor,
        image,
        generic
    }
}