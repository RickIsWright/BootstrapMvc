﻿using System;
using BootstrapMvc.Core;

namespace BootstrapMvc.Forms
{
    public class FormGroupLabel : AnyContentElement
    {
        public FormGroupLabel(IBootstrapContext context)
            : base(context)
        {
            // Nothing
        }

        protected override string WriteSelfStartTag(System.IO.TextWriter writer)
        {
            var form = Context.PeekNearest<Form>();
            var groupContext = Context.PeekNearest<FormGroup>();
            var controlContext = groupContext == null ? null : groupContext.ControlContextValue;

            var required = groupContext == null ? false : groupContext.IsRequiredValue;
            var name = controlContext == null ? null : controlContext.Name;

            var tb = Context.CreateTagBuilder("label");

            if (form != null && form.TypeValue == FormType.Horizontal)
            {
                tb.AddCssClass("control-label");
                tb.AddCssClass(form.LabelWidthValue.ToCssClass());
            }

            if (!string.IsNullOrEmpty(name))
            {
                tb.MergeAttribute("for", name);
            }
            if (required)
            {
                tb.AddCssClass("required");
            }

            ApplyCss(tb);
            ApplyAttributes(tb);

            writer.Write(tb.GetStartTag());

            if (form != null && form.TypeValue == FormType.Inline)
            {
                return tb.GetEndTag() + " "; // trailing space is important for inline forms! Bootstrap does not provide spacing between groups in css!
            }

            return tb.GetEndTag();
        }
    }
}
