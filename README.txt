https://asawicki.info/Mirror/Car%20Physics%20for%20Games/Car%20Physics%20for%20Games.html

Foreach Object(Wheel) -> Calculate Force -> Send to Parent(Vehicle) -> Apply Force

Wheel -> Raycast(GetSurfaceFriction) -> Calculate(GetWishForce[Torque * Friction])

Force = Stiffness * CompressionRate(speed) * AmtOfCompression(hitpos)
https://vehiclephysics.com/advanced/how-suspensions-work/