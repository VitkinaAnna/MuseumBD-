SELECT Exhibitions.price
FROM Workers INNER JOIN  Exhibitions
ON  Workers.workerId = Exhibitions.workerId
WHERE ((Workers.WorkerId = w1));
