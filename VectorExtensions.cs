using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Microsoft.Xna.Framework;

public static class VectorExtensions
{
	public static void Normalize(ref this Vector2 vector)
	{
		vector = Vector2.Normalize(vector);
	}

	public static void Normalize(ref this Vector3 vector)
	{
		vector = Vector3.Normalize(vector);
	}

	public static void Normalize(ref this Vector4 vector)
	{
		vector = Vector4.Normalize(vector);
	}
}
