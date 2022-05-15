SELECT Dinosaurs.name
FROM Halls INNER JOIN  Dinosaurs
ON  Halls.hallId = Dinosaurs.hallId
WHERE ((Halls.HallId = h1));
