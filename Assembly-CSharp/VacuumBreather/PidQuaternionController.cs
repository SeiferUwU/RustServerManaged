using System;
using UnityEngine;

namespace VacuumBreather;

public class PidQuaternionController
{
	private readonly PidController[] _internalController;

	public float Kp
	{
		get
		{
			return _internalController[0].Kp;
		}
		set
		{
			if (value < 0f)
			{
				throw new ArgumentOutOfRangeException("value", "Kp must be a non-negative number.");
			}
			_internalController[0].Kp = value;
			_internalController[1].Kp = value;
			_internalController[2].Kp = value;
			_internalController[3].Kp = value;
		}
	}

	public float Ki
	{
		get
		{
			return _internalController[0].Ki;
		}
		set
		{
			if (value < 0f)
			{
				throw new ArgumentOutOfRangeException("value", "Ki must be a non-negative number.");
			}
			_internalController[0].Ki = value;
			_internalController[1].Ki = value;
			_internalController[2].Ki = value;
			_internalController[3].Ki = value;
		}
	}

	public float Kd
	{
		get
		{
			return _internalController[0].Kd;
		}
		set
		{
			if (value < 0f)
			{
				throw new ArgumentOutOfRangeException("value", "Kd must be a non-negative number.");
			}
			_internalController[0].Kd = value;
			_internalController[1].Kd = value;
			_internalController[2].Kd = value;
			_internalController[3].Kd = value;
		}
	}

	public PidQuaternionController(float kp, float ki, float kd)
	{
		if (kp < 0f)
		{
			throw new ArgumentOutOfRangeException("kp", "kp must be a non-negative number.");
		}
		if (ki < 0f)
		{
			throw new ArgumentOutOfRangeException("ki", "ki must be a non-negative number.");
		}
		if (kd < 0f)
		{
			throw new ArgumentOutOfRangeException("kd", "kd must be a non-negative number.");
		}
		_internalController = new PidController[4]
		{
			new PidController(kp, ki, kd),
			new PidController(kp, ki, kd),
			new PidController(kp, ki, kd),
			new PidController(kp, ki, kd)
		};
	}

	public static Quaternion MultiplyAsVector(Matrix4x4 matrix, Quaternion quaternion)
	{
		Vector4 vector = new Vector4(quaternion.w, quaternion.x, quaternion.y, quaternion.z);
		Vector4 vector2 = matrix * vector;
		return new Quaternion(vector2.y, vector2.z, vector2.w, vector2.x);
	}

	public static Quaternion ToEulerAngleQuaternion(Vector3 eulerAngles)
	{
		return new Quaternion(eulerAngles.x, eulerAngles.y, eulerAngles.z, 0f);
	}

	public Vector3 ComputeRequiredAngularAcceleration(Quaternion currentOrientation, Quaternion desiredOrientation, Vector3 currentAngularVelocity, float deltaTime)
	{
		Quaternion quaternion = QuaternionExtensions.RequiredRotation(currentOrientation, desiredOrientation);
		Quaternion error = QuaternionExtensions.Subtract(Quaternion.identity, quaternion);
		Quaternion delta = ToEulerAngleQuaternion(currentAngularVelocity) * quaternion;
		Matrix4x4 matrix = new Matrix4x4
		{
			m00 = (0f - quaternion.x) * (0f - quaternion.x) + (0f - quaternion.y) * (0f - quaternion.y) + (0f - quaternion.z) * (0f - quaternion.z),
			m01 = (0f - quaternion.x) * quaternion.w + (0f - quaternion.y) * (0f - quaternion.z) + (0f - quaternion.z) * quaternion.y,
			m02 = (0f - quaternion.x) * quaternion.z + (0f - quaternion.y) * quaternion.w + (0f - quaternion.z) * (0f - quaternion.x),
			m03 = (0f - quaternion.x) * (0f - quaternion.y) + (0f - quaternion.y) * quaternion.x + (0f - quaternion.z) * quaternion.w,
			m10 = quaternion.w * (0f - quaternion.x) + (0f - quaternion.z) * (0f - quaternion.y) + quaternion.y * (0f - quaternion.z),
			m11 = quaternion.w * quaternion.w + (0f - quaternion.z) * (0f - quaternion.z) + quaternion.y * quaternion.y,
			m12 = quaternion.w * quaternion.z + (0f - quaternion.z) * quaternion.w + quaternion.y * (0f - quaternion.x),
			m13 = quaternion.w * (0f - quaternion.y) + (0f - quaternion.z) * quaternion.x + quaternion.y * quaternion.w,
			m20 = quaternion.z * (0f - quaternion.x) + quaternion.w * (0f - quaternion.y) + (0f - quaternion.x) * (0f - quaternion.z),
			m21 = quaternion.z * quaternion.w + quaternion.w * (0f - quaternion.z) + (0f - quaternion.x) * quaternion.y,
			m22 = quaternion.z * quaternion.z + quaternion.w * quaternion.w + (0f - quaternion.x) * (0f - quaternion.x),
			m23 = quaternion.z * (0f - quaternion.y) + quaternion.w * quaternion.x + (0f - quaternion.x) * quaternion.w,
			m30 = (0f - quaternion.y) * (0f - quaternion.x) + quaternion.x * (0f - quaternion.y) + quaternion.w * (0f - quaternion.z),
			m31 = (0f - quaternion.y) * quaternion.w + quaternion.x * (0f - quaternion.z) + quaternion.w * quaternion.y,
			m32 = (0f - quaternion.y) * quaternion.z + quaternion.x * quaternion.w + quaternion.w * (0f - quaternion.x),
			m33 = (0f - quaternion.y) * (0f - quaternion.y) + quaternion.x * quaternion.x + quaternion.w * quaternion.w
		};
		Quaternion quaternion2 = ComputeOutput(error, delta, deltaTime);
		quaternion2 = MultiplyAsVector(matrix, quaternion2);
		Quaternion quaternion3 = QuaternionExtensions.Multiply(quaternion2, -2f) * Quaternion.Inverse(quaternion);
		return new Vector3(quaternion3.x, quaternion3.y, quaternion3.z);
	}

	private Quaternion ComputeOutput(Quaternion error, Quaternion delta, float deltaTime)
	{
		return new Quaternion
		{
			x = _internalController[0].ComputeOutput(error.x, delta.x, deltaTime),
			y = _internalController[1].ComputeOutput(error.y, delta.y, deltaTime),
			z = _internalController[2].ComputeOutput(error.z, delta.z, deltaTime),
			w = _internalController[3].ComputeOutput(error.w, delta.w, deltaTime)
		};
	}
}
