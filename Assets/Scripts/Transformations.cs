using UnityEngine;

public static class Transformations
{

    public static Vector3Int F_transformation(Vector3Int input)
    {
        return Vector3Int.RoundToInt(F_matrix.MultiplyPoint(input));
    }




    /*

    {
        get
        {
            Matrix4x4 rotationMatrix = new Matrix4x4();

            rotationMatrix.SetRow(0, new Vector4(0, 1, 0, 0));
            rotationMatrix.SetRow(1, new Vector4(-1, 0, 0, 0));
            rotationMatrix.SetRow(2, new Vector4(0, 0, 1, 0));
            rotationMatrix.SetRow(3, new Vector4(0, 0, 0, 1));

            return rotationMatrix;
        }
    }*/
    public static Matrix4x4 F_matrix => new Matrix4x4
    {
        m00 = 0, m01 = 1, m02 = 0, m03 = 0,
        m10 = 1, m11 = 0, m12 = 0, m13 = 0,
        m20 = 0, m21 = 0, m22 = 1, m23 = 0,
        m30 = 0, m31 = 0, m32 = 0, m33 = 1
    };
    

    public static Matrix4x4 F_inv_matrix => Matrix4x4.Inverse(F_matrix);

    public static Matrix4x4 R_matrix => new Matrix4x4
    {
        m00 = 1, m01 = 0, m02 = 0, m03 = 0,
        m10 = 0, m11 = 0, m12 = 1, m13 = 0,
        m20 = 0, m21 = -1, m22 = 0, m23 = 0,
        m30 = 0, m31 = 0, m32 = 0, m33 = 1
    };

    public static Matrix4x4 R_inv_matrix => Matrix4x4.Inverse(R_matrix);
    public static Matrix4x4 U_matrix => new Matrix4x4
    {
        m00 = 0, m01 = 0, m02 = -1, m03 = 0,
        m10 = 0, m11 = 1, m12 = 0, m13 = 0,
        m20 = 1, m21 = 0, m22 = 0, m23 = 0,
        m30 = 0, m31 = 0, m32 = 0, m33 = 1
    };
    public static Matrix4x4 U_inv_matrix => Matrix4x4.Inverse(U_matrix);
    public static Matrix4x4 L_matrix => new Matrix4x4
    {
        m00 = 1, m01 = 0, m02 = 0, m03 = 0,
        m10 = 0, m11 = 0, m12 = -1, m13 = 0,
        m20 = 0, m21 = 1, m22 = 0, m23 = 0,
        m30 = 0, m31 = 0, m32 = 0, m33 = 1
    };
    public static Matrix4x4 L_inv_matrix => Matrix4x4.Inverse(L_matrix);
    public static Matrix4x4 B_matrix => new Matrix4x4
    {
        m00 = 0, m01 = 1, m02 = 0, m03 = 0,
        m10 = -1, m11 = 0, m12 = 0, m13 = 0,
        m20 = 0, m21 = 0, m22 = 1, m23 = 0,
        m30 = 0, m31 = 0, m32 = 0, m33 = 1
    };
    public static Matrix4x4 B_inv_matrix => Matrix4x4.Inverse(B_matrix);
    public static Matrix4x4 D_matrix => new Matrix4x4
    {
        m00 = 1, m01 = 0, m02 = 0, m03 = 0,
        m10 = 0, m11 = 1, m12 = 0, m13 = 0,
        m20 = -1, m21 = 0, m22 = 0, m23 = 0,
        m30 = 0, m31 = 0, m32 = 0, m33 = 1
    };
    public static Matrix4x4 D_inv_matrix => Matrix4x4.Inverse(D_matrix);
}
