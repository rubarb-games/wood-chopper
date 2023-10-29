using Godot;

public partial class Player : CharacterBody3D {
	
	private Camera3D cam;
	private Node3D   pivot;

	[Export]
	private float gravity = -30;
	[Export]
	private float maxSpeed = 8;
	[Export]
	private float mouseSensitivity = 0.002f; // radians / pixel

	private Vector3 velocity;
	
	public override void _Ready() {
		pivot = GetNode<Node3D>("HeadPivot");
		cam   = GetNode<Camera3D>("HeadPivot/Camera3D");
	}

	public override void _PhysicsProcess(double delta) {
		velocity.Y += gravity * (float)delta;
		var desiredVelocity = GetInput() * maxSpeed;
		velocity.X = desiredVelocity.X;
		velocity.Z = desiredVelocity.Z;
		MoveAndSlide();
	}

	private Vector3 GetInput() {
		var inputDir = new Vector3();
		var tBasis   = GlobalTransform.Basis;
		
		if (Input.IsActionPressed("move_forward")) {
			inputDir += -tBasis.Z;
		}

		if (Input.IsActionPressed("move_back")) {
			inputDir += tBasis.Z;
		}

		if (Input.IsActionPressed("strafe_left")) {
			inputDir += -tBasis.X;
		}

		if (Input.IsActionPressed("strafe_right")) {
			inputDir += tBasis.X;
		}

		inputDir = inputDir.Normalized();
		return inputDir;
	}

	public override void _UnhandledInput(InputEvent @event) {
		if (@event is not InputEventMouseMotion evtMouse) {
			return;
		}

		if (Input.MouseMode != Input.MouseModeEnum.Captured) {
			return;
		}
		
		RotateY(-evtMouse.Relative.X * mouseSensitivity);
		pivot.RotateX(-evtMouse.Relative.Y * mouseSensitivity);
		var pivotRotation = pivot.Rotation;
		pivotRotation.X = Mathf.Clamp(pivotRotation.X, -1.2f, 1.2f);
		pivot.Rotation  = pivotRotation;
	}
}
