﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing.Design;
using System.Linq;
using System.Text;

namespace CSharpGL
{
    /// <summary>
    /// 用在IList&lt;GLSwitch&gt;类型的属性上。
    /// </summary>
    class GLSwithListEditor : UITypeEditor
    {

        public override UITypeEditorEditStyle GetEditStyle(System.ComponentModel.ITypeDescriptorContext context)
        {
            // 指定为模式窗体属性编辑器类型
            return UITypeEditorEditStyle.Modal;
        }

        public override object EditValue(ITypeDescriptorContext context, IServiceProvider provider, object value)
        {
            // 打开属性编辑器修改数据
            var editor = new FormGLSwitchListEditor(value as IList<GLSwitch>);
            if (editor.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                var list = value as IList<GLSwitch>;
                list.Clear();
                foreach (var item in editor.List)
                {
                    list.Add(item);
                }
            }

            return value;
        }
    }
}
