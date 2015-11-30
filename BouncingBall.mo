model BouncingBall "bouncing ball model"
  Modelica.SIunits.Acceleration g = 9.81;
  // gravity
  Modelica.SIunits.Acceleration ax = -0.1;
  // aerodrag
  parameter Real cx = 0.70;
  // horizontal elasticity
  parameter Real cy = 0.90;
  // vertical elasticity
  Modelica.SIunits.Distance x(start = 0);
  Modelica.SIunits.Distance y(start = 10);
  Modelica.SIunits.Velocity Vy(start = 0);
  Modelica.SIunits.Velocity Vx(start = 5);
  Modelica.SIunits.Distance stair1x = 30;
  Modelica.SIunits.Distance stair1y = 5;
  Modelica.SIunits.Distance wall = 80;
equation
  der(Vx) = ax;
  der(Vy) = -g;
  der(x) = Vx;
  der(y) = Vy;
  /*  when Vx < 0 and ax < 0 then
              reinit(ax, 0);
              reinit(Vx, 0);
            end when;
            when Vx < 0 and ax > 0 then
              reinit(ax, 0);
              reinit(Vx, 0);
            end when;*/
  when (x < stair1x and y < stair1y or y < 0) and Vy > (-0.2) then
    //reinit(Vx, cx * pre(Vx));
    reinit(g, 0);
    //    reinit(Vy, 0);
  end when;
  when x < stair1x and y < stair1y or y < 0 then
    //reinit(Vx, cx * pre(Vx));
    reinit(Vy, -cy * pre(Vy));
  end when;
  when x > wall then
    reinit(ax, -ax);
    reinit(Vx, -cx * pre(Vx));
  end when;
  annotation(Icon(coordinateSystem(extent = {{-100, -100}, {100, 100}}, preserveAspectRatio = true, initialScale = 0.1, grid = {2, 2})), Diagram(coordinateSystem(extent = {{-100, -100}, {100, 100}}, preserveAspectRatio = true, initialScale = 0.1, grid = {2, 2})), experiment(StartTime = 0, StopTime = 28, Tolerance = 1e-06, Interval = 0.0186791));
end BouncingBall;