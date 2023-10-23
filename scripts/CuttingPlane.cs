using Godot;

[Tool]
public partial class CuttingPlane : Node3D {

    public override void _Ready() {
        DebugDraw3D.Config.ForceUseCameraFromScene = true;
        DebugDraw3D.DebugEnabled                   = true;
        DebugDraw3D.Config.UseFrustumCulling       = false;
    }

    public override void _Process(double delta) {
        DebugDraw3D.Config.ForceUseCameraFromScene = true;
        DebugDraw3D.DebugEnabled                   = true;
        DebugDraw3D.Config.UseFrustumCulling       = false;
        DebugDraw3D.DrawGridXf(Transform, new Vector2I(5, 5), Colors.Aqua);
    }
}
