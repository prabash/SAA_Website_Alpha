using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI.HtmlControls;

namespace TEST_ASP_ALPHA_1.Common
{
    public class CommonHtmlManager
    {
        public static HtmlGenericControl GetItemsPerPageList(List<int> valuesList, int currentValue)
        {
            return GetCommonList<int>(valuesList, currentValue, "count");
        }

        public static HtmlGenericControl GetSortValuesList(List<string> valuesList, string currentValue)
        {
            return GetCommonList<string>(valuesList, currentValue, "sortby");
        }

        private static HtmlGenericControl GetCommonList<T>(List<T> valuesList, T currentValue, string queryStringVal)
        {
            HtmlGenericControl itemSelectedValueControl = HTMLControlsManager.GetCustomTag("li", null);
            {
                var ancherCurrentValue = HTMLControlsManager.GetAnchorTag(queryStringVal + "=" + currentValue, null, currentValue.ToString());
                itemSelectedValueControl.Controls.Add(ancherCurrentValue);
                {
                    var spanRightArrow = HTMLControlsManager.GetCustomTag("span", new[] { "right-arrow" });
                    ancherCurrentValue.Controls.Add(spanRightArrow);
                }

                var otherValuesUnorderedList = HTMLControlsManager.GetCustomTag("ul", null);
                itemSelectedValueControl.Controls.Add(otherValuesUnorderedList);
                {
                    foreach (var item in valuesList.Except(new List<T> { currentValue }).OrderBy(rec => rec))
                    {
                        var otherValueListItemControl = HTMLControlsManager.GetCustomTag("li", null);
                        otherValuesUnorderedList.Controls.Add(otherValueListItemControl);
                        {
                            var addAtt = new Dictionary<string, string>() { { "onclick", "insertParam('" + queryStringVal + "', '" + item + "');" } };

                            var anchorOtherValueControl = HTMLControlsManager.GetAnchorTag("#", null, item.ToString(), null, null, addAtt);
                            otherValueListItemControl.Controls.Add(anchorOtherValueControl);
                        }
                    }
                }
            }
            return itemSelectedValueControl;
        }
    }
}