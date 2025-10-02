using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenTK;
using OpenTK.Windowing.Common;
using OpenTK.Windowing.Desktop;
using OpenTK.Graphics.OpenGL;
using OpenTK.Mathematics;

namespace GAM531
{
    public class Game : GameWindow
    {
        private int vertexBufferHandle;
        private int shaderProgramHandle;
        private int vertexArrayHandle; 
        public Game() 
            : base(GameWindowSettings.Default, NativeWindowSettings.Default) 
        {
            this.CenterWindow(new Vector2i(1280, 720));
        }

        protected override void OnResize(ResizeEventArgs e)
        {
            GL.Viewport(0, 0, e.Width, e.Height);
            base.OnResize(e);
        }

        protected override void OnLoad()
        {
            // clear color
            GL.ClearColor(Color4.CornflowerBlue);

            float[] vertices = new float[]
            {
                0.0f, 0.5f, 0f,   // vertex 0
                0.5f, -0.5f, 0f,  // vertex 1
                -0.5f, -0.5f, 0f   // vertex 3
            };

            this.vertexBufferHandle = GL.GenBuffer();
            GL.BindBuffer(BufferTarget.ArrayBuffer, this.vertexBufferHandle);
            GL.BufferData(BufferTarget.ArrayBuffer, vertices.Length * sizeof(float), vertices, BufferUsageHint.StaticDraw);
            GL.BindBuffer(BufferTarget.ArrayBuffer, 0); // unbind

            // bind vertex array handle
            this.vertexArrayHandle = GL.GenVertexArray();
            GL.BindVertexArray(this.vertexArrayHandle);

            // code that the above binds 
            GL.BindBuffer(BufferTarget.ArrayBuffer, this.vertexBufferHandle);
            GL.VertexAttribPointer(0, 3, VertexAttribPointerType.Float, false, 3 * sizeof(float), 0);
            GL.EnableVertexAttribArray(0);

            //unbind
            GL.BindVertexArray(0);

            string vertexShaderCode = @"
            #version 330 core
            layout (location = 0) in vec3 aPosition;
            void main() 
            {
             gl_Position = vec4(aPosition.xyz, 1.0);
            }";

            string fragmentShaderCode = @"
            #version 330 core
            out vec4 color;
            void main() 
            {
             color = vec4(1.0, 0.0, 0.0, 1.0);
            }";

            // vertex shader
            int vertexShaderHandle = GL.CreateShader(ShaderType.VertexShader);
            GL.ShaderSource(vertexShaderHandle, vertexShaderCode);
            GL.CompileShader(vertexShaderHandle);

            // fragment shader
            int fragmentShaderHandle = GL.CreateShader (ShaderType.FragmentShader);
            GL.ShaderSource (fragmentShaderHandle, fragmentShaderCode);
            GL.CompileShader(fragmentShaderHandle);

            // shader program
            this.shaderProgramHandle = GL.CreateProgram();

            GL.AttachShader(this.shaderProgramHandle, vertexShaderHandle);
            GL.AttachShader(this.shaderProgramHandle, fragmentShaderHandle);

            GL.LinkProgram(this.shaderProgramHandle);

            GL.DetachShader(this.shaderProgramHandle, vertexShaderHandle);
            GL.DetachShader(this.shaderProgramHandle, fragmentShaderHandle);

            GL.DeleteShader(vertexShaderHandle);
            GL.DeleteShader(fragmentShaderHandle);


            base.OnLoad();
        }

        protected override void OnUnload()
        {
            GL.BindBuffer(BufferTarget.ArrayBuffer, 0);
            GL.DeleteBuffer(this.vertexBufferHandle);

            GL.UseProgram(0);
            GL.DeleteProgram(this.shaderProgramHandle);

            base.OnUnload();
        }

        protected override void OnUpdateFrame(FrameEventArgs args)
        {
            base.OnUpdateFrame(args);
        }

        protected override void OnRenderFrame(FrameEventArgs args)
        {

            GL.Clear(ClearBufferMask.ColorBufferBit);

            GL.UseProgram(this.shaderProgramHandle);
            GL.BindVertexArray(this.vertexArrayHandle);
            GL.DrawArrays(PrimitiveType.Triangles, 0, 3);

            this.Context.SwapBuffers();
            base.OnRenderFrame(args);
        }
    }
}
