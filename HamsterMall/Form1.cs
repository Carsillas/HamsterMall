using SharpGLTF.Runtime;
using SharpGLTF.Schema2;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HamsterMall
{

    struct Vertex
    {
        public float X, Y, Z, NX, NY, NZ, U, V;

        public Vertex Converted()
        {
            return new Vertex { X = X * 50.0f, Y = Y * 50.0f, Z = -Z * 50.0f, NX = NX, NY = NY, NZ = -NZ, U = U, V = V }; 
        }
    }


    struct mesh
    {
        public string name;
        public List<geom> geoms;

    }

    struct geom
    {
        public Vector4 ambient;
        public Vector4 diffuse;
        public Vector4 specular;
        public Vector4 emissive;
        public float power;
        public int hasReflection;
        public string texture;
        public List<strip> strips;
    }

    struct strip
    {
        public int triangleCount;
        public int vertexOffset;
    }

    public partial class HamsterMall : Form
    {
        public HamsterMall()
        {
            InitializeComponent();
        }


        private void Ambient_Click(object sender, EventArgs e)
        {
            if (colorDialog1.ShowDialog() == DialogResult.OK)
            {
                Ambient.BackColor = colorDialog1.Color;
            }
        }

        private void Background_Click(object sender, EventArgs e)
        {
            if (colorDialog1.ShowDialog() == DialogResult.OK)
            {
                Background.BackColor = colorDialog1.Color;
            }
        }


        private void loadButton_Click(object sender, EventArgs e)
        {
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                meshFileText.Text = openFileDialog1.FileName;
            }

        }

        private void saveButton_Click(object sender, EventArgs e)
        {
            if (openFileDialog1.FileName != null && saveFileDialog1.ShowDialog() == DialogResult.OK)
            {
                using (FileStream saveFile = File.OpenWrite(saveFileDialog1.FileName))
                {
                    using (CustomWriter writer = new CustomWriter(saveFile))
                    {

                        var model = SharpGLTF.Schema2.ModelRoot.Load(openFileDialog1.FileName);

                        WriteRefPoints(writer, model);
                        WriteSplines(writer, model);
                        WriteLights(writer, model);
                        WriteBackgroundAndAmbient(writer);
                        WriteVertices(writer, model);

                        var saveFileInfo = new FileInfo(saveFileDialog1.FileName);
                        var textureDirectoryPath = Path.Combine(saveFileInfo.DirectoryName, "textures");
                        EnsureClearDirectory(textureDirectoryPath);
                        WriteTextures(model, textureDirectoryPath);
                    }
                }
            }
        }

        private static void EnsureClearDirectory(string path)
        {
            if (Directory.Exists(path))
            {
                Directory.Delete(path, true);
            }

            Directory.CreateDirectory(path);
        }

        private void WriteTextures(ModelRoot model, string textureDirectoryPath)
        {
            var textures = model.LogicalNodes
                .SelectMany(node => node.Mesh?.Primitives ?? Enumerable.Empty<MeshPrimitive>())
                .Select(primitive => primitive.Material?.Channels?.FirstOrDefault(channel => channel.Key == "BaseColor").Texture)
                .Where(texture => texture != null)
                .GroupBy(texture => texture.PrimaryImage.Name)
                .Select(texture => texture.First());

            foreach (var texture in textures)
            {
                var image = texture.PrimaryImage;
                var pngBytes = image.Content.Content.ToArray();
                var pngPath = Path.Combine(textureDirectoryPath, image.Name + ".png");
                File.WriteAllBytes(pngPath, pngBytes);
            }
        }

        private void WriteRefPoints(CustomWriter writer, ModelRoot model)
        {

            var Nodes = model.LogicalNodes.Where(node =>
                node.Name.StartsWith("START") ||
                node.Name.StartsWith("SAFESPOT") || 
                node.Mesh == null
            ).ToList();

            writer.Write(Nodes.Count);

            foreach (var node in Nodes)
            {
                int length = node.Name.LastIndexOf(".");
                length = length == -1 ? node.Name.Length : length;

                writer.Write(node.Name.Substring(0, length));
                writer.Write(node.WorldMatrix.Translation.X * 50.0f);
                writer.Write(node.WorldMatrix.Translation.Y * 50.0f);
                writer.Write(-node.WorldMatrix.Translation.Z * 50.0f);

                writer.Write(0.0f);
                writer.Write(0.0f);
                writer.Write(0.0f);

                writer.Write(0);
            }



        }


        private void WriteSplines(CustomWriter writer, ModelRoot model)
        {
            //List<Node> CameraSpline = model.LogicalNodes.Where(Node => Node.VisualParent?.Name == "CameraLocus").OrderBy(Node => Node.Name).ToList();

            //writer.Write(1);
            //writer.Write("CameraLocus");
            //writer.Write(CameraSpline.Count);

            //foreach(Node node in CameraSpline)
            //{
            //    Vector3 Pos = node.WorldMatrix.Translation;
            //    Vertex v = new Vertex { X = Pos.X, Y = Pos.Y, Z = Pos.Z }.Converted();
            //    writer.Write(v.X);
            //    writer.Write(v.Y);
            //    writer.Write(v.Z);
            //}

            //No need to write a spline apparently the camera just follows if i don't populate this at all
            writer.Write(0);
        }

        private void WriteLights(CustomWriter writer, ModelRoot model)
        {
            writer.Write(1);
            writer.Write(0);
            writer.Write(350.671691894531f);
            writer.Write(525.585083007812f);
            writer.Write(76.9814834594727f);
            writer.Write(195.976226806641f);
            writer.Write(419.277282714844f);
            writer.Write(24.076717376709f);
            writer.Write(1.0f);
            writer.Write(1.0f);
            writer.Write(1.0f);
        }

        private void WriteBackgroundAndAmbient(CustomWriter writer)
        {
            writer.Write(Background.BackColor.R / 255.0f);
            writer.Write(Background.BackColor.G / 255.0f);
            writer.Write(Background.BackColor.B / 255.0f);
            writer.Write(Ambient.BackColor.R / 255.0f);
            writer.Write(Ambient.BackColor.G / 255.0f);
            writer.Write(Ambient.BackColor.B / 255.0f);
        }

        private void WriteVertices(CustomWriter writer, ModelRoot model)
        {

            List<Vertex> verts = BuildVertList(model, out List<mesh> meshes);
            writer.Write(verts.Count);
            foreach (Vertex v in verts)
            {
                writer.Write(v);
            }

    


            //Cube
            writer.Write(-1000000.0f);
            writer.Write(-1000000.0f);
            writer.Write(-1000000.0f);

            writer.Write(1000000.0f);
            writer.Write(1000000.0f);
            writer.Write(1000000.0f);

            writer.Write(meshes.Count); // "submesh" count

            foreach (mesh m in meshes)
            {
                writer.Write(-1000000.0f);
                writer.Write(-1000000.0f);
                writer.Write(-1000000.0f);

                writer.Write(1000000.0f);
                writer.Write(1000000.0f);
                writer.Write(1000000.0f);


                writer.Write(0); // 0 submeshes
                writer.Write(m.geoms.Count); // geom count

                foreach(geom g in m.geoms)
                {
                    writer.Write(m.name ?? "");
                    writer.Write(Vector4.Zero);//ambient
                    writer.Write(g.diffuse);//diffuse
                    writer.Write(Vector4.One / 4.0f);//spec
                    writer.Write(g.emissive);//emissive
                    writer.Write(1.0f); // power?
                    writer.Write(1); //has reflection

                    if (g.texture != null)
                    {
                        writer.Write(1);
                        writer.Write(g.texture);
                    }
                    else
                    {
                        writer.Write(0);
                    }

                    writer.Write(g.strips.Count); // strip count

                    foreach(strip s in g.strips)
                    {
                        writer.Write(s.triangleCount);
                        writer.Write(s.vertexOffset);
                    }

                }


            }


        }
        private List<Vertex> BuildVertList(ModelRoot Root, out List<mesh> meshes)
        {
            List<Vertex> verts = new List<Vertex>();
            meshes = new List<mesh>();



            foreach (var Node  in Root.LogicalNodes)
            {
                if(Node.Mesh == null)
                {
                    continue;
                }

                Mesh Mesh = Node.Mesh;

                mesh m = new mesh();
                m.name = Mesh.Name;
                m.geoms = new List<geom>();
                foreach (MeshPrimitive Primitive in Mesh.Primitives)
                {
                    geom g = new geom();
                    g.strips = new List<strip>();

                    g.diffuse = Primitive.Material?.Channels?.First(channel => channel.Key == "BaseColor").Parameter ?? Vector4.One;
                    
                    g.diffuse = Primitive.Material?.Channels?.First(channel => channel.Key == "Emission").Parameter ?? Vector4.Zero;
                    
                    var texture = Primitive.Material?.Channels?.FirstOrDefault(channel => channel.Key == "BaseColor").Texture;
                    if (texture != null)
                    {
                        g.texture = texture.PrimaryImage.Name + ".png";
                    }

                    GetVertexBuffer(Primitive, out List<Vector3> Vertices);
                    GetNormalBuffer(Primitive, out List<Vector3> Normals);
                    GetTexCoordBuffer(Primitive, out List<Vector2> Uvs);
                    Vector3[] vs = Vertices.ToArray();
                    Vector3[] ns = Normals.ToArray();
                    Vector2[] uvs = Uvs.ToArray();
                    GetIndexBuffer(Primitive, out List<(int A, int B, int C)> Indices);

                    //TODO stripify triangles


                    foreach (var tri in Indices)
                    {
                        g.strips.Add(new strip { triangleCount = 1, vertexOffset = verts.Count });
                        Vector4 PosC = new Vector4(vs[tri.C].X, vs[tri.C].Y, vs[tri.C].Z, 1);
                        Vector4 PosB = new Vector4(vs[tri.B].X, vs[tri.B].Y, vs[tri.B].Z, 1);
                        Vector4 PosA = new Vector4(vs[tri.A].X, vs[tri.A].Y, vs[tri.A].Z, 1);
                        PosC = Vector4.Transform(PosC, Node.WorldMatrix);
                        PosB = Vector4.Transform(PosB, Node.WorldMatrix);
                        PosA = Vector4.Transform(PosA, Node.WorldMatrix);
                        verts.Add(new Vertex { X = PosC.X, Y = PosC.Y, Z = PosC.Z, NX = ns[tri.C].X, NY = ns[tri.C].Y, NZ = ns[tri.C].Z, U = uvs[tri.C].X, V = uvs[tri.C].Y }.Converted());
                        verts.Add(new Vertex { X = PosB.X, Y = PosB.Y, Z = PosB.Z, NX = ns[tri.B].X, NY = ns[tri.B].Y, NZ = ns[tri.B].Z, U = uvs[tri.B].X, V = uvs[tri.B].Y }.Converted());
                        verts.Add(new Vertex { X = PosA.X, Y = PosA.Y, Z = PosA.Z, NX = ns[tri.A].X, NY = ns[tri.A].Y, NZ = ns[tri.A].Z, U = uvs[tri.A].X, V = uvs[tri.A].Y }.Converted());
                    }
                    m.geoms.Add(g);
                }
                meshes.Add(m);
            }

            return verts;
        }


        private static bool GetVertexBuffer(MeshPrimitive Primitive, out List<Vector3> VertexBuffer)
        {
            VertexBuffer = Primitive.GetVertexAccessor("POSITION")?.AsVector3Array().ToList();
            if (VertexBuffer?.Count < 3 || Primitive.DrawPrimitiveType != SharpGLTF.Schema2.PrimitiveType.TRIANGLES)
            {
                return false;
            }
            return true;
        }

        private static bool GetIndexBuffer(MeshPrimitive Primitive, out List<(int A, int B, int C)> IndexBuffer)
        {
            IndexBuffer = Primitive.GetTriangleIndices().ToList();
            if (IndexBuffer?.Count == 0)
            {
                return false;
            }
            return true;
        }
        private static bool GetNormalBuffer(MeshPrimitive Primitive, out List<Vector3> NormalBuffer)
        {
            NormalBuffer = Primitive.GetVertexAccessor("NORMAL")?.AsVector3Array().ToList();

            if (NormalBuffer?.Count == 0)
            {
                return false;
            }
            return true;
        }

        private static bool GetTexCoordBuffer(MeshPrimitive Primitive, out List<Vector2> TexCoordBuffer)
        {
            TexCoordBuffer = Primitive.GetVertexAccessor("TEXCOORD_0")?.AsVector2Array().ToList();

            if (TexCoordBuffer?.Count == 0)
            {
                return false;
            }

            return true;
        }

    }
}
