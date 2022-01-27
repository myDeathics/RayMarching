using System;
using static System.Math;
using System.Numerics;
using System.Runtime.InteropServices;

namespace ASCII_Donut.NewGraphic
{
    internal class Graphic
    {
        public struct vec2
        {
            public double x { get; set; }
            public double y { get; set; }

            public vec2(double x, double y)
            {
                this.x = x;
                this.y = y;
            }

            public static vec2 operator +(vec2 a, vec2 b) { return new vec2(a.x + b.x, a.y + b.y); }
            public static vec2 operator +(vec2 a, double b) { return new vec2(a.x + b, a.y + b); }
            public static vec2 operator +(double b, vec2 a) { return new vec2(a.x + b, a.y + b); }

            public static vec2 operator *(vec2 a, vec2 b) { return new vec2(a.x * b.x, a.y * b.y); }
            public static vec2 operator *(vec2 a, double b) { return new vec2(a.x * b, a.y * b); }
            public static vec2 operator *(double b, vec2 a) { return new vec2(a.x * b, a.y * b); }

            public static vec2 operator /(vec2 a, vec2 b) { return new vec2(a.x / b.x, a.y / b.y); }
            public static vec2 operator /(vec2 a, double b) { return new vec2(a.x / b, a.y / b); }
            public static vec2 operator /(double b, vec2 a) { return new vec2(b / a.x, b / a.y); }

            public static vec2 operator -(vec2 a, vec2 b) { return new vec2(a.x - b.x, a.y - b.y); }
            public static vec2 operator -(vec2 a, double b) { return new vec2(a.x - b, a.y - b); }
            public static vec2 operator -(double b, vec2 a) { return new vec2(b - a.x, b - a.y); }
        }
        public struct vec3
        {
            public double x { get; set; }
            public double y { get; set; }
            public double z { get; set; }

            public vec3(double x, double y, double z)
            {
                this.x = x;
                this.y = y;
                this.z = z;
            }

            public static vec3 operator +(vec3 a, vec3 b) { return new vec3(a.x + b.x, a.y + b.y, a.z + b.z); }
            public static vec3 operator +(vec3 a, double b) { return new vec3(a.x + b, a.y + b, a.z + b); }
            public static vec3 operator +(double b, vec3 a) { return new vec3(a.x + b, a.y + b, a.z + b); }

            public static vec3 operator *(vec3 a, vec3 b) { return new vec3(a.x * b.x, a.y * b.y, a.z * b.z); }
            public static vec3 operator *(vec3 a, double b) { return new vec3(a.x * b, a.y * b, a.z * b); }
            public static vec3 operator *(double b, vec3 a) { return new vec3(a.x * b, a.y * b, a.z * b); }

            public static vec3 operator /(vec3 a, vec3 b) { return new vec3(a.x / b.x, a.y / b.y, a.z / b.z); }
            public static vec3 operator /(vec3 a, double b) { return new vec3(a.x / b, a.y / b, a.z / b); }
            public static vec3 operator /(double b, vec3 a) { return new vec3(b / a.x, b / a.y, b / a.z); }

            public static vec3 operator -(vec3 a, vec3 b) { return new vec3(a.x - b.x, a.y - b.y, a.z - b.z); }
            public static vec3 operator -(vec3 a, double b) { return new vec3(a.x - b, a.y - b, a.z - b); }
            public static vec3 operator -(double b, vec3 a) { return new vec3(b - a.x, b - a.y, b - a.z); }

            public static bool operator ==(vec3 vec3, vec3 b)
            {
                if (vec3 == b)
                {
                    return true;
                }
                return false;
            }
            public static bool operator !=(vec3 vec3, vec3 b)
            {
                if (vec3 != b)
                {
                    return true;
                }
                return false;
            }
        }
        private static string ASCII = " .:!/r(l1Z4H9W8$@";
        private static int height;
        private static int width;
        private static float pixelAspect = 16f / 24.0f;

        private static float aspect;
        private const int MAX_STEPS = 75;
        private static vec3 rotateX(vec3 a, double angle)
        {
            vec3 b = a;
            b.z = a.z * Cos(angle) - a.y * Sin(angle);
            b.y = a.z * Sin(angle) + a.y * Cos(angle);
            return b;
        }
        private static vec3 rotateY(vec3 a, double angle)
        {
            vec3 b = a;
            b.x = a.x * Cos(angle) - a.z * Sin(angle);
            b.z = a.x * Sin(angle) + a.z * Cos(angle);
            return b;
        }
        private static vec3 rotateZ(vec3 a, double angle)
        {
            vec3 b = a;
            b.x = a.x * Cos(angle) - a.y * Sin(angle);
            b.y = a.x * Sin(angle) + a.y * Cos(angle);
            return b;
        }
        public static class ConsoleHelper
        {
            [DllImport("kernel32.dll", CharSet = CharSet.Unicode)]
            private static extern bool WriteConsoleOutputCharacter(IntPtr hConsoleOutput, string lpCharacter, uint nLength, Point16 dwWriteCoord, out uint lpNumberOfCharsWritten);
            [DllImport("kernel32.dll")]
            private static extern IntPtr GetStdHandle(int nStdHandle);

