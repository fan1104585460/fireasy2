﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Fireasy.Windows.Forms
{
    public partial class TreeList
    {
        private bool controlPressed;
        private bool shiftPressed;
        private int lastRowIndex = -1;
        private int shiftRowIndex = -1;

        protected override void OnKeyDown(KeyEventArgs e)
        {
            base.OnKeyDown(e);

            if (MultiSelect) //允许多选
            {
                //按下Control
                if (e.KeyCode == Keys.ControlKey && e.Modifiers == Keys.Control && !controlPressed)
                {
                    controlPressed = true;
                }
                //按下Shift
                else if (e.KeyCode == Keys.ShiftKey && e.Modifiers == Keys.Shift && !shiftPressed)
                {
                    shiftPressed = true;
                }
            }
        }

        protected override void OnKeyUp(KeyEventArgs e)
        {
            base.OnKeyUp(e);

            //全选
            if (MultiSelect && e.Modifiers == Keys.Control && e.KeyCode == Keys.A)
            {
                SelectAll();
            }
            //取消Control键
            else if (controlPressed)
            {
                controlPressed = false;
            }
            //取消Shift键
            else if (shiftPressed)
            {
                shiftPressed = false;
                shiftRowIndex = -1;
            }
        }

        /// <summary>
        /// 处理键盘消息。
        /// </summary>
        /// <param name="msg"></param>
        /// <returns></returns>
        public override bool PreProcessMessage(ref Message msg)
        {
            if (SelectedItems.Count == 0)
            {
                return base.PreProcessMessage(ref msg);
            }

            var item = SelectedItems[0];
            var vitem = virMgr.Items.FirstOrDefault(s => s.Item == item);
            var index = vitem.Index;

            if (msg.Msg == 0x100) //避免移动方向键时跳出控件
            {
                switch (msg.WParam.ToInt32())
                {
                    case 40: //下移
                        if (index++ < virMgr.Items.Count - 1)
                        {
                            virMgr.Items[index].Item.Selected = true;
                        }

                        break;
                    case 38: //上移
                        if (index-- > 0)
                        {
                            virMgr.Items[index].Item.Selected = true;
                        }

                        break;
                }

                return false;
            }

            return base.PreProcessMessage(ref msg);
        }

        /// <summary>
        /// 全选所有行。
        /// </summary>
        private void SelectAll()
        {
            foreach (var item in virMgr.Items)
            {
                if (item.Item is TreeListItem)
                {
                    SelectItem(((TreeListItem)item.Item), true, false);
                }
            }

            Invalidate();
        }
    }
}
