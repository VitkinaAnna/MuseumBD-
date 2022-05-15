SELECT Workers.name
FROM Positions INNER JOIN Workers
ON Positions.positionId = Workers.positionId
WHERE ((Positions.PositionId = p1));
