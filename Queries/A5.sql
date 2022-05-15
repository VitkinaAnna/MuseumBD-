SELECT Tickets.name
FROM Exhibitions INNER JOIN Tickets
ON Exhibitions.exhibitionId = Tickets.exhibitionId
WHERE ((Exhibitions.ExhibitionId = e1));
