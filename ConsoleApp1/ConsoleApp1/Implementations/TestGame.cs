﻿using GameEngine;
using OpenTK.Graphics.OpenGL4;
using OpenTK.Mathematics;

namespace WindowEngine
{
    internal class TestGame : Game
    {
        public TestGame(string windowTitle, int initialWindowWidth, int initialWindowHeight) : base(windowTitle, initialWindowWidth, initialWindowHeight) { }

        private readonly float[] _vertices =
        {
            -0.5f, -0.5f, // top left
            0.5f, -0.5f,  // top right
            -0.5f, 0.5f, // bottom left
            0.5f, 0.5f, // bottom right
        };

        private int _vertexBufferObject;
        private int _vertexArrayObject;

        private int _shaderHandle;
        protected override void Initialize()
        {

        }

        protected override void LoadContent()
        {
            // Shader stuff
            string vertexShader = @"
            #version 330 core
            layout (location = 0) in vec3 aPosition;
            void main() 
            {
             gl_Position = vec4(aPosition.xyz, 1.0);
            }";

            string fragementShader = @"
            #version 330 core
            out vec4 color;
            void main() 
            {
             color = vec4(1.0, 0.0, 0.0, 1.0);
            }";

            int vertexShaderId = GL.CreateShader(ShaderType.VertexShader);
            GL.ShaderSource(vertexShaderId, vertexShader);
            GL.CompileShader(vertexShaderId);
            GL.GetShader(vertexShaderId, ShaderParameter.CompileStatus, out var vertexShaderCompilationCode);
            if (vertexShaderCompilationCode != (int)All.True)
            {
                Console.WriteLine(GL.GetShaderInfoLog(vertexShaderId));
            }

            int fragmentShaderId = GL.CreateShader(ShaderType.FragmentShader);
            GL.ShaderSource(fragmentShaderId, fragementShader);
            GL.CompileShader(fragmentShaderId);
            GL.GetShader(fragmentShaderId, ShaderParameter.CompileStatus, out var fragmentShaderCompilationCode);
            if (fragmentShaderCompilationCode != (int)All.True)
            {
                Console.WriteLine(GL.GetShaderInfoLog(fragmentShaderId));
            }

            _shaderHandle = GL.CreateProgram();
            GL.AttachShader(_shaderHandle, vertexShaderId);
            GL.AttachShader(_shaderHandle, fragmentShaderId);
            GL.LinkProgram(_shaderHandle);

            GL.DetachShader(_shaderHandle, vertexShaderId);
            GL.DetachShader(_shaderHandle, fragmentShaderId);

            GL.DeleteShader(vertexShaderId);
            GL.DeleteShader(fragmentShaderId);

            _vertexBufferObject = GL.GenBuffer();
            GL.BindBuffer(BufferTarget.ArrayBuffer, _vertexBufferObject);
            GL.BufferData(BufferTarget.ArrayBuffer, _vertices.Length * sizeof(float), _vertices, BufferUsageHint.StaticDraw);

            _vertexArrayObject = GL.GenVertexArray();
            GL.BindVertexArray(_vertexArrayObject);

            GL.VertexAttribPointer(0, 2, VertexAttribPointerType.Float, false, 0, 0);
            GL.EnableVertexAttribArray(0);
        }

        protected override void Update(GameTime gameTime)
        {

        }

        protected override void Render(GameTime gameTime)
        {
            GL.Clear(ClearBufferMask.ColorBufferBit);
            GL.ClearColor(Color4.CornflowerBlue);

            GL.UseProgram(_shaderHandle);

            GL.BindVertexArray(_vertexArrayObject);
            GL.DrawArrays(PrimitiveType.TriangleStrip, 0, 4);
        }

    }
}
