﻿using CSharpGL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GridViewer
{
    public partial class CodedColorValueRenderer : Renderer
    {

        private TextModel textModel;

        public IFontTexture FontTextureService { get; set; }

        private string content = string.Empty;
        public string Text
        {
            get { return content; }
            set
            {
                if (this.textModel != null) { this.textModel.SetText(value, this.FontTextureService); }
                this.content = value;
            }
        }

        private BlendSwitch blendSwitch = new BlendSwitch(BlendingSourceFactor.SourceAlpha, BlendingDestinationFactor.One);
        public BlendSwitch BlendSwitch
        {
            get { return blendSwitch; }
        }

        public CodedColorValueRenderer(TextModel textModel, ShaderCode[] shaderCodes,
            PropertyNameMap propertyNameMap, params GLSwitch[] switches)
            : base(textModel, shaderCodes, propertyNameMap, switches)
        {
            this.textModel = textModel;
            this.FontTextureService = FontTexture.Default;
        }

        protected override void DoInitialize()
        {
            base.DoInitialize();

            this.SetUniform("fontTexture", this.FontTextureService.GetSamplerValue());
        }
        protected override void DoRender(RenderEventArg arg)
        {
            blendSwitch.On();

            base.DoRender(arg);

            blendSwitch.Off();
        }
    }
}
