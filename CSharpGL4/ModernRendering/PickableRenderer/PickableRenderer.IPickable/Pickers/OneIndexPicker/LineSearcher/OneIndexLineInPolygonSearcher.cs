﻿using System;

namespace CSharpGL
{
    internal class OneIndexLineInPolygonSearcher : OneIndexLineSearcher
    {
        /// <summary>
        ///
        /// </summary>
        /// <param name="arg"></param>
        /// <param name="x">mouse position(Left Down is (0, 0)).</param>
        /// <param name="y">mouse position(Left Down is (0, 0)).</param>
        /// <param name="primitiveInfo"></param>
        /// <param name="modernRenderer"></param>
        /// <returns></returns>
        internal override uint[] Search(PickEventArgs arg,
            RecognizedPrimitiveInfo primitiveInfo,
            OneIndexPicker picker)
        {
            uint[] indexList = primitiveInfo.VertexIds;
            if (indexList.Length < 3) { throw new ArgumentException(); }

            OneIndexBuffer buffer = indexList.GenIndexBuffer(DrawMode.LineLoop, BufferUsage.StaticDraw);
            picker.Renderer.Render4InnerPicking(arg, buffer);
            uint id = ColorCodedPicking.ReadStageVertexId(arg.Position.X, arg.Position.Y);

            buffer.Dispose();

            if (id == indexList[0])
            { return new uint[] { indexList[indexList.Length - 1], id, }; }
            else
            {
                uint[] result = null;
                for (int i = 1; i < indexList.Length; i++)
                {
                    if (id == indexList[i])
                    {
                        result = new uint[] { indexList[i - 1], indexList[i], };
                        break;
                    }
                }

                if (result != null)
                { return result; }
                else
                { throw new Exception("This should not happen!"); }
            }
        }
    }
}