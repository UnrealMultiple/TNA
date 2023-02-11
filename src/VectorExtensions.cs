using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Microsoft.Xna.Framework;

public static class VectorExtensions
{
	public static void Normalize(this ref Vector2 vector2)
	{
		vector2 = Vector2.Normalize(vector2);
	}

	public static void Normalize(this ref Vector3 vector3)
	{
		vector3 = Vector3.Normalize(vector3);
	}

	public static void Normalize(this ref Vector4 vector4)
	{
		vector4 = Vector4.Normalize(vector4);
	}

	public static Vector2 CatmullRom(
			Vector2 value1,
			Vector2 value2,
			Vector2 value3,
			Vector2 value4,
			float amount
		)
	{
		return new Vector2(
			MathHelper.CatmullRom(value1.X, value2.X, value3.X, value4.X, amount),
			MathHelper.CatmullRom(value1.Y, value2.Y, value3.Y, value4.Y, amount)
		);
	}

	public static Vector2 Hermite(
			Vector2 value1,
			Vector2 tangent1,
			Vector2 value2,
			Vector2 tangent2,
			float amount
		)
	{
		Vector2 result = new Vector2();
		Hermite(ref value1, ref tangent1, ref value2, ref tangent2, amount, out result);
		return result;
	}

	public static void Hermite(
		ref Vector2 value1,
		ref Vector2 tangent1,
		ref Vector2 value2,
		ref Vector2 tangent2,
		float amount,
		out Vector2 result
	)
	{
		result.X = MathHelper.Hermite(value1.X, tangent1.X, value2.X, tangent2.X, amount);
		result.Y = MathHelper.Hermite(value1.Y, tangent1.Y, value2.Y, tangent2.Y, amount);
	}

	public static Vector2 SmoothStep(Vector2 value1, Vector2 value2, float amount)
	{
		return new Vector2(
			MathHelper.SmoothStep(value1.X, value2.X, amount),
			MathHelper.SmoothStep(value1.Y, value2.Y, amount)
		);
	}

	public static void SmoothStep(
		ref Vector2 value1,
		ref Vector2 value2,
		float amount,
		out Vector2 result
	)
	{
		result.X = MathHelper.SmoothStep(value1.X, value2.X, amount);
		result.Y = MathHelper.SmoothStep(value1.Y, value2.Y, amount);
	}
}
