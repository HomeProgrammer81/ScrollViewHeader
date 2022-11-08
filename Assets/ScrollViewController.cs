using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

namespace Assets
{
    // スクロールビューコントローラー
    class ScrollViewController : MonoBehaviour
    {
        private GameObject viewport;
        private GameObject content;
        private GameObject panel;
        private GameObject header;
        private GameObject columnHeader;
        private GameObject rowHeader;
        private GameObject cell;

        // 開始
        private void Start()
        {
            viewport = transform.Find("Viewport").gameObject;
            content = viewport.transform.Find("Content").gameObject;
            panel = content.transform.Find("Panel").gameObject;
            
            header = panel.transform.Find("Panel_Header").gameObject;
            columnHeader = panel.transform.Find("Panel_ColumnHeader").gameObject;
            rowHeader = panel.transform.Find("Panel_RowHeader").gameObject;

            cell = panel.transform.Find("Panel_Cell").gameObject;

            float cellWidth = header.GetComponent<RectTransform>().sizeDelta.x;
            float cellHeight = header.GetComponent<RectTransform>().sizeDelta.y;

            // 列ヘッダー数10
            for ( int i = 0; i<10; i++)
            {
                GameObject columnHeaderChild = GameObject.Instantiate(columnHeader.transform.Find("Panel_ColumnHeader_Org")).gameObject;
                columnHeaderChild.transform.parent = columnHeader.transform;
                columnHeaderChild.transform.localPosition = new Vector2(i * cellWidth, 0);

                Text text = columnHeaderChild.transform.Find("Text").GetComponent<Text>();
                text.text = "Column" + (i + 1);

                columnHeaderChild.name = "Column" + (i + 1);
                columnHeaderChild.SetActive(true);
            }

            // 行ヘッダー数15
            for (int j = 0; j < 15; j++)
            {
                GameObject rowHeaderChild = GameObject.Instantiate(rowHeader.transform.Find("Panel_RowHeader_Org")).gameObject;
                rowHeaderChild.transform.parent = rowHeader.transform;
                rowHeaderChild.transform.localPosition = new Vector2(0, -j * cellHeight);

                Text text = rowHeaderChild.transform.Find("Text").GetComponent<Text>();
                text.text = "Row" + (j + 1);

                rowHeaderChild.name = "Row" + (j + 1);
                rowHeaderChild.SetActive(true);
            }

            // セル 10×15
            for (int i = 0; i < 10; i++)
            {
                for (int j = 0; j < 15; j++)
                {
                    GameObject cellChild = GameObject.Instantiate(cell.transform.Find("Panel_Cell_Org")).gameObject;
                    cellChild.transform.parent = cell.transform;
                    cellChild.transform.localPosition = new Vector2(i * cellWidth, -j * cellHeight);

                    Text text = cellChild.transform.Find("Text").GetComponent<Text>();
                    text.text = "Cell" + (i + 1) + (j + 1);

                    cellChild.name = "Cell" + (i + 1) + (j + 1);
                    cellChild.SetActive(true);
                }
            }

            // 
            content.GetComponent<RectTransform>().sizeDelta = new Vector2(cellWidth * (1 + 10), cellHeight * (1 + 15));
        }

        // 水平スクロールバー移動
        public void ScrollView_Horizontal(float value)
        {
            float yContent = content.GetComponent<RectTransform>().anchoredPosition.y;

            float contentWidth = content.GetComponent<RectTransform>().rect.width;
            float viewportWidth = viewport.GetComponent<RectTransform>().rect.width;
            
            float diff = (contentWidth - viewportWidth) * value;
            if (diff < 0)
            {
                diff = 0;
            }

            float cellHeight = header.GetComponent<RectTransform>().sizeDelta.y;
            rowHeader.GetComponent<RectTransform>().anchoredPosition = new Vector2(diff, -cellHeight);

            header.GetComponent<RectTransform>().anchoredPosition = new Vector2(diff, -yContent);

        }

        // 垂直スクロールバー移動
        public void ScrollView_Vertical(float value)
        {
            float xContent = content.GetComponent<RectTransform>().anchoredPosition.x;

            float contactHeight = content.GetComponent<RectTransform>().rect.height;
            float viewportHeight = viewport.GetComponent<RectTransform>().rect.height;
            
            float diff = (contactHeight - viewportHeight) * (1-value);
            if (diff < 0)
            {
                diff = 0;
            }

            float cellWidth = header.GetComponent<RectTransform>().sizeDelta.x;
            columnHeader.GetComponent<RectTransform>().anchoredPosition = new Vector2(cellWidth, -diff);

            header.GetComponent<RectTransform>().anchoredPosition = new Vector2(-xContent, -diff);
        }
    }
}
