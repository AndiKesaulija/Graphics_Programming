using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace Graphics_programming
{
	class Lesson3 : Lesson
	{
		[StructLayout(LayoutKind.Sequential, Pack = 1)]
		public struct VertexPositionColorNormal : IVertexType
		{
			public Vector3 Position;
			public Color Color;
			public Vector3 Normal;
			public Vector2 Texture;

			static readonly VertexDeclaration _vertexDeclaration = new VertexDeclaration
			(
				new VertexElement(0, VertexElementFormat.Vector3, VertexElementUsage.Position, 0),
				new VertexElement(12, VertexElementFormat.Color, VertexElementUsage.Color, 0),
				new VertexElement(16, VertexElementFormat.Vector3, VertexElementUsage.Normal, 0),
				new VertexElement(28, VertexElementFormat.Vector2, VertexElementUsage.TextureCoordinate, 0)
			);

			VertexDeclaration IVertexType.VertexDeclaration => _vertexDeclaration;

			public VertexPositionColorNormal(Vector3 position, Color color, Vector3 normal, Vector2 texture)
			{
				Position = position;
				Color = color;
				Normal = normal;
				Texture = texture;
			}
		}

		//private BasicEffect effect;

		private Effect myEffect;
		private Texture2D boxTexture, boxNormal, boxSpecular;
		Vector3 LightPosition = Vector3.Right * 2 + Vector3.Up * 1 + Vector3.Backward * 2;
		Vector3 AmbientPosition = Vector3.Right * 2 + Vector3.Up * 1 + Vector3.Backward * 2;

		private Texture2D day, night, clouds;
		private Model sphere;
		public override void LoadContent(ContentManager Content, GraphicsDeviceManager graphics, SpriteBatch spriteBatch)
		{
			//effect = new BasicEffect(graphics.GraphicsDevice);

			myEffect = Content.Load<Effect>("Lesson3");//load .fx File
			boxTexture = Content.Load<Texture2D>("BoxImg");
			boxNormal = Content.Load<Texture2D>("NormalMap");
			boxSpecular = Content.Load<Texture2D>("SpecularMap");

			day = Content.Load<Texture2D>("day");
			night = Content.Load<Texture2D>("night");
			clouds = Content.Load<Texture2D>("clouds");

			sphere = Content.Load<Model>("uv_sphere");

			foreach(ModelMesh mesh in sphere.Meshes)
            {
				foreach (ModelMeshPart meshPart in mesh.MeshParts)
                {
					meshPart.Effect = myEffect;
                }
            }
		}
		void RenderModel(Model m, Matrix parentMatrix)
        {
			Matrix[] transforms = new Matrix[m.Bones.Count];
			m.CopyAbsoluteBoneTransformsTo(transforms);

			myEffect.CurrentTechnique.Passes[0].Apply();

			foreach(ModelMesh mesh in m.Meshes)
            {
				myEffect.Parameters["World"].SetValue(parentMatrix * transforms[mesh.ParentBone.Index]);

				mesh.Draw();
            }
        }

		public override void Draw(GameTime gameTime, GraphicsDeviceManager graphics, SpriteBatch spriteBatch)
		{
			GraphicsDevice device = graphics.GraphicsDevice;

			float time = (float)gameTime.TotalGameTime.TotalSeconds;
			LightPosition = new Vector3(MathF.Cos(time), 1, MathF.Sin(time)) * 2;
			Vector3 cameraPos = -Vector3.Forward * 10 + Vector3.Up * 5 + Vector3.Right * 5;

			//LightPosition = -cameraPos;
			//AmbientPosition = cameraPos;
			//AmbientPosition = Vector3.Forward * 10 + Vector3.Up * 5 + -Vector3.Right * 5;//OppositeSide

			Matrix World = Matrix.CreateWorld(Vector3.Zero, Vector3.Forward, Vector3.Up);
			Matrix View = Matrix.CreateLookAt(cameraPos, Vector3.Zero, Vector3.Up);

			//effect.VertexColorEnabled = true;

			myEffect.Parameters["World"].SetValue(World);//effect.World = World;
			myEffect.Parameters["View"].SetValue(View);//effect.View = View;
			myEffect.Parameters["Projection"].SetValue(Matrix.CreatePerspectiveFieldOfView((MathF.PI / 180f) * 25f, device.Viewport.AspectRatio, 0.001f, 1000f));//effect.Projection = Matrix.CreatePerspectiveFieldOfView((MathF.PI / 180f) * 25f, device.Viewport.AspectRatio, 0.001f, 1000f);

			myEffect.Parameters["DayTex"].SetValue(day);
			//myEffect.Parameters["NightTex"].SetValue(night);
			//myEffect.Parameters["CloudsTex"].SetValue(clouds);


			myEffect.Parameters["LightPosition"].SetValue(LightPosition);
			//myEffect.Parameters["AmbientPosition"].SetValue(AmbientPosition);
			//myEffect.Parameters["CameraPosition"].SetValue(cameraPos);

			myEffect.CurrentTechnique.Passes[0].Apply();//effect.CurrentTechnique.Passes[0].Apply();



			device.Clear(Color.Black);

			RenderModel(sphere, Matrix.CreateScale(0.01f) * World);

			//device.DrawUserIndexedPrimitives(PrimitiveType.TriangleList, vertices, 0, vertices.Length, indices, 0, indices.Length / 3);
		}
	}
}