            private const int STD_OUTPUT_HANDLE = -11;
            private const int STD_INPUT_HANDLE = -10;
            private const int STD_ERROR_HANDLE = -12;
            private static readonly IntPtr _stdOut = GetStdHandle(STD_OUTPUT_HANDLE);

            [StructLayout(LayoutKind.Sequential)]
            private struct Point16
            {
                public short X;
                public short Y;

                public Point16(short x, short y)
                    => (X, Y) = (x, y);
            };

            public static void WriteToBufferAt(string text, int x, int y)
            {
                WriteConsoleOutputCharacter(_stdOut, text, (uint)text.Length, new Point16((short)x, (short)y), out uint _);
            }
        }
        private static vec3 MaxVec3Number(vec3 a, float b)
        {
            if (a.x > b && a.y > b && a.z > b)
            {
                return a;
            }
            return new vec3(0, 0, 0);
        }
        public static void SetConsoleSize(int Width, int Height)
        {
            width = Width;
            height = Height + 1;
            Console.SetWindowSize(width, height);
            Console.SetBufferSize(width, height + 1);
            aspect = width / height;
        }
        private static float Clamp(float value, float min, float max)
        {
            return Max(Min(value, max), min);
        }
        private static float Clamp(vec3 v, float min, float max)
        {
            return Max(Min((float)(v.x + v.y + v.z) / 3, max), min);
        }
        private static vec3 Abs(vec3 v)
        {
            return new vec3(Math.Abs(v.x), Math.Abs(v.y), Math.Abs(v.z));
        }
        private static float length(vec3 v)
        {
            return (float)Sqrt(Pow(v.x, 2) + Pow(v.y, 2) + Pow(v.z, 2));
        }
        private static float length(vec2 v)
        {
            return (float)Sqrt(Pow(v.x, 2) + Pow(v.y, 2));
        }
        private static float Sphere(vec3 ro, vec3 sphPos, float r)
        {
            return length(ro - sphPos) - r;
        }
        private static float sdTorus(vec3 p, vec2 t)
        {
            vec2 q = new vec2(length(new vec2(p.x, p.y)) - t.x, p.z);
            return (float)(length(q) - t.y);
        }
        private static vec3 norm(vec3 vec)
        {
            return new vec3(vec.x / length(vec), vec.y / length(vec), vec.z / length(vec));
        }
        private static float Plane(vec3 p)
        {
            return (float)p.y;
        }
        private static float sdBox(vec3 p, vec3 b)
        {
            vec3 d = Abs(p) - b;
            return (float)Min(Max(d.x, Max(d.y, d.z)), 0) + length(MaxVec3Number(d, 0));
        }
        private static float sceneSDF(in vec3 p, vec3 pos = new vec3(), vec3 pos2 = new vec3())
        {
            float sphere = Sphere(p, pos, 1f);
            //float box = sdBox(p, pos);
            //float torus = sdTorus(p, new vec2(1, 0.5));
            //float plane = Plane(new vec3(0, 5, 2));
            // Later we might have sphere_1, sphere_2, cube_3, etc...

            return sphere;
        }
        private static float Dot(vec3 a, vec3 b) { return (float)(a.x * b.x + a.y * b.y + a.z * b.z); }
        public static void Draw()
        {
            char[] path = new char[width * height + 1];
            path[width * height] = '\0';

            int pixel = 0;

            for (int t = 0;t < 10000;t++)
            {
                vec3 ro = new vec3(0, 0, -5);
                vec3 lp = norm(new vec3(3, Sin(t * 0.1), -3));
                for (int w = 0;w < width;w++)
                {
                    for (int h = 0;h < height;h++)
                    {
                        vec2 uv = new vec2(w, h) / new vec2(width, height) * 2 - 1;
                        uv.x *= aspect * pixelAspect;
                        vec3 rd = new vec3(uv.x, uv.y, 1);

                        float distanceTraveled = 0;
                        const float MIN_DIST = 0.001f;
                        const float MAX_DIST = 100;
                        for (int step = 0;step < MAX_STEPS;step++)
                        {
                            vec3 p = ro + distanceTraveled * rd;
                            float closestDistance = sceneSDF(p, new vec3(0, 0, -2));

                            if (closestDistance < MIN_DIST)
                            {
                                float diff = Dot(p, lp);
                                pixel = (int)Clamp(diff * 7, 0, ASCII.Length - 1);
                            }

                            if (distanceTraveled > MAX_DIST)
                            {
                                pixel = 0;
                                break;
                            }
                            distanceTraveled += closestDistance;
                        }
                        path[w + h * width] = ASCII[pixel];
                    }
                }
                string l = new string(path);
                ConsoleHelper.WriteToBufferAt(l, 0, 0);
            }
        }
    }
}
